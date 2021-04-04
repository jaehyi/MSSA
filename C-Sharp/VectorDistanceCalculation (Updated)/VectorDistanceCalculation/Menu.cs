using System;
using System.Linq;
using System.Threading;

namespace VectorDistanceCalculation
{
    class Menu
    {
        private static int selection;
        private static int[] validSelections = { 1, 2, 3 };
        public static void Run()
        {
            DisplayWelcomeMessage();

            while (true)
            {
                selection = GetValidUserInput();
                switch (selection)
                {
                    case 1:
                        PointCollection<Point2D> p2c = new PointCollection<Point2D>(100, 1, 100);
                        Console.WriteLine(p2c);
                        p2c.DisplayTwoClosestPoints();
                        break;
                    case 2:
                        PointCollection<Point3D> p3c = new PointCollection<Point3D>(1000, 1, 1000);
                        Console.WriteLine(p3c);
                        p3c.DisplayTwoClosestPoints();
                        break;
                    case 3:
                        Console.WriteLine("Good bye!");
                        Thread.Sleep(2000);
                        return;
                    default:
                        Console.Write("Unexpected selected made. Press any key to exit... ");
                        Console.ReadLine();
                        break;
                }
            }
        }


        private static int GetValidUserInput(int attempts = 5)
        {
            if (attempts == 0) return 3;

            DisplaySelectionMenu();
            string userInput = Console.ReadLine();
            try
            {
                int selection = int.Parse(userInput);
                if (!validSelections.Any(s => s == selection)) throw new Exception();
                return selection;
            }
            catch (Exception)
            {
                Console.WriteLine($"You have entered an invalid selection.  Please try again. - {attempts - 1} attempts remaining\n\n");
                return GetValidUserInput(attempts - 1);
            }

        }

        private static void DisplaySelectionMenu()
        {
            Console.WriteLine("Please make a valid selection:\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\t[1] Display the two dimensional points with the shortest distance");
            Console.WriteLine("\t[2] Display the three dimensional points with the shortest distance");
            Console.WriteLine("\t[3] Exit");
            Console.ResetColor();
            Console.Write("\nYour Selection >> ");
        }

        private static void DisplayWelcomeMessage()
        {
            Console.WriteLine("----------------------------------------------------------------------------");
            Console.WriteLine("                     Welcome to Programming Exercise 11           ");
            Console.WriteLine("----------------------------------------------------------------------------\n");
            Console.WriteLine("                        Vector Distance Calculation               \n");
            Console.WriteLine();
        }
    }
}
