using System;
using System.Diagnostics;

namespace PE12MathGames
{
    public static class Util
    {
        private static Random random = new Random();
        private static Stopwatch sw = new Stopwatch();
        private static string quitOption = "You can always type in \"Q\" to quit the game early\n";
        private static int GenerateRandomNumber(int difficultyLevel)
        {
            if (difficultyLevel == 1) return random.Next(1, 11);
            if (difficultyLevel == 2) return random.Next(1, 51);
            return random.Next(1, 101); // difficulty level 3
        }
        public static void Add(int numberOfProblems, int difficultyLevel)
        {
            sw.Restart();
            int correctAnswers = 0;
            int totalNumberOfProblems = numberOfProblems;
            int i = 1;
            Console.WriteLine(quitOption);

            while (numberOfProblems > 0)
            {
                int left = GenerateRandomNumber(difficultyLevel);
                int right = GenerateRandomNumber(difficultyLevel);
                Console.Write($"{i++}.  {left} + {right} = ");
                string response = Console.ReadLine();
                if (response.ToUpper().Equals("Q")) break;

                if (int.TryParse(response, out int answer) && answer == left + right)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Correct!");
                    Console.ResetColor();
                    correctAnswers++;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Incorrect!  The correct answer is {left + right}");
                    Console.ResetColor();
                }
                numberOfProblems--;
            }
            sw.Stop();
            DisplaySummary(correctAnswers, totalNumberOfProblems, sw);
        }

        public static void Subtract(int numberOfProblems, int difficultyLevel)
        {
            sw.Restart();
            int correctAnswers = 0;
            int totalNumberOfProblems = numberOfProblems;
            int i = 1;
            Console.WriteLine(quitOption);

            while (numberOfProblems > 0)
            {
                int left = GenerateRandomNumber(difficultyLevel);
                int right = GenerateRandomNumber(difficultyLevel);
                while (right > left)
                {
                    right = GenerateRandomNumber(difficultyLevel);
                }
                Console.Write($"{i++}.  {left} - {right} = ");
                string response = Console.ReadLine();
                if (response.ToUpper().Equals("Q")) break;

                if (int.TryParse(response, out int answer) && answer == left - right)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Correct!");
                    Console.ResetColor();
                    correctAnswers++;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Incorrect!  The correct answer is {left + right}");
                    Console.ResetColor();
                }
                numberOfProblems--;
            }
            sw.Stop();
            DisplaySummary(correctAnswers, totalNumberOfProblems, sw);
        }

        public static void Multiply(int numberOfProblems, int difficultyLevel)
        {
            sw.Restart();
            int correctAnswers = 0;
            int totalNumberOfProblems = numberOfProblems;
            int i = 1;
            Console.WriteLine(quitOption);

            while (numberOfProblems > 0)
            {
                int left = GenerateRandomNumber(difficultyLevel);
                int right = GenerateRandomNumber(difficultyLevel);
                Console.Write($"{i++}.  {left} * {right} = ");
                string response = Console.ReadLine();
                if (response.ToUpper().Equals("Q")) break;

                if (int.TryParse(response, out int answer) && answer == left * right)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Correct!");
                    Console.ResetColor();
                    correctAnswers++;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Incorrect!  The correct answer is {left * right}");
                    Console.ResetColor();
                }
                numberOfProblems--;
            }
            sw.Stop();
            DisplaySummary(correctAnswers, totalNumberOfProblems, sw);
        }

        public static void Divide(int numberOfProblems, int difficultyLevel)
        {
            sw.Restart();
            int correctAnswers = 0;
            int totalNumberOfProblems = numberOfProblems;
            int i = 1;
            Console.WriteLine(quitOption);

            while (numberOfProblems > 0)
            {
                int left = GenerateRandomNumber(difficultyLevel);
                int right = GenerateRandomNumber(difficultyLevel);
                Console.Write($"{i++}.  {left} / {right} = ");
                string response = Console.ReadLine();
                if (response.ToUpper().Equals("Q")) break;

                if (double.TryParse(response, out double answer) && Math.Round(answer, 2) == Math.Round((double)left / right, 2))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Correct!");
                    Console.ResetColor();
                    correctAnswers++;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Incorrect!  The correct answer is {Math.Round((double)left / right, 2)}");
                    Console.ResetColor();
                }
                numberOfProblems--;
            }
            sw.Stop();
            DisplaySummary(correctAnswers, totalNumberOfProblems, sw);
        }

        private static void DisplaySummary(int correctAnswers, int totalNumberOfProblems, Stopwatch sw)
        {
            Console.WriteLine($"\nYou got {correctAnswers} out of {totalNumberOfProblems} correct in {sw.ElapsedMilliseconds / 1000.0} seconds. " +
                $"Your grade is {(int)Math.Ceiling(((double)correctAnswers / totalNumberOfProblems * 100))}.");
            Console.WriteLine("\nPlease press any key to continue...");
            Console.ReadLine();
        }

        public static void DrawLine(int number)
        {
            for (int i = 0; i < number; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine();
        }
    }
}
