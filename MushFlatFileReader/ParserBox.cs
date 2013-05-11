using System;
using System.Linq;
using MushFlatFileReader.GameHeaders;
using Sprache;

namespace MushFlatFileReader
{
	public static class ParserBox
	{
		private static bool _showObjectId;
		private static bool _showAttributes;

		/// <summary>
		/// Should parse the entire document.
		/// </summary>
		/// <param name="showId">Indicates if the game object Id should be output.</param>
		/// <param name="showAttribute">Indicates if the game object attribute Ids should be output</param>
		public static Parser<string> Headers(bool showId = false, bool showAttribute = false)
		{
			_showObjectId = showId;
			_showAttributes = showAttribute;

			return
				from h in HeaderLine().Many()
				from e in ReadEntry().Many()
				from end in HeaderEnd()
				select "";
		}

		/// <summary>
		/// Parse the end of the dump.
		/// </summary>
		/// <returns></returns>
		public static Parser<MushHeader> HeaderEnd()
		{
			return
				from a in Parse.Char('*')
				from rest in Parse.AnyChar.Until(Parse.Char('\n'))
				from e in Parse.Char('\n')
				select new HeaderEnding("");
		}

		/// <summary>
		/// Reads the possible header lines.
		/// </summary>
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

		/// <summary>
		/// Reads the record number of players on the MUSH.
		/// </summary>
		public static Parser<HeaderRecordPlayers> RecordPlayers()
		{
			return
				from c in Parse.Char('-')
				from l in Parse.Char('R')
				from n in Parse.Number
				from e in Parse.Char('\n')
				select new HeaderRecordPlayers(n);
		}

		/// <summary>
		/// Reads the game version the flat file is from.
		/// </summary>
		public static Parser<HeaderVersion> GameVersion()
		{
			return
				from c in Parse.Char('+')
				from v in Parse.Char(gv => gv == 'V' || gv == 'X' || gv == 'T', "Type of game engine")
				from n in Parse.Number
				from e in Parse.Char('\n')
				select new HeaderVersion(n,v);
		}

		/// <summary>
		/// Reads the size of the game.
		/// </summary>
		public static Parser<HeaderSize> GameSize()
		{
			return
				from c in Parse.Char('+')
				from l in Parse.Char('S')
				from n in Parse.Number
				from e in Parse.Char('\n')
				select new HeaderSize(n);
		}

		/// <summary>
		/// Reads the open user attribute slot
		/// </summary>
		public static Parser<HeaderFreeAttribute> FreeAttribute()
		{
			return
				from c in Parse.Char('+')
				from l in Parse.Char('F')
				from n in Parse.Number
				from e in Parse.Char('\n')
				select new HeaderFreeAttribute(n);
		}

		/// <summary>
		/// Reads the next attribute to allocate when there's no freelist.
		/// </summary>
		/// <returns></returns>
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

		/// <summary>
		/// Parses '"' and '\n' at end of string.
		/// </summary>
		public static Parser<string> CharSpecialEnd()
		{
			return
				from c1 in Parse.Char('"')
				from c2 in Parse.Char('\n')
				select "\"";
		}

		/// <summary>
		/// Parses CRLF in multiline text.
		/// </summary>
		/// <returns></returns>
		public static Parser<string> CharSpecialCrLf()
		{
			return
				from c1 in Parse.Char('\r')
				from c2 in Parse.Char('\n')
				select "\r\n";
		}

		/// <summary>
		/// Reads an escaped '\n'
		/// </summary>
		public static Parser<string> CharSpecialLf()
		{
			return
				from c1 in Parse.Char('\\')
				from c2 in Parse.Char('n')
				select "\n";
		}

		/// <summary>
		/// Reads an escaped '\r'
		/// </summary>
		public static Parser<string> CharSpecialCr()
		{
			return
				from c1 in Parse.Char('\\')
				from c2 in Parse.Char('r')
				select "\r";
		}

		/// <summary>
		/// Reads an escaped '\t'
		/// </summary>
		public static Parser<string> CharSpecialTab()
		{
			return
				from c1 in Parse.Char('\\')
				from c2 in Parse.Char('t')
				select "\t";
		}

		/// <summary>
		/// Reads an eescaped \e (ESC representation)
		/// </summary>
		/// <returns></returns>
		public static Parser<string> CharSpecialEscape()
		{
			return
				from c1 in Parse.Char('\\')
				from c2 in Parse.Char('e')
				select new string((char) 27, 1);
		}

		/// <summary>
		/// Reads an escaped '"'
		/// </summary>
		public static Parser<string> CharSpecialQuote()
		{
			return
				from c1 in Parse.Char('\\')
				from c2 in Parse.Char('"')
				select "\\\"";
		}

		/// <summary>
		/// Reads an escaped backslash.
		/// </summary>
		public static Parser<string> CharBackslash()
		{
			return
				from c1 in Parse.Char('\\')
				from c2 in Parse.Char('\\')
				select "\\\\";
		}

		/// <summary>
		/// Dumps high ASCII
		/// </summary>
		public static Parser<string> CharHighAscii()
		{
			return
				from c in Parse.Char(ch => ch > (char) 127, "Strip High Ascii")
				select "";
		}

		/// <summary>
		/// Accepts ASCII characters
		/// </summary>
		/// <returns></returns>
		public static Parser<string> CharDefault()
		{
			return
				from c in Parse.Char(ch => ch <= (char) 127 && ch != '"', "ASCII only")
				select new string(c, 1);
		}

		/// <summary>
		/// Readsa a single acceptable character.
		/// </summary>
		public static Parser<string> ValidChar()
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
				select s;
		}
		#endregion

		/// <summary>
		/// Readsa a string based on the proper format of the
		/// game configuration.
		/// </summary>
		/// <remarks>In some versions, string start with a '"', and in others, they don't.</remarks>
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

		/// <summary>
		/// Read an attribute name/definition.
		/// </summary>
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

		/// <summary>
		/// Reads the numeric id of an object.
		/// </summary>
		public static Parser<long> GetObjectId()
		{
			return
				from c in Parse.Char('!')
				from n in Parse.Number
				from e in Parse.Char('\n')
				select long.Parse(n);
		}

		/// <summary>
		/// Reads the object definition.
		/// </summary>
		/// <remarks>Some games store the name as a separate attribute.</remarks>
		public static Parser<MushEntry> ReadEntry()
		{
			if (Universe.ReadName)
			{
				return
					from l in GetObjectId()
					let w = Write(_showObjectId, "Object: ", l.ToString())
					from name in GetString()
					from blob in Parse.Char(c => c != '<' && c != '>', "blah data for now").Many()
					from a in ReadAttributeContent().Many()
					from r in Parse.Char('<')
					from e in Parse.Char('\n')
					select new MushEntry(l, name, new string(blob.ToArray()), a);
			}
			return
				from l in GetObjectId()
				let w = Write(_showObjectId, "Object: ", l.ToString())
				from blob in Parse.Char(c => c != '<' && c != '>', "blah data for now").Many()
				from a in ReadAttributeContent().Many()
				from r in Parse.Char('<')
				from e in Parse.Char('\n')
				select new MushEntry(l, "", new string(blob.ToArray()), a);
		}

		/// <summary>
		/// Reads the attribute id and text.
		/// </summary>
		public static Parser<MushEntryAttribute> ReadAttributeContent()
		{
			return
				from angle in Parse.Char('>')
				from n in Parse.Number
				let w1 = Write(_showAttributes, n, " ")
				from e in Parse.Char('\n')
				from s in GetString()
				let w2 = Write(_showAttributes,Environment.NewLine,Environment.NewLine)
				select new MushEntryAttribute(long.Parse(n), s);
		}

		/// <summary>
		/// Cheap little debug routine to work with parsers.
		/// </summary>
		/// <param name="condTest">Whether output should occur.</param>
		/// <param name="name">First text to output.</param>
		/// <param name="value">Second text to output.</param>
		private static bool Write(bool condTest, string name, string value)
		{
			if (!condTest || string.IsNullOrEmpty(value))
			{
				return false;
			}

			Console.Write("{0}{1}", name, value);
			return true;
		}
	}
}
