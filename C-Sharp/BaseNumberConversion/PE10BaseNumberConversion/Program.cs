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

    public class Binary
    {
        public string BinaryNumber { get; private set; }
        private char[] validCharacters = { '0', '1' };
        private const int numberBase = 2;
        private Dictionary<int, string> hexLetters = new Dictionary<int, string>()
            { {0,"0"},{1,"1"},{2,"2"},{3,"3"},{4,"4"},{5,"5"},{6,"6"},{7,"7"},{8,"8"},{9,"9"},{10,"A"},{11,"B"},{12,"C"},{13,"D"},{14,"E"},{15,"F"}};
        public Binary(string binaryNumber)
        {
            BinaryNumber = binaryNumber;
        }
        public bool IsValid()
        {
            if (BinaryNumber.Length == 0) return false;
            return BinaryNumber.All(c => validCharacters.Contains(c));
        }
        public string ConvertTo(NumberBase baseToConvertTo)
        {
            switch (baseToConvertTo)
            {
                case NumberBase.Binary:
                    return BinaryNumber;
                case NumberBase.Octal:
                    return toOctal();
                case NumberBase.Decimal:
                    return toDecimal();
                case NumberBase.Hexadecimal:
                    return toHexadecimal();
                default:
                    return string.Empty;
            }
        }
        private string toOctal()
        {
            string result = string.Empty;
            int temp = 0;
            int power = 0;

            for (int i = BinaryNumber.Length - 1; i >= 0; i--)
            {
                if (power == 3)
                {
                    result = temp.ToString() + result;
                    power = 0;
                    temp = 0;
                }
                temp += int.Parse(BinaryNumber[i].ToString()) * (int)Math.Pow(numberBase, power++);
            }
            return temp.ToString() + result;
        }
        private string toDecimal()
        {
            int temp = 0;
            int power = 0;

            for (int i = BinaryNumber.Length - 1; i >= 0; i--)
                temp += int.Parse(BinaryNumber[i].ToString()) * (int)Math.Pow(numberBase, power++);

            return temp.ToString();
        }
        private string toHexadecimal()
        {
            string result = string.Empty;
            int temp = 0;
            int power = 0;

            for (int i = BinaryNumber.Length - 1; i >= 0; i--)
            {
                if (power == 4)
                {
                    result = hexLetters[temp] + result;
                    power = 0;
                    temp = 0;
                }
                temp += int.Parse(BinaryNumber[i].ToString()) * (int)Math.Pow(numberBase, power++);
            }
            return hexLetters[temp] + result;
        }
    }

    public class Octal
    {
        public string OctalNumber { get; private set; }
        private char[] validCharacters = { '0', '1', '2', '3', '4', '5', '6', '7' };
        private const int numberBase = 8;
        private Dictionary<int, string> hexLetters = new Dictionary<int, string>()
            { {0,"0"}, {1,"1"},{2,"2"},{3,"3"},{4,"4"},{5,"5"},{6,"6"},{7,"7"},{8,"8"},{9,"9"},{10,"A"},{11,"B"},{12,"C"},{13,"D"},{14,"E"},{15,"F"}};
        public Octal(string octalNumber)
        {
            OctalNumber = octalNumber;
        }
        public bool IsValid()
        {
            if (OctalNumber.Length == 0) return false;
            return OctalNumber.All(c => validCharacters.Contains(c));
        }
        public string ConvertTo(NumberBase baseToConvertTo)
        {
            switch (baseToConvertTo)
            {
                case NumberBase.Binary:
                    return toBinary();
                case NumberBase.Octal:
                    return OctalNumber;
                case NumberBase.Decimal:
                    return toDecimal();
                case NumberBase.Hexadecimal:
                    return toHexadecimal();
                default:
                    return string.Empty;
            }
        }

        private string toBinary()
        {
            int[] temp = new int[OctalNumber.Length * 3];
            int j = temp.Length - 1;
            int h = temp.Length - 1;

            for (int i = OctalNumber.Length - 1; i >= 0; i--)
            {
                int quotient = int.Parse(OctalNumber[i].ToString());
                int remainder;
                do
                {
                    int tempQuotient = quotient;
                    quotient /= (int)NumberBase.Binary;
                    remainder = tempQuotient % (int)NumberBase.Binary;
                    temp[j--] = remainder;
                } while (quotient > 0);
                j = j != h - 3 ? h - 3 : j;
                h = j;
            }

            StringBuilder sb = new StringBuilder();
            foreach (int num in temp)
                sb.Append(num);

            return sb.ToString();
        }

        private string toDecimal()
        {
            long temp = 0;
            int power = 0;

            try
            {
                for (int i = OctalNumber.Length - 1; i >= 0; i--)
                    temp += int.Parse(OctalNumber[i].ToString()) * (int)Math.Pow(numberBase, power++);
            }
            catch (Exception)
            {
                return "Error";
            }

            return temp.ToString();
        }

        private string toHexadecimal()
        {
            int arraySize = OctalNumber.Length % 4 == 0 ? (OctalNumber.Length / 4) * 4 : (OctalNumber.Length / 4) * 4 + 4;
            int[] octalDigits = new int[arraySize];
            for (int i = OctalNumber.Length - 1, j = octalDigits.Length - 1; i >= 0; i--, j--)
                octalDigits[j] = int.Parse(OctalNumber[i].ToString());

            string result = string.Empty;

            for (int i = 0; i < octalDigits.Length; i += 4)
            {
                string firstHexDigit = hexLetters[((octalDigits[i] * 2) + (octalDigits[i + 1] / 4)) % 16];
                string secondHexDigit = hexLetters[((octalDigits[i + 1] * 4) + (octalDigits[i + 2] / 2)) % 16];
                string thirdHexDigit = hexLetters[((octalDigits[i + 2] * 8) + (octalDigits[i + 3] / 1)) % 16];
                result += firstHexDigit + secondHexDigit + thirdHexDigit;
            }

            return result;
        }
    }

    public class Decimal
    {
        public string DecimalNumber { get; private set; }
        private char[] validCharacters = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        private Dictionary<int, string> hexLetters = new Dictionary<int, string>()
            { {0,"0"}, {1,"1"},{2,"2"},{3,"3"},{4,"4"},{5,"5"},{6,"6"},{7,"7"},{8,"8"},{9,"9"},{10,"A"},{11,"B"},{12,"C"},{13,"D"},{14,"E"},{15,"F"}};
        public Decimal(string decimalNumber)
        {
            DecimalNumber = decimalNumber;
        }
        public bool IsValid()
        {
            if (DecimalNumber.Length == 0) return false;
            return DecimalNumber.All(c => validCharacters.Contains(c));
        }
        public string ConvertTo(NumberBase baseToConvertTo)
        {
            switch (baseToConvertTo)
            {
                case NumberBase.Binary:
                    return toBaseNumber(NumberBase.Binary);
                case NumberBase.Octal:
                    return toBaseNumber(NumberBase.Octal);
                case NumberBase.Decimal:
                    return DecimalNumber;
                case NumberBase.Hexadecimal:
                    return toBaseNumber(NumberBase.Hexadecimal);
                default:
                    return string.Empty;
            }
        }
        private string toBaseNumber(NumberBase numberBase)
        {
            int decimalNumber;
            try
            {
                decimalNumber = int.Parse(DecimalNumber);
            }
            catch (Exception)
            {
                return "Error";
            }

            if (decimalNumber == 0) return "0";

            int quotient = decimalNumber;
            string result = string.Empty;

            while (quotient > 0)
            {
                int temp = quotient;
                quotient /= (int)numberBase;
                if (numberBase == NumberBase.Hexadecimal)
                    result = hexLetters[temp % (int)numberBase] + result;
                else
                    result = (temp % (int)numberBase).ToString() + result;
            }

            return result;
        }

    }

    public class Hexadecimal
    {
        public string HexNumber { get; private set; }
        private char[] validCharacters = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
        private const int numberBase = 16;
        private Dictionary<string, int> hexNumbers = new Dictionary<string, int>()
            { {"0",0}, {"1",1},{"2",2},{"3",3},{"4",4},{"5",5},{"6",6},{"7",7},{"8",8},{"9",9},{"A",10},{"B",11},{"C",12},{"D",13},{"E",14},{"F",15}};
        public Hexadecimal(string hexNumber)
        {
            HexNumber = hexNumber.ToUpper();
        }
        public bool IsValid()
        {
            if (HexNumber.Length == 0) return false;
            return HexNumber.All(c => validCharacters.Contains(c));
        }
        public string ConvertTo(NumberBase baseToConvertTo)
        {
            switch (baseToConvertTo)
            {
                case NumberBase.Binary:
                    return toBinary();
                case NumberBase.Octal:
                    return toOctal();
                case NumberBase.Decimal:
                    return toDecimal();
                case NumberBase.Hexadecimal:
                    return HexNumber;
                default:
                    return string.Empty;
            }
        }
        private string toBinary()
        {
            int[] binaryArray = new int[HexNumber.Length * 4];
            for (int i = 3, j = 0; i < binaryArray.Length; i += 4, j++)
            {
                int hexNumber = hexNumbers[HexNumber[j].ToString()];
                int quotient = hexNumber;
                if (quotient == 0) continue;
                int index = i;
                while (quotient > 0)
                {
                    binaryArray[index--] = quotient % 2;
                    quotient /= 2;
                }
            }

            StringBuilder sb = new StringBuilder();
            foreach (int binary in binaryArray)
                sb.Append(binary);

            return sb.ToString();
        }

        private string toOctal()
        {
            string binary = toBinary();
            string result = string.Empty;
            int temp = 0;
            int power = 0;

            for (int i = binary.Length - 1; i >= 0; i--)
            {
                if (power == 3)
                {
                    result = temp.ToString() + result;
                    power = 0;
                    temp = 0;
                }
                temp += int.Parse(binary[i].ToString()) * (int)Math.Pow((int)NumberBase.Binary, power++);
            }
            return temp.ToString() + result;
        }
        private string toDecimal()
        {
            long temp = 0;
            int power = 0;

            try
            {
                for (int i = HexNumber.Length - 1; i >= 0; i--)
                    temp += hexNumbers[HexNumber[i].ToString()] * (int)Math.Pow(numberBase, power++);
            }
            catch (Exception)
            {
                return "Error";
            }

            return temp.ToString();
        }
    }
    public enum NumberBase
    {
        Binary = 2, Octal = 8, Decimal = 10, Hexadecimal = 16
    }
}
