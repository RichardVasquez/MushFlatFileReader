using System;

namespace MushFlatFileReader
{
	[Flags]
	// ReSharper disable InconsistentNaming
	public enum TinyMushPowers2 : long
	{
		///<summary>
		///	Can build 
		///</summary>
		BUILDER = 0x00000001,
		///<summary>
		///	Can link an exit to "variable" 
		///</summary>
		LINKVAR = 0x00000002,
		///<summary>
		///	Can link to any object 
		///</summary>
		LINKTOANY = 0x00000004,
		///<summary>
		///	Can open from anywhere 
		///</summary>
		OPENANYLOC = 0x00000008,
		///<summary>
		///	Can use SQL queries directly 
		///</summary>
		USE_SQL = 0x00000010,
		///<summary>
		///	Can link object to any home 
		///</summary>
		LINKHOME = 0x00000020,
		///<summary>
		///	Can vanish from sight via DARK 
		///</summary>
		CLOAK = 0x00000040
	}
	// ReSharper restore InconsistentNaming
}