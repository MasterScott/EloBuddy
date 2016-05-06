using EloBuddy;
using EloBuddy.SDK;
using System;
using System.Linq;

namespace VodkaJax
{
    public static class SpellManager
    {
        public static Spell.Targeted Q { get; private set; }
        public static Spell.Active W { get; private set; }
        public static Spell.Active E { get; private set; }
        public static Spell.Active R { get; private set; }
        public static Spell.Targeted Ignite { get; private set; }
        public static Spell.Active Recall { get; private set; }
        static SpellManager()
        {
            // Initialize spells
            Q = new Spell.Targeted(SpellSlot.Q, 700);
            W = new Spell.Active(SpellSlot.W);
            E = new Spell.Active(SpellSlot.E, 180);
            R = new Spell.Active(SpellSlot.R);

            Recall = new Spell.Active(SpellSlot.Recall);

            var ignite = Player.Spells.FirstOrDefault(s => s.SData.Name.ToLower().Contains("summonerdot"));
            if(ignite != null)
                Ignite = new Spell.Targeted(ignite.Slot, 600);
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
