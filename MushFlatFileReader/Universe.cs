using System;
using System.Collections.Generic;
using MushFlatFileReader.Construction.GameHeaders;
using MushFlatFileReader.Construction.NamedTypes;

namespace MushFlatFileReader
{
	/// <summary>
	/// This is a big repository for later collation by the <see cref="TinyMushObjectFactory"/>
	/// </summary>
	public static class Universe
	{
		public static Dictionary<string, IMushHeader> Headers = new Dictionary<string, IMushHeader>();
		public static Dictionary<long,HeaderAttribute> Attributes = new Dictionary<long, HeaderAttribute>();

		public static Dictionary<long, MushEntry> Entries = new Dictionary<long, MushEntry>();

		public static bool HeaderGotten { get { return Headers.ContainsKey("GameFormat"); } }
		public static bool MyDebug = false;

		public static bool HasData
		{
			get { return Headers.Count > 0 && Attributes.Count > 0 && Entries.Count > 0; }
		}

		public static void Reset()
		{
			Headers = new Dictionary<string, IMushHeader>();
			Attributes = new Dictionary<long, HeaderAttribute>();
			Entries = new Dictionary<long, MushEntry>();
			MyDebug = false;
		}

		public static bool ReadNewStrings
		{
			get
			{
				if (!HeaderGotten)
				{
					return false;
				}

				var h = Headers[ "GameFormat" ] as HeaderVersion;
				if (h == null)
				{
					return false;
				}

				return h.ReadNewStrings != 0;
			}
		}

		public static void RegisterHeader(string name, IMushHeader header)
		{
			if (Headers.ContainsKey(name))
			{
				Console.WriteLine("Duplicate header: {0}", header.GetType().Name);
			}
			Headers[ name ] = header;
		}

		public static void RegisterAttribute(HeaderAttribute ha)
		{
			if (ha == null)
			{
				return;
			}
			if (ha.Number >= 0 && !Attributes.ContainsKey(ha.Number))
			{
				Attributes[ ha.Number ] = ha;
			}
		}

		public static bool ReadName
		{
			get
			{
				if (!Headers.ContainsKey("GameFormat"))
				{
					return false;
				}

				var gf = Headers["GameFormat"] as HeaderVersion;
				return gf != null && gf.ReadName;
			}
		}

		public static bool ReadAttributes
		{
			get
			{
				if (!Headers.ContainsKey("GameFormat"))
				{
					return false;
				}

				var gf = Headers["GameFormat"] as HeaderVersion;
				return gf != null && gf.ReadAttributes;
			}
		}

		private static bool? _deduceVersion;
		public static bool DeduceVersion
		{
			get
			{
				if (_deduceVersion.HasValue)
				{
					return _deduceVersion.Value;
				}
				if (!Headers.ContainsKey("GameFormat"))
				{
					return true;
				}

				var gf = Headers["GameFormat"] as HeaderVersion;
				return gf == null || gf.DeduceVersion;
			}
			set { _deduceVersion = value; }
		}

		private static bool? _deduceZone;
		public static bool DeduceZone
		{
			get
			{
				if (_deduceZone.HasValue)
				{
					return _deduceZone.Value;
				}
				if (!Headers.ContainsKey("GameFormat"))
				{
					return true;
				}

				var gf = Headers["GameFormat"] as HeaderVersion;
				return gf == null || gf.DeduceZone;
			}
			set { _deduceZone = value; }
		}

		private static bool? _readZone;
		public static bool ReadZone
		{
			get
			{
				if (_readZone.HasValue)
				{
					return _readZone.Value;
				}
				if (!Headers.ContainsKey("GameFormat"))
				{
					return true;
				}

				var gf = Headers["GameFormat"] as HeaderVersion;
				return gf == null || gf.ReadZone != 0;
			}
			set { _readZone = value; }
			
		}

		private static bool? _deduceName;
		public static bool DeduceName
		{
			get
			{
				if (_deduceName.HasValue)
				{
					return _deduceName.Value;
				}
				if (!Headers.ContainsKey("GameFormat"))
				{
					return true;
				}

				var gf = Headers["GameFormat"] as HeaderVersion;
				return gf == null || gf.DeduceName;
			}
			set { _deduceName = value; }
			
		}

		private static GameType? _gameType;
		public static GameType GameFormat
		{
			get
			{
				if (_gameType.HasValue)
				{
					return _gameType.Value;
				}
				if (!Headers.ContainsKey("GameFormat"))
				{
					return GameType.Unknown;
				}

				var gf = Headers["GameFormat"] as HeaderVersion;
				return gf == null ? GameType.Unknown : gf.GameFormat;
			}
			set { _gameType = value; }
		}

		private static long? _gameVersion;
		public static long GameVersion
		{
			get
			{
				if (_gameVersion.HasValue)
				{
					return _gameVersion.Value;
				}
				if (!Headers.ContainsKey("GameFormat"))
				{
					return 0;
				}

				var gf = Headers["GameFormat"] as HeaderVersion;
				return gf == null ? 0 : gf.GameVersion;
			}
			set { _gameVersion = value; }
		}

		private static bool? _readLink;
		public static bool ReadLink
		{
			get
			{
				if (_readLink.HasValue)
				{
					return _readLink.Value;
				}
				if (!Headers.ContainsKey("GameFormat"))
				{
					return false;
				}

				var gf = Headers["GameFormat"] as HeaderVersion;
				if (gf == null)
				{
					return false;
				}
				return gf.ReadLink != 0;
			}
			set { _readLink = value; }
		}

		private static bool? _readTimeStamps;
		public static bool ReadTimeStamps
		{
			get
			{
				if (_readTimeStamps.HasValue)
				{
					return _readTimeStamps.Value;
				}
				if (!Headers.ContainsKey("GameFormat"))
				{
					return false;
				}

				var gf = Headers["GameFormat"] as HeaderVersion;
				if (gf == null)
				{
					return false;
				}
				return gf.ReadTimeStamps != 0;
			}
			set { _readTimeStamps = value; }
		}

		private static bool? _readPowers;
		public static bool ReadPowers
		{
			get
			{
				if (_readPowers.HasValue)
				{
					return _readPowers.Value;
				}
				if (!Headers.ContainsKey("GameFormat"))
				{
					return false;
				}

				var gf = Headers["GameFormat"] as HeaderVersion;
				if (gf == null)
				{
					return false;
				}
				return gf.ReadPowers != 0;
			}
			set { _readPowers = value; }
		}

		public static bool TimeChecking = true;

		public static void RegisterEntry(MushEntry mushEntry)
		{
			if (mushEntry == null || mushEntry.Number < 0)
			{
				return;
			}
			if (Entries.ContainsKey(mushEntry.Number))
			{
				return;
			}
			Entries[ mushEntry.Number ] = mushEntry;
		}
	}
}