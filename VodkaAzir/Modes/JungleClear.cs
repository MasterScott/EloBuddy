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
            var monsters = EntityManager.MinionsAndMonsters.GetJungleMonsters(_PlayerPos, 800.0f).Where(m => m.IsValidTarget()).ToList();
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
                foreach (var soldier in Orbwalker.AzirSoldiers)
                {
                    var farmLoc = EntityManager.MinionsAndMonsters.GetLineFarmLocation(monsters, Q.Width, 300,
                        soldier.Position.To2D());
                    if (farmLoc.HitNumber >= Settings.MinQTargets)
                    {
                        Q.Cast(farmLoc.CastPosition);
                        Debug.WriteChat("Casting Q in Jungle Clear on {0} minions.", farmLoc.HitNumber.ToString());
                        break;
                    }
                }
            }
        }
    }
}

