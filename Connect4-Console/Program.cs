using System;
using System.Configuration;

namespace Connect4_Console
{
    public class Program
    {
        //TODO: no_of_rows / no_of_columns to be variables
        public static int board_rows = 5; //int.Parse(ConfigurationManager.AppSettings["rows"]);
        public static int board_columns = 5;
        public string[,] board = new string[board_rows, board_columns];
        public static string Empty = "O";
        public static string Red = "R";
        public static string Yellow = "Y";


        public Program()
        {
            // populating empty board       
            for (int row = 0; row < board_rows; row++)
            {
                for (int column = 0; column < board_columns; column++)
                {
                    board[row, column] = Empty;
                }
            }
        }

        public int countDiskOnBoard()
        {
            int count = 0;
            for (int row = 0; row < board_rows; row++)
            {
                for (int column = 0; column < board_columns; column++)
                {
                    if (board[row, column] != Empty) count++;
                }
            }
            return count;
        }

        static void Main(string[] args)
        {
            Program p = new Program();
            

            Console.Read();
        }
    }
}
