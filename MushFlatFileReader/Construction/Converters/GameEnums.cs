using System;
using System.Collections.Generic;
using System.Linq;
using MushFlatFileReader.GameHeaders;

namespace MushFlatFileReader.Construction.Converters
{
	public static class GameEnums
	{
		public static List<T> Match<T>(MushEntry me, long inFlags) where T : IConvertible
		{
			var values = Enum.GetValues(typeof(T)).Cast<T>().ToList();
			List<T> test =
				values.Where(flag => (inFlags & (long)Convert.ChangeType(flag, typeof(long))) != 0)
				      .ToList();
			return test;
		}
		
	}
}