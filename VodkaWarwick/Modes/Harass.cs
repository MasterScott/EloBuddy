using EloBuddy;
using EloBuddy.SDK;
using Settings = VodkaWarwick.Config.ModesMenu.Harass;
using SettingsMana = VodkaWarwick.Config.ManaManagerMenu;

namespace VodkaWarwick.Modes
{
    public sealed class Harass : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass);
        }

        public override void Execute()
        {
            if (Settings.UseQ && Q.IsReady() && PlayerMana >= SettingsMana.MinQMana)
            {
                var target = TargetSelector.GetTarget(Q.Range, DamageType.Magical);
                if (target != null && target.IsValidTarget() && !target.HasBuffOfType(BuffType.SpellImmunity) &&
                    !target.HasBuffOfType(BuffType.SpellShield))
                {
                    Q.Cast(target);
                    Debug.WriteChat("Casting Q in Harass on {0}", target.ChampionName);
                }
            }
        }
    }
}
