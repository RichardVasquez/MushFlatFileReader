using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sprache;

namespace MushFlatFileReader
{

	public interface IMushHeader
	{
		long Number { get; }
	}

	public abstract class MushHeader:IMushHeader
	{
		protected MushHeader(string val)
		{
			Parse(val);
		}

		private void Parse(string val)
		{
			long l;
			try
			{
				if (!long.TryParse(val, out l))
				{
					l = -1;
				}
			}
			catch (Exception)
			{
				l = -1;
			}
			Number = l;
		}

		#region Implementation of IMushHeader
		public long Number { get; private set; }
		#endregion
	}

	public class HeaderRecordPlayers:MushHeader
	{
		public HeaderRecordPlayers(string val) : base(val)
		{
		}
	}

	public class HeaderVersion:MushHeader
	{
		public HeaderVersion(string val, char c) : base(val)
		{
		}
	}

	public class HeaderSize:MushHeader
	{
		public HeaderSize(string val) : base(val)
		{
		}
	}

	public class HeaderAttribute:MushHeader
	{
		public HeaderAttribute(string val) : base(val)
		{
		}
	}

	public class HeaderFreeAttribute:MushHeader
	{
		public HeaderFreeAttribute(string val) : base(val)
		{
		}
	}

	public class HeaderNextAttribute:MushHeader
	{
		public HeaderNextAttribute(string val) : base(val)
		{
		}
	}

	public static class ParserBox
	{
		public static Parser<MushHeader> Header()
		{
			return
				from h in
					RecordPlayers()
					.Or<MushHeader>(GameSize())
					.Or<MushHeader>(GameVersion())
					.Or<MushHeader>(FreeAttribute())
					.Or<MushHeader>(NextAttribute())
				select h;
		}

		public static Parser<HeaderRecordPlayers> RecordPlayers()
		{
			return
				from c in Parse.Char('-')
				from l in Parse.Char('R')
				from n in Parse.Number
				from e in Parse.Char('\n')
				select new HeaderRecordPlayers(n);
		}

		public static Parser<HeaderVersion> GameVersion()
		{
			return
				from c in Parse.Char('+')
				from v in Parse.Char(gv => gv == 'V' || gv == 'X' || gv == 'T', "Type of game engine")
				from n in Parse.Number
				from e in Parse.Char('\n')
				select new HeaderVersion(n,v);
		}

		public static Parser<HeaderSize> GameSize()
		{
			return
				from c in Parse.Char('-')
				from l in Parse.Char('S')
				from n in Parse.Number
				from e in Parse.Char('\n')
				select new HeaderSize(n);
		}

		public static Parser<HeaderFreeAttribute> FreeAttribute()
		{
			return
				from c in Parse.Char('-')
				from l in Parse.Char('F')
				from n in Parse.Number
				from e in Parse.Char('\n')
				select new HeaderFreeAttribute(n);
		}

		public static Parser<HeaderNextAttribute> NextAttribute()
		{
			return
				from c in Parse.Char('-')
				from l in Parse.Char('N')
				from n in Parse.Number
				from e in Parse.Char('\n')
				select new HeaderNextAttribute(n);
		}

	}
}
