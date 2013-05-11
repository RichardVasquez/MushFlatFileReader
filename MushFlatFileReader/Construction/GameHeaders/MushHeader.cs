using System;
using System.Diagnostics;

namespace MushFlatFileReader.Construction.GameHeaders
{
	[DebuggerDisplay("{Original}")]
	public abstract class MushHeader:IMushHeader
	{
		protected MushHeader(string val)
		{
			Parse(val);
		}

		public virtual string Original { get; protected set; }

		private void Parse(string val)
		{
			long l;
			try
			{
				if (!long.TryParse(val, out l))
				{
					l = -1;
				}
			}
			catch (Exception)
			{
				l = -1;
			}
			Number = l;
		}

		protected abstract void Register();

		#region Implementation of IMushHeader
		public long Number { get; private set; }
		#endregion
	}
}