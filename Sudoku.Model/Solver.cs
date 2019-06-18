using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Model
{
	internal class Solver
	{
		public static bool Solve(Puzzle puzzle) => Process(puzzle, useRandomStrategy: puzzle.IsEmpty());

		public static bool RandomFill(Puzzle puzzle) => Process(puzzle, useRandomStrategy: true);

		public static bool FindUniqueSolution(Puzzle puzzle) => Process(puzzle, findUnique: true);

		private static bool Process(Puzzle puzzle, bool findUnique = false, bool useRandomStrategy = false)
		{
			// Implements brute force backtracking algorithm
			var pendingCells = new HashSet<Cell>(puzzle.EmptyCells);
			if (pendingCells.Count == 0) return true;
			var processStack = new Stack<ProcessingCell>();
			bool solutionFound = false;
			while (true)
			{
				// Try select next cell
				if (pendingCells.Count != 0)
				{
					// Select next cell to process
					var next = SelectNextCell(pendingCells, useRandomStrategy);
					pendingCells.Remove(next.Cell);
					processStack.Push(next);
				}
				else
				{
					// No more pending cells - the puzzle is solved
					if (!findUnique) return true;
					// We are just searching for unique solution
					if (solutionFound)
					{
						// Second solution found - restore the cells and return failure
						foreach (var item in processStack)
							item.Restore();
						return false;
					}
					// First solution found - remember that and continue
					solutionFound = true;
				}
				// Value selection loop
				while (true)
				{
					// Try select next value
					var last = processStack.Peek();
					if (last.SelectNextValue()) break;
					// Move back
					processStack.Pop();
					last.Restore();
					pendingCells.Add(last.Cell);
					if (processStack.Count == 0) return solutionFound; // All cells processed
				}
			}
		}

		struct ProcessingCell
		{
			public readonly Cell Cell;
			private readonly IEnumerator<int> ValueEnumerator;
			public ProcessingCell(Cell cell) : this(cell, cell.FreeValues) { }
			public ProcessingCell(Cell cell, IEnumerable<int> values)
			{
				Cell = cell;
				ValueEnumerator = values.GetEnumerator();
			}
			public bool SelectNextValue()
			{
				if (ValueEnumerator.MoveNext())
				{
					Cell.Value = ValueEnumerator.Current;
					return true;
				}
				Restore();
				return false;
			}
			public void Restore() => Cell.Value = null;
		}

		private static ProcessingCell SelectNextCell(IReadOnlyCollection<Cell> pendingCells, bool useRandomStrategy)
		{
			if (pendingCells.Count == 1) // Only one cell remaining
				return new ProcessingCell(pendingCells.First());
			// Search for a cell with the following priority
			// (1) Zero available values (no solution)
			// (2) Single available value
			// (3) Minimum available values. Depending on the strategy store first or all candidates.
			Cell singleValueCell = null;
			Cell singleCell = null;
			List<Cell> candidates = null;
			int minFreeValues = int.MaxValue;
			foreach (var cell in pendingCells)
			{
				var freeValues = cell.FreeValues.Count;
				if (freeValues == 0) return new ProcessingCell(cell);
				if (singleValueCell != null) continue;
				if (freeValues == 1)
					singleValueCell = cell;
				else if (freeValues == minFreeValues && useRandomStrategy)
					candidates.Add(cell);
				else if (freeValues < minFreeValues)
				{
					minFreeValues = freeValues;
					if (!useRandomStrategy)
						singleCell = cell;
					else
					{
						if (candidates == null) candidates = new List<Cell>();
						candidates.Add(cell);
					}
				}
			}
			if (singleValueCell != null)
				return new ProcessingCell(singleValueCell);
			if (!useRandomStrategy)
				return new ProcessingCell(singleCell);
			else
			{
				// Select a random cell from candidates and also randomize the value list
				var result = candidates.Count < 2 ? candidates[0] : candidates[Utils.Random.Next(0, candidates.Count)];
				return new ProcessingCell(result, result.FreeValues.RandomShuffle());
			}
		}
	}
}
