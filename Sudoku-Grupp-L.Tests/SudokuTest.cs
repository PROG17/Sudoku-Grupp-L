using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sudoku_Grupp_L.Tests
{
	[TestClass]
	public class SudokuTest
	{
		[TestMethod]
		public void TestCompletedBoard()
		{
			const string solution = "974236158638591742125487936316754289742918563589362417867125394253649871491873625";

			var sudoku = new Sudoku(solution);
			sudoku.Solve();

			Assert.AreEqual(solution, sudoku.ConvertToString());
			Assert.IsTrue(sudoku.Solved);
			Assert.IsFalse(sudoku.Invalid);
		}

		[TestMethod]
		public void TestEmptySquare()
		{
			const string board = "2564891733746159829817234565932748617128.6549468591327635147298127958634849362715";
			const string solution = "256489173374615982981723456593274861712836549468591327635147298127958634849362715";

			var sudoku = new Sudoku(board);
			sudoku.Solve();

			Assert.AreEqual(solution, sudoku.ConvertToString());
			Assert.IsTrue(sudoku.Solved);
			Assert.IsFalse(sudoku.Invalid);
		}

		[TestMethod]
		public void TestNakedSingles()
		{
			const string board = "3.542.81.4879.15.6.29.5637485.793.416132.8957.74.6528.2413.9.655.867.192.965124.8";
			const string solution = "365427819487931526129856374852793641613248957974165283241389765538674192796512438";

			var sudoku = new Sudoku(board);
			sudoku.Solve();

			Assert.AreEqual(solution, sudoku.ConvertToString());
			Assert.IsTrue(sudoku.Solved);
			Assert.IsFalse(sudoku.Invalid);
		}

		[TestMethod]
		public void TestHiddenSingles()
		{
			const string board = "..2.3...8.....8....31.2.....6..5.27..1.....5.2.4.6..31....8.6.5.......13..531.4..";
			const string solution = "672435198549178362831629547368951274917243856254867931193784625486592713725316489";

			var sudoku = new Sudoku(board);
			sudoku.Solve();

			Assert.AreEqual(solution, sudoku.ConvertToString());
			Assert.IsTrue(sudoku.Solved);
			Assert.IsFalse(sudoku.Invalid);
		}
	}
}
