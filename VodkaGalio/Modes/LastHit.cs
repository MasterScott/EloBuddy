using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using Settings = VodkaGalio.Config.ModesMenu.LastHit;
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
                var minion = EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, Player.Instance.Position, Q.Range).Where(m => !m.IsDead && m.IsValid && !m.IsInvulnerable && m.Distance(Player.Instance)> 300 && m.Health <= Player.Instance.CalculateDamageOnUnit(m, DamageType.Magical, Damages.QRawDamage())).OrderByDescending(m => m.Health).FirstOrDefault();
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
                var minion = EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, Player.Instance.Position, E.Range).Where(m => !m.IsDead && m.IsValid && !m.IsInvulnerable && m.Distance(Player.Instance) > 300 && m.Health <= Player.Instance.CalculateDamageOnUnit(m, DamageType.Magical, Damages.ERawDamage())).OrderByDescending(m => m.Health).FirstOrDefault();
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
