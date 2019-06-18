using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Model
{
	static class Utils
	{
		/// <summary>
		/// Shared random number generator
		/// </summary>
		public static readonly Random Random = new Random();

		public static T[] RandomShuffle<T>(this IEnumerable<T> source)
		{
			// Fisher–Yates shuffle (https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle)
			var result = source.ToArray();
			for (int i = 0; i < result.Length - 1; i++)
			{
				int j = Random.Next(i, result.Length);
				if (i == j) continue;
				var temp = result[i];
				result[i] = result[j];
				result[j] = temp;
			}
			return result;
		}
	}
}
