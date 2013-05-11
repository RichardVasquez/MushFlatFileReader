using System;

namespace MushFlatFileReader.Construction.Converters
{
	public static class Time
	{
		public static DateTime MakeFromEpoch(long offset)
		{
			DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0);
			epoch = epoch.AddSeconds(offset);
			return epoch;
		}
	}
}
