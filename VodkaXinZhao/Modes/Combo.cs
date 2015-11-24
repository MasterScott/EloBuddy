using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using Settings = VodkaXinZhao.Config.ModesMenu.Combo;
using SettingsMana = VodkaXinZhao.Config.ManaManagerMenu;

namespace VodkaXinZhao.Modes
{
    public sealed class Combo : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo);
        }

        public override void Execute()
        {

            if (Settings.UseE && E.IsReady() && PlayerMana >= SettingsMana.MinEMana)
            {
                var target = TargetSelector.GetTarget(E.Range, DamageType.Magical);
                if (target == null || Player.Instance.Distance(target) < Settings.MinEDistance)
                {
                    return;
                }
                Debug.WriteChat("Casting E in Combo, Target: {0}, Distance: {1}", target.ChampionName, "" + Player.Instance.Distance(target));
                E.Cast(target);
            }
            if (Settings.UseR && R.IsReady() && PlayerMana >= SettingsMana.MinRMana)
            {
                var enemies = EntityManager.Heroes.Enemies.Where(e => !e.IsDead && !e.IsRecalling() && !e.IsZombie && !e.IsInvulnerable && R.IsInRange(e)).ToList();
                if (enemies.Count() >= Settings.MinRTargets)
                {
                    Debug.WriteChat("Casting R in combo, Enemies in range: {0}", "" + enemies.Count());
                    R.Cast();
                    return;
                }
            }
        }
    }
}
