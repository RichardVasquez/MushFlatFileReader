using System.Collections.Generic;
using System.Linq;
using MushFlatFileReader.NamedTypes;

namespace MushFlatFileReader.Construction.GameObject
{
	public class TinyMushObject
	{
		public TinyMushObjectData Data { get; private set; }
		public string Name { get; private set; }

		public HashSet<TinyMushObjectFlags> Flags = new HashSet<TinyMushObjectFlags>();
		public HashSet<TinyMushObjectPowers> Powers = new HashSet<TinyMushObjectPowers>();
		public List<TinyMushObjectAttribute> Attributes = new List<TinyMushObjectAttribute>();

		public TinyMushObject(
			TinyMushObjectData data,
			HashSet<TinyMushObjectFlags> flags,
			HashSet<TinyMushObjectPowers> powers,
			IEnumerable<TinyMushObjectAttribute> attributes,
			string name)
		{
			Data = data;
			Flags = flags;
			Powers = powers;
			Attributes = attributes.ToList();
			Name = name;
		}
	}
}