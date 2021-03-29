using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE10BaseNumberConversion
{
    public class Hexadecimal
    {
        public string HexNumber { get; private set; }

        private const int numberBase = 16;

        public Hexadecimal(string hexNumber)
        {
            HexNumber = hexNumber.ToUpper();
        }
        public bool IsValid()
        {
            if (HexNumber.Length == 0) return false;
            return HexNumber.All(c => NumberBaseUtility.ValidHexCharacters.Contains(c));
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
                int hexNumber = NumberBaseUtility.HexNumbers[HexNumber[j].ToString()];
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
                    temp += NumberBaseUtility.HexNumbers[HexNumber[i].ToString()] * (int)Math.Pow(numberBase, power++);
            }
            catch (Exception)
            {
                return "Error";
            }

            return temp.ToString();
        }
    }
}
