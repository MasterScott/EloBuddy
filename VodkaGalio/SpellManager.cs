using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

namespace VodkaGalio
{
    public static class SpellManager
    {
        public static Spell.Skillshot Q { get; private set; }
        public static Spell.Targeted W { get; private set; }
        public static Spell.Skillshot E { get; private set; }
        public static Spell.Active R { get; private set; }
        public static Spell.Targeted Ignite { get; private set; }

        static SpellManager()
        {
            // Initialize spells
            Q = new Spell.Skillshot(SpellSlot.Q, 900, SkillShotType.Circular, 250, 1300, 235);
            Q.AllowedCollisionCount = int.MaxValue;
            W = new Spell.Targeted(SpellSlot.W, 800);
            E = new Spell.Skillshot(SpellSlot.E, 1130, SkillShotType.Linear, 250, 1300, 160);
            E.AllowedCollisionCount = int.MaxValue;
            R = new Spell.Active(SpellSlot.R, 550);

            Ignite = new Spell.Targeted(Player.Instance.GetSpellSlotFromName("summonerdot"), 600);
        }

        public static void Initialize()
        {

        }

        public static bool isUlting()
        {
            return Player.Instance.Spellbook.IsChanneling || Player.Instance.HasBuff("GalioIdolOfDurand");
        }
    }
}
