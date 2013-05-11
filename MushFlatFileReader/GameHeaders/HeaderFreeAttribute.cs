namespace MushFlatFileReader.GameHeaders
{
	public sealed class HeaderFreeAttribute:MushHeader
	{
		public HeaderFreeAttribute(string val) : base(val)
		{
			Register();
			Original = "+F" + val;
		}

		#region Overrides of MushHeader
		protected override void Register()
		{
			Universe.RegisterHeader("NextFreeAttribute", this);
		}
		#endregion
	}
}