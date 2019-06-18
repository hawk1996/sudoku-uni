using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Model
{
	public class Cell
	{
		public Puzzle Owner { get; }
		public CellSet Row { get; }
		public CellSet Column { get; }
		public CellSet Box { get; }

		// Called from Puzzle constructor
		internal Cell(Puzzle owner, CellSet row, CellSet column, CellSet box)
		{
			Owner = owner;
			Row = row;
			Column = column;
			Box = box;
		}

		private int? value;
		public int? Value
		{
			get { return value; }
			set
			{
				if (this.value == value) return;
				if (value != null)
				{
					if (value.Value < 1 || value.Value > 9) throw new Exception(string.Format(
						"Invalid value {0}. Must be a number between 1 and 9.", value));
					if (!FreeValues.Contains(value.Value)) throw new Exception(string.Format(
						"Value {0} has already been used in the row {1}, column {2} or box {3}.",
						value, Row.Index + 1, Column.Index + 1, Box.Index + 1));
				}
				var oldValue = this.value;
				this.value = value;
				Row.OnValueChanged(this, oldValue);
				Column.OnValueChanged(this, oldValue);
				Box.OnValueChanged(this, oldValue);
				Owner.OnValueChanged(this, oldValue);
			}
		}

		public bool IsEmpty => Value == null;

		public ValueSet FreeValues => Row.FreeValues.Intersect(Column.FreeValues).Intersect(Box.FreeValues);

		public override string ToString() => string.Format("Cell [{0},{1}]: {2}", 
			Row.Index, Column.Index, Value != null ? Value.ToString() : "*");
	}
}
