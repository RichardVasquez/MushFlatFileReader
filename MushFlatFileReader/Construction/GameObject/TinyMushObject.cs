using System;
using System.Collections.Generic;
using System.Linq;
using MushFlatFileReader.Construction.Converters;
using MushFlatFileReader.Construction.Parsers;
using MushFlatFileReader.GameHeaders;
using MushFlatFileReader.LegacyTypes;
using MushFlatFileReader.NamedTypes;
using Sprache;

namespace MushFlatFileReader.Construction.GameObject
{
	public class TinyMushObject
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

		public string LockKey { get; set; }
		public string Name { get; private set; }

		public TinyMushObjectType ObjectType { get; private set; }

		public HashSet<TinyMushObjectFlags> Flags = new HashSet<TinyMushObjectFlags>();
		public HashSet<TinyMushObjectPowers> Powers = new HashSet<TinyMushObjectPowers>();
		public List<TinyMushObjectAttribute> Attributes = new List<TinyMushObjectAttribute>();

		public TinyMushObject(MushEntry me)
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
			AccessTime = MakeTime(me.AccessTime);
			ModTime = MakeTime(me.ModTime);

			var tryName = ObjectDataParsers.StripAnsi().TryParse(me.Name);
			Name = tryName.WasSuccessful ? tryName.Value : me.Name;

			IEnumerable<TinyMushObjectFlags> flags = ObjectFlags.GetFlags(me);
			foreach (TinyMushObjectFlags flag in flags)
			{
				Flags.Add(flag);
			}

			IEnumerable<TinyMushObjectPowers> powers = ObjectPowers.SetPowers(me);
			foreach (TinyMushObjectPowers power in powers)
			{
				Powers.Add(power);
			}

			IEnumerable<TinyMushObjectAttribute> attributes = SetAttributes(me, Owner);
			Attributes.AddRange(attributes);

			ObjectType = ObjectTypes.MatchType(me);
		}

		private static DateTime MakeTime(long accessTime)
		{
			DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0);
			epoch = epoch.AddSeconds(accessTime);
			return epoch;
		}

		#region Attributes

		private static IEnumerable<TinyMushObjectAttribute> SetAttributes(MushEntry me, long owner)
		{
			const char marker = '\u0001';
			List<TinyMushObjectAttribute> res = new List<TinyMushObjectAttribute>();
			foreach (MushEntryAttribute attribute in me.Attributes)
			{
				var attr = new TinyMushObjectAttribute();
				if (Enum.IsDefined(typeof (ObjectGameBaseAttributeValues), (ObjectGameBaseAttributeValues)attribute.Id))
				{
					//	Dealing with stock game attribute
					attr.Name = Enum.ToObject(typeof (ObjectGameBaseAttributeValues), attribute.Id).ToString();
				}
				else
				{
					if (Universe.Attributes.ContainsKey(attribute.Id))
					{
						string tempAttrName = Universe.Attributes[ attribute.Id ].Text;
						if (!tempAttrName.Contains(":"))
						{
							continue;
						}
						var parts = tempAttrName.Split(new[] {':'});
						string tmpFlag;
						string tmpName;

						if (parts.Length < 2)
						{
							continue;
						}
						if (parts.Length > 2)
						{
							tmpFlag = parts[ 0 ];
							parts = parts.Skip(1).ToArray();
							tmpName = string.Join(":", parts);
						}
						else
						{
							tmpFlag = parts[ 0 ];
							tmpName = parts[ 1 ];
						}
						long newflag;
						if (long.TryParse(tmpFlag, out newflag))
						{
							AttributeFlags.GetFlags(newflag, attr);
						}
						attr.Name = tmpName;
					}
					else
					{
						continue;
					}
				}

				attr.Id = attribute.Id;
				if (attribute.Text[ 0 ] != marker)
				{
					attr.Text = attribute.Text;
					attr.Owner = owner;
					res.Add(attr);
					continue;
				}

				string temp = attribute.Text.Substring(1);
				var p = ObjectDataParsers.AttributeParser().TryParse(temp);
				if (p.WasSuccessful)
				{
					var t = p.Value;
					attr.Owner = owner;
					if (t.Item1 != "")
					{
						long own;
						bool didOwn = long.TryParse(t.Item1, out own);
						if (didOwn)
						{
							attr.Owner = own;
						}
					}

					if (t.Item2 != "")
					{
						long flags;
						bool didFlags = long.TryParse(t.Item2, out flags);
						if (didFlags)
						{
							AttributeFlags.GetFlags(flags, attr);
						}
					}

					if (t.Item3 == "")
					{
						attr = null;
					}
					else
					{
						attr.Text = t.Item3;
					}
				}

				if (attr != null)
				{
					res.Add(attr);
				}
			}
			return res;
		}
		#endregion
	}
}