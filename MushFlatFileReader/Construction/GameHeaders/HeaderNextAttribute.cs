namespace MushFlatFileReader.Construction.GameHeaders
{
	public sealed class HeaderNextAttribute:MushHeader
	{
		public HeaderNextAttribute(string val) : base(val)
		{
			Register();
			Original = "+N" + val;
		}

		#region Overrides of MushHeader
		protected override void Register()
		{
			Universe.RegisterHeader("NextAttribute", this);
		}
		#endregion
	}
}