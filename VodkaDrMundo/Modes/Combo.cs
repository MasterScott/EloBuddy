using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using Settings = VodkaDrMundo.Config.ModesMenu.Combo;
using SettingsPrediction = VodkaDrMundo.Config.PredictionMenu;
using SettingsHealth = VodkaDrMundo.Config.HealthManagerMenu;

namespace VodkaDrMundo.Modes
{
    public sealed class Combo : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo);
        }

        public override void Execute()
        {
            if (Settings.UseQ && Q.IsReady() && PlayerHealth >= SettingsHealth.MinQHealth)
            {
                var target = TargetSelector.GetTarget(Settings.MaxQDistance, DamageType.Magical);
                if (target != null)
                {
                    var pred = Q.GetPrediction(target);
                    if (pred.HitChance >= SettingsPrediction.MinQHCCombo)
                    {
                        Q.Cast(pred.CastPosition);
                        Debug.WriteChat("Casting Q in Combo, Target: {0}, HitChance: {1}", target.ChampionName, pred.HitChance.ToString());
                    }
                }
            }
            if (Settings.UseW && W.IsReady() && !WActive && PlayerHealth >= SettingsHealth.MinWHealth)
            {
                var enemy =
                    EntityManager.Heroes.Enemies
                        .FirstOrDefault(e => e.IsValidTarget(W.Range));
                if (enemy != null)
                {
                    W.Cast();
                    Debug.WriteChat("Casting W in Combo");
                }
            }
        }
    }
}
