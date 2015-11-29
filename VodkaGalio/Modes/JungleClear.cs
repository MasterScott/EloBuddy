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
                var farmPos = EntityManager.MinionsAndMonsters.GetCircularFarmLocation(monsters, Q.Width, (int) Q.Range);
                if (farmPos.HitNumber >= Settings.MinQTargets)
                {
                    Q.Cast(farmPos.CastPosition);
                    Debug.WriteChat("Casting Q in JungleClear, Targets: {0}", ""+farmPos.HitNumber);
                    return;
                }
            }
            if (Settings.UseE && E.IsReady() && PlayerMana >= SettingsMana.MinEMana)
            {
                var monsters = EntityManager.MinionsAndMonsters.GetJungleMonsters(Player.Instance.Position, E.Range).Where(t => t.IsValidTarget());
                var farmPos = EntityManager.MinionsAndMonsters.GetLineFarmLocation(monsters, E.Width, (int)E.Range);
                if (farmPos.HitNumber >= Settings.MinQTargets)
                {
                    E.Cast(farmPos.CastPosition);
                    Debug.WriteChat("Casting E in JungleClear, Targets: {0}", "" + farmPos.HitNumber);
                    return;
                }
            }
        }
    }
}
