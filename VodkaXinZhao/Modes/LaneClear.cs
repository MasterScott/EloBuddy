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
                    EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy,
                        Player.Instance.Position, E.Range);
                foreach (var m in minions)
                {
                    var around = EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy,
                        m.Position, 100.0f).Count();
                    if (around >= Settings.MinETargets)
                    {
                        E.Cast(m);
                        Debug.WriteChat("Casting E in LaneClear on {0} enemy minions", "" + around);
                        return;
                    }
                }
            }
        }
    }
}
