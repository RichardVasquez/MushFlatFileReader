using System;

namespace MushFlatFileReader.Construction.LegacyTypes
{
	[Flags]
	// ReSharper disable InconsistentNaming
	public enum TinyMushObjectFlags1 : long
	{
		///<summary>
		///  Can see through to the other side 
		///</summary>
		SEETHRU = 0x00000008,
		///<summary>
		///  gets automatic control 
		///</summary>
		WIZARD = 0x00000010,
		///<summary>
		///  anybody can link to this room 
		///</summary>
		LINK_OK = 0x00000020,
		///<summary>
		///  Don't show contents or presence 
		///</summary>
		DARK = 0x00000040,
		///<summary>
		///  Others may @tel here 
		///</summary>
		JUMP_OK = 0x00000080,
		///<summary>
		///  Object goes home when dropped 
		///</summary>
		STICKY = 0x00000100,
		///<summary>
		///  Others may @destroy 
		///</summary>
		DESTROY_OK = 0x00000200,
		///<summary>
		///  No killing here, or no pages 
		///</summary>
		HAVEN = 0x00000400,
		///<summary>
		///  Prevent 'feelgood' messages 
		///</summary>
		QUIET = 0x00000800,
		///<summary>
		///  object cannot perform actions 
		///</summary>
		HALT = 0x00001000,
		///<summary>
		///  Generate evaluation trace output 
		///</summary>
		TRACE = 0x00002000,
		///<summary>
		///  object is available for recycling 
		///</summary>
		GOING = 0x00004000,
		///<summary>
		///  Process ^x:action listens on obj? 
		///</summary>
		MONITOR = 0x00008000,
		///<summary>
		///  See things as nonowner/nonwizard 
		///</summary>
		MYOPIC = 0x00010000,
		///<summary>
		///  Relays ALL messages to owner 
		///</summary>
		PUPPET = 0x00020000,
		///<summary>
		///  Object may be @chowned freely 
		///</summary>
		CHOWN_OK = 0x00040000,
		///<summary>
		///  Object may be ENTERed 
		///</summary>
		ENTER_OK = 0x00080000,
		///<summary>
		///  Everyone can see properties 
		///</summary>
		VISUAL = 0x00100000,
		///<summary>
		///  Object can't be killed 
		///</summary>
		IMMORTAL = 0x00200000,
		///<summary>
		///  Load some attrs at startup 
		///</summary>
		HAS_STARTUP = 0x00400000,
		///<summary>
		///  Can't see inside 
		///</summary>
		OPAQUE = 0x00800000,
		///<summary>
		///  Tells owner everything it does. 
		///</summary>
		VERBOSE = 0x01000000,
		///<summary>
		///  Gets owner's privs. (i.e. Wiz) 
		///</summary>
		INHERIT = 0x02000000,
		///<summary>
		///  Report originator of all actions. 
		///</summary>
		NOSPOOF = 0x04000000,
		///<summary>
		///  Player is a ROBOT 
		///</summary>
		ROBOT = 0x08000000,
		///<summary>
		///  Need /override to @destroy 
		///</summary>
		SAFE = 0x10000000,
		///<summary>
		///  Sees like a wiz, but ca't modify 
		///</summary>
		ROYALTY = 0x20000000,
		///<summary>
		///  Can hear out of this obj or exit 
		///</summary>
		HEARTHRU = 0x40000000,
		///<summary>
		///  Only show room name on look 
		///</summary>
		TERSE = 0x80000000
	}
	// ReSharper restore InconsistentNaming
}