using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using Settings = VodkaGalio.Config.ModesMenu.LastHit;
using SettingsPrediction = VodkaGalio.Config.PredictionMenu;
using SettingsMana = VodkaGalio.Config.ManaManagerMenu;

namespace VodkaGalio.Modes
{
    public sealed class LastHit : ModeBase
    {
        private static int lastQCast = Environment.TickCount; // Prevent Q into E spam

        public override bool ShouldBeExecuted()
        {
            
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LastHit);
        }

        public override void Execute()
        {
            if (Settings.UseQ && Q.IsReady() && PlayerMana >= SettingsMana.MinQMana)
            {
                var minion = EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, Player.Instance.Position, Q.Range).FirstOrDefault(m => m.IsValidTarget() && m.Distance(Player.Instance)> 300 && m.Health <= Damages.QDamage(m) && Q.GetPrediction(m).HitChance >= SettingsPrediction.MinQHCLastHit);
                if (minion != null)
                {
                    lastQCast = Environment.TickCount;
                    Debug.WriteChat("Casting Q in Last Hit, Target: {0}, Distance: {1}, Target HP: {2}", minion.Name, ""+minion.Distance(Player.Instance), ""+minion.Health);
                    Q.Cast(minion);
                    return;
                }
            }
            if (Settings.UseE && E.IsReady() && PlayerMana >= SettingsMana.MinEMana)
            {
                var minion = EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, Player.Instance.Position, E.Range).FirstOrDefault(m => m.IsValidTarget() && m.Distance(Player.Instance) > 300 && m.Health <= Damages.QDamage(m) && E.GetPrediction(m).HitChance >= SettingsPrediction.MinEHCLastHit);
                if (minion != null)
                {
                    if (Environment.TickCount - lastQCast < 1500)
                    {
                        return;
                    }
                    Debug.WriteChat("Casting E in Last Hit, Target: {0}, Distance: {1}, Target HP: {2}", minion.Name, "" + minion.Distance(Player.Instance), "" + minion.Health);
                    E.Cast(minion);
                    return;
                }
            }
        }
    }
}
