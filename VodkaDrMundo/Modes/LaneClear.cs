using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using SharpDX.Direct3D9;
using Settings = VodkaDrMundo.Config.ModesMenu.LaneClear;
using SettingsCombo = VodkaDrMundo.Config.ModesMenu.Combo;
using SettingsHealth = VodkaDrMundo.Config.HealthManagerMenu;

namespace VodkaDrMundo.Modes
{
    public sealed class LaneClear : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear);
        }

        public override void Execute()
        {
            if (Settings.UseQ && Q.IsReady() && PlayerHealth >= SettingsHealth.MinQHealth)
            {
                var minion =
                EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, _PlayerPos, Q.Range)
                    .Where(e => e.IsValidTarget(SettingsCombo.MaxQDistance) && Q.GetPrediction(e).HitChance >= HitChance.Low).OrderByDescending(e => e.Health).FirstOrDefault();
                if (minion != null)
                {
                    var pred = Q.GetPrediction(minion);
                    if (pred.HitChance >= HitChance.Low)
                    {
                        Q.Cast(pred.CastPosition);
                        Debug.WriteChat("Casting Q in LaneClear");
                    }
                }

            }
            if (Settings.UseW && W.IsReady() && !WActive && PlayerHealth >= SettingsHealth.MinWHealth)
            {
                var minion =
                    EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, _PlayerPos, W.Range)
                        .FirstOrDefault(e => e.IsValidTarget());
                if (minion != null)
                {
                    W.Cast();
                    Debug.WriteChat("Casting W in LaneClear");
                }
            }
        }
    }
}
