using System;
using System.Text;
using System.Threading;
using System.Media;

namespace PE07Roulette
{
    class Program
    {
        // TODO: Set the text color to green, red, or black depending on the ball location
        // TODO: Add timer object to simulate the ball spinning

        static void Main(string[] args)
        {
            Roulette roulette = new Roulette();
            roulette.DisplayWelcomeMessage();
            Player player = new Player();

            int userChoiceNumber = Menu.GetUserChoiceFromMainMenu();

            while (userChoiceNumber <= 3)
            {
                switch (userChoiceNumber)
                {
                    case 1:
                        while (userChoiceNumber == 1)
                        {
                            roulette.DisplayAllBins();
                            roulette.DisplayBettingArea();
                            Console.Write($"\nYou are starting out with {player.Asset:C}.");
                            Console.Write("\nPlease place your bet: ");
                            int betAmount = player.Bet();

                            // player.WinBetMoney(betAmount, 2); TODO

                            roulette.Spin();
                            roulette.DisplayWinningBets();
                            ClearScreen();
                            userChoiceNumber = Menu.GetUserChoiceFromMainMenu();
                        }
                        break;
                    case 2:
                        while (userChoiceNumber == 2)
                        {
                            roulette.DisplayRules();
                            ClearScreen();
                            userChoiceNumber = Menu.GetUserChoiceFromMainMenu();
                        }
                        break;
                    default:
                        Console.WriteLine("Thank you for playing!");
                        Thread.Sleep(TimeSpan.FromSeconds(2));
                        return;
                        //Environment.Exit(0);
                        //break;
                }
            }
        }

        static void ClearScreen()
        {
            Console.Write("Press any key to continue...");
            Console.ReadLine();
            Console.Clear();
        }
    }

    static class Menu
    {
        public static int GetUserChoiceFromMainMenu()
        {
            Console.WriteLine("Press the number corresponding to the menu item below:\n");
            Console.WriteLine("  [1] Play the Roulette Game");
            Console.WriteLine("  [2] Read the rules of the Game");
            Console.WriteLine("  [3] Exit the Game");
            Console.Write("\nEnter your choice: ");
            Console.ForegroundColor = ConsoleColor.Green;
            string userChoice = Console.ReadLine();
            Console.ResetColor();
            try
            {
                int userChoiceNumber = int.Parse(userChoice);
                switch (userChoiceNumber)
                {
                    case 1:
                    case 2:
                    case 3:
                        return userChoiceNumber;
                    default:
                        throw new Exception();
                }
            }
            catch (Exception)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please enter a valid choice.\n");
                Console.ResetColor();
                return GetUserChoiceFromMainMenu();
            }
        }

        public static void PerformUserChoice(int userChoiceNumber)
        {
            // TODO:
        }
    }
    class Player
    {
        public int BetAmount { get; set; }
        public int Asset { get; private set; } = 100; // player starts out with $100

        public int Bet()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            string userInput = Console.ReadLine();
            Console.ResetColor();
            try
            {
                int betAmount = int.Parse(userInput);
                Asset -= betAmount;
                return betAmount;
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Please enter a valid bet amount: ");
                Console.ResetColor();
            }
            return Bet();
        }
        public void WinBetMoney(int betAmount, int multiplier)
        {
            Asset = betAmount * multiplier;
        }
        
    }
    class Roulette
    {
        private const int binSize = 38;
        private Random random = new Random();
        public Bin[] Bins { get; private set; } = new Bin[binSize];
        public Bin BallLocation { get; private set; }
        private Bin[] sortedBins = new Bin[36]; // I might use this later
        public int MaximumBetAllowed { get; set; } = 50;

        public Roulette()
        {
            initializeEachBin();
            setNonZeroBins();
            setSingleAndDoubleZerosToGreen();
        }

        public void DisplayRules()
        {
            Console.WriteLine("\nPlease read the rules below:\n");
            Console.WriteLine("1.  Numbers: the number of the slot/bin");
            Console.WriteLine("2.  Evens/Odds: even or odd numbers");
            Console.WriteLine("3.  Reds/Blacks: red or black colored numbers");
            Console.WriteLine("4.  Lows/Highs: low (1 - 18) or high (19 - 36) numbers");
            Console.WriteLine("5.  Dozens: row thirds, 1 - 12, 13 - 24, or 25 - 36");
            Console.WriteLine("6.  Columns: first, second, or third columns");
            Console.WriteLine("7.  Street: rows, e.g., 1 / 2 / 3 or 22 / 23 / 24");
            Console.WriteLine("8.  6 Numbers: double rows, e.g., 1 / 2 / 3 / 4 / 5 / 6 or 22 / 23 / 24 / 25 / 26 / 26");
            Console.WriteLine("9.  Split: at the edge of any two contiguous numbers, e.g., 1 / 2, 11 / 14, and 35 / 36");
            Console.WriteLine("10. Corner: at the intersection of any four contiguous numbers, e.g., 1 / 2 / 4 / 5, or 23 / 24 / 26 / 27");
        }
        public void DisplayWelcomeMessage()
        {
            string message = "#########  Welcome to the Las Vegas Casino  #########".ToUpper();
            displayTextWithForegroundColor(message, ConsoleColor.DarkGreen);
            Console.WriteLine("         Where the House Doesn't Always Win!");
            Console.WriteLine(" Try your luck at our House and see what you can win!\n\n");
        }
        public void DisplayBettingArea()
        {
            Console.WriteLine("\nHere is the layout of the betting area:\n");
            Bin[] tempBinArray = new Bin[binSize];
            Bins.CopyTo(tempBinArray, 0);
            Array.Sort(tempBinArray);

            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" Rows   01    02    03    04    05    06    07    08    09    10    11    12            ");
            Console.WriteLine("----------------------------------------------------------------------------------------");
            Console.BackgroundColor = ConsoleColor.Green;
            displayTextWithForegroundColor("  00  ", ConsoleColor.White, false);
            displayBettingAreaRow(tempBinArray, 4); // draws top row
            Console.BackgroundColor = ConsoleColor.Green;
            displayTextWithForegroundColor("      ", ConsoleColor.White, false);
            displayBettingAreaRow(tempBinArray, 3); // draws middle row
            Console.BackgroundColor = ConsoleColor.Green;
            displayTextWithForegroundColor("  0   ", ConsoleColor.White, false);
            displayBettingAreaRow(tempBinArray, 2);
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.White; 
            Console.WriteLine("----------------------------------------------------------------------------------------");
            Console.WriteLine("Dozens|        1st 12         |        2nd 12         |        3rd 12         |         ");
            Console.WriteLine("----------------------------------------------------------------------------------------");
            Console.Write("Others| 01 to 18 |    EVEN    |    ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("RED");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("    |   ");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("BLACK");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("   |   ODD    |  19 to 36  |         ");
            Console.ResetColor();
            Console.WriteLine();
        }

        private void displayBettingAreaRow(Bin[] tempBinArray, int startPosition)
        {
            for (int i = startPosition; i < tempBinArray.Length; i += 3)
            {
                string strNumber = "0";
                if (tempBinArray[i].Number < 10) strNumber += tempBinArray[i].Number.ToString();
                else strNumber = tempBinArray[i].Number.ToString();
                strNumber = "  " + strNumber + "  ";

                switch (tempBinArray[i].Color)
                {
                    case Color.Red:
                        Console.BackgroundColor = ConsoleColor.Green;
                        displayTextWithForegroundColor(strNumber, ConsoleColor.Red, false);
                        break;
                    case Color.Black:
                        Console.BackgroundColor = ConsoleColor.Green;
                        displayTextWithForegroundColor(strNumber, ConsoleColor.Black, false);
                        break;
                }
            }
            Console.BackgroundColor = ConsoleColor.Green;
            displayTextWithForegroundColor("  2 to 1  ",ConsoleColor.White);
        }
        public void Spin()
        {
            BallLocation = Bins[random.Next(binSize)];
            Console.Write("\nPlease press any key to spin the wheel...");
            Console.ReadLine();
            Console.WriteLine("\nThe wheel is spinning... please wait.");
            playWheelSpinningSound(@"C:\Users\Jae Yi\Desktop\Spinning.wav");
           
            //Thread.Sleep(TimeSpan.FromSeconds(3));

            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("\nThe ball has landed in ");
            
            switch (BallLocation.Color)
            {
                case Color.Red:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{BallLocation}\n");
                    break;
                case Color.Black:
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine($"{BallLocation}\n");
                    break;
                case Color.Green:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{BallLocation}\n");
                    break;
            }

            Thread.Sleep(TimeSpan.FromSeconds(1));
            Console.ResetColor();
        }
        private void playWheelSpinningSound(string filepath)
        {
            try
            {
                using (SoundPlayer sp = new SoundPlayer(filepath))
                {
                    sp.PlaySync();
                }
            }
            catch (Exception)
            {
                Console.WriteLine("The sound file is missing. Simulating the ball spinning around the wheel...");
                Thread.Sleep(TimeSpan.FromSeconds(5));
            }
        }
        private void displayTextWithBackgroundColor(string text, ConsoleColor color, bool newLine = true)
        {
            Console.BackgroundColor = color;
            if (newLine) Console.WriteLine(text);
            else Console.Write(text);
            Console.ResetColor();
        }
        private void displayTextWithForegroundColor(string text, ConsoleColor color, bool newLine = true)
        {
            Console.ForegroundColor = color;
            if (newLine) Console.WriteLine(text);
            else Console.Write(text);
            Console.ResetColor();
        }
        public void DisplayWinningBets()
        {
            if (BallLocation.Number == 0)
            {
                Console.WriteLine("Sorry, but the House wins!".ToUpper());
                return;
            }

            string numberBet = getSingleNumberBetWinner();
            string evenOddBet = getEvenOddBetWinner();
            string redBlackBet = getRedBlackBetWinner();
            string lowHighBet = getLowHighBetWinner();
            string dozenBet = getDozenBetWinner();
            string columnBet = getColumnBetWinner();
            string streetBet = getRowBetWinner();
            string doubleRowBet = getDoubleRowBetWinner();
            string splitBet = getSplitBetWinner();
            string cornerBet = getCornerBetWinner();

            Console.WriteLine(combineBetResults(numberBet, evenOddBet, redBlackBet, lowHighBet, dozenBet,
                columnBet, streetBet, doubleRowBet, splitBet, cornerBet));
        }
        private string getSingleNumberBetWinner()
        {
            return $"The single number bet winner is {BallLocation.Number}.";
        }
        private string getEvenOddBetWinner()
        {
            if (BallLocation.Number % 2 == 0)
                return "The even/odd number bet winner is even.";
            else
                return "The even/odd number bet winner is odd.";
        }
        private string getRedBlackBetWinner()
        {
            if (BallLocation.Color == Color.Black)
                return "The red/black bet winner is black.";
            else
                return "The red/black bet winner is red.";
        }
        private string getLowHighBetWinner()
        {
            if (BallLocation.Number >= 1 && BallLocation.Number <= 18)
                return "The low/high bet winner is low.";
            else
                return "The low/high bet winner is high.";
        }
        private string getDozenBetWinner()
        {
            if (BallLocation.Number >= 1 && BallLocation.Number <= 12)
                return "The thirds bet winner is the 1st 12.";
            else if (BallLocation.Number >= 13 && BallLocation.Number <= 24)
                return "The thirds bet winner is the 2nd 12.";
            else
                return "The thirds bet winner is the 3rd 12.";
        }
        private string getColumnBetWinner()
        {
            if (BallLocation.Number % 3 == 1)
                return "The column bet winner is the first column.";
            else if (BallLocation.Number % 3 == 2)
                return "The column bet winner is the second column.";
            else
                return "The column bet winner is the third column.";
        }
        private string getRowBetWinner() // Row is the same as street
        {
            int rowNumber = (int)(BallLocation.Number / 3.1) + 1;
            return $"The row bet winner is the row {rowNumber}.";
        }
        private string getDoubleRowBetWinner()
        {
            string rowNumbers;
            /* Do we want the row numbers or the first number of each row? This is for the latter.
             * 
            int rowNumber = (int)(BallLocation.Number / 3.1) + (int)(BallLocation.Number / 3.1) * 2 + 1;
            if (rowNumber == 1)
                rowNumbers = $"{rowNumber}, {rowNumber + 3}";
            else if (rowNumber == 34)
                rowNumbers = $"{rowNumber - 3}, {rowNumber}";
            else
                rowNumbers = $"{rowNumber - 3}, {rowNumber}, {rowNumber + 3}";
            */

            int rowNumber = (int)(BallLocation.Number / 3.1) + 1;
            if (rowNumber == 1)
                rowNumbers = $"{rowNumber}, {rowNumber + 1}";
            else if (rowNumber == 12)
                rowNumbers = $"{rowNumber - 1}, {rowNumber}";
            else
                rowNumbers = $"{rowNumber - 1}, {rowNumber}, {rowNumber + 1}";

            return $"The double row bet winners are the rows {rowNumbers}.";
        }
        private string getSplitBetWinner()
        {
            string splitWinners;
            if (BallLocation.Number % 3 == 1)
            {
                if (BallLocation.Number == 1)
                    splitWinners = $"{BallLocation.Number}, {BallLocation.Number + 1}, {BallLocation.Number + 3}";
                else if (BallLocation.Number == 34)
                    splitWinners = $"{BallLocation.Number - 3}, {BallLocation.Number}, {BallLocation.Number + 1}";
                else
                    splitWinners = $"{BallLocation.Number - 3}, {BallLocation.Number}, {BallLocation.Number + 1}, {BallLocation.Number + 3}";
            }
            else if (BallLocation.Number % 3 == 2)
            {
                if (BallLocation.Number == 2)
                    splitWinners = $"{BallLocation.Number - 1}, {BallLocation.Number}, {BallLocation.Number + 1}, {BallLocation.Number + 3}";
                else if (BallLocation.Number == 35)
                    splitWinners = $"{BallLocation.Number - 3}, {BallLocation.Number - 1}, {BallLocation.Number}, {BallLocation.Number + 1}";
                else
                    splitWinners = $"{BallLocation.Number - 3}, {BallLocation.Number - 1}, {BallLocation.Number}, {BallLocation.Number + 1}, {BallLocation.Number + 3}";
            }
            else
            {
                if (BallLocation.Number == 3)
                    splitWinners = $"{BallLocation.Number - 1}, {BallLocation.Number}, {BallLocation.Number + 3}";
                else if (BallLocation.Number == 36)
                    splitWinners = $"{BallLocation.Number - 3}, {BallLocation.Number - 1}, {BallLocation.Number}";
                else
                    splitWinners = $"{BallLocation.Number - 3}, {BallLocation.Number - 1}, {BallLocation.Number}, {BallLocation.Number + 3}";
            }

            return $"The split bet winners are the numbers {splitWinners}.";
        }
        private string getCornerBetWinner()
        {
            string cornerWinners;
            if (BallLocation.Number % 3 == 1)
            {
                if (BallLocation.Number == 1)
                    cornerWinners = $"{BallLocation.Number}, {BallLocation.Number + 1}, {BallLocation.Number + 3}, {BallLocation.Number + 4}";
                else if (BallLocation.Number == 34)
                    cornerWinners = $"{BallLocation.Number - 3}, {BallLocation.Number - 2}, {BallLocation.Number}, {BallLocation.Number + 1}";
                else
                    cornerWinners = $"{BallLocation.Number - 3}, {BallLocation.Number - 2}, {BallLocation.Number}, {BallLocation.Number + 1}, {BallLocation.Number + 3}, {BallLocation.Number + 4}";
            }
            else if (BallLocation.Number % 3 == 2)
            {
                if (BallLocation.Number == 2)
                    cornerWinners = $"{BallLocation.Number - 1}, {BallLocation.Number}, {BallLocation.Number + 1}, {BallLocation.Number + 2}, {BallLocation.Number + 3}, {BallLocation.Number + 4}";
                else if (BallLocation.Number == 35)
                    cornerWinners = $"{BallLocation.Number - 4}, {BallLocation.Number - 3}, {BallLocation.Number - 2}, {BallLocation.Number - 1}, {BallLocation.Number}, {BallLocation.Number + 1}";
                else
                    cornerWinners = $"{BallLocation.Number - 4}, {BallLocation.Number - 3}, {BallLocation.Number - 2}, {BallLocation.Number - 1}, {BallLocation.Number}, {BallLocation.Number + 1}, {BallLocation.Number + 2}, {BallLocation.Number + 3}, {BallLocation.Number + 4}";
            }
            else
            {
                if (BallLocation.Number == 3)
                    cornerWinners = $"{BallLocation.Number - 1}, {BallLocation.Number}, {BallLocation.Number + 2}, {BallLocation.Number + 3}";
                else if (BallLocation.Number == 36)
                    cornerWinners = $"{BallLocation.Number - 4}, {BallLocation.Number - 3}, {BallLocation.Number - 1}, {BallLocation.Number}";
                else
                    cornerWinners = $"{BallLocation.Number - 4}, {BallLocation.Number - 3}, {BallLocation.Number - 1}, {BallLocation.Number}, {BallLocation.Number + 2}, {BallLocation.Number + 3}";
            }

            return $"The corner bet winners are the numbers {cornerWinners}.";
        }

        // public void DescribeAllBins()
        // {
        //     Console.WriteLine("Here are all the numbers and colors of each slot:\n");
        //     for (int i = 0; i < Bins.Length; i++)
        //     {
        //         Console.WriteLine($"Slot {i.ToString().PadLeft(2)}:    {Bins[i].Number.ToString().PadLeft(2)}, {Bins[i].Color}");
        //     }
        //     Console.WriteLine();
        // }

        public void DisplayAllBins()
        {
            Console.WriteLine("\nHere is the layout of the wheel:\n");
            for (int i = 0; i < Bins.Length; i++)
            {
                if (Bins[i].Color == Color.Green)
                {
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    if (i == 37) displayTextWithForegroundColor($"Slot 00, {Bins[i].Color.ToString().PadRight(5)}", ConsoleColor.Green, false);
                    else displayTextWithForegroundColor($"Slot {Bins[i].Number.ToString().PadLeft(2)}, {Bins[i].Color.ToString().PadRight(5)}", ConsoleColor.Green, false);
                }
                else if (Bins[i].Color == Color.Red)
                {
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    displayTextWithForegroundColor($"Slot {Bins[i].Number.ToString().PadLeft(2)}, {Bins[i].Color.ToString().PadRight(5)}", ConsoleColor.Red, false);
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    displayTextWithForegroundColor($"Slot {Bins[i].Number.ToString().PadLeft(2)}, {Bins[i].Color.ToString().PadRight(5)}", ConsoleColor.Black, false);
                }
                
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.Write("          ");

                if (i % 4 == 3)
                {
                    Console.WriteLine();
                }
            }
            Console.WriteLine();
            Console.ResetColor();
        }

        private static string combineBetResults(params string[] bets)
        {
            StringBuilder sb = new StringBuilder();

            foreach (string bet in bets)
            {
                if (!String.IsNullOrEmpty(bet))
                    sb.Append(bet + "\n");
            }

            return sb.ToString();
        }
        private void setNonZeroBins()
        {
            int[] binNumbers = populate36BinNumbers();

            for (int i = 0; i < binNumbers.Length;)
            {
                int binIndexNumber = random.Next(binNumbers.Length);
                if (binNumbers[binIndexNumber] == -1) continue;
                Bins[i + 1].Number = binNumbers[binIndexNumber];
                Bins[i + 1].Color = (i % 2 == 0) ? Color.Black : Color.Red;
                binNumbers[binIndexNumber] = -1;
                i++;
            }
        }
        private void initializeEachBin()
        {
            for (int i = 0; i < binSize; i++)
                Bins[i] = new Bin();
        }
        private void setSingleAndDoubleZerosToGreen()
        {
            Bins[0].Color = Color.Green;
            Bins[37].Color = Color.Green;
        }
        private int[] populate36BinNumbers()
        {
            int[] binNumbers = new int[36];
            for (int i = 0; i < binNumbers.Length; i++)
                binNumbers[i] = i + 1;
            return binNumbers;
        }
    }
    class Bin : IComparable<Bin>
    {
        public int Number { get; set; }
        public Color Color { get; set; }

        public int CompareTo(Bin bin)
        {
            return Number.CompareTo(bin.Number);
        }

        public override string ToString()
        {
            return $"Slot Number: {Number}, Color: {Color}";
        }
    }
    enum Color
    {
        Red, Black, Green
    }
}
