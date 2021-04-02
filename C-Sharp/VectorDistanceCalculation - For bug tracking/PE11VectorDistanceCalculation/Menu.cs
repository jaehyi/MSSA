using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE11VectorDistanceCalculation
{
    class Menu
    {
        private static int selection;
        private static int[] validSelections = { 1, 2, 3 };
        public static void Run()
        {
            displayWelcomeMessage();
            
            while (true)
            {
                selection = getValidUserInput();
                switch (selection)
                {
                    case 1:
                        Point2DCollection p2c = new Point2DCollection();
                        Console.WriteLine(p2c);
                        Console.WriteLine($"\nTwo closest points are Point ({p2c.TwoClosestPoints[0]}) and Point ({p2c.TwoClosestPoints[1]}) " +
                            $"and the distance between them is {p2c.DistanceBetweenTwoClosestPoints:N4}\n");
                        break;
                    case 2:
                        Point3DCollection p3c = new Point3DCollection();
                        Console.WriteLine($"\nTwo closest points are Point ({p3c.TwoClosestPoints[0]}) and Point ({p3c.TwoClosestPoints[1]}) " +
                            $"and the distance between them is {p3c.DistanceBetweenTwoClosestPoints:N4}\n");
                        break;
                    case 3:
                        return;
                }
            }

        }

        private static int getValidUserInput()
        {
            displaySelectionMenu();
            string userInput = Console.ReadLine();
            try
            {
                int selection = int.Parse(userInput);
                if (!validSelections.Any(s => s == selection)) throw new Exception();
                return selection;

            }
            catch (Exception)
            {
                Console.WriteLine("You have entered an invalid selection.  Please try again.\n\n");
                return getValidUserInput();
            }

        }

        private static void displaySelectionMenu()
        {
            Console.WriteLine("Please make a valid selection:\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("  [1] Display the two dimensional points with the shortest distance");
            Console.WriteLine("  [2] Display the three dimensional points with the shortest distance");
            Console.WriteLine("  [3] Exit");
            Console.ResetColor();
            Console.Write("\nYour Selection >> ");
        }

        private static void displayWelcomeMessage()
        {
            Console.WriteLine("----------------------------------------------------------------------------");
            Console.WriteLine("                     Welcome to Programming Exercise 11           ");
            Console.WriteLine("----------------------------------------------------------------------------\n");
            Console.WriteLine("                        Vector Distance Calculation               \n");
            Console.WriteLine();
        }
    }
}
