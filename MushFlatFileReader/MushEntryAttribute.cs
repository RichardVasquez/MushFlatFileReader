namespace MushFlatFileReader
{
	public class MushEntryAttribute
	{
		public long Id;
		public string Text;

		public MushEntryAttribute(long parse, string s)
		{
			Id = parse;
			Text = s;
		}
	}
}