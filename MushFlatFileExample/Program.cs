using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using MushFlatFileReader;
using Newtonsoft.Json;
using TinyMushDataStructures.GameObject;

namespace MushFlatFileExample
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				Reader.Read("flatfile.txt");
				Reader.Parse();
			}
			catch (Exception e)
			{
				Console.WriteLine("Problem:");
				Console.WriteLine(e.Message);
				Console.ReadLine();
				Environment.Exit(1);
			}
			if (!Universe.HasData)
			{
				Console.WriteLine("Problem: No data in universe after reading and parsing file.");
				Console.ReadLine();
				Environment.Exit(1);
			}

			Stopwatch sw1 = new Stopwatch();
			var keys = Universe.Entries.Keys;
			List<TinyMushObject> gameObjects =
				keys
					.Select(TinyMushObjectFactory.Get)
					.Where(tmo => tmo != null).ToList();
			sw1.Stop();

			Stopwatch sw2 = new Stopwatch();
			sw2.Start();
			string json = JsonConvert.SerializeObject(gameObjects, Formatting.Indented);
			sw2.Stop();

			using (StreamWriter str = new StreamWriter("tinymush.json"))
			{
				str.Write(json);
				str.Close();
			}

			Console.WriteLine("Read file in:         {0}", Reader.GetReadTime());
			Console.WriteLine("Parsed file in:       {0}", Reader.GetParseTime());
			Console.WriteLine("Total time:           {0}", Reader.GetTotalTime());
			Console.WriteLine();
			Console.WriteLine("Extracted objects in: {1} ({0} objects total)", gameObjects.Count, sw1.Elapsed);
			Console.WriteLine("Serialized file in:   {0}", sw2.Elapsed);
			Console.WriteLine();
			Console.WriteLine("Press a key to finish.");
			Console.ReadKey(true);
		}
	}
}
