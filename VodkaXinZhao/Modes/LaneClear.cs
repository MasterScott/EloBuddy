using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using Settings = VodkaXinZhao.Config.ModesMenu.LaneClear;
using SettingsMana = VodkaXinZhao.Config.ManaManagerMenu;

namespace VodkaXinZhao.Modes
{
    public sealed class LaneClear : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear);
        }

        public override void Execute()
        {
            if (Settings.UseE && E.IsReady() && PlayerMana >= SettingsMana.MinEMana)
            {
                var minions =
                    EntityManager.MinionsAndMonsters.GetLaneMinions()
                        .Where(e => !e.IsDead && e.Health > 0 && !e.IsInvulnerable && e.IsVisible);
                foreach (var m in minions)
                {
                    var around = EntityManager.MinionsAndMonsters.GetLaneMinions()
                        .Count(e => !e.IsDead && e.Health > 0 && !e.IsInvulnerable && e.IsVisible && m.Distance(e) <= 100);
                    if (around >= Settings.MinETargets)
                    {
                        Debug.WriteChat("Casting E in LaneClear on {0} enemies", ""+around);
                        E.Cast(m);
                        return;
                    }
                }
            }
        }
    }
}
