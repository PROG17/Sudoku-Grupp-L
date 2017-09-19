using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Grupp_L
{
	public static class RutaExtensions
	{

		public static List<Ruta> OnlyImpossibles(this List<Ruta> arr)
		{
			var list = new List<Ruta>();

			foreach (Ruta ruta in arr)
			{
				if (ruta.num != 0)
				{
					list.Add(ruta);
				}
			}

			return list;
		}

		private static readonly Random random = new Random();
		public static IEnumerable<Ruta> InShuffledOrder(this Ruta[,] gameBoard)
		{
			var newList = new List<Ruta>(gameBoard.Cast<Ruta>());

			int n = newList.Count;
			while (n > 1)
			{
				n--;
				int k = random.Next(n + 1);
				Ruta value = newList[k];
				newList[k] = newList[n];
				newList[n] = value;
			}

			return newList;
		}

	}
}
