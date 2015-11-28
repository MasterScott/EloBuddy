using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using Settings = VodkaGalio.Config.ModesMenu.JungleClear;
using SettingsPrediction = VodkaGalio.Config.PredictionMenu;
using SettingsMana = VodkaGalio.Config.ManaManagerMenu;

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
            if (Settings.UseQ && Q.IsReady() && PlayerMana >= SettingsMana.MinQMana)
            {
                var monsters = EntityManager.MinionsAndMonsters.GetJungleMonsters(Player.Instance.Position, Q.Range).Where(t => t.IsValidTarget());
                foreach (var m in monsters)
                {
                    var pred = Q.GetPrediction(m);
                    if (pred.HitChance < SettingsPrediction.MinQHCJungleClear)
                    {
                        continue;
                    }
                    var cols =
                        pred.CollisionObjects.Count(t => t.IsValidTarget());
                    if (cols >= Settings.MinQTargets - 1)
                    {
                        Debug.WriteChat("Casting Q in JungleClear, Target: {0}, Distance: {1}, Collisions: {2}", m.Name, "" + m.Distance(Player.Instance), "" + (cols + 1));
                        Q.Cast(pred.CastPosition);
                        return;
                    }
                }
            }
            if (Settings.UseE && E.IsReady() && PlayerMana >= SettingsMana.MinEMana)
            {
                var monsters = EntityManager.MinionsAndMonsters.GetJungleMonsters(Player.Instance.Position, Q.Range).Where(t => t.IsValidTarget());
                foreach (var m in monsters)
                {
                    var pred = E.GetPrediction(m);
                    if (pred.HitChance < SettingsPrediction.MinEHCJungleClear)
                    {
                        continue;
                    }
                    var cols =
                        pred.CollisionObjects.Count(t => t.IsValidTarget());
                    if (cols >= Settings.MinQTargets - 1)
                    {
                        Debug.WriteChat("Casting E in JungleClear, Target: {0}, Distance: {1}, Collisions: {2}", m.Name, "" + m.Distance(Player.Instance), "" + (cols + 1));
                        E.Cast(pred.CastPosition);
                        return;
                    }
                }
            }
        }
    }
}
