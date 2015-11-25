using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

namespace VodkaTwitch
{
    public static class SpellManager
    {
        public static Spell.Active Q { get; private set; }
        public static Spell.Skillshot W { get; private set; }
        public static Spell.Active E { get; private set; }
        public static Spell.Active R { get; private set; }
        public static Spell.Targeted Ignite { get; private set; }

        static SpellManager()
        {
            // Initialize spells
            Q = new Spell.Active(SpellSlot.Q);
            W = new Spell.Skillshot(SpellSlot.W, 900, SkillShotType.Circular, 250, 1400, 275);
            W.AllowedCollisionCount = Int32.MaxValue;
            E = new Spell.Active(SpellSlot.E, 1200);
            R = new Spell.Active(SpellSlot.R, 850);

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

        public static bool HasIgnite()
        {
            return Ignite != null;
        }

        public static bool RActive
        {
            get { return Player.Instance.HasBuff("TwitchFullAutomatic"); }
        }

        public static bool QActive
        {
            get { return Player.Instance.HasBuff("TwitchHideInShadows"); }
        }

        public static int EStacks(Obj_AI_Base obj)
        {
            return obj.GetBuffCount("twitchdeadlyvenom");
        }
    }
}
