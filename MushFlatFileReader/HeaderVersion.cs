namespace MushFlatFileReader
{
	public sealed class HeaderVersion:MushHeader
	{
		public GameType GameFormat { get; private set; }
		private readonly char _inputChar;
		private readonly string _inputNumber;
		public string Header { get { return "+"+ _inputChar + _inputNumber; } }

		public long ReadZone { get; private set; }
		public long ReadLink { get; private set; }
		public long ReadKey { get; private set; }
		public long ReadParent { get; private set; }
		public long ReadMoney { get; private set; }
		public long ReadExtFlags { get; private set; }
		public long HasTypedQuotas { get; private set; }
		public long ReadTimeStamps { get; private set; }
		public long HasVisualAttributes { get; private set; }
		public long GameFlags { get; private set; }
		public long ReadGameFlags3Words { get; private set; }
		public long ReadPowers { get; private set; }
		public long ReadNewStrings { get; private set; }
		public long GameVersion { get; private set; }
		public bool ReadAttributes { get; private set; }
		public bool ReadName { get; private set; }
		public bool DeduceVersion { get; private set; }
		public bool DeduceZone { get; private set; }
		public bool DeduceName { get; private set; }

		public HeaderVersion(string val, char c) : base(val)
		{
			SetVersion(c);
			_inputChar = c;
			_inputNumber = val;
			SetBasics();
			SetSpecifics();
			Register();
			Original = "+" + c + val;
			ReadName = true;
			ReadAttributes = true;
			DeduceVersion = true;
			DeduceZone = true;
			DeduceName = true;
		}

		private void SetVersion(char c)
		{
			switch (c)
			{
				case 'T':
					GameFormat=GameType.TinyMush;
					DeduceVersion = false;
					DeduceZone = false;
					DeduceName = false;
					break;
				case 'V':
					GameFormat = GameType.Mush;
					DeduceVersion = false;
					DeduceZone = false;
					DeduceName = false;
					break;
				case 'X':
					GameFormat = GameType.Mux;
					DeduceVersion = false;
					DeduceZone = false;
					DeduceName = false;
					break;
				default:
					GameFormat = GameType.Unknown;
					break;
			}
		}

		private void SetBasics()
		{
			if (( Number & (long) DatabaseVersionFlags.Gdbm ) != 0)
			{
				ReadAttributes = false;
				ReadName = ( Number & (long) DatabaseVersionFlags.AtrName ) == 0;
			}

			ReadZone = Number & (long)DatabaseVersionFlags.Zone;
			ReadLink = Number & (long) DatabaseVersionFlags.Link;
			ReadKey = Number & (long) DatabaseVersionFlags.AtrKey;
			ReadParent = Number & (long) DatabaseVersionFlags.Parent;
			ReadMoney = Number & (long) DatabaseVersionFlags.AtrMoney;
			ReadExtFlags = Number & (long) DatabaseVersionFlags.Xflags;
			HasTypedQuotas = Number & (long) DatabaseVersionFlags.Tquotas;
			ReadTimeStamps = Number & (long) DatabaseVersionFlags.Timestamps;
			HasVisualAttributes = Number & (long) DatabaseVersionFlags.VisualAttrs;
			GameFlags = Number & (long) ~DatabaseVersionFlags.Mask;
			GameVersion = Number & (long) DatabaseVersionFlags.Mask;
		}

		private void SetSpecifics()
		{
			ReadGameFlags3Words = 0;
			ReadPowers = 0;
			ReadNewStrings = 0;
			switch (GameFormat)
			{
				case GameType.Mux:
				case GameType.TinyMush:
					ReadGameFlags3Words = Number & (long) DatabaseVersionFlags.Flags3;
					ReadPowers = Number & (long)DatabaseVersionFlags.Powers;
					ReadNewStrings = Number & (long)DatabaseVersionFlags.Quoted;
					break;
			}
		}

		#region Overrides of MushHeader
		protected override void Register()
		{
			Universe.RegisterHeader("GameFormat", this);
		}
		#endregion
	}
}