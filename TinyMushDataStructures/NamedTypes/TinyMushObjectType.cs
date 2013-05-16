using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TinyMushDataStructures.NamedTypes
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum TinyMushObjectType:long
	{
		Room = 0,
		Thing = 1,
		Exit = 2,
		Player = 3,
		Zone = 4,
		Garbage = 5
	}
}