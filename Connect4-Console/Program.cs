using System;
using System.Text;
using System.Text.RegularExpressions;


namespace Connect4_Console
{
    public class Program
    {
        private string[,] _board= {};
        private static int BoardRows
        {
            get
            {
                return string.IsNullOrEmpty(Helper.ReadSetting("Rows")) ? 5 : Convert.ToInt32(Helper.ReadSetting("Rows"));
            }
        }
        private static int BoardColumns
        {
            get
            {
                return string.IsNullOrEmpty(Helper.ReadSetting("Columns")) ? 5 : Convert.ToInt32(Helper.ReadSetting("Columns"));
            }
        }
        public string Red
        {
            get
            {
                return string.IsNullOrEmpty(Helper.ReadSetting("Gamer1Symbol")) ? "R" : Helper.ReadSetting("Gamer1Symbol");
            }
        }
        public string Yellow
        {
            get
            {
                return string.IsNullOrEmpty(Helper.ReadSetting("Gamer2Symbol")) ? "Y" : Helper.ReadSetting("Gamer2Symbol");
            }
        }
        private string Empty
        {
            get
            {
                return string.IsNullOrEmpty(Helper.ReadSetting("EmptySymbol")) ? "0" : Helper.ReadSetting("EmptySymbol");
            }
        }
        private static int DiscToWin
        {
            get
            {
                return string.IsNullOrEmpty(Helper.ReadSetting("DiscsToWin")) ? 4 : Convert.ToInt32(Helper.ReadSetting("DiscsToWin"));
            }
        }
        private string CurrentPlayer { get; set; }
        private string Winner { get; set; }

        public Program()
        {
            SetupBoard();
            CurrentPlayer = Red;
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
                Console.Write(p.GetCurrentGamer() + "'s Turn. ");
                var input = 0;

                while (input < 1 || input > BoardColumns)
                {
                    Console.Write("Please enter a number between 1 and " + BoardColumns + ": ");
                    input = Helper.ConvertToInt(Console.ReadLine());
                }

                while (p.IsColumnFull(input))
                {
                    Console.Write("This column is full, please enter a new integer: ");
                    input = Helper.ConvertToInt(Console.ReadLine());
                }

                p.InsertDiscInColumn(input);
            }

            Console.Read();
        }

        public void SetupBoard()
        {
            _board = new string[BoardRows, BoardColumns];
            // populating empty board       
            for (int row = 0; row < BoardRows; row++)
            {
                for (int column = 0; column < BoardColumns; column++)
                {
                    _board[row, column] = Empty;
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

        public bool IsColumnFull(int col)
        {
            return (CountDiscsInColumn(col - 1) == BoardRows);
        }

        private int CountDiscsInColumn(int column)
        {
            var count = 0;
            for (int row = 0; row < BoardRows; row++)
            {
                if (_board[row, column] != Empty) count++;
            }

            return count;
        }

        public int InsertDiscInColumn(int col)
        {
            if (col <= 0 || col > BoardColumns)
                throw new Exception("Invalid input, Please enter a number between 1 and :" + BoardColumns);

            int row = CountDiscsInColumn(col - 1);

            if (row == BoardRows)
                throw new Exception("Column is full");

            _board[row, col - 1] = CurrentPlayer;
            DisplayBoard(_board);
            GameRules(row, col - 1);
            SwitchGamer();

            return row;
        }

        public string GetCurrentGamer()
        {
            return CurrentPlayer;
        }

        public bool IsFinished()
        {
            if (!string.IsNullOrEmpty(Winner))
            {
                Console.WriteLine(Winner + ", You have won!");
                return true;
            }

            return CountDiscsOnBoard() == BoardRows * BoardColumns;
        }

        public string GetWinner()
        {
            return Winner;
        }

        void SwitchGamer()
        {
            CurrentPlayer = (Red == CurrentPlayer) ? Yellow : Red;
        }

        void GameRules(int row, int col)
        {
            string pattern = @".*" + CurrentPlayer + "{" + DiscToWin + "}.*"; //@"{.*R{4}.*}";
            Regex regex = new Regex(pattern);
            StringBuilder sb = new StringBuilder();
            for (var i = 0; i < BoardRows; i++)
            {
                sb.Append(_board[i, col]); //Vertical
            }
            if (regex.IsMatch(sb.ToString()))
            {
                Winner = GetCurrentGamer();
            }


            sb = new StringBuilder();
            for (var i = 0; i < BoardColumns; i++)
            {
                sb.Append(_board[row, i]); //Horizontal
            }
            if (regex.IsMatch(sb.ToString()))
            {
                Winner = GetCurrentGamer();
            }

            //LHS
            int offset = Math.Min(row, col);
            int currnetColumn = col - offset;
            int currnetRow = row - offset;
            sb = new StringBuilder();
            do
            {
                sb.Append(_board[currnetRow++, currnetColumn++]);
            } while (currnetRow < BoardRows && currnetColumn < BoardColumns);
            if (regex.IsMatch(sb.ToString()))
                Winner = GetCurrentGamer();

            //RHS
            offset = Math.Min(BoardRows - 1 - row, col);
            currnetColumn = col - offset;
            currnetRow = row + offset;
            sb = new StringBuilder();
            do
            {
                sb.Append(_board[currnetRow--, currnetColumn++]);
            } while (currnetColumn < BoardColumns && currnetRow >= 0);
            if (regex.IsMatch(sb.ToString()))
                Winner = GetCurrentGamer();

        }

        void DisplayBoard(string[,] board)
        {
            var sb = new StringBuilder();
            for (int row = BoardRows - 1; row >= 0; row--)
            {
                sb.Append("|");
                for (int col = 0; col < BoardColumns; col++)
                {
                    sb.Append(board[row, col]);
                }
                sb.Append("| \n");
            }
            sb.Append("\n");
            Console.Write(sb.ToString());
        }

        static void GameConfiguration()
        {
            Console.WriteLine("Configuration: ");
            Console.WriteLine("=======================");

            Console.Write("Enter number of Rows: ");
            //int rows = ConfigUserInput();
            int rows = Helper.ConvertToInt(Console.ReadLine());
            if (rows >= DiscToWin)
                Helper.AddUpdateAppSettings("Rows", rows.ToString());
            else
                Console.WriteLine("Sorry your input is invalid. default value is: " + BoardColumns);


            Console.Write("Enter number of Columns: ");
            int cols = Helper.ConvertToInt(Console.ReadLine());
            if (cols >= DiscToWin)
                Helper.AddUpdateAppSettings("Columns", cols.ToString());
            else
                Console.WriteLine("Sorry your input is invalid. default value is: " + BoardColumns);

        }
    }
}
