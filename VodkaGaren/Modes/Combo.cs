using EloBuddy;
using EloBuddy.SDK;
using System.Linq;

// Using the config like this makes your life easier, trust me
using Settings = VodkaGaren.Config.Modes.Combo;

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
            int count = EntityManager.Heroes.Enemies.Count(enemy => enemy.IsValid && !enemy.IsDead && enemy.Distance(Player.Instance) <= 400);
            if (Config.Modes.Combo.UseR && SpellManager.R.IsReady() && Damages.RDamage(target) > target.Health && SpellManager.R.IsInRange(target))
            {
                if (!target.HasBuffOfType(BuffType.SpellImmunity) && !target.HasBuffOfType(BuffType.SpellShield))
                {
                    SpellManager.R.Cast(target);
                    return;
                }
            }
            if (Q.IsReady() && Config.Modes.Combo.UseQ &&
                    (ObjectManager.Player.Distance(target) < 700))
            {
                Q.Cast();
            }

            if (W.IsReady() && Config.Modes.Combo.UseW &&
                count >= Config.Modes.Combo.MinWEnemies)
            {
                W.Cast();
            }

            if (E.IsReady() && Config.Modes.Combo.UseE && !Player.HasBuff("GarenQ") &&
                !Player.HasBuff("GarenE") && (ObjectManager.Player.Distance(target) < E.Range))
            {
                E.Cast();
            }
        }
    }
}
