using System;
using System.Collections.Generic;
using System.Linq;
using MushFlatFileReader.Construction.GameHeaders;

namespace MushFlatFileReader.Construction.Converters
{
	public static class GameEnums
	{
		/// <summary>
		/// Collects the matching flag values based off the bits of the long.
		/// </summary>
		/// <typeparam name="T">Should be an enum of Flags and long</typeparam>
		/// <param name="me">NOTE: Not really sure why this is here.  May be deletable.</param>
		/// <param name="inFlags">The long that needs to be examined for bit masked values.</param>
		/// <returns>A list of matching T values found in <see cref="inFlags"/></returns>
		/// <remarks>
		/// Going through my notes and bookmarks, I believe the base for this was found at
		/// http://stackoverflow.com/a/12022617/1964356
		/// </remarks>
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