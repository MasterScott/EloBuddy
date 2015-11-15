using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX.Direct3D9;
using Settings = VodkaJanna.Config.ModesMenu.LaneClear;

namespace VodkaJanna.Modes
{
    public sealed class LaneClear : ModeBase
    {
        public override bool ShouldBeExecuted()
        {

            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear);
        }

        public override void Execute()
        {
            {
                if (Settings.UseQ && QCastable())
                {
                    var minions =
                        EntityManager.MinionsAndMonsters.EnemyMinions.Where(
                            m =>
                                m.IsEnemy && !m.IsDead && m.IsValid && !m.IsInvulnerable &&
                                m.IsInRange(Player.Instance.Position, Q.Range));

                    foreach (var m in minions)
                    {
                        var cols =
                            Q.GetPrediction(m).CollisionObjects.Count(t => t.IsEnemy && !t.IsDead && t.IsValid && !t.IsInvulnerable);
                        if (cols >= Settings.MinQTargets - 1)
                        {
                            Debug.WriteChat("Castin Q in LaneClear, Target: {0}, Distance: {1}, Collisions: {2}", m.Name, "" + m.Distance(Player.Instance), "" + (cols+1));
                            Q.Cast(m);
                            Core.DelayAction(() => { Q.Cast(m); }, 10);
                            return;
                        }

                    }
                }
                if (Settings.UseW && W.IsReady())
                {
                    var minion = EntityManager.MinionsAndMonsters.EnemyMinions.Where(m => !m.IsDead && m.IsValid && !m.IsInvulnerable && W.IsInRange(m) && m.Health <= Player.Instance.CalculateDamageOnUnit(m, DamageType.Magical, SpellManager.WRawDamage())).OrderByDescending(m => m.Health).FirstOrDefault();
                    if (minion != null)
                    {
                        W.Cast(minion);
                    }
                }
            }
        }
    }
}
