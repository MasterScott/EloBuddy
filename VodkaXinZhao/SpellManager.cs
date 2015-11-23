using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

namespace VodkaXinZhao
{
    public static class SpellManager
    {
        public static Spell.Active Q { get; private set; }
        public static Spell.Active W { get; private set; }
        public static Spell.Targeted E { get; private set; }
        public static Spell.Active R { get; private set; }
        public static Spell.Targeted Ignite { get; private set; }

        static SpellManager()
        {
            // Initialize spells
            Q = new Spell.Active(SpellSlot.Q, 0);
            W = new Spell.Active(SpellSlot.W, 0);
            E = new Spell.Targeted(SpellSlot.E, 600);
            R = new Spell.Active(SpellSlot.R, 185);
            
            Ignite = new Spell.Targeted(Player.Instance.GetSpellSlotFromName("summonerdot"), 600);
        }

        public static void Initialize()
        {

        }

        public static bool HasIgnite()
        {
            return Ignite != null;
        }
    }
}
