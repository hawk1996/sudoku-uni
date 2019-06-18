using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Model
{
	public class Puzzle
	{
		private CellSet[] rows;
		private CellSet[] columns;
		private CellSet[] boxes;

		public Puzzle()
		{
			rows = new CellSet[9];
			columns = new CellSet[9];
			boxes = new CellSet[9];
			for (int i = 0; i < 9; i++)
			{
				rows[i] = new CellSet(this, CellSetType.Row, i);
				columns[i] = new CellSet(this, CellSetType.Column, i);
				boxes[i] = new CellSet(this, CellSetType.Box, i);
			}
			for (int ri = 0; ri < 9; ri++)
			{
				var row = rows[ri];
				for (int ci = 0; ci < 9; ci++)
				{
					var column = columns[ci];
					var box = boxes[3 * (ri / 3) + ci / 3];
					var cell = new Cell(this, row, column, box);
					row.Add(cell);
					column.Add(cell);
					box.Add(cell);
				}
			}
		}

		public IReadOnlyList<CellSet> Rows => rows;

		public IReadOnlyList<CellSet> Columns => columns;

		public IReadOnlyList<CellSet> Boxes => boxes;

		public Cell Cell(int row, int column) => Rows[row].Cells[column];

		public IEnumerable<Cell> Cells => Rows.SelectMany(row => row.Cells);

		public IEnumerable<Cell> EmptyCells => Cells.Empty();

		public IEnumerable<Cell> NonEmptyCells => Cells.NonEmpty();

		public event EventHandler ContentChanged;

		private void RaiseContentChanged()
		{
			if (ContentChanged != null)
				ContentChanged(this, EventArgs.Empty);
		}

		private int updateCount;
		private bool hasPendingChanges;

		public bool IsUpdating => updateCount > 0;

		public void BeginUpdate()
		{
			updateCount++;
		}

		public void EndUpdate()
		{
			if (!IsUpdating) return;
			updateCount--;
			if (!IsUpdating && hasPendingChanges)
			{
				hasPendingChanges = false;
				RaiseContentChanged();
			}
		}

		/// <remarks>
		/// Called from cell when Value property changed
		/// </remarks>
		internal void OnValueChanged(Cell cell, int? oldValue)
		{
			if (IsUpdating)
				hasPendingChanges = true;
			else
				RaiseContentChanged();
		}

		public void Clear()
		{
			BeginUpdate();
			foreach (var cell in Cells)
				cell.Value = null;
			EndUpdate();
		}

		public bool IsEmpty() => Cells.All(cell => cell.IsEmpty);

		public bool IsSolved() => !EmptyCells.Any();

		public bool IsSolvable() => EmptyCells.All(cell => cell.FreeValues.Any());

		public bool Solve()
		{
			if (IsSolved()) return true;
			BeginUpdate();
			bool solved = Solver.Solve(this);
			EndUpdate();
			return solved;
		}

		const string ExportCellValues = "*123456789";

		public string ExportContent()
		{
			var sb = new StringBuilder();
			foreach (var row in rows)
			{
				if (row.Index > 0) sb.AppendLine();
				foreach (var cell in row.Cells)
					sb.Append(ExportCellValues[cell.Value ?? 0]);
			}
			return sb.ToString();
		}

		public static Puzzle FromContent(string[] data)
		{
			// Validate format
			if (data == null || data.Length != 9) throw InvalidFormatException();
			for (int row = 0; row < 9; row++)
			{
				var line = data[row];
				if (line == null || line.Length != 9) throw InvalidFormatException();
				for (int col = 0; col < 9; col++)
				{
					if (ExportCellValues.IndexOf(line[col]) < 0) throw InvalidFormatException();
				}
			}
			// Create and populate a new puzzle
			var puzzle = new Puzzle();
			for (int row = 0; row < 9; row++)
			{
				var line = data[row];
				for (int col = 0; col < 9; col++)
				{
					var cell = puzzle.Cell(row, col);
					int value = ExportCellValues.IndexOf(line[col]);
					if (value > 0) cell.Value = value;
				}
			}
			return puzzle;
		}

		private static Exception InvalidFormatException() => new Exception("Invalid format.");

		public override string ToString() => ExportContent();
	}
}
