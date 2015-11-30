using EloBuddy;
using EloBuddy.SDK;
using SharpDX;

namespace VodkaAzir.Modes
{
    public abstract class ModeBase
    {
        protected Spell.Skillshot Q
        {
            get { return SpellManager.Q; }
        }
        protected Spell.Skillshot W
        {
            get { return SpellManager.W; }
        }
        protected Spell.Active E
        {
            get { return SpellManager.E; }
        }
        protected Spell.Skillshot R
        {
            get { return SpellManager.R; }
        }
        protected Spell.Targeted Ignite
        {
            get { return SpellManager.Ignite; }
        }

        protected float PlayerHealth
        {
            get { return Player.Instance.HealthPercent; }
        }

        protected float PlayerMana
        {
            get { return Player.Instance.ManaPercent; }
        }

        protected float PlayerManaExact
        {
            get { return Player.Instance.Mana; }
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
