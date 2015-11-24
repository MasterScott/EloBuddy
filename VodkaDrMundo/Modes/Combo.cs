using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using Settings = VodkaDrMundo.Config.ModesMenu.Combo;
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
                if (target == null)
                {
                    return;
                }
                var pred = Q.GetPrediction(target);
                if (pred.HitChance >= HitChance.Medium)
                {
                    Q.Cast(pred.CastPosition);
                    Debug.WriteChat("Casting Q in Combo, Target: {0}", target.ChampionName);
                }
            }
            if (Settings.UseW && W.IsReady() && !WActive && PlayerHealth >= SettingsHealth.MinWHealth)
            {
                var enemy =
                    EntityManager.Heroes.Enemies
                        .FirstOrDefault(e => !e.IsDead && e.Health > 0 && e.IsVisible && e.IsValidTarget() && _Player.Distance(e) < W.Range);
                if (enemy != null)
                {
                    W.Cast();
                    Debug.WriteChat("Casting W in Combo");
                }
            }
        }
    }
}
