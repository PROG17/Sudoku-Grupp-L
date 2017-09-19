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

		public Ruta(int num)
		{
			this.num = num;
			this.possibles = new List<int>();
		}

		public char DisplayNum => num == 0 ? ' ' : num.ToString()[0];
	}
}
