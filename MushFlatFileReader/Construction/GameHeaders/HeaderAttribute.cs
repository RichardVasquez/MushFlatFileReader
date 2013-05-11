using System.Diagnostics;
using MushFlatFileReader.Construction;

namespace MushFlatFileReader.GameHeaders
{
	[DebuggerDisplay("{Number}:{Text}")]
	public sealed class HeaderAttribute:MushHeader
	{
		public string Text { get; private set; }

		public HeaderAttribute(string number, string text) : base(number)
		{
			Text = text;
			Register();
		}

		#region Overrides of MushHeader
		protected override void Register()
		{
			if (Number >= 0)
			{
				Universe.RegisterAttribute(this);
			}
		}
		#endregion
	}
}