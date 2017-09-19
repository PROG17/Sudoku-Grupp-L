using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Sudoku_Grupp_L
{
    using System.Security.Cryptography.X509Certificates;

    class Sudoku
    {
        Ruta[,] gameBoard = new Ruta [9,9];
        public bool Solved { get; private set; } = false;
        public bool Processed { get; private set; } = false;

        public Sudoku(string numbersInput)
        {
			if (numbersInput.Length != 81)
				throw new ArgumentException("Fel längd på bräde! Måste vara 81 tecken långt", nameof(numbersInput));

            for (int i = 0; i < numbersInput.Length; i++)
            {
                char number = numbersInput[i];
                int.TryParse(number.ToString(), out int parsedNumber);
                int x = i % 9;
                int y = i / 9;
                this.gameBoard[x, y] = new Ruta(parsedNumber);
            }
        }

        public bool Solve()
		{
			SolveViaMethod1();

			this.Processed = true;

			this.PrintToScreen();

			return this.Solved;
		}

		private void SolveViaMethod1()
		{
			bool anySolved = true;
			bool allSolved = true;

			while (anySolved)
			{
				anySolved = false;
				allSolved = true;

				for (int y = 0; y < 9; y++)
				{
					for (int x = 0; x < 9; x++)
					{
						if (this.TrySolve(x, y))
						{
							anySolved = true;
							this.PrintToScreen();
							Thread.Sleep(50);
						}
						else if (this.gameBoard[x, y].num == 0)
						{
							allSolved = false;
						}
					}
				}
			}

			this.Solved = allSolved;
		}


		/// <summary>
		/// The MASCHIIIIINE
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		private bool TrySolve(int x, int y)
        {
            if (this.gameBoard[x, y].num != 0)
            {
                return false;
            }

            List<int> possibleNumbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            foreach (int number in this.GetImpossibleNumbersFromColumn(x))
            {
                possibleNumbers.Remove(number);
            }

            foreach (int number in this.GetImpossibleNumbersFromRow(y))
            {
                possibleNumbers.Remove(number);
            }

            foreach (int number in this.GetImpossibleNumbersFromBox(x, y))
            {
                possibleNumbers.Remove(number);
            }

            if (possibleNumbers.Count == 1)
            {
                this.gameBoard[x, y].num = possibleNumbers[0];
                return true;
            }
            else
            {
	            this.gameBoard[x, y].possibles = possibleNumbers;
                return false;
            }
        }

        /// <summary>Här finns information</summary>
        /// <param name="x">The column</param>
        private List<int> GetImpossibleNumbersFromColumn(int x)
        {
            List<int> impossibleNumbers = new List<int>();

            for (int y = 0; y < 9; y++)
            {
                if (this.gameBoard[x,y].num != 0)
                {
                    impossibleNumbers.Add(this.gameBoard[x, y].num);
                }
            }

            return impossibleNumbers;
        }

        /// <summary>
        /// Kollar siffror som finns i raden.
        /// </summary>
        /// <param name="y">the Row</param>
        private List<int> GetImpossibleNumbersFromRow(int y)
        {
            List<int> impossibleNumbers = new List<int>();
            for (int x = 0; x < 9; x++)
            {
                if (this.gameBoard[x, y].num != 0)
                {
                    impossibleNumbers.Add(this.gameBoard[x, y].num);
                }                
            }
            return impossibleNumbers;
        }

        private List<int> GetImpossibleNumbersFromBox(int x, int y)
        {
            List<int> impossibleNumbers = new List<int>();
            x = (x / 3) * 3;
            y = (y / 3) * 3;

            for (int yOffset = 0; yOffset < 3; yOffset++)
            {
                for (int xOffset = 0; xOffset < 3; xOffset++)
                {
                    int yReal = y + yOffset;
                    int xReal = x + xOffset;
                    if (this.gameBoard[xReal, yReal].num !=0)
                    {
                        impossibleNumbers.Add(this.gameBoard[xReal, yReal].num);
                    }
                }
            }
            return impossibleNumbers;
        }

        public void PrintToScreen()
        {
            if (this.Processed)
                Console.ForegroundColor = this.Solved ? ConsoleColor.Green : ConsoleColor.Red;
            else
                Console.ForegroundColor = ConsoleColor.Gray;

            const int width = 21;
            const int height = 11;
            int left = (Console.WindowWidth - width) / 2;
            int top = (Console.WindowHeight - height) / 2;
            Console.SetCursorPosition(left, top);

            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    Console.Write(this.gameBoard[x, y].DisplayNum + " ");

                    if ((x % 3 == 2) && (x != 8))
                    {
                        Console.Write("| ");
                    }

                }

                Console.WriteLine();

                if ((y % 3 == 2) && (y != 8))
                {
                    Console.CursorLeft = left;
                    Console.WriteLine("---------------------");
                }

                Console.CursorLeft = left;
            }
        }
    }
}


