using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE10BaseNumberConversion
{
    public class Decimal
    {
        public string DecimalNumber { get; private set; }

        public Decimal(string decimalNumber)
        {
            DecimalNumber = decimalNumber;
        }
        public bool IsValid()
        {
            if (DecimalNumber.Length == 0) return false;
            return DecimalNumber.All(c => NumberBaseUtility.ValidDecimalCharacters.Contains(c));
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
                    result = NumberBaseUtility.HexLetters[temp % (int)numberBase] + result;
                else
                    result = (temp % (int)numberBase).ToString() + result;
            }

            return result;
        }

    }
}
