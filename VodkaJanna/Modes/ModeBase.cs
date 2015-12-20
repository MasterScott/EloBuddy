using EloBuddy;
using EloBuddy.SDK;

namespace VodkaJanna.Modes
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
        protected Spell.Targeted E
        {
            get { return SpellManager.E; }
        }
        protected Spell.Active R
        {
            get { return SpellManager.R; }
        }

        protected float PlayerMana
        {
            get { return Player.Instance.ManaPercent; }
        }

        protected bool QCastable()
        {
            return SpellManager.Q.IsReady() && Player.Instance.Spellbook.GetSpell(SpellSlot.Q).ToggleState != 2;
        }

        protected bool IsUlting
        {
            get { return SpellManager.IsUlting(); }
        }

        public abstract bool ShouldBeExecuted();

        public abstract void Execute();
    }
}
