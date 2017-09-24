using System;

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
        private string CurrentGamer = Red;

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
            int row = CountDiscsInColumn(column);

            if (row == BoardRows)
                throw new ApplicationException("Column is full");

            Board[row, column] = CurrentGamer;
            DisplayBoard(Board);
            SwitchGamer();

            return row;
        }

        public string GetCurrentGamer()
        {
            return CurrentGamer;
        }

        private void SwitchGamer()
        {
            CurrentGamer = (Red == CurrentGamer) ? Yellow : Red;
        }

        public bool IsFinished()
        {
            return CountDiscsOnBoard() == BoardRows * BoardColumns;
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
