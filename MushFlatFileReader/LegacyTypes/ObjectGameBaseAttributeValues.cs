namespace MushFlatFileReader.LegacyTypes
{
	// ReSharper disable InconsistentNaming
	public enum ObjectGameBaseAttributeValues
	{
		///<summary>
		///  Nothing 
		///</summary>
		NULL = 0,
		///<summary>
		///  Others success message 
		///</summary>
		OSUCC = 1,
		///<summary>
		///  Others fail message 
		///</summary>
		OFAIL = 2,
		///<summary>
		///  Invoker fail message 
		///</summary>
		FAIL = 3,
		///<summary>
		///  Invoker success message 
		///</summary>
		SUCC = 4,
		///<summary>
		///  Password (only meaningful for players) 
		///</summary>
		PASS = 5,
		///<summary>
		///  Description 
		///</summary>
		DESC = 6,
		///<summary>
		///  Sex 
		///</summary>
		SEX = 7,
		///<summary>
		///  Others drop message 
		///</summary>
		ODROP = 8,
		///<summary>
		///  Invoker drop message 
		///</summary>
		DROP = 9,
		///<summary>
		///  Others kill message 
		///</summary>
		OKILL = 10,
		///<summary>
		///  Invoker kill message 
		///</summary>
		KILL = 11,
		///<summary>
		///  Success action list 
		///</summary>
		ASUCC = 12,
		///<summary>
		///  Failure action list 
		///</summary>
		AFAIL = 13,
		///<summary>
		///  Drop action list 
		///</summary>
		ADROP = 14,
		///<summary>
		///  Kill action list 
		///</summary>
		AKILL = 15,
		///<summary>
		///  Use action list 
		///</summary>
		AUSE = 16,
		///<summary>
		///  Number of charges remaining 
		///</summary>
		CHARGES = 17,
		///<summary>
		///  Actions done when no more charges 
		///</summary>
		RUNOUT = 18,
		///<summary>
		///  Actions run when game started up 
		///</summary>
		STARTUP = 19,
		///<summary>
		///  Actions run when obj is cloned 
		///</summary>
		ACLONE = 20,
		///<summary>
		///  Actions run when given COST pennies 
		///</summary>
		APAY = 21,
		///<summary>
		///  Others pay message 
		///</summary>
		OPAY = 22,
		///<summary>
		///  Invoker pay message 
		///</summary>
		PAY = 23,
		///<summary>
		///  Number of pennies needed to invoke xPAY 
		///</summary>
		COST = 24,
		///<summary>
		///  Value or Wealth (internal) 
		///</summary>
		MONEY = 25,
		///<summary>
		///  (Wildcarded) string to listen for 
		///</summary>
		LISTEN = 26,
		///<summary>
		///  Actions to do when anyone says LISTEN str 
		///</summary>
		AAHEAR = 27,
		///<summary>
		///  Actions to do when I say LISTEN str 
		///</summary>
		AMHEAR = 28,
		///<summary>
		///  Actions to do when others say LISTEN str 
		///</summary>
		AHEAR = 29,
		///<summary>
		///  Date/time of last login (players only) 
		///</summary>
		LAST = 30,
		///<summary>
		///  Max. # of entries obj has in the queue 
		///</summary>
		QUEUEMAX = 31,
		///<summary>
		///  Inside description (ENTER to get inside) 
		///</summary>
		IDESC = 32,
		///<summary>
		///  Invoker enter message 
		///</summary>
		ENTER = 33,
		///<summary>
		///  Others enter message in dest 
		///</summary>
		OXENTER = 34,
		///<summary>
		///  Enter action list 
		///</summary>
		AENTER = 35,
		///<summary>
		///  Describe action list 
		///</summary>
		ADESC = 36,
		///<summary>
		///  Others describe message 
		///</summary>
		ODESC = 37,
		///<summary>
		///  Relative object quota 
		///</summary>
		RQUOTA = 38,
		///<summary>
		///  Actions run when player connects 
		///</summary>
		ACONNECT = 39,
		///<summary>
		///  Actions run when player disconnects 
		///</summary>
		ADISCONNECT = 40,
		///<summary>
		///  Daily allowance, if diff from default 
		///</summary>
		ALLOWANCE = 41,
		///<summary>
		///  Object lock 
		///</summary>
		LOCK = 42,
		///<summary>
		///  Object name 
		///</summary>
		NAME = 43,
		///<summary>
		///  Wizard-accessible comments 
		///</summary>
		COMMENT = 44,
		///<summary>
		///  Invoker use message 
		///</summary>
		USE = 45,
		///<summary>
		///  Others use message 
		///</summary>
		OUSE = 46,
		///<summary>
		///  Semaphore control info 
		///</summary>
		SEMAPHORE = 47,
		///<summary>
		///  Per-user disconnect timeout 
		///</summary>
		TIMEOUT = 48,
		///<summary>
		///  Absolute quota (to speed up @quota) 
		///</summary>
		QUOTA = 49,
		///<summary>
		///  Invoker leave message 
		///</summary>
		LEAVE = 50,
		///<summary>
		///  Others leave message in src 
		///</summary>
		OLEAVE = 51,
		///<summary>
		///  Leave action list 
		///</summary>
		ALEAVE = 52,
		///<summary>
		///  Others enter message in src 
		///</summary>
		OENTER = 53,
		///<summary>
		///  Others leave message in dest 
		///</summary>
		OXLEAVE = 54,
		///<summary>
		///  Invoker move message 
		///</summary>
		MOVE = 55,
		///<summary>
		///  Others move message 
		///</summary>
		OMOVE = 56,
		///<summary>
		///  Move action list 
		///</summary>
		AMOVE = 57,
		///<summary>
		///  Alias for player names 
		///</summary>
		ALIAS = 58,
		///<summary>
		///  ENTER lock 
		///</summary>
		LENTER = 59,
		///<summary>
		///  LEAVE lock 
		///</summary>
		LLEAVE = 60,
		///<summary>
		///  PAGE lock 
		///</summary>
		LPAGE = 61,
		///<summary>
		///  USE lock 
		///</summary>
		LUSE = 62,
		///<summary>
		///  Give lock (who may give me away?) 
		///</summary>
		LGIVE = 63,
		///<summary>
		///  Alternate names for ENTER 
		///</summary>
		EALIAS = 64,
		///<summary>
		///  Alternate names for LEAVE 
		///</summary>
		LALIAS = 65,
		///<summary>
		///  Invoker entry fail message 
		///</summary>
		EFAIL = 66,
		///<summary>
		///  Others entry fail message 
		///</summary>
		OEFAIL = 67,
		///<summary>
		///  Entry fail action list 
		///</summary>
		AEFAIL = 68,
		///<summary>
		///  Invoker leave fail message 
		///</summary>
		LFAIL = 69,
		///<summary>
		///  Others leave fail message 
		///</summary>
		OLFAIL = 70,
		///<summary>
		///  Leave fail action list 
		///</summary>
		ALFAIL = 71,
		///<summary>
		///  Rejected page return message 
		///</summary>
		REJECT = 72,
		///<summary>
		///  Not_connected page return message 
		///</summary>
		AWAY = 73,
		///<summary>
		///  Success page return message 
		///</summary>
		IDLE = 74,
		///<summary>
		///  Invoker use fail message 
		///</summary>
		UFAIL = 75,
		///<summary>
		///  Others use fail message 
		///</summary>
		OUFAIL = 76,
		///<summary>
		///  Use fail action list 
		///</summary>
		AUFAIL = 77,
		///<summary>
		///  Invoker teleport message 
		///</summary>
		TPORT = 79,
		///<summary>
		///  Others teleport message in src 
		///</summary>
		OTPORT = 80,
		///<summary>
		///  Others teleport message in dst 
		///</summary>
		OXTPORT = 81,
		///<summary>
		///  Teleport action list 
		///</summary>
		ATPORT = 82,
		///<summary>
		///  Recent login information 
		///</summary>
		LOGINDATA = 84,
		///<summary>
		///  Teleport lock (can others @tel to me?) 
		///</summary>
		LTPORT = 85,
		///<summary>
		///  Drop lock (can I be dropped or @tel'ed) 
		///</summary>
		LDROP = 86,
		///<summary>
		///  Receive lock (who may give me things?) 
		///</summary>
		LRECEIVE = 87,
		///<summary>
		///  Last site logged in from, in cleartext 
		///</summary>
		LASTSITE = 88,
		///<summary>
		///  Prefix on incoming messages into objects 
		///</summary>
		INPREFIX = 89,
		///<summary>
		///  Prefix used by exits/objects when audible 
		///</summary>
		PREFIX = 90,
		///<summary>
		///  Filter to zap incoming text into objects 
		///</summary>
		INFILTER = 91,
		///<summary>
		///  Filter to zap text forwarded by audible. 
		///</summary>
		FILTER = 92,
		///<summary>
		///  Who may link to here 
		///</summary>
		LLINK = 93,
		///<summary>
		///  Who may teleport out from here 
		///</summary>
		LTELOUT = 94,
		///<summary>
		///  Recipients of AUDIBLE output 
		///</summary>
		FORWARDLIST = 95,
		///<summary>
		///  @mail folders 
		///</summary>
		MAILFOLDERS = 96,
		///<summary>
		///  Spare lock not referenced by server 
		///</summary>
		LUSER = 97,
		///<summary>
		///  Who may @parent to me if PARENT_OK set 
		///</summary>
		LPARENT = 98,
		///<summary>
		///  Who controls me if CONTROL_OK set 
		///</summary>
		LCONTROL = 99,
		///<summary>
		///  VA attribute (VB-VZ follow) 
		///</summary>
		VA = 100,
		VB = 101,
		VC = 102,
		VD = 103,
		VE = 104,
		VF = 105,
		VG = 106,
		VH = 107,
		VI = 108,
		VJ = 109,
		VK = 110,
		VL = 111,
		VM = 112,
		VN = 113,
		VO = 114,
		VP = 115,
		VQ = 116,
		VR = 117,
		VS = 118,
		VT = 119,
		VU = 120,
		VV = 121,
		VW = 122,
		VX = 123,
		VY = 124,
		VZ = 125,
		///<summary>
		///  Give fail message 
		///</summary>
		GFAIL = 129,
		///<summary>
		///  Others give fail message 
		///</summary>
		OGFAIL = 130,
		///<summary>
		///  Give fail action 
		///</summary>
		AGFAIL = 131,
		///<summary>
		///  Receive fail message 
		///</summary>
		RFAIL = 132,
		///<summary>
		///  Others receive fail message 
		///</summary>
		ORFAIL = 133,
		///<summary>
		///  Receive fail action 
		///</summary>
		ARFAIL = 134,
		///<summary>
		///  Drop fail message 
		///</summary>
		DFAIL = 135,
		///<summary>
		///  Others drop fail message 
		///</summary>
		ODFAIL = 136,
		///<summary>
		///  Drop fail action 
		///</summary>
		ADFAIL = 137,
		///<summary>
		///  Teleport (to) fail message 
		///</summary>
		TFAIL = 138,
		///<summary>
		///  Others teleport (to) fail message 
		///</summary>
		OTFAIL = 139,
		///<summary>
		///  Teleport fail action 
		///</summary>
		ATFAIL = 140,
		///<summary>
		///  Teleport (from) fail message 
		///</summary>
		TOFAIL = 141,
		///<summary>
		///  Others teleport (from) fail message 
		///</summary>
		OTOFAIL = 142,
		///<summary>
		///  Teleport (from) fail action 
		///</summary>
		ATOFAIL = 143,
		///<summary>
		///  Who is the mail Cc'ed to? 
		///</summary>
		MAILCC = 198,
		///<summary>
		///  Who is the mail Bcc'ed to? 
		///</summary>
		MAILBCC = 199,
		///<summary>
		///  Player last paged 
		///</summary>
		LASTPAGE = 200,
		///<summary>
		///  Message echoed to sender 
		///</summary>
		MAIL = 201,
		///<summary>
		///  Action taken when mail received 
		///</summary>
		AMAIL = 202,
		///<summary>
		///  Mail signature 
		///</summary>
		SIGNATURE = 203,
		///<summary>
		///  Daily attribute to be executed 
		///</summary>
		DAILY = 204,
		///<summary>
		///  Who is the mail to? 
		///</summary>
		MAILTO = 205,
		///<summary>
		///  The mail message itself 
		///</summary>
		MAILMSG = 206,
		///<summary>
		///  The mail subject 
		///</summary>
		MAILSUB = 207,
		///<summary>
		///  The current @mail folder 
		///</summary>
		MAILCURF = 208,
		///<summary>
		///  Speechlocks 
		///</summary>
		LSPEECH = 209,
		///<summary>
		///  Command for execution by @prog 
		///</summary>
		PROGCMD = 210,
		///<summary>
		///  Flags for extended mail 
		///</summary>
		MAILFLAGS = 211,
		///<summary>
		///  Who is destroying this object? 
		///</summary>
		DESTROYER = 212,
		///<summary>
		///  New object array 
		///</summary>
		NEWOBJS = 213,
		///<summary>
		///  Player-specified contents format 
		///</summary>
		LCON_FMT = 214,
		///<summary>
		///  Player-specified exits format 
		///</summary>
		LEXITS_FMT = 215,
		///<summary>
		///  Variable exit destination 
		///</summary>
		EXITVARDEST = 216,
		///<summary>
		///  ChownLock 
		///</summary>
		LCHOWN = 217,
		///<summary>
		///  Last IP address logged in from 
		///</summary>
		LASTIP = 218,
		///<summary>
		///  DarkLock 
		///</summary>
		LDARK = 219,
		///<summary>
		///  URL of the VRML scene for this object 
		///</summary>
		VRML_URL = 220,
		///<summary>
		///  HTML @desc 
		///</summary>
		HTDESC = 221,
		///<summary>
		///  Player-specified name format 
		///</summary>
		NAME_FMT = 222,
		///<summary>
		///  Who is this player seen by? (presence) 
		///</summary>
		LKNOWN = 223,
		///<summary>
		///  Who is this player heard by? (speech) 
		///</summary>
		LHEARD = 224,
		///<summary>
		///  Who notices this player moving? 
		///</summary>
		LMOVED = 225,
		///<summary>
		///  Who does this player see? (presence) 
		///</summary>
		LKNOWS = 226,
		///<summary>
		///  Who does this player hear? (speech) 
		///</summary>
		LHEARS = 227,
		///<summary>
		///  Who does this player notice moving? 
		///</summary> 
		LMOVES = 228,
		///<summary>
		///  Format speech 
		///</summary>
		SPEECHFMT = 229,
		///<summary>
		///  Last paged as part of this group 
		///</summary>
		PAGEGROUP = 230,
		///<summary>
		///  Property directory dbref list 
		///</summary>
		PROPDIR = 231,
		LIST = 253,
		TEMP = 255
	}
	// ReSharper restore InconsistentNaming
}