using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Model
{
	public enum Difficulty { Easy, Medium, Hard, Extreme }

	public static class Generator
	{
		public static Puzzle NewPuzzle(Difficulty difficulty)
		{
			// Create a new puzzle and populate it with random solution
			var puzzle = new Puzzle();
			Solver.RandomFill(puzzle);
			// Generate number of empty cells based on difficulty
			int emptyCellCount = GenerateEmptyCellCount(difficulty);
			// Prepare array with randomly shuffled puzzle cells
			// The first emptyCellCount slots will hold the empty cell candidates
			var cells = puzzle.Cells.RandomShuffle();
			// Preserve the values of the empty cell candidates and clear them
			var emptyCellValues = new int[emptyCellCount];
			for (int i = 0; i < emptyCellCount; i++)
			{
				emptyCellValues[i] = cells[i].Value.Value;
				cells[i].Value = null;
			}
			// Check for unique solution
			var timer = Stopwatch.StartNew();
			int attempts = 0;
			bool uniqueSolution = false;
			while (true)
			{
				attempts++;
				uniqueSolution = Solver.FindUniqueSolution(puzzle);
				if (uniqueSolution || timer.ElapsedMilliseconds >= 500) break;
				// Exchange random empty cell with random nonempty cell
				int indexA = Utils.Random.Next(0, emptyCellCount);
				int indexB = Utils.Random.Next(emptyCellCount, cells.Length);
				var cellA = cells[indexA];
				var cellB = cells[indexB];
				cells[indexA] = cellB;
				cells[indexB] = cellA;
				// Restore the empty cell
				cellA.Value = emptyCellValues[indexA];
				// Preserve and clear the nonempty cell
				emptyCellValues[indexA] = cellB.Value.Value;
				cellB.Value = null;
			}
			timer.Stop();
			return puzzle;
		}

		private static int GenerateEmptyCellCount(Difficulty difficulty)
		{
			switch (difficulty)
			{
				case Difficulty.Easy: return Utils.Random.Next(40, 46);
				case Difficulty.Medium: return Utils.Random.Next(46, 50);
				case Difficulty.Hard: return Utils.Random.Next(50, 54);
				default: return Utils.Random.Next(54, 59);
			}
		}
	}
}
