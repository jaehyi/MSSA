using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace PE10BaseNumberConversion
{
    // Assume that all numbers input by user will be positive.
    class Program
    {
        static void Main(string[] args)
        {
            Menu.Run();
        }
    }

    class Menu
    {
        private static int menuSelection;
        private static string numberToConvert;
        private static int[] validMenuSelections = { 1, 2, 3, 4, 5 };
        public static void Run()
        {
            Console.WriteLine("*****  Welcome to Programming Exercise 10  *****\n");
            Console.WriteLine("           ( Base Number Conversion )\n\n");

            do
            {
                menuSelection = getMenuSelection();
                if (menuSelection != validMenuSelections[validMenuSelections.Length - 1]) 
                    numberToConvert = getNumberToConvert();

                switch (menuSelection)
                {
                    case 1:
                        Binary bin = new Binary(numberToConvert);
                        if (!bin.IsValid()) displayErrorMessage();
                        else
                        {
                            Console.WriteLine($"Octal Conversion:".PadRight(30) + $"{bin.ConvertTo(NumberBase.Octal)}");
                            Console.WriteLine($"Decimal Conversion:".PadRight(30) + $"{bin.ConvertTo(NumberBase.Decimal)}");
                            Console.WriteLine($"Hexadecimal Conversion:".PadRight(30) + $"{bin.ConvertTo(NumberBase.Hexadecimal)}");
                        }
                        break;
                    case 2:
                        Octal oct = new Octal(numberToConvert);
                        if (!oct.IsValid()) displayErrorMessage();
                        else
                        {
                            Console.WriteLine($"Binary Conversion:".PadRight(30) + $"{oct.ConvertTo(NumberBase.Binary)}");
                            Console.WriteLine($"Decimal Conversion:".PadRight(30) + $"{oct.ConvertTo(NumberBase.Decimal)}");
                            Console.WriteLine($"Hexadecimal Conversion:".PadRight(30) + $"{oct.ConvertTo(NumberBase.Hexadecimal)}");
                        }
                        break;
                    case 3:
                        Decimal dec = new Decimal(numberToConvert);
                        if (!dec.IsValid()) displayErrorMessage();
                        else
                        {
                            Console.WriteLine($"Binary Conversion:".PadRight(30) + $"{dec.ConvertTo(NumberBase.Binary)}");
                            Console.WriteLine($"Octal Conversion:".PadRight(30) + $"{dec.ConvertTo(NumberBase.Octal)}");
                            Console.WriteLine($"Hexadecimal Conversion:".PadRight(30) + $"{dec.ConvertTo(NumberBase.Hexadecimal)}");
                        }
                        break;
                    case 4:
                        Hexadecimal hex = new Hexadecimal(numberToConvert);
                        if (!hex.IsValid()) displayErrorMessage();
                        else
                        {
                            Console.WriteLine($"Binary Conversion:".PadRight(30) + $"{hex.ConvertTo(NumberBase.Binary)}");
                            Console.WriteLine($"Octal Conversion:".PadRight(30) + $"{hex.ConvertTo(NumberBase.Octal)}");
                            Console.WriteLine($"Decimal Conversion:".PadRight(30) + $"{hex.ConvertTo(NumberBase.Decimal)}");
                        }
                        break;
                    case 5:
                        return;
                }
                //Console.Write("\n\n\nPlease press any key to continue... ");
                //Console.ReadLine();
                //Console.Clear();
                Thread.Sleep(2000);
                Console.WriteLine();

            } while (true);
        }

        private static void displayErrorMessage()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("One or more digits you entered is invalid.  Please try again.\n");
            Console.ResetColor();
        }

        private static string getNumberToConvert()
        {
            Console.Write("Please enter the number to convert >> ");
            Console.ForegroundColor = ConsoleColor.Green;
            string result = Console.ReadLine();
            Console.ResetColor();
            Console.WriteLine();
            return result;
        }

        private static int getMenuSelection()
        {
            try
            {
                displayMenu();
                Console.ForegroundColor = ConsoleColor.Green;
                menuSelection = int.Parse(Console.ReadLine());
                Console.ResetColor();
                if (!validMenuSelections.Contains(menuSelection)) throw new Exception();
                return menuSelection;
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nInvalid selection entered.  Please try again.\n");
                Console.ResetColor();
                return getMenuSelection();
            }
        }

        private static void displayMenu()
        {
            Console.WriteLine("\nWhat number base would you like to perform conversion on?\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("  [1] Binary (base 2)");
            Console.WriteLine("  [2] Octal (base 8)");
            Console.WriteLine("  [3] Decimal (base 10)");
            Console.WriteLine("  [4] Hexadecimal (base 16)");
            Console.WriteLine("  [5] Exit");
            Console.ResetColor();
            Console.Write("\nPlease make your selection >> ");
        }
    }
}
