using System;
using System.Collections.Generic;
using System.Linq;
using MushFlatFileReader.GameHeaders;
using MushFlatFileReader.LegacyTypes;
using MushFlatFileReader.NamedTypes;
using Sprache;

namespace MushFlatFileReader
{
	public class TinyMushObject
	{
		public long DbRef { get; private set; }
		public long Location { get; private set; }
		public long Zone { get; private set; }
		public long Contents { get; private set; }
		public long Exits { get; private set; }
		public long Link { get; private set; }
		public long Next { get; private set; }
		public long Owner { get; private set; }
		public long Parent { get; private set; }
		public long Money { get; private set; }

		public DateTime AccessTime { get; private set; }
		public DateTime ModTime { get; private set; }

		public string LockKey { get; set; }
		public string Name { get; private set; }

		public TinyMushObjectType ObjectType { get; private set; }

		public HashSet<TinyMushObjectFlags> Flags = new HashSet<TinyMushObjectFlags>();
		public HashSet<TinyMushObjectPowers> Powers = new HashSet<TinyMushObjectPowers>();
		public List<TinyMushObjectAttribute> Attributes = new List<TinyMushObjectAttribute>();

		public TinyMushObject(MushEntry me)
		{
			DbRef = me.Number;
			Location = me.Location;
			Zone = me.Zone;
			Contents = me.Contents;
			Exits = me.Exits;
			Link = me.Link;
			Next = me.Next;
			Owner = me.Owner;
			Parent = me.Parent;
			Money = me.Money;
			AccessTime = MakeTime(me.AccessTime);
			ModTime = MakeTime(me.ModTime);

			var tryName = StripAnsi().TryParse(me.Name);
			Name = tryName.WasSuccessful ? tryName.Value : me.Name;

			IEnumerable<TinyMushObjectFlags> flags = SetFlags(me);
			foreach (TinyMushObjectFlags flag in flags)
			{
				Flags.Add(flag);
			}

			IEnumerable<TinyMushObjectPowers> powers = SetPowers(me);
			foreach (TinyMushObjectPowers power in powers)
			{
				Powers.Add(power);
			}

			IEnumerable<TinyMushObjectAttribute> attributes = SetAttributes(me, Owner);
			Attributes.AddRange(attributes);

			ObjectType = GetTinyMushType(me);
		}

		private static TinyMushObjectType GetTinyMushType(MushEntry me)
		{
			long type = me.Flags1 & 7;

			switch (type)
			{
				case 0:
					return TinyMushObjectType.Room;
				case 1:
					return TinyMushObjectType.Thing;
				case 2:
					return TinyMushObjectType.Exit;
				case 3:
					return TinyMushObjectType.Player;
				case 4:
					return TinyMushObjectType.Zone;
				default:
					return TinyMushObjectType.Garbage;
			}
		}

		private static DateTime MakeTime(long accessTime)
		{
			DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0);
			epoch = epoch.AddSeconds(accessTime);
			return epoch;
		}

		private static List<T> MatchGameEnums<T>(MushEntry me, long inFlags) where T : IConvertible
		{
			var values = Enum.GetValues(typeof(T)).Cast<T>().ToList();
			List<T> test =
				values.Where(flag => (inFlags & (long)Convert.ChangeType(flag, typeof(long))) != 0)
				      .ToList();
			return test;
		}

		private static Parser<string> StripAnsi()
		{
			return
				from s in Letter().Many().End()
				select string.Concat(s);
		}

		private static Parser<string> Letter()
		{
			return
				(
					from c1 in Parse.Char(c => c >= ' ' && c <= (char) 0x7f, "ASCII only")
					select new string(c1, 1)
				)
					.Or
					(
					 from a1 in StripAnsiColor()
					 select a1
					);
		}

		private static Parser<string> StripAnsiColor()
		{
			return
				from c in Parse.Char((char) 27)
				from b in Parse.Char('[')
				from n1 in Parse.Number
				from n in
					(
						from c1 in Parse.Char(';')
						from n2 in Parse.Number
						select ""
					).Many().Optional()
				from m in Parse.Char('m')
				select "";
		}


		#region Flags
		private static IEnumerable<TinyMushObjectFlags> SetFlags(MushEntry me)
		{
			List<TinyMushObjectFlags1> f1 = MatchGameEnums<TinyMushObjectFlags1>(me, me.Flags1);
			List<TinyMushObjectFlags2> f2 = MatchGameEnums<TinyMushObjectFlags2>(me, me.Flags2);
			List<TinyMushObjectFlags3> f3 = MatchGameEnums<TinyMushObjectFlags3>(me, me.Flags3);

			List<TinyMushObjectFlags> res = new List<TinyMushObjectFlags>();

			res.AddRange(ParseFlags1(f1));
			res.AddRange(ParseFlags2(f2));
			res.AddRange(ParseFlags3(f3));

			return res;
		}

		private static IEnumerable<TinyMushObjectFlags> ParseFlags1(IEnumerable<TinyMushObjectFlags1> f1)
		{
			List<TinyMushObjectFlags> res = new List<TinyMushObjectFlags>();
			foreach (TinyMushObjectFlags1 flag in f1)
			{
				switch (flag)
				{
					case TinyMushObjectFlags1.SEETHRU:
						res.Add(TinyMushObjectFlags.SeeThru);
						break;
					case TinyMushObjectFlags1.WIZARD:
						res.Add(TinyMushObjectFlags.Wizard);
						break;
					case TinyMushObjectFlags1.LINK_OK:
						res.Add(TinyMushObjectFlags.LinkOk);
						break;
					case TinyMushObjectFlags1.DARK:
						res.Add(TinyMushObjectFlags.Dark);
						break;
					case TinyMushObjectFlags1.JUMP_OK:
						res.Add(TinyMushObjectFlags.JumpOk);
						break;
					case TinyMushObjectFlags1.STICKY:
						res.Add(TinyMushObjectFlags.Sticky);
						break;
					case TinyMushObjectFlags1.DESTROY_OK:
						res.Add(TinyMushObjectFlags.DestroyOk);
						break;
					case TinyMushObjectFlags1.HAVEN:
						res.Add(TinyMushObjectFlags.Haven);
						break;
					case TinyMushObjectFlags1.QUIET:
						res.Add(TinyMushObjectFlags.Quiet);
						break;
					case TinyMushObjectFlags1.HALT:
						res.Add(TinyMushObjectFlags.Halt);
						break;
					case TinyMushObjectFlags1.TRACE:
						res.Add(TinyMushObjectFlags.Trace);
						break;
					case TinyMushObjectFlags1.GOING:
						res.Add(TinyMushObjectFlags.Going);
						break;
					case TinyMushObjectFlags1.MONITOR:
						res.Add(TinyMushObjectFlags.Monitor);
						break;
					case TinyMushObjectFlags1.MYOPIC:
						res.Add(TinyMushObjectFlags.Myopic);
						break;
					case TinyMushObjectFlags1.PUPPET:
						res.Add(TinyMushObjectFlags.Puppet);
						break;
					case TinyMushObjectFlags1.CHOWN_OK:
						res.Add(TinyMushObjectFlags.ChownOk);
						break;
					case TinyMushObjectFlags1.ENTER_OK:
						res.Add(TinyMushObjectFlags.EnterOk);
						break;
					case TinyMushObjectFlags1.VISUAL:
						res.Add(TinyMushObjectFlags.Visual);
						break;
					case TinyMushObjectFlags1.IMMORTAL:
						res.Add(TinyMushObjectFlags.Immortal);
						break;
					case TinyMushObjectFlags1.HAS_STARTUP:
						res.Add(TinyMushObjectFlags.HasStartup);
						break;
					case TinyMushObjectFlags1.OPAQUE:
						res.Add(TinyMushObjectFlags.Opaque);
						break;
					case TinyMushObjectFlags1.VERBOSE:
						res.Add(TinyMushObjectFlags.Verbose);
						break;
					case TinyMushObjectFlags1.INHERIT:
						res.Add(TinyMushObjectFlags.Inherit);
						break;
					case TinyMushObjectFlags1.NOSPOOF:
						res.Add(TinyMushObjectFlags.NoSpoof);
						break;
					case TinyMushObjectFlags1.ROBOT:
						res.Add(TinyMushObjectFlags.Robot);
						break;
					case TinyMushObjectFlags1.SAFE:
						res.Add(TinyMushObjectFlags.Safe);
						break;
					case TinyMushObjectFlags1.ROYALTY:
						res.Add(TinyMushObjectFlags.Royalty);
						break;
					case TinyMushObjectFlags1.HEARTHRU:
						res.Add(TinyMushObjectFlags.HearThru);
						break;
					case TinyMushObjectFlags1.TERSE:
						res.Add(TinyMushObjectFlags.Terse);
						break;
				}
			}
			return res;
		}

		private static IEnumerable<TinyMushObjectFlags> ParseFlags2(IEnumerable<TinyMushObjectFlags2> f2)
		{
			List<TinyMushObjectFlags> res = new List<TinyMushObjectFlags>();
			foreach (TinyMushObjectFlags2 flag in f2)
			{
				switch (flag)
				{
					case TinyMushObjectFlags2.KEY:
						res.Add(TinyMushObjectFlags.Key);
						break;
					case TinyMushObjectFlags2.ABODE:
						res.Add(TinyMushObjectFlags.Abode);
						break;
					case TinyMushObjectFlags2.FLOATING:
						res.Add(TinyMushObjectFlags.Floating);
						break;
					case TinyMushObjectFlags2.UNFINDABLE:
						res.Add(TinyMushObjectFlags.Unfindable);
						break;
					case TinyMushObjectFlags2.PARENT_OK:
						res.Add(TinyMushObjectFlags.ParentOk);
						break;
					case TinyMushObjectFlags2.LIGHT:
						res.Add(TinyMushObjectFlags.Light);
						break;
					case TinyMushObjectFlags2.HAS_LISTEN:
						res.Add(TinyMushObjectFlags.HasListen);
						break;
					case TinyMushObjectFlags2.HAS_FWDLIST:
						res.Add(TinyMushObjectFlags.HasFwdList);
						break;
					case TinyMushObjectFlags2.AUDITORIUM:
						res.Add(TinyMushObjectFlags.Auditorium);
						break;
					case TinyMushObjectFlags2.ANSI:
						res.Add(TinyMushObjectFlags.Ansi);
						break;
					case TinyMushObjectFlags2.HEAD_FLAG:
						res.Add(TinyMushObjectFlags.HeadFlag);
						break;
					case TinyMushObjectFlags2.FIXED:
						res.Add(TinyMushObjectFlags.Fixed);
						break;
					case TinyMushObjectFlags2.UNINSPECTED:
						res.Add(TinyMushObjectFlags.Uninspected);
						break;
					case TinyMushObjectFlags2.ZONE_PARENT:
						res.Add(TinyMushObjectFlags.ZoneParent);
						break;
					case TinyMushObjectFlags2.DYNAMIC:
						res.Add(TinyMushObjectFlags.Dynamic);
						break;
					case TinyMushObjectFlags2.NOBLEED:
						res.Add(TinyMushObjectFlags.Nobleed);
						break;
					case TinyMushObjectFlags2.STAFF:
						res.Add(TinyMushObjectFlags.Staff);
						break;
					case TinyMushObjectFlags2.HAS_DAILY:
						res.Add(TinyMushObjectFlags.HasDaily);
						break;
					case TinyMushObjectFlags2.GAGGED:
						res.Add(TinyMushObjectFlags.Gagged);
						break;
					case TinyMushObjectFlags2.HAS_COMMANDS:
						res.Add(TinyMushObjectFlags.HasCommands);
						break;
					case TinyMushObjectFlags2.STOP_MATCH:
						res.Add(TinyMushObjectFlags.StopMatch);
						break;
					case TinyMushObjectFlags2.BOUNCE:
						res.Add(TinyMushObjectFlags.Bounce);
						break;
					case TinyMushObjectFlags2.CONTROL_OK:
						res.Add(TinyMushObjectFlags.ControlOk);
						break;
					case TinyMushObjectFlags2.CONSTANT_ATTRS:
						res.Add(TinyMushObjectFlags.ConstantAttrs);
						break;
					case TinyMushObjectFlags2.VACATION:
						res.Add(TinyMushObjectFlags.Vacation);
						break;
					case TinyMushObjectFlags2.PLAYER_MAILS:
						res.Add(TinyMushObjectFlags.PlayerMails);
						break;
					case TinyMushObjectFlags2.HTML:
						res.Add(TinyMushObjectFlags.Html);
						break;
					case TinyMushObjectFlags2.BLIND:
						res.Add(TinyMushObjectFlags.Blind);
						break;
					case TinyMushObjectFlags2.SUSPECT:
						res.Add(TinyMushObjectFlags.Suspect);
						break;
					case TinyMushObjectFlags2.WATCHER:
						res.Add(TinyMushObjectFlags.Watcher);
						break;
					case TinyMushObjectFlags2.CONNECTED:
						res.Add(TinyMushObjectFlags.Connected);
						break;
					case TinyMushObjectFlags2.SLAVE:
						res.Add(TinyMushObjectFlags.Slave);
						break;
				}
			}
			return res;
		}

		private static IEnumerable<TinyMushObjectFlags> ParseFlags3(IEnumerable<TinyMushObjectFlags3> f3)
		{
			List<TinyMushObjectFlags> res = new List<TinyMushObjectFlags>();
			foreach (TinyMushObjectFlags3 flag in f3)
			{
				switch (flag)
				{
					case TinyMushObjectFlags3.REDIR_OK:
						res.Add(TinyMushObjectFlags.RedirOk);
						break;
					case TinyMushObjectFlags3.HAS_REDIRECT:
						res.Add(TinyMushObjectFlags.HasRedirect);
						break;
					case TinyMushObjectFlags3.ORPHAN:
						res.Add(TinyMushObjectFlags.Orphan);
						break;
					case TinyMushObjectFlags3.HAS_DARKLOCK:
						res.Add(TinyMushObjectFlags.HasDarklock);
						break;
					case TinyMushObjectFlags3.DIRTY:
						res.Add(TinyMushObjectFlags.Dirty);
						break;
					case TinyMushObjectFlags3.NODEFAULT:
						res.Add(TinyMushObjectFlags.NoDefault);
						break;
					case TinyMushObjectFlags3.PRESENCE:
						res.Add(TinyMushObjectFlags.Presence);
						break;
					case TinyMushObjectFlags3.HAS_SPEECHMOD:
						res.Add(TinyMushObjectFlags.HasSpeechmod);
						break;
					case TinyMushObjectFlags3.HAS_PROPDIR:
						res.Add(TinyMushObjectFlags.HasPropdir);
						break;
					case TinyMushObjectFlags3.MARK_0:
						res.Add(TinyMushObjectFlags.Mark0);
						break;
					case TinyMushObjectFlags3.MARK_1:
						res.Add(TinyMushObjectFlags.Mark1);
						break;
					case TinyMushObjectFlags3.MARK_2:
						res.Add(TinyMushObjectFlags.Mark2);
						break;
					case TinyMushObjectFlags3.MARK_3:
						res.Add(TinyMushObjectFlags.Mark3);
						break;
					case TinyMushObjectFlags3.MARK_4:
						res.Add(TinyMushObjectFlags.Mark4);
						break;
					case TinyMushObjectFlags3.MARK_5:
						res.Add(TinyMushObjectFlags.Mark5);
						break;
					case TinyMushObjectFlags3.MARK_6:
						res.Add(TinyMushObjectFlags.Mark6);
						break;
					case TinyMushObjectFlags3.MARK_7:
						res.Add(TinyMushObjectFlags.Mark7);
						break;
					case TinyMushObjectFlags3.MARK_8:
						res.Add(TinyMushObjectFlags.Mark8);
						break;
					case TinyMushObjectFlags3.MARK_9:
						res.Add(TinyMushObjectFlags.Mark9);
						break;
				}
			}
			return res;
		}
		#endregion

		#region Attributes

		private static IEnumerable<TinyMushObjectAttribute> SetAttributes(MushEntry me, long owner)
		{
			const char marker = '\u0001';
			List<TinyMushObjectAttribute> res = new List<TinyMushObjectAttribute>();
			foreach (MushEntryAttribute attribute in me.Attributes)
			{
				var attr = new TinyMushObjectAttribute();
				if (Enum.IsDefined(typeof (ObjectGameBaseAttributeValues), (ObjectGameBaseAttributeValues)attribute.Id))
				{
					//	Dealing with stock game attribute
					attr.Name = Enum.ToObject(typeof (ObjectGameBaseAttributeValues), attribute.Id).ToString();
				}
				else
				{
					if (Universe.Attributes.ContainsKey(attribute.Id))
					{
						string tempAttrName = Universe.Attributes[ attribute.Id ].Text;
						if (!tempAttrName.Contains(":"))
						{
							continue;
						}
						var parts = tempAttrName.Split(new[] {':'});
						string tmpFlag;
						string tmpName;

						if (parts.Length < 2)
						{
							continue;
						}
						if (parts.Length > 2)
						{
							tmpFlag = parts[ 0 ];
							parts = parts.Skip(1).ToArray();
							tmpName = string.Join(":", parts);
						}
						else
						{
							tmpFlag = parts[ 0 ];
							tmpName = parts[ 1 ];
						}
						long newflag;
						if (long.TryParse(tmpFlag, out newflag))
						{
							ParseAttributeFlags(newflag,attr);
						}
						attr.Name = tmpName;
					}
					else
					{
						continue;
					}
				}

				attr.Id = attribute.Id;
				if (attribute.Text[ 0 ] != marker)
				{
					attr.Text = attribute.Text;
					attr.Owner = owner;
					res.Add(attr);
					continue;
				}

				string temp = attribute.Text.Substring(1);
				var p = AttributeParser().TryParse(temp);
				if (p.WasSuccessful)
				{
					var t = p.Value;
					attr.Owner = owner;
					if (t.Item1 != "")
					{
						long own;
						bool didOwn = long.TryParse(t.Item1, out own);
						if (didOwn)
						{
							attr.Owner = own;
						}
					}

					if (t.Item2 != "")
					{
						long flags;
						bool didFlags = long.TryParse(t.Item2, out flags);
						if (didFlags)
						{
							ParseAttributeFlags(flags, attr);
						}
					}

					if (t.Item3 == "")
					{
						attr = null;
					}
					else
					{
						attr.Text = t.Item3;
					}
				}

				if (attr != null)
				{
					res.Add(attr);
				}
			}
			return res;
		}

		private static void ParseAttributeFlags(long flags, TinyMushObjectAttribute attr)
		{
			List<TinyMushAttributeFlags> res = new List<TinyMushAttributeFlags>();

			if ((flags & (long)AttributeFlags1.ODARK) != 0)
			{
				res.Add(TinyMushAttributeFlags.Odark);
			}
			if ((flags & (long)AttributeFlags1.DARK) != 0)
			{
				res.Add(TinyMushAttributeFlags.Dark);
			}
			if ((flags & (long)AttributeFlags1.WIZARD) != 0)
			{
				res.Add(TinyMushAttributeFlags.Wizard);
			}
			if ((flags & (long)AttributeFlags1.MDARK) != 0)
			{
				res.Add(TinyMushAttributeFlags.Mdark);
			}
			if ((flags & (long)AttributeFlags1.INTERNAL) != 0)
			{
				res.Add(TinyMushAttributeFlags.Internal);
			}
			if ((flags & (long)AttributeFlags1.NOCMD) != 0)
			{
				res.Add(TinyMushAttributeFlags.NoCmd);
			}
			if ((flags & (long)AttributeFlags1.LOCK) != 0)
			{
				res.Add(TinyMushAttributeFlags.Lock);
			}
			if ((flags & (long)AttributeFlags1.DELETED) != 0)
			{
				res.Add(TinyMushAttributeFlags.Deleted);
			}
			if ((flags & (long)AttributeFlags1.NOPROG) != 0)
			{
				res.Add(TinyMushAttributeFlags.NoProg);
			}
			if ((flags & (long)AttributeFlags1.GOD) != 0)
			{
				res.Add(TinyMushAttributeFlags.God);
			}
			if ((flags & (long)AttributeFlags1.IS_LOCK) != 0)
			{
				res.Add(TinyMushAttributeFlags.IsLock);
			}
			if ((flags & (long)AttributeFlags1.VISUAL) != 0)
			{
				res.Add(TinyMushAttributeFlags.Visual);
			}
			if ((flags & (long)AttributeFlags1.PRIVATE) != 0)
			{
				res.Add(TinyMushAttributeFlags.Private);
			}
			if ((flags & (long)AttributeFlags1.HTML) != 0)
			{
				res.Add(TinyMushAttributeFlags.Html);
			}
			if ((flags & (long)AttributeFlags1.NOPARSE) != 0)
			{
				res.Add(TinyMushAttributeFlags.NoParse);
			}
			if ((flags & (long)AttributeFlags1.REGEXP) != 0)
			{
				res.Add(TinyMushAttributeFlags.Regexp);
			}
			if ((flags & (long)AttributeFlags1.NOCLONE) != 0)
			{
				res.Add(TinyMushAttributeFlags.NoClone);
			}
			if ((flags & (long)AttributeFlags1.CONST) != 0)
			{
				res.Add(TinyMushAttributeFlags.Const);
			}
			if ((flags & (long)AttributeFlags1.CASE) != 0)
			{
				res.Add(TinyMushAttributeFlags.Case);
			}
			if ((flags & (long)AttributeFlags1.STRUCTURE) != 0)
			{
				res.Add(TinyMushAttributeFlags.Structure);
			}
			if ((flags & (long)AttributeFlags1.DIRTY) != 0)
			{
				res.Add(TinyMushAttributeFlags.Dirty);
			}
			if ((flags & (long)AttributeFlags1.DEFAULT) != 0)
			{
				res.Add(TinyMushAttributeFlags.Default);
			}
			if ((flags & (long)AttributeFlags1.NONAME) != 0)
			{
				res.Add(TinyMushAttributeFlags.NoName);
			}
			if ((flags & (long)AttributeFlags1.RMATCH) != 0)
			{
				res.Add(TinyMushAttributeFlags.Rmatch);
			}
			if ((flags & (long)AttributeFlags1.NOW) != 0)
			{
				res.Add(TinyMushAttributeFlags.Now);
			}
			if ((flags & (long)AttributeFlags1.TRACE) != 0)
			{
				res.Add(TinyMushAttributeFlags.Trace);
			}

			foreach (TinyMushAttributeFlags tmFlag in res)
			{
				attr.Flags.Add(tmFlag);
			}
		}

		private static Parser<Tuple<string, string, string>> AttributeParser()
		{
			return
				(
					from min in Parse.Char('-').Optional()
					from n1 in Parse.Number
					from c1 in Parse.Char(':')
					from n2 in Parse.Number
					from c2 in Parse.Char(':')
					from s in Parse.AnyChar.Many()
					select new Tuple<string, string, string>(n1, n2, new string(s.ToArray()))
				)
					.Or
					(
					 from min in Parse.Char('-').Optional()
					 from n1 in Parse.Number
					 from c1 in Parse.Char(':')
					 from s in Parse.AnyChar.Many()
					 select new Tuple<string, string, string>(n1, "", new string(s.ToArray()))
					)
					.Or
					(
					 from s in Parse.AnyChar.Many()
					 select new Tuple<string, string, string>("", "", new string(s.ToArray()))
					);
		}

		#endregion

		#region Powers

		private static IEnumerable<TinyMushObjectPowers> SetPowers(MushEntry me)
		{
			List<TinyMushPowers1> p1 = MatchGameEnums<TinyMushPowers1>(me, me.Powers1);
			List<TinyMushPowers2> p2 = MatchGameEnums<TinyMushPowers2>(me, me.Powers2);

			List<TinyMushObjectPowers> res = new List<TinyMushObjectPowers>();

			res.AddRange(ParsePowers1(p1));
			res.AddRange(ParsePowers2(p2));

			return res;
		}

		private static IEnumerable<TinyMushObjectPowers> ParsePowers1(List<TinyMushPowers1> p1)
		{
			List<TinyMushObjectPowers> res = new List<TinyMushObjectPowers>();
			foreach (TinyMushPowers1 power in p1)
			{
				switch (power)
				{
					case TinyMushPowers1.CHG_QUOTAS:
						res.Add(TinyMushObjectPowers.ChangeQuotas);
						break;
					case TinyMushPowers1.CHOWN_ANY:
						res.Add(TinyMushObjectPowers.ChownAny);
						break;
					case TinyMushPowers1.ANNOUNCE:
						res.Add(TinyMushObjectPowers.Announce);
						break;
					case TinyMushPowers1.BOOT:
						res.Add(TinyMushObjectPowers.Boot);
						break;
					case TinyMushPowers1.HALT:
						res.Add(TinyMushObjectPowers.Halt);
						break;
					case TinyMushPowers1.CONTROL_ALL:
						res.Add(TinyMushObjectPowers.ControlAll);
						break;
					case TinyMushPowers1.WIZARD_WHO:
						res.Add(TinyMushObjectPowers.WizardWho);
						break;
					case TinyMushPowers1.EXAM_ALL:
						res.Add(TinyMushObjectPowers.ExamineAll);
						break;
					case TinyMushPowers1.FIND_UNFIND:
						res.Add(TinyMushObjectPowers.FindUnfindable);
						break;
					case TinyMushPowers1.FREE_MONEY:
						res.Add(TinyMushObjectPowers.FreeMoney);
						break;
					case TinyMushPowers1.FREE_QUOTA:
						res.Add(TinyMushObjectPowers.FreeQuota);
						break;
					case TinyMushPowers1.HIDE:
						res.Add(TinyMushObjectPowers.Hide);
						break;
					case TinyMushPowers1.IDLE:
						res.Add(TinyMushObjectPowers.NoIdleLimit);
						break;
					case TinyMushPowers1.SEARCH:
						res.Add(TinyMushObjectPowers.Search);
						break;
					case TinyMushPowers1.LONGFINGERS:
						res.Add(TinyMushObjectPowers.LongFingers);
						break;
					case TinyMushPowers1.PROG:
						res.Add(TinyMushObjectPowers.Prog);
						break;
					case TinyMushPowers1.MDARK_ATTR:
						res.Add(TinyMushObjectPowers.ReadDarkAttributes);
						break;
					case TinyMushPowers1.WIZ_ATTR:
						res.Add(TinyMushObjectPowers.WriteWizardAttributes);
						break;
					case TinyMushPowers1.COMM_ALL:
						res.Add(TinyMushObjectPowers.CommChannelWizard);
						break;
					case TinyMushPowers1.SEE_QUEUE:
						res.Add(TinyMushObjectPowers.SeeQueue);
						break;
					case TinyMushPowers1.SEE_HIDDEN:
						res.Add(TinyMushObjectPowers.SeeHidden);
						break;
					case TinyMushPowers1.WATCH:
						res.Add(TinyMushObjectPowers.Watch);
						break;
					case TinyMushPowers1.POLL:
						res.Add(TinyMushObjectPowers.Poll);
						break;
					case TinyMushPowers1.NO_DESTROY:
						res.Add(TinyMushObjectPowers.NoDestroy);
						break;
					case TinyMushPowers1.GUEST:
						res.Add(TinyMushObjectPowers.Guest);
						break;
					case TinyMushPowers1.PASS_LOCKS:
						res.Add(TinyMushObjectPowers.PassLocks);
						break;
					case TinyMushPowers1.STAT_ANY:
						res.Add(TinyMushObjectPowers.StatsAny);
						break;
					case TinyMushPowers1.STEAL:
						res.Add(TinyMushObjectPowers.Steal);
						break;
					case TinyMushPowers1.TEL_ANYWHR:
						res.Add(TinyMushObjectPowers.TeleportAnywhere);
						break;
					case TinyMushPowers1.TEL_UNRST:
						res.Add(TinyMushObjectPowers.TeleportAnything);
						break;
					case TinyMushPowers1.UNKILLABLE:
						res.Add(TinyMushObjectPowers.Unkillable);
						break;
				}
			}
			return res;
		}

		private static IEnumerable<TinyMushObjectPowers> ParsePowers2(List<TinyMushPowers2> p2)
		{
			List<TinyMushObjectPowers> res = new List<TinyMushObjectPowers>();
			foreach (TinyMushPowers2 power in p2)
			{
				switch (power)
				{
					case TinyMushPowers2.BUILDER:
						res.Add(TinyMushObjectPowers.Builder);
						break;
					case TinyMushPowers2.LINKVAR:
						res.Add(TinyMushObjectPowers.LinkVariable);
						break;
					case TinyMushPowers2.LINKTOANY:
						res.Add(TinyMushObjectPowers.LinkToAny);
						break;
					case TinyMushPowers2.OPENANYLOC:
						res.Add(TinyMushObjectPowers.OpenAnyLocation);
						break;
					case TinyMushPowers2.USE_SQL:
						res.Add(TinyMushObjectPowers.UseSql);
						break;
					case TinyMushPowers2.LINKHOME:
						res.Add(TinyMushObjectPowers.LinkToAnyHome);
						break;
					case TinyMushPowers2.CLOAK:
						res.Add(TinyMushObjectPowers.Cloak);
						break;
				}
			}
			return res;
		}

		#endregion

	}
}