using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using Settings = VodkaGalio.Config.ModesMenu.Combo;

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
            if (Settings.UseR && R.IsReady())
            {
                var enemies = EntityManager.Heroes.Enemies.Where(e => !e.IsDead && !e.IsRecalling() && !e.IsZombie && !e.IsInvulnerable && R.IsInRange(e)).ToList();
                if (enemies.Count() >= Settings.MinRTargets)
                {
                    if (Settings.UseW && W.IsReady() && Player.Instance.Mana >= 160)
                    {
                        W.Cast(Player.Instance);
                    }
                    Debug.WriteChat("Casting R in combo, Enemies in range: {0}", "" + "" + enemies.Count());
                    R.Cast();
                    return;
                }
            }
            if (Settings.UseQ && Q.IsReady() && !isUlting())
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
            if (Settings.UseE && E.IsReady() && !isUlting())
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
