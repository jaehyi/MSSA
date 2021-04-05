using System;

namespace PE12MathGames
{
    public static class Menu
    {
        private static string welcomeMessage = "Welcome to Programming Exercise 12";
        private static string title = "Math Games";
        public static void Run()
        {
            Util.DrawLine(80);
            DisplayMessage(welcomeMessage, 80);
            DisplayMessage(title, 80);
            Util.DrawLine(80);

            while (true)
            {
                DisplayMenu();
                int menuSelection = GetSelection(1, 5);
                if (menuSelection == 5) return;
                PromptForNumberOfProblems();
                int problemsToTry = GetSelection(1, 12);
                PromptForDifficultLevel();
                int difficultyLevel = GetSelection(1, 3);
                DisplayUserOptions(menuSelection, problemsToTry, difficultyLevel);

                switch (menuSelection)
                {
                    case 1: // Addition
                        Util.Add(problemsToTry, difficultyLevel);
                        break;
                    case 2: // Subtraction
                        Util.Subtract(problemsToTry, difficultyLevel);
                        break;
                    case 3:
                        Util.Multiply(problemsToTry, difficultyLevel);
                        break;
                    case 4:
                        Util.Divide(problemsToTry, difficultyLevel);
                        break;
                    default:
                        Console.WriteLine("Unexpected entry. Press any key to exit the program...");
                        Console.ReadLine();
                        return;
                }
                Console.Clear();
            }
        }

        private static void PromptForDifficultLevel()
        {
            Console.WriteLine("\nEnter the difficulty level: [1] Easy    [2] Medium    [3] Hard");
        }

        private static void DisplayUserOptions(int menuSelection, int problemsToTry, int difficultyLevel)
        {
            Console.WriteLine("\n<<< Here is the summary of your choices >>>\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"  Category: {(MathCategory)menuSelection}");
            Console.WriteLine($"  Number of Problem(s): {problemsToTry}");
            Console.WriteLine($"  Difficulty Level: {difficultyLevel}");
            Console.ResetColor();
            Console.WriteLine($"\n\n\nGood luck!");
            Console.WriteLine("\nPress any key to begin the game...");
            Console.ReadLine();
            Console.Clear();
        }

        private static void PromptForNumberOfProblems()
        {
            Console.WriteLine("\nEnter the number of problems to try between 1 and 12:");
        }

        private static int GetSelection(int min, int max, int numberOfAttempts = 5)
        {
            if (numberOfAttempts <= 0) return -1; // This is the base case

            Console.Write("  Enter your selection >>> ");
            string userInput = Console.ReadLine();
            if ((int.TryParse(userInput, out int selection), selection >= min, selection <= max) == (true, true, true))
            {
                return selection;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Invalid selection made.  Please try again. Number of attempts remaining: {--numberOfAttempts}");
                Console.ResetColor();
                return GetSelection(min, max, numberOfAttempts);
            }
        }

        private static void DisplayMenu()
        {
            Console.WriteLine("Please make a selection from the following options:\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\t[1]  Addition");
            Console.WriteLine("\t[2]  Subtraction");
            Console.WriteLine("\t[3]  Multiplication");
            Console.WriteLine("\t[4]  Division");
            Console.WriteLine("\t[5]  Exit");
            Console.WriteLine();
            Console.ResetColor();
        }

        private static void DisplayMessage(string message, int lineLength)
        {
            int startPlace = (lineLength - message.Length) / 2;
            if (message.Length > lineLength)
            {
                Console.WriteLine(message);
            }
            else
            {
                for (int i = 0; i < startPlace; i++)
                {
                    Console.Write(" ");
                }
                Console.WriteLine(message);
            }
        }
    }
}
