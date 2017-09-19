using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Grupp_L
{
	public class Ruta
	{
		public int num;
		public List<int> possibles;
		public readonly int x;
		public readonly int y;
		public bool isOriginal = true;
		public bool isGuess = false;

		public Ruta(int x, int y, int num)
		{
			this.x = x;
			this.y = y;
			this.num = num;
			this.isOriginal = num != 0;
			this.possibles = new List<int>();
		}

		public ConsoleColor DisplayColor =>
			isOriginal ? ConsoleColor.White : (isGuess ? ConsoleColor.Yellow : ConsoleColor.Green);

		public char DisplayNum =>
			num == 0 ? ' ' : num.ToString()[0];
	}
}
