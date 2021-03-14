using System;

namespace PE08GuessMyNumberGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Programming Exercise 08!\n\n");
            int menuChoice = getMenuChoice(1, 4);
            while (menuChoice != 4)
            {
                Random random = new Random();

                switch (menuChoice)
                {
                    case 1:
                        int[] list = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                        displayList(list);
                        int searchNumber = getUserInput(list);
                        BisectionSearch(list, searchNumber);
                        break;
                    case 2:
                        int randomNumberByComputer = random.Next(1, 1001);
                        Console.WriteLine("The computer has selected a number between 1 and 1,000. Your mission, " +
                            "should you choose to accept, is to guess the number in the least number of tries possible.\n");
                        GuessMyNumberHumanPlays(randomNumberByComputer);
                        break;
                    case 3:
                        Console.WriteLine("Please pick a number between 1 and 100, and the computer will try to guess the number " +
                        "in the least number of tries possible.\n");
                        int randomNumberByUser = getUserInput(1, 100);
                        GuessMyNumberComputerPlays(randomNumberByUser, random);
                        break;
                    default:
                        break;
                }
                Console.ReadLine();
                Console.Clear();
                menuChoice = getMenuChoice(1, 4);
            }
        }

        private static int getMenuChoice(int lowerBound, int upperBound)
        {
            Console.WriteLine("Here are your options:\n");
            Console.WriteLine("  [1] Run a simple bisection algorithm");
            Console.WriteLine("  [2] Guess my number - Human Plays");
            Console.WriteLine("  [3] Guess my number - Computer Plays");
            Console.WriteLine("  [4] Exit");
            Console.Write("\nPlease enter your selection: ");

            Console.ForegroundColor = ConsoleColor.Green;
            string stringUserInput = Console.ReadLine();
            Console.ResetColor();

            try
            {
                int intUserInput = int.Parse(stringUserInput);
                if (!(intUserInput >= lowerBound && intUserInput <= upperBound)) throw new Exception();
                return intUserInput;
            }
            catch (Exception)
            {
                displayMessageInRed("Your input was invalid.  Please try again.");
            }
            return getUserInput(lowerBound, upperBound);
        }

        private static void displayList(int[] list)
        {
            Console.Write("\nThe list consists of: ");
            Array.ForEach(list, i => Console.Write($"{i} "));
            Console.WriteLine("\n");
        }

        static void BisectionSearch(int[] intArray, int searchNumber, int numOfIteration = 0)
        {
            //if (isArrayNullOrEmpty(intArray))
            //    displayMessageInRed("The argument to the first parameter is null or empty.  " +
            //            "Please run the function with a valid array of integers");

            //if (searchNumber > intArray[intArray.GetUpperBound(0)] || searchNumber < intArray[intArray.GetLowerBound(0)])
            //{
            //    displayMessageInRed($"The value {searchNumber} does not exist.");
            //    return;
            //}

            if (isArrayNullOrEmpty(intArray))
            {
                displayMessageInRed($"The number you searched, {searchNumber}, for does NOT exist in the array.");
                return;
            }

            int middleIndex = intArray.GetUpperBound(0) / 2; // intArray.Length was my initial value
            if (intArray[middleIndex] == searchNumber)
            {
                displayMessageInGreen($"The value is equal to {searchNumber}. ", false);
                displayMessageInGreen($"It has been found after {++numOfIteration} iterations.");
            }
            else if (intArray[middleIndex] < searchNumber)
            {
                displayMessageInRed($"The value is higher than {intArray[middleIndex]}.\n");
                int[] temp = new int[intArray.GetUpperBound(0) - middleIndex];
                Array.Copy(intArray, middleIndex + 1, temp, 0, temp.Length);
                BisectionSearch(temp, searchNumber, ++numOfIteration);
            }
            else
            {
                displayMessageInRed($"The value is lower than {intArray[middleIndex]}.\n");
                int[] temp = new int[middleIndex];
                Array.Copy(intArray, 0, temp, 0, middleIndex);
                BisectionSearch(temp, searchNumber, ++numOfIteration);
            }
        }

        public static void GuessMyNumberHumanPlays(int randomNumberByComputer, int lowerBound = 1, int upperBound = 1000, int numberOfIteration = 0)
        {
            int middleValue = (upperBound + lowerBound) / 2;
            numberOfIteration++;
            int userGuess = getUserInput(lowerBound, upperBound);

            if (userGuess == randomNumberByComputer)
            {
                string tryOrTries = (numberOfIteration == 1) ? "try" : "tries";
                displayMessageInGreen($"\nCongratulations!  You've correctly guessed the number after {numberOfIteration} {tryOrTries}.");
                return;
            }
            else if (userGuess < randomNumberByComputer)
            {
                displayMessageInRed("Your guess was too low.\n");
                if (randomNumberByComputer <= middleValue)
                    upperBound = middleValue;
                else
                    lowerBound = middleValue + 1;
            }
            else
            {
                displayMessageInRed("Your guess was too high.\n");
                if (randomNumberByComputer <= middleValue)
                    upperBound = middleValue;
                else
                    lowerBound = middleValue + 1;
            }
            GuessMyNumberHumanPlays(randomNumberByComputer, lowerBound, upperBound, numberOfIteration);
        }

        public static void GuessMyNumberComputerPlays(int randomNumberByUser, Random random, int lowerBound = 1, int upperBound = 100, int numberOfIteration = 0)
        {
            int middleValue = (upperBound + lowerBound) / 2;
            numberOfIteration++;
            int computerGuess = random.Next(lowerBound, upperBound + 1);

            displayMessageInBlue($"\nThe computer guesses number {computerGuess}.\n");
            int userChoice = getUserInput();

            if (userChoice == 3)
            {
                string tryOrTries = (numberOfIteration == 1) ? "try" : "tries";
                displayMessageInGreen($"\nThe computer correctly guessed your number after {numberOfIteration} {tryOrTries}.");
                return;
            }
            else if (userChoice == 1)
            {
                if (randomNumberByUser <= middleValue)
                    upperBound = middleValue;
                else
                    lowerBound = middleValue + 1;
            }
            else
            {
                if (randomNumberByUser <= middleValue)
                    upperBound = middleValue;
                else
                    lowerBound = middleValue + 1;
            }
            GuessMyNumberComputerPlays(randomNumberByUser, random, lowerBound, upperBound, numberOfIteration);
        }

        private static bool isArrayNullOrEmpty(int[] intArray) // This method is not used
        {
            if (intArray == null || intArray.Length == 0) return true;
            return false;
        }
        private static void displayMessageInRed(string message, bool newLine = true)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            if (newLine) Console.WriteLine(message);
            else Console.Write(message);
            Console.ResetColor();
        }
        private static void displayMessageInGreen(string message, bool newLine = true)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            if (newLine) Console.WriteLine(message);
            else Console.Write(message);
            Console.ResetColor();
        }

        private static void displayMessageInBlue(string message, bool newLine = true)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            if (newLine) Console.WriteLine(message);
            else Console.Write(message);
            Console.ResetColor();
        }

        private static int getUserInput()
        {
            Console.WriteLine("If the guess is too low, enter 1");
            Console.WriteLine("If the guess is too high, enter 2");
            Console.WriteLine("If the guess is correct, enter 3");
            Console.Write("\nPlease enter your choice here: ");
            Console.ForegroundColor = ConsoleColor.Green;
            string stringUserInput = Console.ReadLine();
            Console.ResetColor();

            try
            {
                int intUserInput = int.Parse(stringUserInput);
                if (!(intUserInput >= 1 && intUserInput <= 3)) throw new Exception();
                return intUserInput;
            }
            catch (Exception)
            {
                displayMessageInRed("Your input was invalid.  Please try again.");
            }
            return getUserInput();
        }
        private static int getUserInput(int lowerBound, int upperBound)
        {
            Console.Write($"Please enter a number between {lowerBound} and {upperBound}: ");
            Console.ForegroundColor = ConsoleColor.Green;
            string stringUserInput = Console.ReadLine();
            Console.ResetColor();

            try
            {
                int intUserInput = int.Parse(stringUserInput);
                if (!(intUserInput >= lowerBound && intUserInput <= upperBound)) throw new Exception();
                return intUserInput;
            }
            catch (Exception)
            {
                displayMessageInRed("Your input was invalid.  Please try again.");
            }
            return getUserInput(lowerBound, upperBound);
        }

        private static int getUserInput(int[] intArray)
        {
            Console.Write($"Please enter a number between {intArray[0]} and {intArray[intArray.GetUpperBound(0)]}: ");
            Console.ForegroundColor = ConsoleColor.Green;
            string stringUserInput = Console.ReadLine();
            Console.ResetColor();

            try
            {
                int intUserInput = int.Parse(stringUserInput);
                if (!(intUserInput >= intArray[0] && intUserInput <= intArray[intArray.GetUpperBound(0)])) throw new Exception();
                return intUserInput;
            }
            catch (Exception)
            {
                displayMessageInRed("Your input was invalid.  Please try again.");
            }
            return getUserInput(intArray);
        }
    }
}
