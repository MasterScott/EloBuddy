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

        static SpellManager()
        {
            // Initialize spells
            Q = new Spell.Skillshot(SpellSlot.Q, 800, SkillShotType.Linear, 10, 900, 120);
            Q.AllowedCollisionCount = int.MaxValue;
            //Q = new Spell.Chargeable(SpellSlot.Q, 800, 1700, 3000, 10, 900, 120);
            W = new Spell.Targeted(SpellSlot.W, 550);
            E = new Spell.Targeted(SpellSlot.E, 750);
            R = new Spell.Active(SpellSlot.R, 675);
        }

        public static void Initialize()
        {

        }

        public static float WRawDamage()
        {
            return
                (int)
                    (new int[] { 60, 115, 170, 225, 280 }[SpellManager.W.Level - 1] +
                     0.5 * (ObjectManager.Player.TotalMagicalDamage));
        }

        public static bool QCastable()
        {
            return Q.IsReady() && Player.Instance.Spellbook.GetSpell(SpellSlot.Q).ToggleState != 2;
        }
    }
}
