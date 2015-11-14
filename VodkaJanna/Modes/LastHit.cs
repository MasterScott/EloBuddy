using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using Settings = VodkaJanna.Config.Modes.LastHit;

namespace VodkaJanna.Modes
{
    public sealed class LastHit : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LastHit);
        }

        public override void Execute()
        {
            if (Settings.UseW && W.IsReady())
            {
                var minion = EntityManager.MinionsAndMonsters.EnemyMinions.Where(m => !m.IsDead && m.IsValid && !m.IsInvulnerable && m.IsInRange(Player.Instance.Position, W.Range) && m.Health <= Player.Instance.CalculateDamageOnUnit(m, DamageType.Magical, SpellManager.WRawDamage())).OrderByDescending(m => m.Health).FirstOrDefault();
                if (minion != null)
                {
                    W.Cast(minion);
                }
            }
        }
    }
}
