using System;
using MushFlatFileReader.Construction.Converters;
using MushFlatFileReader.GameHeaders;
using MushFlatFileReader.NamedTypes;

namespace MushFlatFileReader.Construction.GameObject
{
	public class TinyMushObjectData
	{
		public long DbRef { get; private set; }
		public long Location { get; private set; }
		public long Zone { get; private set; }
		public long Contents { get; private set; }
		public long Exits { get; private set; }
		public long Link { get; private set; }
		public long Next { get; private set; }
		public long Owner { get; private set; }
		public long Parent { get; private set; }
		public long Money { get; private set; }

		public DateTime AccessTime { get; private set; }
		public DateTime ModTime { get; private set; }

		public string LockKey { get; private set; }

		public TinyMushObjectType ObjectType { get; private set; }

		public TinyMushObjectData(MushEntry me)
		{
			DbRef = me.Number;
			Location = me.Location;
			Zone = me.Zone;
			Contents = me.Contents;
			Exits = me.Exits;
			Link = me.Link;
			Next = me.Next;
			Owner = me.Owner;
			Parent = me.Parent;
			Money = me.Money;
			AccessTime = Time.MakeFromEpoch(me.AccessTime);
			ModTime = Time.MakeFromEpoch(me.ModTime);
			LockKey = me.LockKey;
			ObjectType = ObjectTypes.MatchType(me);
		}
	}
}
