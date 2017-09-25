using System;
using System.Text;
using System.Text.RegularExpressions;


namespace Connect4_Console
{
    public class Program
    {
        public static int BoardRows
        {
            get
            {
                return string.IsNullOrEmpty(Helper.ReadSetting("Rows")) ? 5 : Convert.ToInt32(Helper.ReadSetting("Rows"));
            }
        }
        public static int BoardColumns
        {
            get
            {
                return string.IsNullOrEmpty(Helper.ReadSetting("Columns")) ? 5 : Convert.ToInt32(Helper.ReadSetting("Columns"));
            }
        }
        public string[,] Board;
        public static string Empty = "O";

        public static string Red
        {
            get
            {
                return string.IsNullOrEmpty(Helper.ReadSetting("Gamer1Symbol")) ? "R" : Helper.ReadSetting("Gamer1Symbol");
            }
        }
        public static string Yellow
        {
            get
            {
                return string.IsNullOrEmpty(Helper.ReadSetting("Gamer2Symbol")) ? "Y" : Helper.ReadSetting("Gamer2Symbol");
            }
        }
        private string currentGamer = Red;
        private string winner = "";

        private static int DiscToWin
        {
            get
            {
                return string.IsNullOrEmpty(Helper.ReadSetting("DiscsToWin")) ? 4 : Convert.ToInt32(Helper.ReadSetting("DiscsToWin"));
            }
        }

        public Program()
        {
            Board = new string[BoardRows, BoardColumns];
            // populating empty board       
            for (int row = 0; row < BoardRows; row++)
            {
                for (int column = 0; column < BoardColumns; column++)
                {
                    Board[row, column] = Empty;
                }
            }
        }

        public int CountDiscsOnBoard()
        {
            int count = 0;

            for (int column = 0; column < BoardColumns; column++)
            {
                count += CountDiscsInColumn(column);
            }

            return count;
        }

        private int CountDiscsInColumn(int column)
        {
            var count = 0;
            for (int row = 0; row < BoardRows; row++)
            {
                if (Board[row, column] != Empty) count++;
            }

            return count;
        }

        public static bool ValidateGameUserinput(string input)
        {
            int rows, cols;

            var inputs = input.Trim().Split(' ');

            if (inputs.Length != 2 || !int.TryParse(inputs[0], out rows) || !int.TryParse(inputs[1], out cols))
            {
                Console.WriteLine("Not a valid input, please try again.");
                return false;
            }

            Helper.AddUpdateAppSettings("Rows", rows.ToString());
            Helper.AddUpdateAppSettings("Columns", cols.ToString());

            return true;
        }

        public int InsertDiscInColumn(int col)
        {
            if (col <= 0 || col > BoardColumns)
                throw new ApplicationException("Invalid input, Please enter a number between 1 and :" + BoardColumns);

            int row = CountDiscsInColumn(col - 1);

            if (row == BoardRows)
                throw new ApplicationException("Column is full");

            Board[row, col - 1] = currentGamer;
            DisplayBoard(Board);
            GameRules(row, col - 1);
            SwitchGamer();

            return row;
        }

        public string GetCurrentGamer()
        {
            return currentGamer;
        }

        private void SwitchGamer()
        {
            currentGamer = (Red == currentGamer) ? Yellow : Red;
        }

        public bool IsFinished()
        {
            if (!string.IsNullOrEmpty(winner)) return true;

            return CountDiscsOnBoard() == BoardRows * BoardColumns;
        }

        public string GetWinner()
        {
            return winner;
        }

        private void GameRules(int row, int col)
        {
            string pattern = @".*" + currentGamer + "{" + DiscToWin + "}.*"; //@"{.*R{4}.*}";
            Regex regex = new Regex(pattern);
            StringBuilder sb = new StringBuilder();
            for (var i = 0; i < BoardRows; i++)
            {
                sb.Append(Board[i, col]); //Vertical
            }
            if (regex.IsMatch(sb.ToString()))
            {
                winner = GetCurrentGamer();
            }


            sb = new StringBuilder();
            for (var i = 0; i < BoardColumns; i++)
            {
                sb.Append(Board[row, i]); //Horizontal
            }
            if (regex.IsMatch(sb.ToString()))
            {
                winner = GetCurrentGamer();
            }

            //LHS
            int offset = Math.Min(row, col);
            int currnetColumn = col - offset;
            int currnetRow = row - offset;
            sb = new StringBuilder();
            do
            {
                sb.Append(Board[currnetRow++, currnetColumn++]);
            } while (currnetRow < BoardRows && currnetColumn < BoardColumns);
            if (regex.IsMatch(sb.ToString()))
                winner = GetCurrentGamer();

            //RHS
            offset = Math.Min(BoardRows - 1 - row, col);
            currnetColumn = col - offset;
            currnetRow = row + offset;
            sb = new StringBuilder();
            do
            {
                sb.Append(Board[currnetRow--, currnetColumn++]);
            } while (currnetColumn < BoardColumns && currnetRow >= 0);
            if (regex.IsMatch(sb.ToString()))
                winner = GetCurrentGamer();

        }


        public void DisplayBoard(string[,] board)
        {

            for (int row = BoardRows - 1; row >= 0; row--)
            {
                Console.Write("|");
                for (int col = 0; col < BoardColumns; col++)
                {
                    Console.Write(board[row, col]);
                }
                Console.Write("| \n");
            }
            Console.WriteLine("\n");
        }

        private static void GameConfiguration()
        {
            Console.WriteLine("Configuration: ");
            Console.WriteLine("=======================");

            Console.Write("Enter number of Rows: ");
            //int rows = ConfigUserInput();
            int rows = Helper.StrToInt(Console.ReadLine());
            if (rows >= DiscToWin)
                Helper.AddUpdateAppSettings("Rows", rows.ToString());
            else
                Console.WriteLine("Sorry your input is invalid. default value is: " + BoardColumns);


            Console.Write("Enter number of Columns: ");
            int cols = Helper.StrToInt(Console.ReadLine());
            if (cols >= DiscToWin)
                Helper.AddUpdateAppSettings("Columns", cols.ToString());
            else
                Console.WriteLine("Sorry your input is invalid. default value is: " + BoardColumns);

        }

        static void Main(string[] args)
        {
            Console.WriteLine("let's play connect 4: <press a key to begin or '9' to configure>");
            var options = Console.ReadLine();
            if (options == "9")
            {
                GameConfiguration();
            }

            Program p = new Program();

            while (!p.IsFinished())
            {
                Console.Write(p.GetCurrentGamer() + ", where would you like to place your next disc?\n");
                p.InsertDiscInColumn(Convert.ToInt32(Console.ReadLine()));
            }

            Console.WriteLine(p.GetWinner() + ", You have won!");

            Console.Read();
        }
    }
}
