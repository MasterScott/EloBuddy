using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using Settings = VodkaGalio.Config.ModesMenu.Combo;
using SettingsMana = VodkaGalio.Config.ManaManagerMenu;

namespace VodkaGalio.Modes
{
    public sealed class Combo : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo);
        }

        public override void Execute()
        {
            if (isUlting())
            {
                return;
            }
            if (Settings.UseR && R.IsReady() && PlayerMana >= SettingsMana.MinRMana)
            {
                var enemies = EntityManager.Heroes.Enemies.Where(e => !e.IsDead && !e.IsRecalling() && !e.IsZombie && !e.IsInvulnerable && R.IsInRange(e)).ToList();
                if (enemies.Count() >= Settings.MinRTargets)
                {
                    var wCasted = false;
                    if (Settings.UseW && W.IsReady() && Player.Instance.Mana >= 160 && PlayerMana >= SettingsMana.MinWMana)
                    {
                        W.Cast(Player.Instance);
                        wCasted = true;
                    }
                    Debug.WriteChat("Casting R{0} in combo, Enemies in range: {1}", wCasted ? "+W" : "", "" + enemies.Count());
                    R.Cast();
                    return;
                }
            }
            if (Settings.UseQ && Q.IsReady() && !isUlting() && PlayerMana >= SettingsMana.MinQMana)
            {
                var target = TargetSelector.GetTarget(Q.Range, DamageType.Magical);
                if (target == null)
                {
                    return;
                }
                var pred = Q.GetPrediction(target);
                if (pred.HitChance >= HitChance.Medium)
                {
                    Debug.WriteChat("Casting Q in Combo, Target: {0}, Distance: {1}, HitChance: {2}", target.ChampionName, ""+target.Distance(Player.Instance), pred.HitChance.ToString());
                    Q.Cast(pred.CastPosition);
                    return;
                }
            }
            if (Settings.UseE && E.IsReady() && !isUlting() && PlayerMana >= SettingsMana.MinEMana)
            {
                var target = TargetSelector.GetTarget(E.Range, DamageType.Magical);
                if (target == null)
                {
                    return;
                }
                var pred = E.GetPrediction(target);
                if (pred.HitChance >= HitChance.Medium)
                {
                    Debug.WriteChat("Casting E in Combo, Target: {0}, Distance: {1}, HitChance: {2}", target.ChampionName, "" + target.Distance(Player.Instance), pred.HitChance.ToString());
                    E.Cast(pred.CastPosition);
                }
            }
        }
    }
}
