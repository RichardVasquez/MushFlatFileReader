using System;
using System.Collections.Generic;

namespace MushFlatFileReader
{
	public sealed class MushEntry:MushHeader
	{
		private static bool _checkedGameConditions;
		private object _blob;
		public string Name { get; private set; }
		public List<MushEntryAttribute> Attributes;

		public MushEntry(string val) : base(val)
		{
			if (!_checkedGameConditions)
			{
				CheckGameConditions();
			}
			Register();
		}

		public MushEntry(long val, string name, string blob, IEnumerable<MushEntryAttribute> mushEntryAttributes) : base(val.ToString())
		{
			if (!_checkedGameConditions)
			{
				CheckGameConditions();
			}
			Name = name;
			_blob = blob;
			Attributes=new List<MushEntryAttribute>(mushEntryAttributes);
			Register();
		}

		private static void CheckGameConditions()
		{
			if (Universe.DeduceVersion)
			{
				Universe.GameFormat = GameType.TinyMush;
				Universe.GameVersion = 1;
				Universe.DeduceName = false;
				Universe.DeduceZone = false;
				Universe.DeduceVersion = false;
			}
			else
			{
				if (Universe.DeduceZone)
				{
					Universe.DeduceZone = false;
					Universe.ReadZone = false;
				}
			}
			_checkedGameConditions = true;
		}

		#region Overrides of MushHeader
		protected override void Register()
		{
			Universe.RegisterEntry(this);
		}
		#endregion
	}
	
	public class MushEntryAttribute
	{
		public long Id;
		public string Text;

		public MushEntryAttribute(long parse, string s)
		{
			Id = parse;
			Text = s;
		}
	}
}