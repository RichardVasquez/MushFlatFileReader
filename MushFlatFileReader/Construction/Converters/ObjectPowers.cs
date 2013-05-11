using System.Collections.Generic;
using MushFlatFileReader.GameHeaders;
using MushFlatFileReader.LegacyTypes;
using MushFlatFileReader.NamedTypes;

namespace MushFlatFileReader.Construction.Converters
{
	public static class ObjectPowers
	{
		public static IEnumerable<TinyMushObjectPowers> SetPowers(MushEntry me)
		{
			List<TinyMushPowers1> p1 = GameEnums.Match<TinyMushPowers1>(me, me.Powers1);
			List<TinyMushPowers2> p2 = GameEnums.Match<TinyMushPowers2>(me, me.Powers2);

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
	}
}