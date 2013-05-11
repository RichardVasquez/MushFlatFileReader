using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using MushFlatFileReader.Construction;
using MushFlatFileReader.Construction.GameObject;
using MushFlatFileReader.Construction.Parsers;
using Newtonsoft.Json;
using Sprache;

namespace MushFlatFileReader
{
	public class Program
	{
		private static void Main(string[] args)
		{
			string text = File.ReadAllText("flatfile.txt");
			Stopwatch sw1 = new Stopwatch();
			Stopwatch sw2 = new Stopwatch();
			Stopwatch sw3 = new Stopwatch();
			sw1.Start();
			var ph = FlatFileParsers.Headers().TryParse(text);
			sw2.Start();
			var keys = Universe.Entries.Keys;
			List<TinyMushObject> gameObjects = keys.Select(TinyMushObjectFactory.Get).ToList();
			//List<TinyMushObject> gameObjects = keys.Select(Universe.GetObject).ToList();
			sw1.Stop();
			sw2.Stop();
			sw3.Start();
			string json = JsonConvert.SerializeObject(gameObjects, Formatting.Indented);
			sw3.Stop();
			using (StreamWriter str = new StreamWriter("tinymush.json"))
			{
				str.Write(json);
				str.Close();
			}

			Console.WriteLine("Parsed file in: {0}.", sw1.Elapsed);
			Console.WriteLine("Extracted {0} objects in: {1}.", gameObjects.Count, sw2.Elapsed);
			Console.WriteLine("Serialized file in: {0}.", sw3.Elapsed);
			Console.ReadLine();
		}
	}
}