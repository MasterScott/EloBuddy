using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using Settings = VodkaJanna.Config.ModesMenu.JungleClear;
using SettingsPrediction = VodkaJanna.Config.PredictionMenu;
using SettingsMana = VodkaJanna.Config.ManaManagerMenu;

namespace VodkaJanna.Modes
{
    public sealed class JungleClear : ModeBase
    {
        public override bool ShouldBeExecuted()
        {

            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear);
        }

        public override void Execute()
        {
            if (Settings.UseQ && QCastable() && PlayerMana >= SettingsMana.MinQMana)
            {
                var monsters = EntityManager.MinionsAndMonsters.GetJungleMonsters(Player.Instance.Position, Q.Range).Where(t => t.IsValidTarget());
                var farmPos = EntityManager.MinionsAndMonsters.GetLineFarmLocation(monsters, Q.Width, (int)Q.Range);
                if (farmPos.HitNumber >= Settings.MinQTargets)
                {
                    Q.Cast(farmPos.CastPosition);
                }
            }
            if (Settings.UseW && W.IsReady() && PlayerMana >= SettingsMana.MinWMana)
            {
                var monster = EntityManager.MinionsAndMonsters.GetJungleMonsters(Player.Instance.Position, W.Range).Where(m => m.IsValidTarget()).OrderByDescending(m => m.MaxHealth).FirstOrDefault();
                if (monster != null)
                {
                    W.Cast(monster);
                }
            }
        }
    }
}
