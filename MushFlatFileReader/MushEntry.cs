using System;
using System.Collections.Generic;
using System.Text;

namespace MushFlatFileReader
{
	public sealed class MushEntry:MushHeader
	{
		private static bool _checkedGameConditions;
		private string _blob;
		public string Name { get; private set; }
		public List<MushEntryAttribute> Attributes;

		public long Location;
		public long Zone;
		public long Contents;
		public long Exits;
		public long Link;
		public long Next;
		public string LockKey;
		public long Owner;
		public long Parent;
		public long Money;
		public long Flags1;
		public long Flags2;
		public long Flags3;
		public long Powers1;
		public long Powers2;
		public long AccessTime;
		public long ModTime;

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
			ParseBlob();
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

		private void ParseBlob()
		{
			_blob = _blob.Trim();
			var lines = _blob.Split(new[] {'\n'});
			int end = lines.Length - 1;
			int current = 0;

			Location = long.Parse(lines[ current ]);
			if (Universe.ReadZone)
			{
				current++;
				Zone = long.Parse(lines[ current ]);
			}

			current++;
			Contents = long.Parse(lines[ current ]);
			current++;
			Exits = long.Parse(lines[ current ]);

			if (Universe.ReadLink)
			{
				current++;
				Link = long.Parse(lines[ current ]);
			}

			current++;
			Next = long.Parse(lines[ current ]);
			//	Now work backwards
			current++;

			if (Universe.ReadTimeStamps)
			{
				ModTime = long.Parse(lines[ end-- ]);
				AccessTime = long.Parse(lines[ end-- ]);
			}

			if (Universe.ReadPowers)
			{
				Powers2 = long.Parse(lines[end--]);
				Powers1 = long.Parse(lines[end--]);
			}

			//	TODO: Need to actually check for all 3 sets of flags
			Flags3 = long.Parse(lines[end--]);
			Flags2 = long.Parse(lines[end--]);
			Flags1 = long.Parse(lines[end--]);
			//	TODO: Universe.ReadMoney
			Money = long.Parse(lines[end--]);
			//	TODO: Universe.ReadParent
			Parent = long.Parse(lines[end--]);
			Owner = long.Parse(lines[ end-- ]);

			StringBuilder sb = new StringBuilder();
			for (int k = current; k <= end; k++)
			{
				sb.Append(lines[ k ]);
			}
			LockKey = sb.ToString();
		}


	}
}