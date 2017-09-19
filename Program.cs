using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Grupp_L
{
	internal class Program
    {
	    private const string FREDRIKS =
            "003020600" +
            "900305001" +
            "001806400" +
            "008102900" +
            "700000008" +
            "006708200" +
            "002609500" +
            "800203009" +
            "005010300";

	    private const string EASY =
            "080502001" +
            "031700925" +
            "000000084" +
            "756400200" +
            "000906000" +
            "002007643" +
            "320000000" +
            "578001430" +
            "600304070";

	    private const string MEDIUM =
            "070003400" +
            "004500001" +
            "600000039" +
            "845200100" +
            "000050000" +
            "002008657" +
            "480000002" +
            "700009500" +
            "006300080";

	    private const string HARD =
            "000100085" +
            "061500000" +
            "000042001" +
            "007800002" +
            "028000430" +
            "500009600" +
            "100350000" +
            "000008210" +
            "870001000";

	    private const string EVIL =
            "000079200" +
            "930040010" +
            "260000000" +
            "003100400" +
            "080000020" +
            "002006700" +
            "000000075" +
            "070030081" +
            "001860000";

	    private static void Main(string[] args)
        {
            Console.CursorVisible = false;

            var game = new Sudoku(MEDIUM);

            game.PrintToScreen();
            Console.ReadKey();

            game.Solve();
			game.PrintToScreen();

			Console.WriteLine();
			Console.WriteLine($"Took {game.TookSeconds} seconds to solve");

            Console.ReadLine();
			
        }
    }
}
