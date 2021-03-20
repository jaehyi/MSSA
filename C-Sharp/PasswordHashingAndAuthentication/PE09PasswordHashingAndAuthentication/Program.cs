using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace PE09PasswordHashingAndAuthentication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This is Programming Exercise 09");
            Console.WriteLine("(Password Hashing and Authentication)");
            Console.WriteLine("\nPassword Authentication System".ToUpper());
            Console.WriteLine("\n------------------------------------------------------\n");

            Dictionary<string, string> database = new Dictionary<string, string>();

            (int min, int max) = DisplayMainMenu();
            int userResponse = GetUserResponse(min, max);

            while (userResponse != max) // max will normally be the option to exit the program
            {
                switch (userResponse)
                {
                    case 1:
                        AddUser(database);
                        break;
                    case 2:
                        AuthenticateUser(database);
                        break;
                    case 3:
                        ViewAllUsers(database);
                        break;
                    case 4:
                        DisplayHashedString();
                        break;
                    case 5: // this is currently the max value; when selected, the program will exit
                        break;
                }
                // Pause();

                Console.WriteLine();
                (min, max) = DisplayMainMenu();
                userResponse = GetUserResponse(min, max);
            }
        }

        static (int min, int max) DisplayMainMenu()
        {
            Console.WriteLine("Please select one of the options below: \n");
            displayInColor(" [1]  Create a new user", ConsoleColor.Blue);
            displayInColor(" [2]  Authenticate an existing user", ConsoleColor.Blue);
            displayInColor(" [3]  View all existing users", ConsoleColor.Blue);
            displayInColor(" [4]  View hashed value of any text", ConsoleColor.Blue);
            displayInColor(" [5]  Exit the system", ConsoleColor.Blue);
            return (1, 5); // Adjust the range based on the menu options
        }

        static int GetUserResponse(int min, int max)
        {
            Console.Write("\nPlease enter your selection: ");
            Console.ForegroundColor = ConsoleColor.Blue;
            string userResponseString = Console.ReadLine();
            Console.ResetColor();
            try
            {
                int userResponseInt = int.Parse(userResponseString);
                if (userResponseInt < min || userResponseInt > max) throw new Exception();
                return userResponseInt;
            }
            catch (Exception)
            {
                displayInColor("Your selection was not valid. Please try again.", ConsoleColor.Red);
                return GetUserResponse(min, max);
            }
        }

        static void AddUser(Dictionary<string, string> database)
        {
            Console.Write("\nPlease enter a new user name: ");
            Console.ForegroundColor = ConsoleColor.Blue;
            string user = Console.ReadLine();
            Console.ResetColor();

            while (user.Length == 0)
                user = getNonZeroLengthUserName();

            if (userAlreadyExists(user, database))
            {
                displayInColor("\nThe user name you entered already exists in the system. Want to try again? [Y]es or [N]o: ", ConsoleColor.Red, false);
                string userResponse;

                do
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    userResponse = Console.ReadLine().ToUpper();
                    Console.ResetColor();
                } while (!(userResponse == "Y" || userResponse == "N"));

                if (userResponse == "Y") AddUser(database);
                else return;
            }
            else storePassword(user, database);

            //Console.Write("Do you want to create another user? [Y]es, [N]o: ");
            //string keyPressed = Console.ReadLine().ToUpper();
            //while (!(keyPressed == "Y" || keyPressed == "N"))
            //{
            //    displayInColor("\nPlease enter either Y or N: ", ConsoleColor.Red, false);
            //    keyPressed = Console.ReadLine().ToUpper();
            //}

            //if (keyPressed == "Y") AddUser(database);
        }

        private static void storePassword(string user, Dictionary<string, string> database)
        {
            Console.Write("Please enter password: ");
            Console.ForegroundColor = ConsoleColor.Blue;
            string password = Console.ReadLine();
            Console.ResetColor();
            database[user] = getHashString(password);
            displayInColor("\nA new user account has been created successfully!", ConsoleColor.Green);
            Thread.Sleep(1500);
        }

        private static string getNonZeroLengthUserName()
        {
            displayInColor("User name cannot be zero length.", ConsoleColor.Red, false);
            Console.Write("\nPlease enter a new user name: ");
            Console.ForegroundColor = ConsoleColor.Blue;
            string user = Console.ReadLine();
            Console.ResetColor();
            return user;
        }

        static void AuthenticateUser(Dictionary<string, string> database)
        {
            Console.Write("\nPlease enter a user name: ");
            string user = displayUserInputInColor(ConsoleColor.Blue);
            Console.Write("Please enter the user's password: ");
            string password = displayUserInputInColor(ConsoleColor.Blue);

            if (!database.ContainsKey(user))
                displayInColor("The user does NOT exist in the database.", ConsoleColor.Red);
            else
            {
                if (database[user] == getHashString(password))
                    displayInColor($"AUTHENTICATED:  The user, {user}, exists in the database, and the password matches the one on record", ConsoleColor.Green);
                else
                    displayInColor($"NOT AUTHENTICATED:  The user, {user}, exists in the database, but the password does NOT match the one on record", ConsoleColor.Red);
            }
        }

        private static string displayUserInputInColor(ConsoleColor color)
        {
            Console.ForegroundColor = color;
            string input = Console.ReadLine();
            Console.ResetColor();
            return input;
        }

        static void ViewAllUsers(Dictionary<string, string> database)
        {
            Console.WriteLine("\nHere are all existing users in the database:");
            Console.ForegroundColor = ConsoleColor.Green;

            if (database.Count == 0)
            {
                displayInColor("There are no users in the database to display. Please try again after adding at least one user.", ConsoleColor.Red);
            }
            else
            {
                foreach (var user in database)
                    Console.WriteLine($"User: {user.Key.PadRight(20)}  Password: {user.Value} ");
            }

            Console.ResetColor();
            Console.WriteLine();
        }
        static void DisplayHashedString()
        {
            string userInput = GetUserInput("Please enter text to be hashed: ");
            string hashedUserInput = getHashString(userInput);
            displayInColor($"Original Input: {userInput}    Hashed Output: {hashedUserInput}", ConsoleColor.Green);
            Console.WriteLine();
            Console.Write("Do you have another text to enter? [Y]es, [N]o: ");
            string keyPressed = Console.ReadLine().ToUpper();
            while (!(keyPressed == "Y" || keyPressed == "N"))
            {
                displayInColor("\nPlease enter either Y or N: ", ConsoleColor.Red, false);
                keyPressed = Console.ReadLine().ToUpper();
            }

            if (keyPressed == "Y") DisplayHashedString();
        }

        private static bool userAlreadyExists(string user, Dictionary<string, string> database)
        {
            return database.ContainsKey(user) ? true : false;
        }

        private static string getHashString(string str)
        {
            byte[] hashValue = new SHA256CryptoServiceProvider().ComputeHash(Encoding.ASCII.GetBytes(str));

            StringBuilder sb = new StringBuilder(hashValue.Length);

            for (int i = 0; i < hashValue.Length; i++)
                sb.Append(hashValue[i].ToString("X2"));

            return sb.ToString();
        }

        private static string GetUserInput(string prompt)
        {
            Console.Write($"\n{prompt}");
            string userInput = Console.ReadLine();
            return userInput;
        }

        private static void displayInColor(string text, ConsoleColor color, bool newLine = true)
        {
            Console.ForegroundColor = color;
            if (newLine) Console.WriteLine(text);
            else Console.Write(text);
            Console.ResetColor();
        }
        static void Pause()
        {
            Console.WriteLine("Press any key to continue ...");
            Console.ReadLine();
        }
    }
}
