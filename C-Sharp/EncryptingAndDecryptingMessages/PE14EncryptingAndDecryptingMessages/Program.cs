using System;
using System.Text;

namespace PE14EncryptingAndDecryptingMessages
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
                Console.WriteLine(e.InnerException);
                Console.WriteLine(e.Message);
            }
        }
    }

    public static class Menu
    {
        static string plainText;
        static char singleKey;
        static string multiKey;
        public static void Run()
        {
            while (true)
            {
                Console.Write("Please enter plain text to encode: ");

                plainText = Console.ReadLine();
                string cleanText = CleanText(plainText);
                char userInputKey = AsciiToChar(GetSingleKeyFromUser());
                string userInputMultiKey = GetMultiKeyFromUser();

                Console.WriteLine($"\nYou entered [{plainText}] as plain text");
                Console.WriteLine($"You entered [{singleKey}] as your single key");
                Console.WriteLine($"You entered [{multiKey}] as your multi-key");

                string textEncodedSingleKey = EncodeBySingleKey(cleanText, userInputKey);
                string textEncodedMultiKey = EncodeByMultiKey(cleanText, userInputMultiKey);
                string textEncodedContinousKey = EncodeByContinuousKey(cleanText, userInputMultiKey);

                Console.WriteLine($"\nEncrypted message with single key is [{ textEncodedSingleKey }]");
                Console.WriteLine($"Encrypted message with multi-key is [{textEncodedMultiKey}]");
                Console.WriteLine($"Encrypted message with continuous key is [{textEncodedContinousKey}]");

                string textDecoded = DecodeBySingleKey(textEncodedSingleKey, userInputKey);
                string textDecodedMultiKey = DecodeByMultiKey(textEncodedMultiKey, userInputMultiKey);
                string textDecodedContinousKey = DecodeByContinuousKey(textEncodedContinousKey, userInputMultiKey);

                Console.WriteLine($"\nDecrypted message with single key is [{ textDecoded}]");
                Console.WriteLine($"Decrypted message with multi-key is [{textDecodedMultiKey}]");
                Console.WriteLine($"Decrypted message with continuous key is [{textDecodedContinousKey}]");

                Console.Write("\nDo you want to try again? Press Y for Yes or any other key for No: ");
                if (!Console.ReadLine().ToUpper().Equals("Y")) return;
                else Console.Clear();
            }
        }

        public static int GetSingleKeyFromUser(int attempts = 5)
        {
            if (attempts == 0) return 65; // base case: return 65 (A) after five failed attempts
            
            Console.Write("Please enter your single key: ");
            int userInputKey = Console.Read();
            Console.ReadLine();
            if (!IsAlphaChar((char)userInputKey))
            {
                return GetSingleKeyFromUser(--attempts);
            }
            else
            {
                singleKey = (char)userInputKey;
                if (IsLowerAlphaChar((char)userInputKey))
                    return userInputKey - 32;
                else
                    return userInputKey;
            }
        }

        public static string GetMultiKeyFromUser()
        {
            Console.Write("Please enter your multi-key: ");
            multiKey = Console.ReadLine();
            return CleanText(multiKey);
        }

        public static string EncodeBySingleKey(string textToEncode, char key)
        {
            StringBuilder sb = new StringBuilder();
            int positionToAdvance = GetPositionToAdvance(key);
            foreach (char character in textToEncode)
            {
                int codedCharInASCII = character + positionToAdvance;
                sb.Append(AsciiToChar(codedCharInASCII));
            }

            return sb.ToString();
        }

        public static string DecodeBySingleKey(string textToDecode, char key)
        {
            StringBuilder sb = new StringBuilder();
            int positionToAdvance = GetPositionToAdvance(key);
            foreach (char character in textToDecode)
            {
                int codedCharInASCII = character - positionToAdvance;
                sb.Append(AsciiToChar(codedCharInASCII));
            }

            return sb.ToString();
        }

        public static string EncodeByMultiKey(string textToEncode, string multiKey)
        {
            StringBuilder sb = new StringBuilder();
            
            for (int i = 0; i < textToEncode.Length; i++)
            {
                int positionToAdvance = GetPositionToAdvance(multiKey[i % multiKey.Length]);
                int codedCharInASCII = textToEncode[i] + positionToAdvance;
                sb.Append(AsciiToChar(codedCharInASCII));
            }

            return sb.ToString();
        }

        public static string DecodeByMultiKey(string textToDecode, string multiKey)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < textToDecode.Length; i++)
            {
                int positionToAdvance = GetPositionToAdvance(multiKey[i % multiKey.Length]);
                int codedCharInASCII = textToDecode[i] - positionToAdvance;
                sb.Append(AsciiToChar(codedCharInASCII));
            }

            return sb.ToString();
        }

        public static string EncodeByContinuousKey(string textToEncode, string multiKey)
        {
            StringBuilder sb = new StringBuilder();
            int positionToAdvance, codedCharInASCII;

            for (int i = 0; i < textToEncode.Length; i++)
            {
                if (i < multiKey.Length)
                    positionToAdvance = GetPositionToAdvance(multiKey[i % multiKey.Length]);
                else
                    positionToAdvance = GetPositionToAdvance(textToEncode[i - multiKey.Length]);
        
                codedCharInASCII = textToEncode[i] + positionToAdvance;
                sb.Append(AsciiToChar(codedCharInASCII));
            }

            return sb.ToString();
        }
        
        public static string DecodeByContinuousKey(string textToDecode, string multiKey)
        {
            StringBuilder sb = new StringBuilder();
            int positionToAdvance, codedCharInASCII;

            for (int i = 0; i < textToDecode.Length; i++)
            {
                if (i < multiKey.Length)
                    positionToAdvance = GetPositionToAdvance(multiKey[i % multiKey.Length]);
                else
                    positionToAdvance = GetPositionToAdvance(sb[i - multiKey.Length]);

                codedCharInASCII = textToDecode[i] - positionToAdvance;
                sb.Append(AsciiToChar(codedCharInASCII));
            }

            return sb.ToString();
        }

        private static char AsciiToChar(int ascii)
        {
            if (ascii > 90) return (char)(ascii - 26);
            else if (ascii < 65) return (char)(ascii + 26);
            else return (char)ascii;
        }

        public static string CleanText(string text)
        {
            StringBuilder sb = new StringBuilder();
            
            foreach (char character in text)
            {
                if (IsAlphaChar(character))
                {
                    if (IsUpperAlphaChar(character))
                        sb.Append(character);
                    else // it must be a lower case
                        sb.Append((char)(character - 32)); // subtracting 32 would convert a lower case to upper case
                }
            }

            return sb.ToString();
        }

        private static int GetPositionToAdvance(int key)
        {
            return key - 64;
        }

        private static bool IsAlphaChar(char character)
        {
            return IsUpperAlphaChar(character) || IsLowerAlphaChar(character);
        }
        
        private static bool IsUpperAlphaChar(char character)
        {
            return character >= 65 && character <= 90; 
        }

        private static bool IsLowerAlphaChar(char character)
        {
            return character >= 97 && character <= 122;
        }
    }
}
