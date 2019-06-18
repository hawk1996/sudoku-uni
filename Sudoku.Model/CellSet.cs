using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Model
{
	public enum CellSetType { Row, Column, Box }

	/// <summary>
	/// Holds single Sudoku Row, Column or Box cells
	/// </summary>
	public sealed class CellSet
	{
		public Puzzle Owner { get; }
		public CellSetType Type { get; }
		public int Index { get; }

		// Called from Puzzle constructor
		internal CellSet(Puzzle owner, CellSetType type, int index)
		{
			Owner = owner;
			Type = type;
			Index = index;
		}

		private List<Cell> cells = new List<Cell>(9);
		public IReadOnlyList<Cell> Cells => cells;
		public IEnumerable<Cell> EmptyCells => Cells.Empty();
		public IEnumerable<Cell> NonEmptyCells => Cells.NonEmpty();

		private ValueSet freeValues = ValueSet.Full;
		public ValueSet FreeValues => freeValues;
		public ValueSet UsedValues => ValueSet.Full.Except(freeValues);

		// Called from Puzzle constructor
		internal void Add(Cell item) => cells.Add(item);


		/// Called from cell when Value property changed
		internal void OnValueChanged(Cell item, int? oldValue)
		{
			// Update the used and free values info
			if (oldValue != null) freeValues.Add(oldValue.Value);
			if (item.Value != null) freeValues.Remove(item.Value.Value);
		}

		public override string ToString() => string.Format("{0}[{1}]", Type, Index);
	}
}
