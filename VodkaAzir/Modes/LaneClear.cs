using System;
using System.Linq;
using EloBuddy.SDK;
using Settings = VodkaAzir.Config.ModesMenu.LaneClear;
using SettingsMana = VodkaAzir.Config.ManaManagerMenu;
using SettingsPrediction = VodkaAzir.Config.PredictionMenu;

namespace VodkaAzir.Modes
{
    public sealed class LaneClear : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear);
        }

        public override void Execute()
        {
            var minions = EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, _PlayerPos, 1000.0f).ToList();
            if (Orbwalker.AzirSoldiers.Count < Settings.MaxSoldiersForFarming && Settings.UseW && W.IsReady() && PlayerMana >= SettingsMana.MinWMana)
            {
                var farmLoc = EntityManager.MinionsAndMonsters.GetCircularFarmLocation(minions, W.Width, (int)W.Range);
                if (farmLoc.HitNumber >= Settings.MinWTargets)
                {
                    W.Cast(farmLoc.CastPosition);
                }
            }

            if (Orbwalker.AzirSoldiers.Count > 0 && Settings.UseQ && Q.IsReady() && PlayerMana >= SettingsMana.MinQMana)
            {
                foreach (var minion in minions)
                {
                    var qCasted = false;
                    foreach (var soldier in Orbwalker.AzirSoldiers)
                    {
                        var pred = Prediction.Position.PredictLinearMissile(soldier, Q.Range, (int)soldier.BoundingRadius * 2, Q.CastDelay,
                            Q.Speed, Int32.MaxValue, soldier.Position);
                        if (pred.CollisionObjects.Count() >= Settings.MinQTargets - 1)
                        {
                            Q.Cast(minion);
                            qCasted = true;
                            break;
                        }
                    }
                    if (qCasted)
                    {
                        break;
                    }
                }
            }
        }
    }
}
