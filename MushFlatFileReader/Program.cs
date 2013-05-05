using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Sprache;

namespace MushFlatFileReader
{
	class Program
	{
		static void Main(string[] args)
		{
			var res = new Result();
			var lines = File.ReadAllLines("flatfile.txt");
			GetRoughObjectMap(lines, res);


		}

		private static void GetRoughObjectMap(IList<string> lines, Result res)
		{
			int k = 0;
			var tt = BigBoxOfParsers.Tfield().TryParse(lines[ k++ ] + "\n");
			if (tt.WasSuccessful)
			{
				res.FieldT = tt.Value;
			}
			var ts = BigBoxOfParsers.Sfield().TryParse(lines[ k++ ] + "\n");
			if (tt.WasSuccessful)
			{
				res.FieldS = ts.Value;
			}
			var tn = BigBoxOfParsers.Nfield().TryParse(lines[ k++ ] + "\n");
			if (tn.WasSuccessful)
			{
				res.FieldN = tn.Value;
			}
			var tr = BigBoxOfParsers.Rfield().TryParse(lines[ k++ ] + "\n");
			if (tr.WasSuccessful)
			{
				res.FieldR = tr.Value;
			}
			while (true)
			{
				if (lines[ k ].StartsWith("!"))
				{
					break;
				}
				string temp = lines[ k++ ] + "\n" + lines[ k++ ] + "\n";
				var ta = BigBoxOfParsers.AttributeData().TryParse(temp);
				if (ta.WasSuccessful)
				{
					res.Attributes.Add(ta.Value);
				}
			}
			while (true)
			{
				var ro = new RoughGameObject();
				string ids = lines[ k++ ] + "\n";
				var tid = BigBoxOfParsers.ObjectId().TryParse(ids);
				if (!tid.WasSuccessful)
				{
					break;
				}
				ro.Id = tid.Value;
				var tname = BigBoxOfParsers.QuotedText().TryParse(lines[ k++ ] + "\n");
				if (!tname.WasSuccessful)
				{
					break;
				}
				ro.Name = tname.Value;
				StringBuilder sb = new StringBuilder();
				while (true)
				{
					string line = lines[ k++ ];
					if (line.StartsWith(">"))
					{
						ro.Other = sb.ToString();
						k--;
						break;
					}
					sb.Append(line).Append("\n");
				}
				while (true)
				{
					if (!lines[ k++ ].StartsWith("<"))
					{
						k--;
					}
					else
					{
						break;
					}
					//Console.Write("{0}:", k);
					string tempid = lines[ k++ ];
					//Console.WriteLine(tempid);
					sb = new StringBuilder();
					while (true)
					{
						string temp = lines[ k++ ];
						//Console.WriteLine("{0}:{1}",k,temp);
						while (true)
							//!temp.StartsWith(">") && !temp.StartsWith("<"))
						{
							if (temp.StartsWith("<") && temp == "<")
							{
								break;
							}
							if (temp.StartsWith(">"))
							{
								string num = temp.Substring(1);
								long l;
								if (long.TryParse(num, out l))
								{
									break;
								}
							}
							sb.Append(temp).Append("\n");
							temp = lines[ k++ ];
							//Console.WriteLine("{0}:{1}",k,temp);
						}
						k--;
						break;
					}
					string tempval = sb.ToString();
					if (tempval.EndsWith("\n"))
					{
						tempval = tempval.Substring(0, tempval.Length - 1);
					}
					if (!tempid.StartsWith(">") || !tempval.StartsWith(@""""))
					{
						break;
					}
					var tattr = new AttributeText {Id = long.Parse(tempid.Substring(1)), Text = tempval};
					ro.Atts.Add(tattr);
				}
				res.GameObjects.Add(ro);
				k--;
				if (lines[ k ] != "<")
				{
					break;
				}
				k++;
			}
		}
	}

	public enum GameType
	{
		/// <summary>
		/// Unknown
		/// </summary>
		Unknown,
		/// <summary>
		/// 3.0 Version
		/// </summary>
		TinyMush,
		/// <summary>
		/// 2.0 Version
		/// </summary>
		Mush,
		/// <summary>
		/// Mux version
		/// </summary>
		Mux
	}

	public class Universe
	{
		private int _i;
		private int _anum;
		private char _ch;
		//private const char *tstr;
		private bool _headerGotten;
		private int _size_gotten;
		private int _nextattr_gotten;
		private int _read_attribs;
		private int _read_name;
		private int _read_zone;
		private int _read_link;
		private int _read_key;
		private int _read_parent;
		private int _read_extflags;
		private int _read_3flags;
		private int _read_money;
		private int _read_timestamps;
		private int _read_new_strings;
		private long _read_powers;
		private int _read_powers_player;
		private int _read_powers_any;
		private int _has_typed_quotas;
		private int _has_visual_attrs;
		private int _deduce_version;
		private int _deduce_name;
		private int _deduce_zone;
		private int _deduce_timestamps;
		private int _aflags;
		private int _f1;
		private int _f2;
		private int _f3;
		//BOOLEXP *tempbool;
		//time_t tmptime;


		private bool _headerRead;
		private bool _sizeGotten;
		private bool _nextAttrGotten;
		private GameType _gameFormat = GameType.Unknown;
		private long _gameVersion;
		private int _gameFlags;
		private bool _readAttribs = true;
		private bool _readName = true;
		private bool _readZone;
		private bool _readLink;
		private bool _readKey;
		private bool _readParent;
		private bool _readMoney = true;
		private bool _readExtFlags;
		private long _read3Flags;
		private bool _hasTypedQuotas;
		private bool _hasVisualAttributes;
		private bool _readTimestamps;
		private long _readNewStrings;
		private bool _readPowers;
		private bool _readPowersPlayer;
		private bool _readPowersAny;
		private bool _deduceVersion = true;
		private bool _deduceZone = true;
		private bool _deduceTimestamps = true;
		private bool _timeChecking;
		private long _objTime;

		private long _currentObject = 0;

		private readonly Dictionary<string, IMushHeader> _headers = new Dictionary<string, IMushHeader>();
		private readonly Dictionary<long, AttributeDefinition> _attributes = new Dictionary<long, AttributeDefinition>();

		public Universe(string[] lines)
		{
			long ptr = 0;
			bool done = false;
			while (!done)
			{
				ParseLine(lines, ref ptr, ref done);
			}

		}

		private void ParseLine(string[] lines, ref long ptr, ref bool done)
		{
			string line = lines[ ptr ];
			char c = line.Length > 0 ? line[ 0 ] : '\0';
			switch (c)
			{
				case '-':
					ParseMiscFlag(line, ref ptr);
					break;
				case '+':
					ParseMuxMushHeaders(lines, ref ptr, ref done);
					break;
				case '!':
					break;
				case '*':
					done = true;
					break;
				default:
					ErrorMessage(string.Format("Illegal character '{0}' starting line {1}.", c, ptr));
					break;
			}
			ptr++;
		}

		private void ErrorMessage(string s)
		{
			var fg = Console.ForegroundColor;
			var bg = Console.BackgroundColor;
			if (fg == ConsoleColor.Red)
			{
				Console.ForegroundColor = ConsoleColor.White;
				Console.BackgroundColor = ConsoleColor.DarkRed;
			}
			else
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.BackgroundColor = ConsoleColor.White;
			}
			Console.WriteLine();
			Console.WriteLine(s);
			Console.WriteLine();
			Console.ForegroundColor = fg;
			Console.BackgroundColor = bg;
		}

		private void ParseMuxMushHeaders(string[] lines, ref long ptr, ref bool done)
		{
			if (lines[ ptr ].Length < 2)
			{
				return;
			}


			switch (lines[ptr][1])
			{
				case 'V':
					SetGameParameters(lines[ ptr ], ref ptr);
					_gameFormat = GameType.Mush;
					break;
				case 'X':
					SetGameParameters(lines[ ptr ], ref ptr);
					_read3Flags = _gameVersion & (long) DatabaseVersionFlags.Flags3;
					_read_powers = _gameVersion & (long)DatabaseVersionFlags.Powers;
					_readNewStrings = _gameVersion & (long) DatabaseVersionFlags.Quoted;
					_gameFormat = GameType.Mux;
					break;
				case 'T':
					SetGameParameters(lines[ ptr ], ref ptr);
					_gameFormat = GameType.TinyMush;
					_read3Flags = _gameVersion & (long) DatabaseVersionFlags.Flags3;
					_read_powers = _gameVersion & (long)DatabaseVersionFlags.Powers;
					_readNewStrings = _gameVersion & (long) DatabaseVersionFlags.Quoted;
					break;
				case 'S':
					SetSize(lines[ ptr ], ref ptr);
					break;
				case 'A':
					var ad = new AttributeDefinition(lines, ref ptr, _readNewStrings);
					if (ad.Id >= 0)
					{
						_attributes[ ad.Id ] = ad;
					}
					break;
				case 'F':
					break;
				case 'N':
					break;
				default:
					ErrorMessage(string.Format("Unexpected +{0} starting line {1}.", lines[ ptr ][ 1 ], ptr));
					break;
			}
		}

		private void ReadAttribute(string[] lines, ref long ptr)
		{
			
		}

		private void SetSize(string s, ref long ptr)
		{
			if (_sizeGotten)
			{
				ErrorMessage(string.Format("Duplicate size entry at line {0}.", ptr));
				ptr++;
			}

			var se = new SizeEntry(s.Substring(2));
			if (se.Number >= 0)
			{
				_headers[ "SizeEntry" ] = se;
				_sizeGotten = true;
			}
			ptr++;
		}

		private void SetGameParameters(string s, ref long ptr)
		{
			if (_headerGotten)
			{
				string h = s.Substring(0, 1);
				ErrorMessage(string.Format("Duplicare game version header {0} at line {1}.  Ignored.", h, ptr));
				ptr++;
				return;
			}
			GameVersion gv = new GameVersion(s.Substring(2));
			if (gv.Number >= 0)
			{
				_headers[ "GameVersion" ] = gv;

				_deduce_name = 0;
				_headerGotten = true;
				_deduceVersion = false;
				_gameVersion = gv.Number & (long) DatabaseVersionFlags.Mask;
			}
			ptr++;
		}

		private void ParseMiscFlag(string line, ref long ptr)
		{
			if (line.Length < 3)
			{
				ptr++;
				return;
			}

			switch (line[1])
			{
				case 'R':
					var r = new RecordNumberOfPlayers(line.Substring(2));
					if (r.Number >= 0)
					{
						_headers[ "RecordNumberOfPlayers" ] = r;
					}
					break;
			}
			ptr++;
		}
	}

	public class AttributeDefinition
	{
		public long Id { get; private set; }
		public long Flags { get; private set; }
		public string Text { get; private set; }

		public AttributeDefinition(string[] lines, ref long ptr, long readNewStrings)
		{
			if (lines[ ptr ].Length < 2)
			{
				Id = -1;
				return;
			}
			SetId(lines, ptr);
			ptr++;
			if (Id < 0)
			{
				return;
			}

			string txt = CollectAttributeText(lines, ref ptr, readNewStrings!=0);
		}

		private string CollectAttributeText(string[] lines, ref long ptr, bool readNewStrings)
		{
			List<string> temp = new List<string>();

			if (readNewStrings)
			{
				if (lines[ ptr ].StartsWith("\""))
				{
					temp.Add(lines[ptr]);
				}
			}

			while (!lines[ ptr + 1 ].StartsWith(">"))
			{
				temp.Add(lines[ptr++]);
				ptr++;
			}
			



		}

		private void SetId(string[] lines, long ptr)
		{
			long l;
			if (long.TryParse(lines[ ptr ].Substring(2), out l))
			{
				Id = l;
				return;
			}
			Id = -1;
		}
	}

	public abstract class MushNumber:IMushHeader
	{
		protected MushNumber(string s)
		{
			Number = Parse(s);
		}

		protected long Parse(string val)
		{
			long l;
			if (long.TryParse(val, out l))
			{
				if (l >= 0)
				{
					return l;
				}
			}
			return -1;
		}

		#region Implementation of IMushHeader
		public long Number { get; private set; }
		#endregion
	}

	public class SizeEntry:MushNumber
	{
		public SizeEntry(string s):base(s)
		{
		}
	}

	public class RecordNumberOfPlayers:MushNumber
	{
		public RecordNumberOfPlayers(string val) : base(val)
		{
		}
	}

	public class GameVersion:MushNumber
	{
		public long ReadZone { get; private set; }
		public long ReadLink { get; private set; }
		public long ReadKey { get; private set; }
		public long ReadParent { get; private set; }
		public long ReadMoney { get; private set; }
		public long ReadExtFlags { get; private set; }
		public long HasTypedQuotas { get; private set; }
		public long ReadTimeStamps { get; private set; }
		public long HasVisualAttrs { get; private set; }
		public long GameFlags { get; private set; }

	
		public GameVersion(string val) : base(val)
		{
			GetFeatures();
		}

		private void GetFeatures()
		{
			ReadZone = Number & (long) DatabaseVersionFlags.Zone;
			ReadLink = Number & (long) DatabaseVersionFlags.Link;
			ReadKey = Number & (long) DatabaseVersionFlags.AtrKey;
			ReadParent = Number & (long) DatabaseVersionFlags.Parent;
			ReadMoney = Number & (long) DatabaseVersionFlags.AtrMoney;
			ReadExtFlags = Number & (long) DatabaseVersionFlags.Xflags;
			HasTypedQuotas = Number & (long) DatabaseVersionFlags.Tquotas;
			ReadTimeStamps = Number & (long) DatabaseVersionFlags.Timestamps;
			HasVisualAttrs = Number & (long) DatabaseVersionFlags.VisualAttrs;
			GameFlags = Number & (long) ~DatabaseVersionFlags.Mask;
		}
	}

	public interface IMushHeader
	{
		long Number { get; }
	}



	#region Old
	public class GameField
	{
		public bool Sign { get; set; }
		public char Letter { get; set; }
		public long Value { get; set; }
	}

	public class AttributeName:GameField
	{
		public int Flags { get; set; }
		public string Name { get; set; }
	}

	[DebuggerDisplay("{Id}:{Text}")]
	public class AttributeText
	{
		public long Id { get; set; }
		public string Text { get; set; }
	}

	public class Result
	{
		public GameField FieldT;
		public GameField FieldS;
		public GameField FieldN;
		public GameField FieldR;
		public List<AttributeName> Attributes = new List<AttributeName>();
		public List<RoughGameObject> GameObjects = new List<RoughGameObject>();
	}

	[DebuggerDisplay("#{Id}:{Name}")]
	public class RoughGameObject
	{
		public long Id;
		public string Name;
		public object Other;
		public List<AttributeText> Atts = new List<AttributeText>();
	}

	public static class BigBoxOfParsers
	{
		public static Parser<GameField> Tfield()
		{
			return
				from plus in Parse.Char('+')
				from t in Parse.Char('T')
				from number in Parse.Number
				from eol in Parse.Char('\n')
				select new GameField { Letter = 'T', Sign = true, Value = long.Parse(number) };
		}

		public static Parser<GameField> Sfield()
		{
			return
				from plus in Parse.Char('+')
				from t in Parse.Char('S')
				from number in Parse.Number
				from eol in Parse.Char('\n')
				select new GameField { Letter = 'S', Sign = true, Value = long.Parse(number) };
		}

		public static Parser<GameField> Nfield()
		{
			return
				from plus in Parse.Char('+')
				from t in Parse.Char('N')
				from number in Parse.Number
				from eol in Parse.Char('\n')
				select new GameField { Letter = 'N', Sign = true, Value = long.Parse(number) };
		}

		public static Parser<GameField> Rfield()
		{
			return
				from plus in Parse.Char('-')
				from t in Parse.Char('R')
				from number in Parse.Number
				from eol in Parse.Char('\n')
				select new GameField { Letter = 'R', Sign = false, Value = long.Parse(number) };
		}

		public static Parser<AttributeName> AttributeData()
		{
			return
				from plus in Parse.Char('+')
				from a in Parse.Char('A')
				from n1 in Parse.Number
				from eol in Parse.Char('\n')
				from q1 in Parse.Char('"')
				from n2 in Parse.Number
				from c in Parse.Char(':')
				from s in Parse.CharExcept('"').Many()
				from q2 in Parse.Char('"')
				from eol2 in Parse.Char('\n')
				select
					new AttributeName
					{
						Letter = 'A',
						Sign = true,
						Value = long.Parse(n1),
						Flags = int.Parse(n2),
						Name = new string(s.ToArray())
					};
		}

		public static Parser<long> ObjectId()
		{
			return
				from bang in Parse.Char('!')
				from n in Parse.Number
				from eol in Parse.Char('\n')
				select long.Parse(n);
		}

		public static Parser<long> AttributeId()
		{
			return
				from bang in Parse.Char('>')
				from n in Parse.Number
				from eol in Parse.Char('\n')
				select long.Parse(n);
		}

		public static Parser<string> AttributeText()
		{
			return
				from s in QuotedText()
				from eol in Parse.Char('\n')
				select s;
		}

		public static Parser<AttributeText> GetAttribute()
		{
			return
				from id in AttributeId()
				from data in AttributeText()
				select new AttributeText {Id = id, Text = data};
		}

		public static Parser<bool> End()
		{
			return
				from s in Parse.String("***END OF DUMP***")
				from c in Parse.Char('\n')
				select true;
		}

		public static Parser<char> BackQuote()
		{
			return
				from c1 in Parse.Char('\\')
				from c2 in Parse.Char('"')
				select '"';
		}

		public static Parser<string> QuotedText()
		{
			return
				from q1 in Parse.Char('"')
				from c in
					(
						from c1 in BackQuote().Or(Parse.CharExcept('"')).Many()
						select c1
					)
				from q2 in Parse.Char('"')
				select new string(c.ToArray());
		}

		public static Result GetResult(string input)
		{
			var p =
				from t in Tfield()
				from s in Sfield()
				from n in Nfield()
				from r in Rfield()
				from a in AttributeData().Many()
				//from o in RoughParser().Many()
				//from e in End()
				select new Result {FieldT = t, FieldS = s, FieldN = n, FieldR = r, Attributes = a.ToList()};//, GameObjects = o.ToList()};

			var st = File.ReadAllText(input);

			var tp = p.TryParse(st);
			if (tp.WasSuccessful)
			{
				string rest = tp.Remainder.Source.Substring(tp.Remainder.Position);
				var pp =
					from o in RoughParser().Many()
					select o;
				var t = pp.TryParse(rest);
				return tp.Value;
			}
			throw new Exception("Unparsable.");
		}

		public static Parser<string> Line()
		{
			return
				from c1 in Parse.Char(c => c != '>', "generic line")
				from c2 in Parse.CharExcept('\n').Many()
				from eol in Parse.Char('\n')
				let cl1 = new List<char> {c1}
				let cl2 = c2.ToArray()
				select c1 + new string(cl2);
		}

		public static Parser<object> Multiline()
		{
			return
				from l in Line().Many().Until(Parse.Char('>'))
				select l;
		}

		public static Parser<RoughGameObject> RoughParser()
		{
			return
				from id in ObjectId()
				from s in QuotedText()
				from blob in Parse.Char(c=>c!='<'&&c!='>',"blah data for now").Many()// .AnyChar.Many().Contained(Parse.Char('\n'), Parse.Char('>'))
				//from eol1 in Parse.Char('\n')
				//from extra in Multiline()
				////.Many().Until(Parse.Char('>'))
				from atts in GetAttribute().Many()
				from end in Parse.Char('<')
				from eol in Parse.Char('\n')
				select
					new RoughGameObject
					{
						Id = id,
						Name = s,//new string(s.ToArray()),
						Other = new string(blob.ToArray()),
						Atts = atts.ToList()
					};
		}

	}
	#endregion

}
