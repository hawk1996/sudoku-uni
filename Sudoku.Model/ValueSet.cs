using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Model
{
	/// <summary>
	/// Lightweight low memory high performance encapsulation
	/// of a value set containing numbers in range [1..9].
	/// Supports all the basic collection and set operations.
	/// </summary>
	public struct ValueSet : IReadOnlyCollection<int>
	{
		private uint data;

		public ValueSet(IEnumerable<int> values) : this()
		{
			foreach (var value in values)
				Add(value);
		}

		private ValueSet(uint data) { this.data = data; }

		public int Count => BitCount(data);

		public IEnumerator<int> GetEnumerator() => ValueIterator(data);

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		public bool Add(int value)
		{
			EnsureIsValid(value);
			var oldData = data;
			data |= BitMask(value);
			return data != oldData;
		}

		public bool Remove(int value)
		{
			EnsureIsValid(value);
			var oldData = data;
			data &= ~BitMask(value);
			return data != oldData;
		}

		public bool Contains(int value) => IsValid(value) && (data & BitMask(value)) != 0;

		public bool Any() => data != 0;

		public ValueSet Intersect(ValueSet other) => new ValueSet(this.data & other.data);

		public ValueSet Union(ValueSet other) => new ValueSet(this.data | other.data);

		public ValueSet Except(ValueSet other) => new ValueSet(this.data ^ (this.data & other.data));

		public static readonly ValueSet Empty = new ValueSet();

		public static readonly ValueSet Full = new ValueSet(Enumerable.Range(1, 9));

		public override string ToString() => string.Join(",", this);

		private static uint BitMask(int value) => 1U << (value - 1);

		private static bool IsValid(int value) => value >= 1 && value <= 9;

		private static void EnsureIsValid(int value)
		{
			if (!IsValid(value))
				throw new ArgumentOutOfRangeException("value");
		}

		private static int BitCount(uint data)
		{
			int count = 0;
			for (var bits = data; bits != 0; bits &= (bits - 1))
				count++;
			return count;
		}

		private static IEnumerator<int> ValueIterator(uint data)
		{
			int value = 1;
			for (var bits = data; bits != 0; value++, bits >>= 1)
				if ((bits & 1) != 0) yield return value;
		}
	}
}
