using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Threading;

namespace Sudoku_Grupp_L
{
    using System.Security.Cryptography.X509Certificates;

    class Sudoku
    {
	    private int depth = 0;

        Ruta[,] gameBoard = new Ruta [9,9];
        public bool Solved { get; private set; } = false;
        public bool Processed { get; private set; } = false;
		public bool Invalid { get; private set; } = false;
		public double TookSeconds { get; private set; }
		
        public Sudoku(string numbersInput)
        {
			if (numbersInput.Length != 81)
				throw new ArgumentException("Fel längd på bräde! Måste vara 81 tecken långt", nameof(numbersInput));

            for (int i = 0; i < numbersInput.Length; i++)
            {
                char number = numbersInput[i];
				// Tolka alla icke-siffror som 0
	            int.TryParse(number.ToString(), out int parsedNumber);
		        int x = i % 9;
		        int y = i / 9;
		        this.gameBoard[x, y] = new Ruta(x, y, parsedNumber);
            }
        }

        public bool Solve()
		{
			Stopwatch stopwatch = null;
			if (depth == 0) stopwatch = Stopwatch.StartNew();

			SolveViaMethod1();

			if (!this.Solved && !this.Invalid)
				SolveViaMethod2();

			this.Processed = true;

			if (stopwatch != null)
			{
				stopwatch.Stop();
				TookSeconds = stopwatch.ElapsedMilliseconds * 0.001;
			}

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
							if (this.gameBoard[x, y].possibles.Count == 0)
							{
								this.Solved = false;
								this.Invalid = true;
								return;
							}
							allSolved = false;
						}
					}
				}

				// Hitta siffrors enstaka möjliga platser

				for (int x = 0; x < 9; x++)
					AssignUnique(GetColumn(x));

				for (int y = 0; y < 9; y++)
					AssignUnique(GetRow(y));

				for (int x = 0; x < 9; x += 3)
					for (int y = 0; y < 9; y += 3)
						AssignUnique(GetBox(x, y));
			}

			this.Solved = allSolved;
		}

		private static void AssignUnique(List<Ruta> list)
		{
			for (int n = 1; n <= 9; n++)
			{
				Ruta unikRuta = null;
				foreach (Ruta ruta in list)
				{
					if (ruta.num == n)
					{
						unikRuta = null;
						break;
					}

					if (ruta.num != 0) continue;
					if (!ruta.possibles.Contains(n)) continue;

					if (unikRuta != null)
					{
						unikRuta = null;
						break;
					}
					else
					{
						unikRuta = ruta;
					}
				}

				if (unikRuta != null)
				{
					unikRuta.num = n;
					unikRuta.possibles.Clear();
				}
			}
		}

		private void SolveViaMethod2()
	    {
		    foreach (Ruta ruta in this.gameBoard)
		    {
			    if (ruta.num != 0) continue;

			    foreach (int possible in ruta.possibles)
			    {
				    Sudoku newBoard = this.Copy();

					// Applicera "gissning"
				    newBoard.gameBoard[ruta.x, ruta.y].num = possible;

				    newBoard.depth = this.depth + 1;

					if (depth < 4)
						newBoard.PrintToScreen();

				    if (newBoard.Solve())
				    {
					    this.gameBoard = newBoard.gameBoard;
					    this.Solved = true;
					    return;
				    }
			    }
		    }
	    }

	    public Sudoku Copy()
	    {
		    var builder = new StringBuilder(81);

		    for (int y = 0; y < 9; y++)
		    {
			    for (int x = 0; x < 9; x++)
			    {
				    builder.Append(this.gameBoard[x,y].num);
				}
			}

			return new Sudoku(builder.ToString());
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

            var possibleNumbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
			
            foreach (Ruta ruta in this.GetImpossibleNumbers(x, y))
            {
                possibleNumbers.Remove(ruta.num);
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
        private List<Ruta> GetImpossibleNumbers(int x, int y)
        {
	        List<Ruta> list = GetColumn(x);
			list.AddRange(GetRow(y));
			list.AddRange(GetBox(x,y));

	        return list.OnlyImpossibles();
        }

	    /// <param name="x">the Column</param>
	    private List<Ruta> GetColumn(int x)
	    {
		    var list = new List<Ruta>();

		    for (int y = 0; y < 9; y++)
		    {
			    list.Add(this.gameBoard[x, y]);
		    }

		    return list;
		}

	    /// <param name="y">the Row</param>
	    private List<Ruta> GetRow(int y)
	    {
		    var list = new List<Ruta>();

		    for (int x = 0; x < 9; x++)
		    {
			    list.Add(this.gameBoard[x, y]);
		    }

		    return list;
	    }

		private List<Ruta> GetBox(int x, int y)
	    {
		    var list = new List<Ruta>();
		    x = (x / 3) * 3;
		    y = (y / 3) * 3;

		    for (int yOffset = 0; yOffset < 3; yOffset++)
		    {
			    for (int xOffset = 0; xOffset < 3; xOffset++)
			    {
				    int yReal = y + yOffset;
				    int xReal = x + xOffset;
					list.Add(this.gameBoard[xReal, yReal]);
			    }
		    }

		    return list;
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


