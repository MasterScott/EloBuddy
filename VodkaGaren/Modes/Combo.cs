using System;
using EloBuddy;
using EloBuddy.SDK;
using System.Linq;
using Settings = VodkaGaren.Config.ModesMenu.Combo;

namespace VodkaGaren.Modes
{
    public sealed class Combo : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo);
        }

        public override void Execute()
        {
            var target = TargetSelector.GetTarget(700, DamageType.Mixed);
            if (target == null)
            {
                return;
            }
            if (Settings.UseR && SpellManager.R.IsReady() && Damages.RDamage(target) > target.TotalShieldHealth() && SpellManager.R.IsInRange(target))
            {
                if (!target.HasBuffOfType(BuffType.SpellImmunity) && !target.HasBuffOfType(BuffType.SpellShield))
                {
                    SpellManager.R.Cast(target);
                    return;
                }
            }
            if (Q.IsReady() && Settings.UseQ &&
                    (Player.Instance.Distance(target) < 700))
            {
                Q.Cast();
            }

            if (Settings.UseW && W.IsReady())
            {
                int count = EntityManager.Heroes.Enemies.Count(enemy => enemy.IsValid && !enemy.IsDead && !enemy.IsZombie && !enemy.IsInvulnerable && enemy.Health > 0 && enemy.Distance(Player.Instance) <= 400);
                if (
                    count >= Settings.MinWEnemies)
                {
                    W.Cast();
                }
            }

            if (Settings.UseE && E.IsReady() && !Player.HasBuff("GarenE") && !Player.HasBuff("GarenQ") && (Player.Instance.Distance(target) < E.Range - 25))
            {
                E.Cast();
            }
        }
    }
}
