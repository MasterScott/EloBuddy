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
                if (target != null && Player.Instance.Distance(target) >= Settings.MinEDistance)
                {

                    E.Cast(target);
                    Debug.WriteChat("Casting E in Combo, Target: {0}, Distance: {1}", target.ChampionName, "" + Player.Instance.Distance(target));
                }
            }
            if (Settings.UseR && R.IsReady() && PlayerMana >= SettingsMana.MinRMana)
            {
                var enemiesCount = EntityManager.Heroes.Enemies.Count(e => e.IsValidTarget(R.Range));
                if (enemiesCount >= Settings.MinRTargets)
                {
                    R.Cast();
                    Debug.WriteChat("Casting R in combo, Enemies in range: {0}", "" + enemiesCount);
                }
            }
        }
    }
}
