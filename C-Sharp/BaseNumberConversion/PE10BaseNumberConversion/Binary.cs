using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE10BaseNumberConversion
{
    public class Binary
    {
        public string BinaryNumber { get; private set; }
        private const int numberBase = 2;

        public Binary(string binaryNumber)
        {
            BinaryNumber = binaryNumber;
        }
        public bool IsValid()
        {
            if (BinaryNumber.Length == 0) return false;
            return BinaryNumber.All(c => NumberBaseUtility.ValidBinaryCharacters.Contains(c));
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
                    result = NumberBaseUtility.HexLetters[temp] + result;
                    power = 0;
                    temp = 0;
                }
                temp += int.Parse(BinaryNumber[i].ToString()) * (int)Math.Pow(numberBase, power++);
            }
            return NumberBaseUtility.HexLetters[temp] + result;
        }
    }

}
