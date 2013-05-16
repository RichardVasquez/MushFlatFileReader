using System;
using TinyMushDataStructures.NamedTypes;

namespace TinyMushDataStructures.Interfaces
{
	public interface IMushObjectData
	{
		long DbRef { get; }
		long Location { get; }
		long Zone { get; }
		long Contents { get; }
		long Exits { get; }
		long Link { get; }
		long Next { get; }
		long Owner { get; }
		long Parent { get; }
		long Money { get; }

		DateTime AccessTime { get; }
		DateTime ModTime { get; }

		string LockKey { get; }

		TinyMushObjectType ObjectType { get; }
	}
}
