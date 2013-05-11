namespace MushFlatFileReader.Construction.GameHeaders
{
	public sealed class HeaderEnding:MushHeader
	{
		public HeaderEnding(string val) : base(val)
		{
			Original = "*** END ***";
		}

		#region Overrides of MushHeader
		protected override void Register()
		{
		}
		#endregion
	}
}