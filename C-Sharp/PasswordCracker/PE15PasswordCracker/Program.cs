using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PE15PasswordCracker
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Util.Run();
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message}");
                Console.WriteLine("The program will now exit...");
                Thread.Sleep(4000);
            }
        }
    }

    static class Util
    {
        private static char[] printableChars = new char[95];
        private static string crackedPassword = "";
        public static void Run()
        {
            while (true)
            {
                string password = GetPassword();
                Stopwatch sw = Stopwatch.StartNew();

                CrackPasswordSingleThread(password);
                Console.WriteLine($"Using Single Thread => Password: {crackedPassword} | Password cracked in {sw.ElapsedMilliseconds} ms");
                crackedPassword = "";

                sw.Restart();
                CrackPasswordMultiThread(password);
                Console.WriteLine($"Using Multi Thread => Password: {crackedPassword} | Password cracked in {sw.ElapsedMilliseconds} ms");
                crackedPassword = "";
                Console.ReadLine();
            }
        }

        public static void CrackPasswordSingleThread(string password)
        {
            int printableChar = 32; // This is the first printable character on ASCII table
            for (int i = 0; i < printableChars.Length; i++) // This populates the printableChars array
                printableChars[i] = (char)printableChar++;

            // This is the formula to calculate the number of all possible permutations
            long allPermutations = (long)Math.Pow(printableChars.Length, password.Length);

            for (long i = 0; i < allPermutations; i++)
            {
                long[] permutation = new long[password.Length];
                int numberPlace = 0;
                long j = i;
                long r;
                
                while (j > 0) // This loop converts i into base 95
                {
                    r = j % printableChars.Length;
                    permutation[permutation.Length - 1 - numberPlace++] = r;
                    j /= printableChars.Length;
                }

                // Maps the base 95 numbers in permutation to characters
                // then checks to see if the result matches the password
                if (password == ConvertToString(permutation)) 
                {
                    crackedPassword = password;
                    return;
                }
            }
        }

        public static void CrackPasswordMultiThread(string password)
        {
            int printableChar = 32; // This is the first printable character on ASCII table
            for (int i = 0; i < printableChars.Length; i++) // This populates the printableChars array
            {
                printableChars[i] = (char)printableChar++;
            }

            // This is the formula to calculate the number of all possible permutations
            long allPermutations = (long)Math.Pow(printableChars.Length, password.Length);

            Parallel.For(0, allPermutations, (i, p) =>
            {
                long[] permutation = new long[password.Length];
                int numberPlace = 0;
                long j = i;
                long r;

                while (j > 0) // This loop converts i into base 95
                {
                    r = j % printableChars.Length;
                    permutation[permutation.Length - 1 - numberPlace++] = r;
                    j /= printableChars.Length;
                }

                // Maps the base 95 numbers in permutation to characters
                // then checks to see if the result matches the password
                if (password == ConvertToString(permutation))
                {
                    crackedPassword = password;
                    p.Break();
                }
            });
        }

        private static string ConvertToString(long[] permutation)
        {
            char[] result = new char[permutation.Length];
            for (int i = 0; i < permutation.Length; i++)
                result[i] = printableChars[permutation[i]];

            return new string(result);
        }

        public static string GetPassword(int numOfAttempts = 5)
        {
            if (numOfAttempts < 0) throw new ArgumentException("You did not provide a valid password");

            Console.Write("Please enter password: ");
            string userInput = Console.ReadLine();
            if (IsValidPassword(userInput)) return userInput;
            else
            {
                Console.WriteLine("One or more invalid characters. Please try again");
                return GetPassword(--numOfAttempts);
            }
        }

        private static bool IsValidPassword(string password)
        {
            if (password.Length == 0) return false;

            foreach (char character in password)
                if (!IsValidChar(character)) return false;
            
            return true;
        }

        private static bool IsValidChar(char character)
        {
            // ASCII printable characters are between 32 and 126, inclusive; they are the only
            // valid characters accepted in a password in this program
            return character >= 32 && character <= 126; 
        }
    }
}

        // Below methods are my initial attempts to generate all permutations
        // While they work, they are slow
        /*
        public static HashSet<string> GetPurmutation(string password)
        {
            int printableChar = 32;
            for (int i = 0; i < printableChars.Length; i++)
                printableChars[i] = (char)printableChar++;

            long numOfAllCombos = (long)Math.Pow(printableChars.Length, password.Length);
            HashSet<string> result = new HashSet<string>();

            for (int i = 0; i < numOfAllCombos; i++)
            {
                char[] charArray = new char[password.Length];
                int power = password.Length - 1;
                int mod;
                for (int j = 0; j < password.Length; j++)
                {
                    mod = (int)Math.Pow(printableChars.Length, power);
                    if (mod == 1)
                    {
                        mod = (int)Math.Pow(printableChars.Length, 1);
                    }
                    int index = (i / (int)Math.Pow(printableChars.Length, power--)) % mod;
                    index = index >= printableChars.Length ? index % printableChars.Length : index;

                    charArray[j] = printableChars[index];
                }

                result.Add(new string(charArray));
            }
            Console.WriteLine($"There are {result.Count} permutations");
            return result;
        }

        public static ConcurrentBag<string> GetPurmutationMultiThread(string password)
        {
            int printableChar = 32;
            for (int i = 0; i < printableChars.Length; i++)
                printableChars[i] = (char)printableChar++;

            long numOfAllCombos = (long)Math.Pow(printableChars.Length, password.Length);
            ConcurrentBag<string> result = new ConcurrentBag<string>();

            Parallel.For(0, numOfAllCombos, i =>
            {
                char[] charArray = new char[password.Length];
                int power = password.Length - 1;

                for (int j = 0; j < password.Length; j++)
                {
                    int mod = (int)Math.Pow(printableChars.Length, power);
                    if (mod == 1)
                        mod = (int)Math.Pow(printableChars.Length, 1);

                    long index = (i / (long)Math.Pow(printableChars.Length, power--)) % mod;
                    index = index >= printableChars.Length ? index % printableChars.Length : index;
                    charArray[j] = printableChars[index];
                };
                result.Add(new string(charArray));
            }
            );

            Console.WriteLine($"There are {result.Count} permutations");
            return result;
        }
        */


