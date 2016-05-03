using EloBuddy;
using EloBuddy.SDK;
using System;
using System.Linq;

namespace VodkaWarwick
{
    public static class SpellManager
    {
        public static Spell.Targeted Q { get; private set; }
        public static Spell.Active W { get; private set; }
        public static Spell.Active E { get; private set; }
        public static Spell.Targeted R { get; private set; }
        public static Spell.Targeted Ignite { get; private set; }
        public static Spell.Targeted Smite { get; private set; }
        public static Spell.Active Recall { get; private set; }
        static SpellManager()
        {
            // Initialize spells
            Q = new Spell.Targeted(SpellSlot.Q, 400);
            W = new Spell.Active(SpellSlot.W);
            E = new Spell.Active(SpellSlot.E, 1500);
            R = new Spell.Targeted(SpellSlot.R, 700);

            Recall = new Spell.Active(SpellSlot.Recall);

            var ignite = Player.Spells.FirstOrDefault(s => s.SData.Name.ToLower().Contains("summonerdot"));
            if(ignite != null)
                Ignite = new Spell.Targeted(ignite.Slot, 600);
            var smite = Player.Spells.FirstOrDefault(s => s.SData.Name.ToLower().Contains("smite"));
            if (smite != null)
                Smite = new Spell.Targeted(smite.Slot, 570);
        }

        public static void Initialize()
        {

        }

        public static bool HasIgnite()
        {
            return Ignite != null;
        }

        public static bool HasSmite()
        {
            return Smite != null;
        }

        public static bool HasChillingSmite()
        {

            return Smite != null &&
                   Smite.Name.Equals("s5_summonersmiteplayerganker", StringComparison.CurrentCultureIgnoreCase);
        }

        public static bool HasChallengingSmite()
        {
            return Smite != null &&
                   Smite.Name.Equals("s5_summonersmiteduel", StringComparison.CurrentCultureIgnoreCase);
        }

        public static float ERange()
        {
            return (new[] { 1500.0f, 2300.0f, 3100.0f, 3900.0f, 4700.0f }[SpellManager.E.Level - 1]);
        }
    }
}
