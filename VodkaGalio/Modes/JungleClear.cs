using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using Settings = VodkaGalio.Config.ModesMenu.JungleClear;

namespace VodkaGalio.Modes
{
    public sealed class JungleClear : ModeBase
    {
        public override bool ShouldBeExecuted()
        {

            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear);
        }

        public override void Execute()
        {
            if (Settings.UseQ && Q.IsReady())
            {
                var monsters = EntityManager.MinionsAndMonsters.GetJungleMonsters(Player.Instance.Position, Q.Range).Where(t => !t.IsDead && t.IsValid && !t.IsInvulnerable);
                foreach (var m in monsters)
                {
                    var cols =
                        Q.GetPrediction(m).CollisionObjects.Count(t => !t.IsDead && t.IsValid && !t.IsInvulnerable);
                    if (cols >= Settings.MinQTargets - 1)
                    {
                        Debug.WriteChat("Casting Q in JungleClear, Target: {0}, Distance: {1}, Collisions: {2}", m.Name, "" + m.Distance(Player.Instance), "" + (cols + 1));
                        Q.Cast(m);
                        return;
                    }
                }
            }
            if (Settings.UseE && E.IsReady())
            {
                var monsters = EntityManager.MinionsAndMonsters.GetJungleMonsters(Player.Instance.Position, Q.Range).Where(t => !t.IsDead && t.IsValid && !t.IsInvulnerable);
                foreach (var m in monsters)
                {
                    var cols =
                        E.GetPrediction(m).CollisionObjects.Count(t => t is Obj_AI_Minion && !t.IsDead && t.IsValid && !t.IsInvulnerable);
                    if (cols >= Settings.MinQTargets - 1)
                    {
                        Debug.WriteChat("Casting E in JungleClear, Target: {0}, Distance: {1}, Collisions: {2}", m.Name, "" + m.Distance(Player.Instance), "" + (cols + 1));
                        E.Cast(m);
                        return;
                    }
                }
            }
        }
    }
}
