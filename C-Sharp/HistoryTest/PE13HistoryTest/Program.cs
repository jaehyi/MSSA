using System;
using System.Collections.Generic;
using System.IO;

namespace PE13HistoryTest
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Menu.Run();
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.InnerException}");
                Console.WriteLine($"{e.Message}");
            }
        }
    }

    class Menu
    {
        public static void Run()
        {
            DisplayWelcomeMessage();
            while (true)
            {
                string filePath = @"C:\Users\Jae Yi\MSSA2021\ISTA421\Exercises\PE13HistoryTest\PE13HistoryTest\HistoryTest.txt";
                TestBank tb = new TestBank(filePath);
                int numOfQuestions = DisplayMenu(tb.TotalNumberOfQuestions);
                Console.WriteLine("\nPress any key to start the test");
                Console.ReadLine();
                tb.StartTest(numOfQuestions);
                if (!PlayAgain()) break;
                Console.Clear();
            }
        }

        private static bool PlayAgain()
        {
            Console.WriteLine("\n\nWould you like to play again? [Y]es or [N]o");
            Console.Write("  Enter your selection >> ");
            string userInput = Console.ReadLine();
            if (userInput.ToUpper().Equals("Y")) return true;
            return false;
        }

        private static int DisplayMenu(int totalNumberOfQuestions)
        {
            Console.WriteLine($"** Test bank has been initialized with a total of {totalNumberOfQuestions} questions...\n");
            Console.WriteLine($"How many questions would you like to answer? (1 to {totalNumberOfQuestions})");
            return Util.GetUserChoice("  Enter your choice >> ", 1, totalNumberOfQuestions);
        }

        private static void DisplayWelcomeMessage()
        {
            Console.WriteLine("WELCOME TO PROGRAMMING EXERCISE 13\n");
            Console.WriteLine("           History Test\n\n");
        }
      
    }

    static class Util
    {
        public static int GetUserChoice(string prompt, int min, int max, int numOfAttempts = 5)
        {
            if (numOfAttempts <= 0) return -1;
            
            Console.Write(prompt);
            string userInput = Console.ReadLine();
            if (int.TryParse(userInput, out int result) && result >= min && result <= max)
                return result;
            else
            {
                Console.WriteLine($"Your input was invalid. You have {--numOfAttempts} attempts left");
                return GetUserChoice(prompt, min, max, numOfAttempts);
            }
        }

    }

    class TestBank
    {
        private string _filePath;
        public int TotalNumberOfQuestions { get; private set; }
        private Random random = new Random();
        private int questionNumber = 0; // To display the question number incrementally
        public List<string[]> Bank { get; set; } = new List<string[]>();
        private List<int> questionsGenerated = new List<int>(); // To keep track of questions that have been generated

        public TestBank(string filePath)
        {
            _filePath = filePath;
            PopulateBank();
        }

        private void PopulateBank()
        {
            try
            {
                using (StreamReader sr = new StreamReader(_filePath))
                {
                    string line = sr.ReadLine();
                    int counter = 0;
                    while (line != null)
                    {
                        Bank.Add(line.Replace("\"", "").Split(','));
                        counter++;
                        line = sr.ReadLine();
                    }
                    TotalNumberOfQuestions = counter;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("The file with tests could not be found");
                throw;
            } 
        }

        public void StartTest(int numOfQuestions)
        {
            int numberOfQuestions = numOfQuestions; // To store the original number of questions for displaying at the end
            int testNumber = random.Next(TotalNumberOfQuestions); // To pick a random question out of the bank
            int numberOfCorrectAnswers = 0; // To keep track of the number of questions answered correctly
            while (numOfQuestions > 0)
            {
                while (questionsGenerated.Contains(testNumber)) // To regenerate non-repeating question
                    testNumber = random.Next(TotalNumberOfQuestions);
                questionsGenerated.Add(testNumber);  // To keep track of questions that have been generated

                string correctAnswer = Bank[testNumber][1]; // To store the correct answer
                ShuffleAnswers(random, testNumber);
                int correctAnswerIndex = GetCorrectAnswerIndex(correctAnswer, testNumber);
                DisplayQuestionAnswer(testNumber);
                int userAnswer = Util.GetUserChoice("  Enter your answer >> ", 1, 4);
                if (DisplayResult(userAnswer, correctAnswerIndex, correctAnswer))
                    numberOfCorrectAnswers++;
                
                numOfQuestions--;
            }

            DisplayFinalResult(numberOfQuestions, numberOfCorrectAnswers);
        }

        private void DisplayFinalResult(int numberOfQuestions, int totalNumberOfCorrectAnswers)
        {
            Console.WriteLine($"You answered {totalNumberOfCorrectAnswers} out of {numberOfQuestions} correctly" +
                $" and your grade is {totalNumberOfCorrectAnswers * 100 / numberOfQuestions:N2}");
        }

        private void ShuffleAnswers(Random random, int testNumber)
        {
            for (int i = 1; i <= 4; i++)
            {
                int randomNumber = random.Next(1, 5); // generates a random number betwween 1 and 4
                (Bank[testNumber][i], Bank[testNumber][randomNumber]) = (Bank[testNumber][randomNumber], Bank[testNumber][i]);
            }
        }

        private int GetCorrectAnswerIndex(string correctAnswerText, int testNumber)
        {
            for (int i = 1; i <= 4; i++)
                if (Bank[testNumber][i] == correctAnswerText) return i;
            return 0; // This should not occur
        }

        private void DisplayQuestionAnswer(int testNumber)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n{++questionNumber}. {Bank[testNumber][0]}"); // This displays the question in randomQuestion
            Console.ResetColor();
            for (int i = 1; i <= 4; i++)
            {
                Console.WriteLine($"\t{i}) {Bank[testNumber][i]}");
            }
        }

        private bool DisplayResult(int userAnswer, int correctAnswerIndex, string correctAnswerText)
        {
            if (userAnswer == correctAnswerIndex)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Correct!  Good job!\n");
                Console.ResetColor();
                return true;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Incorrect!  The correct answer is \"{correctAnswerText}\"\n");
                Console.ResetColor();
                return false;
            }
        }
    }
}
