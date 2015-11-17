using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX.Direct3D9;
using Settings = VodkaGalio.Config.ModesMenu.LaneClear;
using SettingsMana = VodkaGalio.Config.ManaManagerMenu;

namespace VodkaGalio.Modes
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
                if (Settings.UseQ && Q.IsReady() && PlayerMana >= SettingsMana.MinQMana)
                {
                    var minions =
                        EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, Player.Instance.Position, Q.Range).Where(
                            m => !m.IsDead && m.IsValid && !m.IsInvulnerable);

                    foreach (var m in minions)
                    {
                        var cols =
                            Q.GetPrediction(m).CollisionObjects.Count(t => t.IsEnemy && !t.IsDead && t.IsValid && !t.IsInvulnerable && t is Obj_AI_Minion);
                        if (cols >= Settings.MinQTargets - 1)
                        {
                            Debug.WriteChat("Casting Q in LaneClear, Target: {0}, Distance: {1}, Collisions: {2}", m.Name, "" + m.Distance(Player.Instance), "" + (cols + 1));
                            Q.Cast(m);
                            return;
                        }

                    }
                }
                if (Settings.UseE && E.IsReady() && PlayerMana >= SettingsMana.MinEMana)
                {
                    var minions =
                        EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, Player.Instance.Position, E.Range).Where(
                            m => !m.IsDead && m.IsValid && !m.IsInvulnerable);

                    foreach (var m in minions)
                    {
                        var cols =
                            E.GetPrediction(m).CollisionObjects.Count(t => t.IsEnemy && !t.IsDead && t.IsValid && !t.IsInvulnerable && t is Obj_AI_Minion);
                        if (cols >= Settings.MinQTargets - 1)
                        {
                            Debug.WriteChat("Casting E in LaneClear, Target: {0}, Distance: {1}, Collisions: {2}", m.Name, "" + m.Distance(Player.Instance), "" + (cols + 1));
                            E.Cast(m);
                            return;
                        }

                    }
                }
            }
        }
    }
}
