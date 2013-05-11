namespace MushFlatFileReader.GameHeaders
{
	public sealed class HeaderSize:MushHeader
	{
		public HeaderSize(string val) : base(val)
		{
			Register();
			Original = "+S" + val;
		}

		#region Overrides of MushHeader
		protected override void Register()
		{
			Universe.RegisterHeader("Size", this);
		}
		#endregion
	}
}