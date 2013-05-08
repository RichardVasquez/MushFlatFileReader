using System;

namespace MushFlatFileReader
{
	[Flags]
	// ReSharper disable InconsistentNaming
	public enum AttributeFlags1 : long
	{
		///<summary>
		///  players other than owner can't see it 
		///</summary>
		ODARK = 0x00000001,
		///<summary>
		///  No one can see it 
		///</summary>
		DARK = 0x00000002,
		///<summary>
		///  only wizards can change it 
		///</summary>
		WIZARD = 0x00000004,
		///<summary>
		///  Only wizards can see it. Dark to mortals 
		///</summary>
		MDARK = 0x00000008,
		///<summary>
		///  Don't show even to #1 
		///</summary>
		INTERNAL = 0x00000010,
		///<summary>
		///  Don't create a @ command for it 
		///</summary>
		NOCMD = 0x00000020,
		///<summary>
		///  Attribute is locked 
		///</summary>
		LOCK = 0x00000040,
		///<summary>
		///  Attribute should be ignored 
		///</summary>
		DELETED = 0x00000080,
		///<summary>
		///  Don't process $-commands from this attr 
		///</summary>
		NOPROG = 0x00000100,
		///<summary>
		///  Only #1 can change it 
		///</summary>
		GOD = 0x00000200,
		///<summary>
		///  Attribute is a lock 
		///</summary>
		IS_LOCK = 0x00000400,
		///<summary>
		///  Anyone can see 
		///</summary>
		VISUAL = 0x00000800,
		///<summary>
		///  Not inherited by children 
		///</summary>
		PRIVATE = 0x00001000,
		///<summary>
		///  Don't HTML escape this in did_it() 
		///</summary>
		HTML = 0x00002000,
		///<summary>
		///  Don't evaluate when checking for $-cmds 
		///</summary>
		NOPARSE = 0x00004000,
		///<summary>
		///  Do a regexp rather than wildcard match 
		///</summary>
		REGEXP = 0x00008000,
		///<summary>
		///  Don't copy this attr when cloning. 
		///</summary>
		NOCLONE = 0x00010000,
		///<summary>
		///  No one can change it (set by server) 
		///</summary>
		CONST = 0x00020000,
		///<summary>
		///  Regexp matches are case-sensitive 
		///</summary>
		CASE = 0x00040000,
		///<summary>
		///  Attribute contains a structure 
		///</summary>
		STRUCTURE = 0x00080000,
		///<summary>
		///  Attribute number has been modified 
		///</summary>
		DIRTY = 0x00100000,
		///<summary>
		///  did_it() checks attr_defaults obj 
		///</summary>
		DEFAULT = 0x00200000,
		///<summary>
		///  If used as oattr, no name prepend 
		///</summary>
		NONAME = 0x00400000,
		///<summary>
		///  Set the result of match into regs 
		///</summary>
		RMATCH = 0x00800000,
		///<summary>
		///  execute match immediately 
		///</summary>
		NOW = 0x01000000,
		///<summary>
		///  trace ufunction 
		///</summary>
		TRACE = 0x02000000
	}
	// ReSharper restore InconsistentNaming
}