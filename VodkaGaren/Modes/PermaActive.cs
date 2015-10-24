using System.Linq;
using EloBuddy;
using EloBuddy.SDK;

namespace VodkaGaren.Modes
{
    public sealed class PermaActive : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return true;
        }

        public override void Execute()
        {
            // Kill Steal
            foreach (var enemy in EntityManager.Heroes.Enemies.Where(e => e.IsEnemy && e.IsVisible && !e.IsDead && !e.IsZombie && e.Health > 0))
            {
                if (SpellManager.R.IsLearned && Config.Modes.KillSteal.UseR && SpellManager.R.IsReady() && Damages.RDamage(enemy) > enemy.Health && SpellManager.R.IsInRange(enemy))
                {
                    if (enemy.HasBuffOfType(BuffType.SpellImmunity) || enemy.HasBuffOfType(BuffType.SpellShield))
                    {
                        continue;
                    }
                    SpellManager.R.Cast(enemy);
                    return;
                }

                if (Config.Modes.KillSteal.UseIgnite && SpellManager.Ignite.IsReady() &&
                    Damages.IgniteDmg(enemy) > enemy.Health && SpellManager.Ignite.IsInRange(enemy))
                {
                    SpellManager.Ignite.Cast(enemy);
                    return;
                }
            }

        }
    }
}
