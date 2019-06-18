using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Model
{
	public static class Extensions
	{
		public static IEnumerable<Cell> Empty(this IEnumerable<Cell> cells) => cells.Where(cell => cell.IsEmpty);

		public static IEnumerable<Cell> NonEmpty(this IEnumerable<Cell> cells) => cells.Where(cell => !cell.IsEmpty);
	}
}
