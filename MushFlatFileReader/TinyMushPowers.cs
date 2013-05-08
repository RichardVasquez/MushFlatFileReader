namespace MushFlatFileReader
{
	public enum TinyMushPowers
	{
		///<summary>
		///	May change and see quotas 
		///</summary>
		ChangeQuotas,
		///<summary>
		///	Can @chown anything or to anyone 
		///</summary>
		ChownAny,
		///<summary>
		///	May use @wall 
		///</summary>
		Announce,
		///<summary>
		///	May use @boot 
		///</summary>
		Boot,
		///<summary>
		///	May @halt on other's objects 
		///</summary>
		Halt,
		///<summary>
		///	I control everything 
		///</summary>
		ControlAll,
		///<summary>
		///	See extra WHO information 
		///</summary>
		WizardWho,
		///<summary>
		///	I can examine everything 
		///</summary>
		ExamineAll,
		///<summary>
		///	Can find unfindable players 
		///</summary>
		FindUnfindable,
		///<summary>
		///	I have infinite money 
		///</summary>
		FreeMoney,
		///<summary>
		///	I have infinite quota 
		///</summary>
		FreeQuota,
		///<summary>
		///	Can set themselves DARK 
		///</summary>
		Hide,
		///<summary>
		///	No idle limit 
		///</summary>
		NoIdleLimit,
		///<summary>
		///	Can @search anyone 
		///</summary>
		Search,
		///<summary>
		///	Can get/whisper/etc from a distance 
		///</summary>
		LongFingers,
		///<summary>
		///	Can use the @prog command 
		///</summary>
		Prog,
		///<summary>
		///	Can read AF_MDARK attrs 
		///</summary>
		ReadDarkAttributes,
		///<summary>
		///	Can write AF_WIZARD attrs 
		///</summary>,
		WriteWizardAttributes,
		///<summary>
		///	Channel wiz 
		///</summary>
		CommChannelWizard,
		///<summary>
		///	Player can see the entire queue 
		///</summary>
		SeeQueue,
		///<summary>
		///	Player can see hidden players on WHO list 
		///</summary>
		SeeHidden,
		///<summary>
		///	Player can set or clear WATCHER 
		///</summary>
		Watch,
		///<summary>
		///	Player can set the doing poll 
		///</summary>
		Poll,
		///<summary>
		///	Cannot be destroyed 
		///</summary>
		NoDestroy,
		///<summary>
		///	Player is a guest 
		///</summary>
		Guest,
		///<summary>
		///	Player can pass any lock 
		///</summary>
		PassLocks,
		///<summary>
		///	Can @stat anyone 
		///</summary>
		StatsAny,
		///<summary>
		///	Can give negative money 
		///</summary>
		Steal,
		///<summary>
		///	Teleport anywhere 
		///</summary>
		TeleportAnywhere,
		///<summary>
		///	Teleport anything 
		///</summary>
		TeleportAnything,
		///<summary>
		///	Can't be killed 
		///</summary>,
		Unkillable,
		///<summary>
		///	Can build 
		///</summary>
		Builder,
		///<summary>
		///	Can link an exit to "variable" 
		///</summary>
		LinkVariable,
		///<summary>
		///	Can link to any object 
		///</summary>
		LinkToAny,
		///<summary>
		///	Can open from anywhere 
		///</summary>
		OpenAnyLocation,
		///<summary>
		///	Can use SQL queries directly 
		///</summary>
		UseSql,
		///<summary>
		///	Can link object to any home 
		///</summary>
		LinkToAnyHome,
		///<summary>
		///	Can vanish from sight via DARK 
		///</summary>
		Cloak
	}
}