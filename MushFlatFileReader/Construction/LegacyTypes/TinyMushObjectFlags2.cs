using System;

namespace MushFlatFileReader.Construction.LegacyTypes
{
	[Flags]
	// ReSharper disable InconsistentNaming
	public enum TinyMushObjectFlags2 : long
	{
		///<summary>
		///  No puppets 
		///</summary>
		KEY = 0x00000001,
		///<summary>
		///  May @set home here 
		///</summary>
		ABODE = 0x00000002,
		///<summary>
		///  -- Legacy -- 
		///</summary>
		FLOATING = 0x00000004,
		///<summary>
		///  Can't loc() from afar 
		///</summary>
		UNFINDABLE = 0x00000008,
		///<summary>
		///  Others may @parent to me 
		///</summary>
		PARENT_OK = 0x00000010,
		///<summary>
		///  Visible in dark places 
		///</summary>
		LIGHT = 0x00000020,
		///<summary>
		///  Internal: LISTEN attr set 
		///</summary>
		HAS_LISTEN = 0x00000040,
		///<summary>
		///  Internal: FORWARDLIST attr set 
		///</summary>
		HAS_FWDLIST = 0x00000080,
		///<summary>
		///  Should we check the SpeechLock? 
		///</summary>
		AUDITORIUM = 0x00000100,
		ANSI = 0x00000200,
		HEAD_FLAG = 0x00000400,
		FIXED = 0x00000800,
		UNINSPECTED = 0x00001000,
		///<summary>
		///  Check as local master room 
		///</summary>
		ZONE_PARENT = 0x00002000,
		DYNAMIC = 0x00004000,
		NOBLEED = 0x00008000,
		STAFF = 0x00010000,
		HAS_DAILY = 0x00020000,
		GAGGED = 0x00040000,
		///<summary>
		///  Check it for $commands 
		///</summary>
		HAS_COMMANDS = 0x00080000,
		///<summary>
		///  Stop matching commands if found 
		///</summary>
		STOP_MATCH = 0x00100000,
		///<summary>
		///  Forward messages to contents 
		///</summary>
		BOUNCE = 0x00200000,
		///<summary>
		///  ControlLk specifies who ctrls me 
		///</summary>
		CONTROL_OK = 0x00400000,
		///<summary>
		///  Can't set attrs on this object 
		///</summary>
		CONSTANT_ATTRS = 0x00800000,
		VACATION = 0x01000000,
		///<summary>
		///  Mail message in progress 
		///</summary>
		PLAYER_MAILS = 0x02000000,
		///<summary>
		///  Player supports HTML 
		///</summary>
		HTML = 0x04000000,
		///<summary>
		///  Suppress has arrived / left msgs 
		///</summary>
		BLIND = 0x08000000,
		///<summary>
		///  Report some activities to wizards 
		///</summary>
		SUSPECT = 0x10000000,
		///<summary>
		///  Watch logins 
		///</summary>
		WATCHER = 0x20000000,
		///<summary>
		///  Player is connected 
		///</summary>
		CONNECTED = 0x40000000,
		///<summary>
		///  Disallow most commands 
		///</summary>
		SLAVE = 0x80000000
	}
	// ReSharper restore InconsistentNaming
}