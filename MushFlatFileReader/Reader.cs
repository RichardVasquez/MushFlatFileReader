using System;
using System.Diagnostics;
using System.IO;
using MushFlatFileReader.Construction;
using MushFlatFileReader.Construction.Parsers;
using Sprache;

namespace MushFlatFileReader
{
	public static class Reader
	{
		private static bool _didRead;
		private static bool _didParse;
		private static readonly Stopwatch SwReadFile = new Stopwatch();
		private static readonly Stopwatch SwParse = new Stopwatch();
		private static string _fileContents;

		private static readonly object LockRead = new object();
		private static readonly object LockParse = new object();

		public static void Read(string file)
		{
			lock (LockRead)
			{
				Universe.Reset();
				if (_didRead)
				{
					return;
				}
				SwReadFile.Start();
				try
				{
					_fileContents = File.ReadAllText(file);
				}
				catch (Exception)
				{
					_didRead = false;
					SwReadFile.Reset();
					throw new IOException(string.Format("Error reading {0}", file));
				}
				SwReadFile.Stop();
				_didRead = true;
			}
		}

		public static void Parse(bool showId = false, bool showAttrib = false)
		{
			lock (LockParse)
			{
				if (!_didRead)
				{
					return;
				}
				if (_didParse)
				{
					return;
				}
				SwParse.Start();
				IResult<string> ph;
				try
				{
					ph = FlatFileParsers.Headers(showId, showAttrib).TryParse(_fileContents);
				}
				catch (Exception)
				{
					throw new Exception("Unable to parse text. Resetting.");
				}
				SwParse.Stop();
				if (!ph.WasSuccessful)
				{
					_didRead = false;
					_didParse = false;
					SwReadFile.Reset();
					SwParse.Reset();
					_fileContents = "";
					Universe.Reset();
					return;
				}
				_didParse = true;
			}
		}

		public static TimeSpan GetReadTime()
		{
			return SwReadFile.Elapsed;
		}

		public static TimeSpan GetParseTime()
		{
			return SwParse.Elapsed;
		}

		public static TimeSpan GetTotalTime()
		{
			return SwParse.Elapsed + SwReadFile.Elapsed;
		}
	}
}