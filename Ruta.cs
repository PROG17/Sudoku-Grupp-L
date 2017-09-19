using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Grupp_L
{
	class Ruta
	{
		public int num;
		public List<int> possibles;
		public readonly int x;
		public readonly int y;

		public Ruta(int x, int y, int num)
		{
			this.x = x;
			this.y = y;
			this.num = num;
			this.possibles = new List<int>();
		}

		public char DisplayNum => num == 0 ? ' ' : num.ToString()[0];
	}
}
