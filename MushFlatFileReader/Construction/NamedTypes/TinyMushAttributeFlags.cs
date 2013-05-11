using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MushFlatFileReader.Construction.NamedTypes
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum TinyMushAttributeFlags
	{
		///<summary>
		///  players other than owner can't see it 
		///</summary>
		Odark,
		///<summary>
		///  No one can see it 
		///</summary>
		Dark,
		///<summary>
		///  only wizards can change it 
		///</summary>
		Wizard,
		///<summary>
		///  Only wizards can see it. Dark to mortals 
		///</summary>
		Mdark,
		///<summary>
		///  Don't show even to #1 
		///</summary>
		Internal,
		///<summary>
		///  Don't create a @ command for it 
		///</summary>
		NoCmd,
		///<summary>
		///  Attribute is locked 
		///</summary>
		Lock,
		///<summary>
		///  Attribute should be ignored 
		///</summary>
		Deleted,
		///<summary>
		///  Don't process $-commands from this attr 
		///</summary>
		NoProg,
		///<summary>
		///  Only #1 can change it 
		///</summary>
		God,
		///<summary>
		///  Attribute is a lock 
		///</summary>
		IsLock,
		///<summary>
		///  Anyone can see 
		///</summary>
		Visual,
		///<summary>
		///  Not inherited by children 
		///</summary>
		Private,
		///<summary>
		///  Don't HTML escape this in did_it() 
		///</summary>
		Html,
		///<summary>
		///  Don't evaluate when checking for $-cmds 
		///</summary>
		NoParse,
		///<summary>
		///  Do a regexp rather than wildcard match 
		///</summary>
		Regexp,
		///<summary>
		///  Don't copy this attr when cloning. 
		///</summary>
		NoClone,
		///<summary>
		///  No one can change it (set by server) 
		///</summary>
		Const,
		///<summary>
		///  Regexp matches are case-sensitive 
		///</summary>
		Case,
		///<summary>
		///  Attribute contains a structure 
		///</summary>
		Structure,
		///<summary>
		///  Attribute number has been modified 
		///</summary>
		Dirty,
		///<summary>
		///  did_it() checks attr_defaults obj 
		///</summary>
		Default,
		///<summary>
		///  If used as oattr, no name prepend 
		///</summary>
		NoName,
		///<summary>
		///  Set the result of match into regs 
		///</summary>
		Rmatch,
		///<summary>
		///  execute match immediately 
		///</summary>
		Now,
		///<summary>
		///  trace ufunction 
		///</summary>
		Trace
	}
	// ReSharper restore InconsistentNaming
}