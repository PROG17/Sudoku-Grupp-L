using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Grupp_L
{
	internal class Program
    {
		private static void Main(string[] args)
        {
            Console.CursorVisible = false;

			WebSudoku.Level level = PromptForLevel();
			if (!WebSudoku.TryFetchSudoku(level, out WebSudoku game)) return;
			//Sudoku game = new Sudoku(new string(' ', 81));

	        Console.Clear();

	        game.PrintToScreen();
	        Console.ReadKey();

	        game.Solve();
			game.PrintToScreen();

			Console.ReadLine();
        }

	    private static WebSudoku.Level PromptForLevel()
	    {
			string input = null;
		    WebSudoku.Level level;

		    do {
			    if (input != null)
				    Console.WriteLine("Invalid input! Try again");
			    Console.WriteLine("Choose difficulty");
			    Console.WriteLine("1. Easy\n2. Medium\n3. Hard\n4. Evil");
			    Console.Write("> ");
			    input = Console.ReadLine();

		    } while (!Enum.TryParse(input, true, out level));

		    return level;
	    }
    }
}
