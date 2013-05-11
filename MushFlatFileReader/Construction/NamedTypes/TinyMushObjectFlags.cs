using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MushFlatFileReader.NamedTypes
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum TinyMushObjectFlags
	{
		/// <summary>
		///  Can see through to the other side 
		///</summary>
		SeeThru,
		///<summary>
		///  gets automatic control 
		///</summary>
		Wizard,
		///<summary>
		///  anybody can link to this room 
		///</summary>
		LinkOk,
		///<summary>
		///  Don't show contents or presence 
		///</summary>
		Dark,
		///<summary>
		///  Others may @tel here 
		///</summary>
		JumpOk,
		///<summary>
		///  Object goes home when dropped 
		///</summary>
		Sticky,
		///<summary>
		///  Others may @destroy 
		///</summary>
		DestroyOk,
		///<summary>
		///  No killing here, or no pages 
		///</summary>
		Haven,
		///<summary>
		///  Prevent 'feelgood' messages 
		///</summary>
		Quiet,
		///<summary>
		///  object cannot perform actions 
		///</summary>
		Halt,
		///<summary>
		///  Generate evaluation trace output 
		///</summary>
		Trace,
		///<summary>
		///  object is available for recycling 
		///</summary>
		Going,
		///<summary>
		///  Process ^x:action listens on obj? 
		///</summary>
		Monitor,
		///<summary>
		///  See things as nonowner/nonwizard 
		///</summary>
		Myopic,
		///<summary>
		///  Relays ALL messages to owner 
		///</summary>
		Puppet,
		///<summary>
		///  Object may be @chowned freely 
		///</summary>
		ChownOk,
		///<summary>
		///  Object may be ENTERed 
		///</summary>
		EnterOk,
		///<summary>
		///  Everyone can see properties 
		///</summary>
		Visual,
		///<summary>
		///  Object can't be killed 
		///</summary>
		Immortal,
		///<summary>
		///  Load some attrs at startup 
		///</summary>
		HasStartup,
		///<summary>
		///  Can't see inside 
		///</summary>
		Opaque,
		///<summary>
		///  Tells owner everything it does. 
		///</summary>
		Verbose,
		///<summary>
		///  Gets owner's privs. (i.e. Wiz) 
		///</summary>
		Inherit,
		///<summary>
		///  Report originator of all actions. 
		///</summary>
		NoSpoof,
		///<summary>
		///  Player is a ROBOT 
		///</summary>
		Robot,
		///<summary>
		///  Need /override to @destroy 
		///</summary>
		Safe,
		///<summary>
		///  Sees like a wiz, but ca't modify 
		///</summary>
		Royalty,
		///<summary>
		///  Can hear out of this obj or exit 
		///</summary>
		HearThru,
		///<summary>
		///  Only show room name on look 
		///</summary>
		Terse,
		///<summary>
		///  No puppets 
		///</summary>
		Key,
		///<summary>
		///  May @set home here 
		///</summary>
		Abode,
		///<summary>
		///  -- Legacy -- 
		///</summary>
		Floating,
		///<summary>
		///  Can't loc() from afar 
		///</summary>
		Unfindable,
		///<summary>
		///  Others may @parent to me 
		///</summary>
		ParentOk,
		///<summary>
		///  Visible in dark places 
		///</summary>
		Light,
		///<summary>
		///  Internal: LISTEN attr set 
		///</summary>
		HasListen,
		///<summary>
		///  Internal: FORWARDLIST attr set 
		///</summary>
		HasFwdList,
		///<summary>
		///  Should we check the SpeechLock? 
		///</summary>
		Auditorium,
		Ansi,
		HeadFlag,
		Fixed,
		Uninspected,
		///<summary>
		///  Check as local master room 
		///</summary>
		ZoneParent,
		Dynamic,
		Nobleed,
		Staff,
		HasDaily,
		Gagged,
		///<summary>
		///  Check it for $commands 
		///</summary>
		HasCommands,
		///<summary>
		///  Stop matching commands if found 
		///</summary>
		StopMatch,
		///<summary>
		///  Forward messages to contents 
		///</summary>
		Bounce,
		///<summary>
		///  ControlLk specifies who ctrls me 
		///</summary>
		ControlOk,
		///<summary>
		///  Can't set attrs on this object 
		///</summary>
		ConstantAttrs,
		Vacation,
		///<summary>
		///  Mail message in progress 
		///</summary>
		PlayerMails,
		///<summary>
		///  Player supports HTML 
		///</summary>
		Html,
		///<summary>
		///  Suppress has arrived / left msgs 
		///</summary>
		Blind,
		///<summary>
		///  Report some activities to wizards 
		///</summary>
		Suspect,
		///<summary>
		///  Watch logins 
		///</summary>
		Watcher,
		///<summary>
		///  Player is connected 
		///</summary>
		Connected,
		///<summary>
		///  Disallow most commands 
		///</summary>
		Slave,
		///<summary>
		///  Can be victim of @redirect 
		///</summary>
		RedirOk,
		///<summary>
		///  Victim of @redirect 
		///</summary>
		HasRedirect,
		///<summary>
		///  Don't check parent chain for $cmd 
		///</summary>
		Orphan,
		///<summary>
		///  Has a DarkLock 
		///</summary>
		HasDarklock,
		///<summary>
		///  Temporary flag: object is dirty 
		///</summary>
		Dirty,
		///<summary>
		///  Not subject to attr defaults 
		///</summary>
		NoDefault,
		///<summary>
		///  Check presence-related locks 
		///</summary>
		Presence,
		///<summary>
		///  Check @speechmod attr 
		///</summary>
		HasSpeechmod,
		///<summary>
		///  Internal: has Propdir attr 
		///</summary>
		HasPropdir,
		///<summary>
		///  User-defined flag 0
		///</summary>
		Mark0,
		///<summary>
		///  User-defined flag 1
		///</summary>
		Mark1,
		///<summary>
		///  User-defined flag 2
		///</summary>
		Mark2,
		///<summary>
		///  User-defined flag 3
		///</summary>
		Mark3,
		///<summary>
		///  User-defined flag 4
		///</summary>
		Mark4,
		///<summary>
		///  User-defined flag 5
		///</summary>
		Mark5,
		///<summary>
		///  User-defined flag 6
		///</summary>
		Mark6,
		///<summary>
		///  User-defined flag 7
		///</summary>
		Mark7,
		///<summary>
		///  User-defined flag 8
		///</summary>
		Mark8,
		///<summary>
		///  User-defined flag 9
		///</summary>
		Mark9
	}
}