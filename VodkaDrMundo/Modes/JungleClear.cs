using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using Settings = VodkaDrMundo.Config.ModesMenu.JungleClear;
using SettingsCombo = VodkaDrMundo.Config.ModesMenu.Combo;
using SettingsPrediction = VodkaDrMundo.Config.PredictionMenu;
using SettingsHealth = VodkaDrMundo.Config.HealthManagerMenu;

namespace VodkaDrMundo.Modes
{
    public sealed class JungleClear : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
           
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear);
        }

        public override void Execute()
        {
            if (Settings.UseQ && Q.IsReady() && PlayerHealth >= SettingsHealth.MinQHealth)
            {
                var monster =
                EntityManager.MinionsAndMonsters.GetJungleMonsters(_PlayerPos, SettingsCombo.MaxQDistance)
                    .Where(e => e.IsValidTarget(SettingsCombo.MaxQDistance) && Q.GetPrediction(e).HitChance >= HitChance.Low).OrderByDescending(e => e.Health).FirstOrDefault();
                if (monster != null)
                {
                    var pred = Q.GetPrediction(monster);
                    if (pred.HitChance >= SettingsPrediction.MinQHCJungleClear)
                    {
                        Q.Cast(pred.CastPosition);
                        Debug.WriteChat("Casting Q in JungleClear");
                    }
                }
                
            }
            if (Settings.UseW && W.IsReady() && !WActive && PlayerHealth >= SettingsHealth.MinWHealth)
            {
                var monster =
                    EntityManager.MinionsAndMonsters.GetJungleMonsters(_PlayerPos, W.Range)
                        .FirstOrDefault(e => e.IsValidTarget(SettingsCombo.MaxQDistance));
                if (monster != null)
                {
                    W.Cast();
                    Debug.WriteChat("Casting W in JungleClear");
                }
            }
        }
    }
}
