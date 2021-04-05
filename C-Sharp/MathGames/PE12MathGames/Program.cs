using System;

namespace PE12MathGames
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
                Console.WriteLine("Press any key to continue...");
                Console.ReadLine();
            }
        }
    }
}
