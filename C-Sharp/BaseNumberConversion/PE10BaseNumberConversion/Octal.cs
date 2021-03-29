using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE10BaseNumberConversion
{
    public class Octal
    {
        public string OctalNumber { get; private set; }
        private const int numberBase = 8;

        public Octal(string octalNumber)
        {
            OctalNumber = octalNumber;
        }
        public bool IsValid()
        {
            if (OctalNumber.Length == 0) return false;
            return OctalNumber.All(c => NumberBaseUtility.ValidOctalCharacters.Contains(c));
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
                string firstHexDigit = NumberBaseUtility.HexLetters[((octalDigits[i] * 2) + (octalDigits[i + 1] / 4)) % 16];
                string secondHexDigit = NumberBaseUtility.HexLetters[((octalDigits[i + 1] * 4) + (octalDigits[i + 2] / 2)) % 16];
                string thirdHexDigit = NumberBaseUtility.HexLetters[((octalDigits[i + 2] * 8) + (octalDigits[i + 3] / 1)) % 16];
                result += firstHexDigit + secondHexDigit + thirdHexDigit;
            }

            return result;
        }
    }
}
