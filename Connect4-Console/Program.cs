using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Connect4_Console
{
    public class Program
    {
        public static int BoardRows { get; set; }
        public static int BoardColumns { get; set; }
        public string[,] Board;
        public static string Empty = "O";
        public static string Red = "R";
        public static string Yellow = "Y";
        private string currentGamer = Red;
        private string winner = "";

        public Program(int rows = 5, int cols = 5)
        {
            Board = new string[BoardRows = rows, BoardColumns = cols];
            // populating empty board       
            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < cols; column++)
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
            BoardRows = rows;
            BoardColumns = cols;
            return true;
        }

        public int InsertDiscInColumn(int column)
        {
            int row = CountDiscsInColumn(column-1);

            if (row == BoardRows)
                throw new ApplicationException("Column is full");

            Board[row, column-1] = currentGamer;
            DisplayBoard(Board);
            IsWinner(row, column-1);
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
            return CountDiscsOnBoard() == BoardRows * BoardColumns;
        }

        public string GetWinner()
        {
            return winner;
        }

        private void IsWinner(int row, int col)
        {
            string pattern = @".*"+ currentGamer + "{4}.*"; //@"{.*R{4}.*}";
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
        }


        public void DisplayBoard(string[,] board)
        {

            for (int row = BoardRows - 1; row >= 0; row--)
            {
                Console.Write("|");
                for (int col = 0; col < BoardColumns;col++)
                {
                    Console.Write(board[row, col]);
                }
                Console.Write("| \n");
            }
            Console.WriteLine("\n");
        }

        static void Main(string[] args)
        {
            Console.Write("Enter number of rows/cols: ");

            while (!Program.ValidateGameUserinput(Console.ReadLine())) ;

            Console.WriteLine(BoardRows + " x " + BoardColumns);
            Program p = new Program(BoardRows, BoardColumns);

            while (!p.IsFinished())
            {
                Console.Write(p.GetCurrentGamer() +", where would you like to place your next disc?\n");
                p.InsertDiscInColumn(Convert.ToInt32(Console.ReadLine()));
            }

            Console.Read();
        }
    }
}
