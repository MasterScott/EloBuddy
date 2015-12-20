using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

namespace VodkaJanna
{
    public static class SpellManager
    {
        public static Spell.Skillshot Q { get; private set; }
        public static Spell.Targeted W { get; private set; }
        public static Spell.Targeted E { get; private set; }
        public static Spell.Active R { get; private set; }
        public static Spell.Targeted Ignite { get; private set; }

        static SpellManager()
        {
            // Initialize spells
            Q = new Spell.Skillshot(SpellSlot.Q, 800, SkillShotType.Linear, 10, 900, 120);
            Q.AllowedCollisionCount = int.MaxValue;
            W = new Spell.Targeted(SpellSlot.W, 550);
            E = new Spell.Targeted(SpellSlot.E, 750);
            R = new Spell.Active(SpellSlot.R, 675);

            if (Player.Instance.Spellbook.GetSpell(SpellSlot.Summoner1).Name.Equals("summonerdot", StringComparison.CurrentCultureIgnoreCase))
            {
                Ignite = new Spell.Targeted(SpellSlot.Summoner1, 600);
            }
            else if ((Player.Instance.Spellbook.GetSpell(SpellSlot.Summoner2).Name.Equals("summonerdot", StringComparison.CurrentCultureIgnoreCase)))
            {
                Ignite = new Spell.Targeted(SpellSlot.Summoner2, 600);
            }
        }

        public static void Initialize()
        {

        }

        public static bool QCastable()
        {
            return Q.IsReady() && Player.Instance.Spellbook.GetSpell(SpellSlot.Q).ToggleState != 2;
        }

        public static bool IsUlting()
        {
            return Player.Instance.Spellbook.IsChanneling || Player.Instance.HasBuff("Reap The Whirlwind");
        }
    }
}
