namespace MushFlatFileReader.Construction.GameHeaders
{
	public sealed class HeaderRecordPlayers:MushHeader
	{
		public HeaderRecordPlayers(string val) : base(val)
		{
			Register();
			Original = "-R" + val;
		}

		#region Overrides of MushHeader
		protected override void Register()
		{
			Universe.RegisterHeader("RecordPlayers", this);
		}
		#endregion
	}
}