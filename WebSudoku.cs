using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Sudoku_Grupp_L
{
	public class WebSudoku : Sudoku
	{
		public string WebSudokuLink => $"http://www.websudoku.com/?level={(int) WebSudokuLevel}&set_id={WebSudokuId}";
		public string WebSudokuId { get; }
		public Level WebSudokuLevel { get; }

		public WebSudoku(string numbersInput, string sourceId, Level sourceLevel) : base(numbersInput)
		{
			WebSudokuId = sourceId;
			WebSudokuLevel = sourceLevel;
		}

		/// <summary>
		/// Fetches a freshly generated sudoku from http://www.websudoku.com/
		/// <para>Returns null on failure.</para>
		/// </summary>
		public static bool TryFetchSudoku(Level level, out WebSudoku game)
		{
			try
			{
				Console.WriteLine("Fetching sudoku from www...");
				game = FetchSudoku(level).Result;
				return true;
			}
			catch (Exception e)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Failed to fetch sudoku!");
				Console.WriteLine($"{e.GetType().Name}: {e.Message}");
				game = null;
				return false;
			}
		}

		/// <summary>
		/// Fetches a freshly generated sudoku from http://www.websudoku.com/
		/// <para>Throws exception on failure.</para>
		/// </summary>
		/// <exception cref="FormatException"></exception>
		/// <exception cref="ArgumentException"></exception>
		public static async Task<WebSudoku> FetchSudoku(Level level)
		{
			using (var client = new HttpClient())
			{
				HttpResponseMessage response = await client.GetAsync("http://view.websudoku.com/?level=" + (int) level);
				response.EnsureSuccessStatusCode();

				string result = await response.Content.ReadAsStringAsync();
					
				// Time to parse
				string sourceId = FindInputDigitValueFromID(result, "pid");
				string sourceSolved = FindInputDigitValueFromID(result, "cheat");
				string sourceMask = FindInputDigitValueFromID(result, "editmask");

				if (sourceSolved.Length != 81)
					throw new FormatException($"Invalid length of sudoku board! Expected 81 characters, got {sourceSolved.Length}.");

				string numbersInput = ApplyMask(sourceSolved, sourceMask);

				return new WebSudoku(numbersInput, sourceId, level);
			}
		}

		private static string ApplyMask(string solved, string mask)
		{
			if (solved.Length != mask.Length)
				throw new ArgumentException($"Length of `{nameof(solved)}' (length: {solved.Length}) and `{nameof(mask)}' (length: {mask.Length}) does not match!");

			int length = mask.Length;
			var masked = new StringBuilder(length);
			for (int i = 0; i < length; i++)
			{
				masked.Append(mask[i] == '1' ? '0' : solved[i]);
			}

			return masked.ToString();
		}

		private static string FindInputDigitValueFromID(string result, string id)
		{
			Match match = Regex.Match(result, $"<input[^>]*?id=\"?{id}\"?[^>]*?value=\"?(\\d+)\"?\\s*>", RegexOptions.IgnoreCase);
			if (!match.Success) throw new FormatException($"Unable to obtain field `{id}'.");
			return match.Groups[1].Value;
		}

		public override void PrintToScreen()
		{
			base.PrintToScreen();

			Console.ForegroundColor = ConsoleColor.Gray;
			Console.SetCursorPosition(0,0);
			Console.WriteLine("Difficulty: " + WebSudokuLevel);
			Console.WriteLine("Source: " + WebSudokuLink);
		}

		public enum Level
		{
			Easy = 1,
			Medium = 2,
			Hard = 3,
			Evil = 4,
		}

	}
}
