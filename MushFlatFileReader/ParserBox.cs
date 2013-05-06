using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sprache;

namespace MushFlatFileReader
{
	public static class ParserBox
	{
		public static Parser<object> Headers()
		{
			return
				from h in HeaderLine().Many()
				from e in ReadEntry().Many()
				select h;
		}

		public static Parser<MushHeader> HeaderLine()
		{
			return
				from h in
					RecordPlayers()
					.Or<MushHeader>(GameSize())
					.Or<MushHeader>(GameVersion())
					.Or<MushHeader>(FreeAttribute())
					.Or<MushHeader>(NextAttribute())
					.Or<MushHeader>(GetAttribute())
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
				from c in Parse.Char('+')
				from l in Parse.Char('S')
				from n in Parse.Number
				from e in Parse.Char('\n')
				select new HeaderSize(n);
		}

		public static Parser<HeaderFreeAttribute> FreeAttribute()
		{
			return
				from c in Parse.Char('+')
				from l in Parse.Char('F')
				from n in Parse.Number
				from e in Parse.Char('\n')
				select new HeaderFreeAttribute(n);
		}

		public static Parser<HeaderNextAttribute> NextAttribute()
		{
			return
				from c in Parse.Char('+')
				from l in Parse.Char('N')
				from n in Parse.Number
				from e in Parse.Char('\n')
				select new HeaderNextAttribute(n);
		}

		#region attribute string stuff
		public static Parser<string> CharSpecialEnd()
		{
			return
				from c1 in Parse.Char('"')
				from c2 in Parse.Char('\n')
				select "\"";
		}

		public static Parser<string> CharSpecialCrLf()
		{
			return
				from c1 in Parse.Char('\r')
				from c2 in Parse.Char('\n')
				select "\r\n";
		}

		public static Parser<string> CharSpecialLf()
		{
			return
				from c1 in Parse.Char('\\')
				from c2 in Parse.Char('n')
				select "\n";
		}

		public static Parser<string> CharSpecialCr()
		{
			return
				from c1 in Parse.Char('\\')
				from c2 in Parse.Char('r')
				select "\r";
		}

		public static Parser<string> CharSpecialTab()
		{
			return
				from c1 in Parse.Char('\\')
				from c2 in Parse.Char('t')
				select "\t";
		}

		public static Parser<string> CharSpecialEscape()
		{
			return
				from c1 in Parse.Char('\\')
				from c2 in Parse.Char('e')
				select new string((char) 27, 1);
		}

		public static Parser<string> CharSpecialQuote()
		{
			return
				from c1 in Parse.Char('\\')
				from c2 in Parse.Char('"')
				select "\\\"";
		}

		public static Parser<string> CharBackslash()
		{
			return
				from c1 in Parse.Char('\\')
				from c2 in Parse.Char('\\')
				select "\\\\";
		}

		public static Parser<string> CharHighAscii()
		{
			return
				from c in Parse.Char(ch => ch > (char) 127, "Strip High Ascii")
				select "";
		}

		public static Parser<string> CharDefault()
		{
			return
				from c in Parse.Char(ch => ch <= (char) 127 && ch != '"', "ASCII only")
				select new string(c, 1);
		}

		public static Parser<string> ValidChar()
		{
			if(Universe.MyDebug)
			{
				return
					from s in
						CharSpecialQuote()
						.Or(CharSpecialCrLf())
						.Or(CharSpecialLf())
						.Or(CharSpecialCr())
						.Or(CharSpecialTab())
						.Or(CharSpecialEscape())
						.Or(CharBackslash())
						.Or(CharHighAscii())
						.Or(CharDefault())
					let sc = Write("\t\ts", s)
					select s;
			}
			return
				from s in
					CharSpecialQuote()
					.Or(CharSpecialCrLf())
					.Or(CharSpecialLf())
					.Or(CharSpecialCr())
					.Or(CharSpecialTab())
					.Or(CharSpecialEscape())
					.Or(CharBackslash())
					.Or(CharHighAscii())
					.Or(CharDefault())
				select s;
		}
		#endregion

		public static Parser<string> GetString()
		{
			if (Universe.ReadNewStrings)
			{
				return
					from c in Parse.Char('"')
					from s in ValidChar().Many()
					from e in CharSpecialEnd()
					select string.Concat(s.ToArray());
			}

			return
				from s in ValidChar().Many()
				from c in Parse.Char('\n')
				select string.Concat(s.ToArray());
		}

		public static Parser<HeaderAttribute> GetAttribute()
		{
			return
				from c in Parse.Char('+')
				from l in Parse.Char('A')
				from n in Parse.Number
				from e in Parse.Char('\n')
				from s in GetString()
				select new HeaderAttribute(n, s);
		}

		public static Parser<long> GetObjectId()
		{
			return
				from c in Parse.Char('!')
				from n in Parse.Number
				from e in Parse.Char('\n')
				select long.Parse(n);
		}

		public static Parser<MushEntry> ReadEntry()
		{
			Universe.MyDebug = true;
			if (Universe.ReadName)
			{
				return
					from l in GetObjectId()
					let lc = Write("l", l.ToString())
					from name in GetString()
					let namec = Write("name", name)
					from blob in Parse.Char(c => c != '<' && c != '>', "blah data for now").Many()
					let blobc=Write("blob", blob.ToString())
					from a in ReadAttributeContent().Many()
					let ac = Write("a", a.ToString())
					from r in Parse.Char('<')
					let rc=Write("<",r.ToString())
					from e in Parse.Char('\n')
					let ec = Write("\\n", "\\n")
					select new MushEntry(l, name, new string(blob.ToArray()), a);
			}
			return
				from l in GetObjectId()
				from blob in Parse.Char(c => c != '<' && c != '>', "blah data for now").Many()
				from a in ReadAttributeContent().Many()
				from r in Parse.Char('<')
				from e in Parse.Char('\n')
				select new MushEntry(l, "", new string(blob.ToArray()), a);
		}

		public static Parser<MushEntryAttribute> ReadAttributeContent()
		{
			return
				from angle in Parse.Char('>')
				let ac = Write("\t>",">")
				from n in Parse.Number
				let nc = Write("\tn", n)
				from e in Parse.Char('\n')
				let ec = Write("\te","\\n")
				from s in GetString()
				let sc = Write("\ts",s)
				select new MushEntryAttribute(long.Parse(n), s);
		}

		private static bool Write(string name, string value)
		{
			return true;
			Console.Write(name+": ");
			if (string.IsNullOrEmpty(value))
			{
				Console.WriteLine("[empty]");
				return true;
			}
			if (value.Length < 30)
			{
				Console.WriteLine(value);
				return true;
			}
			Console.WriteLine(value.Substring(0,27) + "...");
			return true;
		}

	}
}
