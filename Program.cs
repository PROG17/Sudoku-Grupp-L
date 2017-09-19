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
            "  3 2 6  " +
            "9  3 5  1" +
            "  18 64  " +
            "  81 29  " +
            "7       8" +
            "  67 82  " +
            "  26 95  " +
            "8  2 3  9" +
            "  5 1 3  ";

	    private const string EASY =
            " 8 5 2  1" +
            " 317  925" +
            "       84" +
            "7564  2  " +
            "   9 6   " +
            "  2  7643" +
            "32       " +
            "578  143 " +
            "6  3 4 7 ";

	    private const string MEDIUM =
            " 7   34  " +
            "  45    1" +
            "6      39" +
            "8452  1  " +
            "    5    " +
            "  2  8657" +
            "48      2" +
            "7    95  " +
            "  63   8 ";

	    private const string HARD =
            "   1   85" +
            " 615     " +
            "    42  1" +
            "  78    2" +
            " 28   43 " +
            "5    96  " +
            "1  35    " +
            "     821 " +
            "87   1   ";

	    private const string EVIL =
            "    792  " +
            "93  4  1 " +
            "26       " +
            "  31  4  " +
            " 8     2 " +
            "  2  67  " +
            "       75" +
            " 7  3  81" +
            "  186    ";

	    private const string EVELKNIEVEL =
		    "         " +
			"         " +
			"         " +
			"         " +
			"         " +
			"         " +
			"         " +
			"         " +
			"         ";

		private static void Main(string[] args)
        {
            Console.CursorVisible = false;

            //var game = new Sudoku(HARD);
	        if (!WebSudoku.TryFetchSudoku(WebSudoku.Level.Evil, out WebSudoku game)) return;

	        Console.Clear();

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
