using System.Collections.Generic;
using System.Linq;
using TinyMushDataStructures.Interfaces;
using TinyMushDataStructures.NamedTypes;

namespace TinyMushDataStructures.GameObject
{
	public class TinyMushObject
	{
		public IMushObjectData Data { get; private set; }
		public string Name { get; private set; }

		public HashSet<TinyMushObjectFlags> Flags = new HashSet<TinyMushObjectFlags>();
		public HashSet<TinyMushObjectPowers> Powers = new HashSet<TinyMushObjectPowers>();
		public List<TinyMushObjectAttribute> Attributes = new List<TinyMushObjectAttribute>();

		public TinyMushObject(
			IMushObjectData data,
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