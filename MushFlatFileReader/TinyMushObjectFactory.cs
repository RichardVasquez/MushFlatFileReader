using System;
using System.Collections.Generic;
using System.Linq;
using MushFlatFileReader.Construction;
using MushFlatFileReader.Construction.Converters;
using MushFlatFileReader.Construction.GameHeaders;
using MushFlatFileReader.Construction.GameObject;
using MushFlatFileReader.Construction.LegacyTypes;
using MushFlatFileReader.Construction.NamedTypes;
using MushFlatFileReader.Construction.Parsers;
using Sprache;

namespace MushFlatFileReader
{
	public static class TinyMushObjectFactory
	{
		private const char Marker = '\u0001';
		private static readonly char[] SplitColon = {':'};

		public static TinyMushObject Get(long id)
		{
			if (!Universe.Entries.ContainsKey(id))
			{
				return null;
			}

			var me = Universe.Entries[ id ];

			var data = new TinyMushObjectData(me);

			HashSet<TinyMushObjectFlags> flags = new HashSet<TinyMushObjectFlags>(ObjectFlags.GetFlags(me));
			HashSet<TinyMushObjectPowers> powers =
				new HashSet<TinyMushObjectPowers>(ObjectPowers.GetPowers(me));
			IEnumerable<TinyMushObjectAttribute> attributes = GetAttributes(me, data.Owner);
			var tinyMushObjectAttributes = attributes as IList<TinyMushObjectAttribute> ?? attributes.ToList();
			string name = GetName(me, tinyMushObjectAttributes);
			if (name == null)
			{
				return null;
			}

			var tmo = new TinyMushObject(data, flags, powers, tinyMushObjectAttributes, name);
			return tmo;
		}

		private static string GetName(MushEntry me, IEnumerable<TinyMushObjectAttribute> attributes)
		{
			string name;
			if (Universe.ReadName)
			{
				name = me.Name;
			}
			else
			{
				if (attributes == null)
				{
					return null;
				}
				var temp = attributes.ToList().FirstOrDefault(a => a.Id == (long)ObjectGameBaseAttributeValues.NAME);
				if (temp == null)
				{
					return null;
				}
				name = temp.Text;
			}
			var p = ObjectDataParsers.StripAnsi().TryParse(name);
			return p.WasSuccessful
				? p.Value
				: null;
		}

		/// <summary>
		/// Collect all the attributes for a <see cref="MushEntry"/>
		/// </summary>
		private static IEnumerable<TinyMushObjectAttribute> GetAttributes(MushEntry me, long owner)
		{
			return
				me.Attributes.Select(attribute => MakeAttribute(attribute, owner))
				  .Where(attr => attr != null)
				  .ToList();
		}

		/// <summary>
		/// Parse out the default name of the attribute if it's already defined in
		/// <see cref="ObjectGameBaseAttributeValues"/> or parse it out cross referencing
		/// with data in <see cref="Universe"/>.  Also grab the flags and textual information.
		/// </summary>
		private static TinyMushObjectAttribute MakeAttribute(MushEntryAttribute mea, long owner)
		{
			var attr = new TinyMushObjectAttribute();
			if (Enum.IsDefined(typeof (ObjectGameBaseAttributeValues), (ObjectGameBaseAttributeValues) mea.Id))
			{
				//	Dealing with stock game attribute
				attr.Name = Enum.ToObject(typeof (ObjectGameBaseAttributeValues), mea.Id).ToString();
			}
			else
			{
				ProcessUserAttribute(mea, ref attr);
				if (attr == null)
				{
					return null;
				}
			}

			attr.Id = mea.Id;
			if (mea.Text[ 0 ] != Marker)
			{
				attr.Text = mea.Text;
				attr.Owner = owner;
				return attr;
			}

			ProcessComplexUserAttribute(mea, owner, ref attr);
			return attr;
		}

		/// <summary>
		/// Break apart the text of an attribute if needed and convert
		/// the owners and flags.
		/// </summary>
		private static void ProcessComplexUserAttribute(
			MushEntryAttribute mea, long owner, ref TinyMushObjectAttribute attr)
		{
			string temp = mea.Text.Substring(1);
			var p = ObjectDataParsers.AttributeParser().TryParse(temp);
			if (!p.WasSuccessful)
			{
				attr = null;
				return;
			}

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
					AttributeFlags.SetFlags(flags, attr);
				}
			}

			if (t.Item3 == "")
			{
				return;
			}
			attr.Text = t.Item3;
		}

		/// <summary>
		/// Find the actual cross reference of a user attribute.
		/// </summary>
		private static void ProcessUserAttribute(MushEntryAttribute mea, ref TinyMushObjectAttribute attr)
		{
			if (!Universe.Attributes.ContainsKey(mea.Id))
			{
				attr = null;
				return;
			}

			string tempAttrName = Universe.Attributes[ mea.Id ].Text;
			if (!tempAttrName.Contains(":"))
			{
				attr = null;
				return;
			}

			//	Set flags for attribute
			var header = ParseAttributeHeader(tempAttrName);
			if (header == null)
			{
				attr = null;
				return;
			}

			AttributeFlags.SetFlags(header.Item1, attr);
			attr.Name = header.Item2;
		}

		/// <summary>
		/// Make sure we've got a good user attribute header.
		/// </summary>
		private static Tuple<long, string> ParseAttributeHeader(string s)
		{
			var parts = s.Split(SplitColon);
			if (parts.Length < 2)
			{
				return null;
			}

			string flag = parts[ 0 ];
			parts = parts.Skip(1).ToArray();
			string name = string.Join(":", parts);

			long newFlag;
			return !long.TryParse(flag, out newFlag)
				? null 
				: new Tuple<long, string>(newFlag,name);
		}
	}
}
