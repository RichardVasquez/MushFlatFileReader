using System.Collections.Generic;
using MushFlatFileReader.Construction.GameObject;
using MushFlatFileReader.LegacyTypes;
using MushFlatFileReader.NamedTypes;

namespace MushFlatFileReader.Construction.Converters
{
	public static class AttributeFlags
	{
		public static void SetFlags(long flags, TinyMushObjectAttribute attr)
		{
			List<TinyMushAttributeFlags> res = new List<TinyMushAttributeFlags>();

			if ((flags & (long)AttributeFlags1.ODARK) != 0)
			{
				res.Add(TinyMushAttributeFlags.Odark);
			}
			if ((flags & (long)AttributeFlags1.DARK) != 0)
			{
				res.Add(TinyMushAttributeFlags.Dark);
			}
			if ((flags & (long)AttributeFlags1.WIZARD) != 0)
			{
				res.Add(TinyMushAttributeFlags.Wizard);
			}
			if ((flags & (long)AttributeFlags1.MDARK) != 0)
			{
				res.Add(TinyMushAttributeFlags.Mdark);
			}
			if ((flags & (long)AttributeFlags1.INTERNAL) != 0)
			{
				res.Add(TinyMushAttributeFlags.Internal);
			}
			if ((flags & (long)AttributeFlags1.NOCMD) != 0)
			{
				res.Add(TinyMushAttributeFlags.NoCmd);
			}
			if ((flags & (long)AttributeFlags1.LOCK) != 0)
			{
				res.Add(TinyMushAttributeFlags.Lock);
			}
			if ((flags & (long)AttributeFlags1.DELETED) != 0)
			{
				res.Add(TinyMushAttributeFlags.Deleted);
			}
			if ((flags & (long)AttributeFlags1.NOPROG) != 0)
			{
				res.Add(TinyMushAttributeFlags.NoProg);
			}
			if ((flags & (long)AttributeFlags1.GOD) != 0)
			{
				res.Add(TinyMushAttributeFlags.God);
			}
			if ((flags & (long)AttributeFlags1.IS_LOCK) != 0)
			{
				res.Add(TinyMushAttributeFlags.IsLock);
			}
			if ((flags & (long)AttributeFlags1.VISUAL) != 0)
			{
				res.Add(TinyMushAttributeFlags.Visual);
			}
			if ((flags & (long)AttributeFlags1.PRIVATE) != 0)
			{
				res.Add(TinyMushAttributeFlags.Private);
			}
			if ((flags & (long)AttributeFlags1.HTML) != 0)
			{
				res.Add(TinyMushAttributeFlags.Html);
			}
			if ((flags & (long)AttributeFlags1.NOPARSE) != 0)
			{
				res.Add(TinyMushAttributeFlags.NoParse);
			}
			if ((flags & (long)AttributeFlags1.REGEXP) != 0)
			{
				res.Add(TinyMushAttributeFlags.Regexp);
			}
			if ((flags & (long)AttributeFlags1.NOCLONE) != 0)
			{
				res.Add(TinyMushAttributeFlags.NoClone);
			}
			if ((flags & (long)AttributeFlags1.CONST) != 0)
			{
				res.Add(TinyMushAttributeFlags.Const);
			}
			if ((flags & (long)AttributeFlags1.CASE) != 0)
			{
				res.Add(TinyMushAttributeFlags.Case);
			}
			if ((flags & (long)AttributeFlags1.STRUCTURE) != 0)
			{
				res.Add(TinyMushAttributeFlags.Structure);
			}
			if ((flags & (long)AttributeFlags1.DIRTY) != 0)
			{
				res.Add(TinyMushAttributeFlags.Dirty);
			}
			if ((flags & (long)AttributeFlags1.DEFAULT) != 0)
			{
				res.Add(TinyMushAttributeFlags.Default);
			}
			if ((flags & (long)AttributeFlags1.NONAME) != 0)
			{
				res.Add(TinyMushAttributeFlags.NoName);
			}
			if ((flags & (long)AttributeFlags1.RMATCH) != 0)
			{
				res.Add(TinyMushAttributeFlags.Rmatch);
			}
			if ((flags & (long)AttributeFlags1.NOW) != 0)
			{
				res.Add(TinyMushAttributeFlags.Now);
			}
			if ((flags & (long)AttributeFlags1.TRACE) != 0)
			{
				res.Add(TinyMushAttributeFlags.Trace);
			}

			foreach (TinyMushAttributeFlags tmFlag in res)
			{
				attr.Flags.Add(tmFlag);
			}
		}

	}
}