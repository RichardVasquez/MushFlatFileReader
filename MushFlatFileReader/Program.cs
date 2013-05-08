using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Sprache;

namespace MushFlatFileReader
{
	public class Program
	{
		private static void Main(string[] args)
		{
			string text = File.ReadAllText("flatfile.txt");
			Stopwatch sw1 = new Stopwatch();
			Stopwatch sw2 = new Stopwatch();
			sw1.Start();
			var ph = ParserBox.Headers().TryParse(text);
			sw2.Start();
			var o = Universe.GetObject(9);
			sw1.Stop();
			sw2.Stop();
		}
	}

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

		public HashSet<TinyMushObjectFlags> Flags = new HashSet<TinyMushObjectFlags>();
		public HashSet<TinyMushPowers> Powers = new HashSet<TinyMushPowers>();
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

			IEnumerable<TinyMushObjectFlags> flags = SetFlags(me);
			foreach (TinyMushObjectFlags flag in flags)
			{
				Flags.Add(flag);
			}

			IEnumerable<TinyMushObjectAttribute> attributes = SetAttributes(me, Owner);
			Attributes.AddRange(attributes);
		}

		private static DateTime MakeTime(long accessTime)
		{
			DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0);
			epoch = epoch.AddSeconds(accessTime);
			return epoch;
		}

		#region Flags
		private static IEnumerable<TinyMushObjectFlags> SetFlags(MushEntry me)
		{
			List<TinyMushObjectFlags1> f1 = MatchObjectFlags<TinyMushObjectFlags1>(me, me.Flags1);
			List<TinyMushObjectFlags2> f2 = MatchObjectFlags<TinyMushObjectFlags2>(me, me.Flags2);
			List<TinyMushObjectFlags3> f3 = MatchObjectFlags<TinyMushObjectFlags3>(me, me.Flags3);

			List<TinyMushObjectFlags> res = new List<TinyMushObjectFlags>();

			res.AddRange(ParseFlags1(f1));
			res.AddRange(ParseFlags2(f2));
			res.AddRange(ParseFlags3(f3));

			return res;
		}

		private static List<T> MatchObjectFlags<T>(MushEntry me, long inFlags) where T:IConvertible
		{
			var values = Enum.GetValues(typeof (T)).Cast<T>().ToList();
			List<T> test =
				values.Where(flag => ( inFlags & (long) Convert.ChangeType(flag, typeof (long)) ) != 0)
				      .ToList();
			return test;
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
				if (Enum.IsDefined(typeof (AttributeValues), (AttributeValues)attribute.Id))
				{
					//	Dealing with game attribute
					attr.Name = Enum.ToObject(typeof (AttributeValues), attribute.Id).ToString();
				}
				else
				{
					if (Universe.Attributes.ContainsKey(attribute.Id))
					{
						attr.Name = Universe.Attributes[ attribute.Id ].Text;
					}
					else
					{
						attr = null;
					}
				}

				if (attr == null)
				{
					continue;
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

	}

	public class TinyMushObjectAttribute
	{
		public string Name;
		public long Id;
		public long Owner;
		public string Text;
		public HashSet<TinyMushAttributeFlags> Flags = new HashSet<TinyMushAttributeFlags>();
	}

}