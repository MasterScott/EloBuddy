using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using Settings = VodkaXinZhao.Config.ModesMenu.JungleClear;
using SettingsMana = VodkaXinZhao.Config.ManaManagerMenu;

namespace VodkaXinZhao.Modes
{
    public sealed class JungleClear : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
           
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear);
        }

        public override void Execute()
        {
            if (Settings.UseE && E.IsReady() && PlayerMana >= SettingsMana.MinEMana)
            {
                var monsters =
                EntityManager.MinionsAndMonsters.GetJungleMonsters(_PlayerPos, E.Range)
                    .Where(e => e.IsValidTarget());
                foreach (var m in monsters)
                {
                    var around = EntityManager.MinionsAndMonsters.GetJungleMonsters(m.Position, 100)
                        .Count(e => e.IsValidTarget());
                    if (around >= Settings.MinETargets)
                    {
                        E.Cast(m);
                        Debug.WriteChat("Casting E in JungleClearClear on {0} enemies", "" + around);
                        return;
                    }
                }
            }
        }
    }
}
