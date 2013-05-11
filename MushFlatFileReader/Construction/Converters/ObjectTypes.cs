using MushFlatFileReader.Construction.GameHeaders;
using MushFlatFileReader.Construction.NamedTypes;

namespace MushFlatFileReader.Construction.Converters
{
	public static class ObjectTypes
	{
		public static TinyMushObjectType MatchType(MushEntry me)
		{
			long type = me.Flags1 & 7;

			switch (type)
			{
				case 0:
					return TinyMushObjectType.Room;
				case 1:
					return TinyMushObjectType.Thing;
				case 2:
					return TinyMushObjectType.Exit;
				case 3:
					return TinyMushObjectType.Player;
				case 4:
					return TinyMushObjectType.Zone;
				default:
					return TinyMushObjectType.Garbage;
			}
		}
	}
}