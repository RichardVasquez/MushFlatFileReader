using System;
using System.Linq;
using Sprache;

namespace MushFlatFileReader.Construction.Parsers
{
	public static class ObjectDataParsers
	{
		public static Parser<Tuple<string, string, string>> AttributeParser()
		{
			return
				(
					from min in Parse.Char('-').Optional()
					from n1 in Parse.Number
					from c1 in Parse.Char(':')
					from n2 in Parse.Number
					from c2 in Parse.Char(':')
					from s in Parse.AnyChar.Many()
					select new Tuple<string, string, string>(n1, n2, new string(s.ToArray()))
				)
					.Or
					(
					 from min in Parse.Char('-').Optional()
					 from n1 in Parse.Number
					 from c1 in Parse.Char(':')
					 from s in Parse.AnyChar.Many()
					 select new Tuple<string, string, string>(n1, "", new string(s.ToArray()))
					)
					.Or
					(
					 from s in Parse.AnyChar.Many()
					 select new Tuple<string, string, string>("", "", new string(s.ToArray()))
					);
		}

		public static Parser<string> StripAnsi()
		{
			return
				from s in Letter().Many().End()
				select string.Concat(s);
		}

		public static Parser<string> Letter()
		{
			return
				(
					from c1 in Parse.Char(c => c >= ' ' && c <= (char)0x7f, "ASCII only")
					select new string(c1, 1)
				)
					.Or
					(
					 from a1 in StripAnsiColor()
					 select a1
					);
		}

		public static Parser<string> StripAnsiColor()
		{
			return
				from c in Parse.Char((char)27)
				from b in Parse.Char('[')
				from n1 in Parse.Number
				from n in
					(
						from c1 in Parse.Char(';')
						from n2 in Parse.Number
						select ""
					).Many().Optional()
				from m in Parse.Char('m')
				select "";
		}

	}
}