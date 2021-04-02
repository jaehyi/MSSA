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
        //Have you guys gone over unit tests?
        //I've seen a few bugs in your code but unit tests will surface those bugs
        public static void Run()
        {
            displayWelcomeMessage();
            
            while (true)
            {
                selection = getValidUserInput();//start all letters with capital letter
                //rule of thumb but all switch statesments should have a a default
                //even though your switch statement is ok now but what happens if future you or another dev adds another valid input?
                switch (selection)
                {
                    case 1:
                        Point2DCollection p2c = new Point2DCollection();//var
                        Console.WriteLine(p2c);
                        //use string verbatim/interpolation here
                        //https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/tokens/
                        //p2c.TwoClosestPoints[0] models/objects should be separated out from this type of logic
                        Console.WriteLine($"\nTwo closest points are Point ({p2c.TwoClosestPoints[0]}) and Point ({p2c.TwoClosestPoints[1]}) " +
                            $"and the distance between them is {p2c.DistanceBetweenTwoClosestPoints:N4}\n");
                        break;
                    case 2:
                        Point3DCollection p3c = new Point3DCollection();//var
                        //same string comment as above
                        Console.WriteLine($"\nTwo closest points are Point ({p3c.TwoClosestPoints[0]}) and Point ({p3c.TwoClosestPoints[1]}) " +
                            $"and the distance between them is {p3c.DistanceBetweenTwoClosestPoints:N4}\n");
                        break;
                    case 3:
                        return;
                }
            }

        }

        private static int getValidUserInput()//Start ALL methods with Capital letter
        {
            displaySelectionMenu();
            string userInput = Console.ReadLine();//var
            try
            {
                int selection = int.Parse(userInput);
                //why have valid selections? why not return invalid error or something? you have a switch statement
                if (!validSelections.Any(s => s == selection)) throw new Exception();//use {}
                return selection;

            }
            catch (Exception) // dont catch exception, if you dont know what exception here
            //see program.cs
            {
                Console.WriteLine("You have entered an invalid selection.  Please try again.\n\n");
                return getValidUserInput();//oh boy this can cause some issues
            }

        }

        private static void displaySelectionMenu()
        {
            //this is dope
            Console.WriteLine("Please make a valid selection:\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            // I think /t would work for a tab here but idk
            Console.WriteLine("  [1] Display the two dimensional points with the shortest distance");
            Console.WriteLine("  [2] Display the three dimensional points with the shortest distance");
            Console.WriteLine("  [3] Exit");
            Console.ResetColor();
            Console.Write("\nYour Selection >> ");
        }

        private static void displayWelcomeMessage()
        {
            //this is dope too
            Console.WriteLine("----------------------------------------------------------------------------");
            Console.WriteLine("                     Welcome to Programming Exercise 11           ");
            Console.WriteLine("----------------------------------------------------------------------------\n");
            Console.WriteLine("                        Vector Distance Calculation               \n");
            Console.WriteLine();
        }
    }
}
