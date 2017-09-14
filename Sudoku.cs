using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Grupp_L
{
    using System.Security.Cryptography.X509Certificates;

    class Sudoku
    {
        int[,] gameBoard = new int [9,9];


        public Sudoku(string numbersInput)
        {
            for (int i = 0; i < numbersInput.Length; i++)
            {
                char number = numbersInput[i];
                int.TryParse(number.ToString(), out int parsedNumber);
                int x = i % 9;
                int y = i / 9;
                this.gameBoard[x, y] = parsedNumber;

            }

        }

        public void Solve()
        {
            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {

                    this.TrySolve(x, y);

                }
            }
        }

        private bool TrySolve(int x, int y)
        {
            if (this.gameBoard[x, y] != 0)
            {
                return false;
            }

            List<int> possibleNumbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            foreach (int number in this.GetImpossibleNumbersFromColumn(x))
            {
                possibleNumbers.Remove(number);
            }
            

        }

        /// <summary>Här finns information</summary>
        /// <param name="x">The column</param>
        private List<int> GetImpossibleNumbersFromColumn(int x)
        {
            List<int> impossibleNumbers = new List<int>();

            for (int y = 0; y < 9; y++)
            {
                if (this.gameBoard[x,y] != 0)
                {
                    impossibleNumbers.Add(this.gameBoard[x, y]);
                }
            }

            return impossibleNumbers;
        }


        public void PrintToScreen()
        {
            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    if (this.gameBoard[x,y] == 0)
                    {
                        Console.Write("  ");
                    }
                    else
                    {
                        Console.Write(this.gameBoard[x, y] + " ");

                    }

                    if ((x % 3 == 2) && (x != 8))
                    {
                        Console.Write("| ");
                    }

                }

                Console.WriteLine();

                if ((y % 3 == 2) && (y != 8))
                {
                    Console.WriteLine("---------------------");

                }



            }
        }
    }
}


