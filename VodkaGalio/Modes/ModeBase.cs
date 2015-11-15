using EloBuddy;
using EloBuddy.SDK;

namespace VodkaGalio.Modes
{
    public abstract class ModeBase
    {
        protected Spell.Skillshot Q
        {
            get { return SpellManager.Q; }
        }
        protected Spell.Targeted W
        {
            get { return SpellManager.W; }
        }
        protected Spell.Skillshot E
        {
            get { return SpellManager.E; }
        }
        protected Spell.Active R
        {
            get { return SpellManager.R; }
        }

        protected bool isUlting()
        {
            return Player.Instance.Spellbook.IsChanneling || Player.Instance.HasBuff("GalioIdolOfDurand");
        }
        public abstract bool ShouldBeExecuted();

        public abstract void Execute();
    }
}
