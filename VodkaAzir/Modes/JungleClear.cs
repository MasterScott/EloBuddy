using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using Settings = VodkaAzir.Config.ModesMenu.JungleClear;
using SettingsMana = VodkaAzir.Config.ManaManagerMenu;
using SettingsPrediction = VodkaAzir.Config.PredictionMenu;

namespace VodkaAzir.Modes
{
    public sealed class JungleClear : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear);
        }

        public override void Execute()
        {
            var monsters = EntityManager.MinionsAndMonsters.GetJungleMonsters(_PlayerPos, 1000.0f).ToList();
            if (Orbwalker.AzirSoldiers.Count < Settings.MaxSoldiersForFarming && Settings.UseW && W.IsReady() && PlayerMana >= SettingsMana.MinWMana)
            {
                var farmLoc = EntityManager.MinionsAndMonsters.GetCircularFarmLocation(monsters, W.Width, (int)W.Range);
                if (farmLoc.HitNumber >= Settings.MinWTargets)
                {
                    W.Cast(farmLoc.CastPosition);
                    Debug.WriteChat("Casting W in Jungle Clear on {0} minions.", farmLoc.HitNumber.ToString());
                }
            }

            if (Orbwalker.AzirSoldiers.Count > 0 && Settings.UseQ && Q.IsReady() && PlayerMana >= SettingsMana.MinQMana)
            {
                foreach (var monster in monsters)
                {
                    var qCasted = false;
                    foreach (var soldier in Orbwalker.AzirSoldiers)
                    {
                        var pred = Prediction.Position.PredictLinearMissile(soldier, Q.Range, (int)soldier.BoundingRadius * 2, Q.CastDelay,
                            Q.Speed, Int32.MaxValue, soldier.Position);
                        var cols = pred.CollisionObjects.Count();
                        if (cols >= Settings.MinQTargets - 1)
                        {
                            Q.Cast(monster);
                            Debug.WriteChat("Casting Q in Jungle Clear on {0} minions.", (cols+1).ToString());
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

