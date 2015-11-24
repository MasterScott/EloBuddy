using EloBuddy;
using EloBuddy.SDK;
using SharpDX;

namespace VodkaXinZhao.Modes
{
    public abstract class ModeBase
    {
        protected Spell.Active Q
        {
            get { return SpellManager.Q; }
        }
        protected Spell.Active W
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
        protected Spell.Targeted Ignite
        {
            get { return SpellManager.Ignite; }
        }

        protected float PlayerMana
        {
            get { return Player.Instance.ManaPercent; }
        }

        protected bool HasIgnite
        {
            get { return SpellManager.HasIgnite(); }
        }

        protected AIHeroClient _Player
        {
            get { return Player.Instance; }
        }

        protected Vector3 _PlayerPos
        {
            get { return Player.Instance.Position; }
        }

        public abstract bool ShouldBeExecuted();

        public abstract void Execute();
    }
}
