using System.Collections.Generic;
using TinyMushDataStructures.NamedTypes;

namespace TinyMushDataStructures.GameObject
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