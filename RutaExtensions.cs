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

	}
}
