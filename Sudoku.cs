using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Grupp_L
{

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
