using System;

namespace MushFlatFileReader.Construction.LegacyTypes
{
	[Flags]
	// ReSharper disable InconsistentNaming
	public enum TinyMushObjectFlags3 : long
	{
		///<summary>
		///  Can be victim of @redirect 
		///</summary>
		REDIR_OK = 0x00000001,
		///<summary>
		///  Victim of @redirect 
		///</summary>
		HAS_REDIRECT = 0x00000002,
		///<summary>
		///  Don't check parent chain for $cmd 
		///</summary>
		ORPHAN = 0x00000004,
		///<summary>
		///  Has a DarkLock 
		///</summary>
		HAS_DARKLOCK = 0x00000008,
		///<summary>
		///  Temporary flag: object is dirty 
		///</summary>
		DIRTY = 0x00000010,
		///<summary>
		///  Not subject to attr defaults 
		///</summary>
		NODEFAULT = 0x00000020,
		///<summary>
		///  Check presence-related locks 
		///</summary>
		PRESENCE = 0x00000040,
		///<summary>
		///  Check @speechmod attr 
		///</summary>
		HAS_SPEECHMOD = 0x00000080,
		///<summary>
		///  Internal: has Propdir attr 
		///</summary>
		HAS_PROPDIR = 0x00000100,
		///<summary>
		///  User-defined flag 0
		///</summary>
		MARK_0 = 0x00400000,
		///<summary>
		///  User-defined flag 1
		///</summary>
		MARK_1 = 0x00800000,
		///<summary>
		///  User-defined flag 2
		///</summary>
		MARK_2 = 0x01000000,
		///<summary>
		///  User-defined flag 3
		///</summary>
		MARK_3 = 0x02000000,
		///<summary>
		///  User-defined flag 4
		///</summary>
		MARK_4 = 0x04000000,
		///<summary>
		///  User-defined flag 5
		///</summary>
		MARK_5 = 0x08000000,
		///<summary>
		///  User-defined flag 6
		///</summary>
		MARK_6 = 0x10000000,
		///<summary>
		///  User-defined flag 7
		///</summary>
		MARK_7 = 0x20000000,
		///<summary>
		///  User-defined flag 8
		///</summary>
		MARK_8 = 0x40000000,
		///<summary>
		///  User-defined flag 9
		///</summary>
		MARK_9 = 0x80000000,
		///<summary>
		///  Bitwise-or of all marker flags 
		///</summary>
		MARK_FLAGS = 0xffc00000
	}
	// ReSharper restore InconsistentNaming
}