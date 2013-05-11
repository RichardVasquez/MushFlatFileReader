using System;

namespace MushFlatFileReader.Construction.LegacyTypes
{
	[Flags]
	// ReSharper disable InconsistentNaming
	public enum TinyMushPowers1 : long
	{
		///<summary>
		///	May change and see quotas 
		///</summary>
		CHG_QUOTAS = 0x00000001,
		///<summary>
		///	Can @chown anything or to anyone 
		///</summary>
		CHOWN_ANY = 0x00000002,
		///<summary>
		///	May use @wall 
		///</summary>
		ANNOUNCE = 0x00000004,
		///<summary>
		///	May use @boot 
		///</summary>
		BOOT = 0x00000008,
		///<summary>
		///	May @halt on other's objects 
		///</summary>
		HALT = 0x00000010,
		///<summary>
		///	I control everything 
		///</summary>
		CONTROL_ALL = 0x00000020,
		///<summary>
		///	See extra WHO information 
		///</summary>
		WIZARD_WHO = 0x00000040,
		///<summary>
		///	I can examine everything 
		///</summary>
		EXAM_ALL = 0x00000080,
		///<summary>
		///	Can find unfindable players 
		///</summary>
		FIND_UNFIND = 0x00000100,
		///<summary>
		///	I have infinite money 
		///</summary>
		FREE_MONEY = 0x00000200,
		///<summary>
		///	I have infinite quota 
		///</summary>
		FREE_QUOTA = 0x00000400,
		///<summary>
		///	Can set themselves DARK 
		///</summary>
		HIDE = 0x00000800,
		///<summary>
		///	No idle limit 
		///</summary>
		IDLE = 0x00001000,
		///<summary>
		///	Can @search anyone 
		///</summary>
		SEARCH = 0x00002000,
		///<summary>
		///	Can get/whisper/etc from a distance 
		///</summary>
		LONGFINGERS = 0x00004000,
		///<summary>
		///	Can use the @prog command 
		///</summary>
		PROG = 0x00008000,
		///<summary>
		///	Can read AF_MDARK attrs 
		///</summary>
		MDARK_ATTR = 0x00010000,
		///<summary>
		///	Can write AF_WIZARD attrs 
		///</summary>,
		WIZ_ATTR = 0x00020000,
		///<summary>
		///	Channel wiz 
		///</summary>
		COMM_ALL = 0x00080000,
		///<summary>
		///	Player can see the entire queue 
		///</summary>
		SEE_QUEUE = 0x00100000,
		///<summary>
		///	Player can see hidden players on WHO list 
		///</summary>
		SEE_HIDDEN = 0x00200000,
		///<summary>
		///	Player can set or clear WATCHER 
		///</summary>
		WATCH = 0x00400000,
		///<summary>
		///	Player can set the doing poll 
		///</summary>
		POLL = 0x00800000,
		///<summary>
		///	Cannot be destroyed 
		///</summary>
		NO_DESTROY = 0x01000000,
		///<summary>
		///	Player is a guest 
		///</summary>
		GUEST = 0x02000000,
		///<summary>
		///	Player can pass any lock 
		///</summary>
		PASS_LOCKS = 0x04000000,
		///<summary>
		///	Can @stat anyone 
		///</summary>
		STAT_ANY = 0x08000000,
		///<summary>
		///	Can give negative money 
		///</summary>
		STEAL = 0x10000000,
		///<summary>
		///	Teleport anywhere 
		///</summary>
		TEL_ANYWHR = 0x20000000,
		///<summary>
		///	Teleport anything 
		///</summary>
		TEL_UNRST = 0x40000000,
		///<summary>
		///	Can't be killed 
		///</summary>,
		UNKILLABLE = 0x80000000
	}
	// ReSharper restore InconsistentNaming
}