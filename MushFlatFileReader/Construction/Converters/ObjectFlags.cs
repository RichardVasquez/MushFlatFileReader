using System.Collections.Generic;
using MushFlatFileReader.GameHeaders;
using MushFlatFileReader.LegacyTypes;
using MushFlatFileReader.NamedTypes;

namespace MushFlatFileReader.Construction.Converters
{
	public static class ObjectFlags
	{
		public static IEnumerable<TinyMushObjectFlags> GetFlags(MushEntry me)
		{
			List<TinyMushObjectFlags1> f1 = GameEnums.Match<TinyMushObjectFlags1>(me, me.Flags1);
			List<TinyMushObjectFlags2> f2 = GameEnums.Match<TinyMushObjectFlags2>(me, me.Flags2);
			List<TinyMushObjectFlags3> f3 = GameEnums.Match<TinyMushObjectFlags3>(me, me.Flags3);

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
	}
}
