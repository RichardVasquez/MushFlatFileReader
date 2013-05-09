using System.Collections.Generic;

namespace MushFlatFileReader
{
	public class TinyMushObjectAttribute
	{
		public string Name;
		public long Id;
		public long Owner;
		public string Text;
		public HashSet<TinyMushAttributeFlags> Flags = new HashSet<TinyMushAttributeFlags>();
	}
}