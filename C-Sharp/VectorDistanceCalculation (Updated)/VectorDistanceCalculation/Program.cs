using System;

namespace VectorDistanceCalculation
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
                Console.WriteLine($"Error: {e.InnerException}");
                Console.WriteLine($"Error: {e.Message}");
                Console.WriteLine("Press any key to exit...");
                Console.ReadLine();
            }
        }
    }
}
