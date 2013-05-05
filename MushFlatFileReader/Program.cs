using System.IO;
using Sprache;

namespace MushFlatFileReader
{
	public class Program
	{
		private static void Main(string[] args)
		{
			string text = File.ReadAllText("flatfile.txt");
			var ph = ParserBox.Headers().TryParse(text);
		}
	}
}