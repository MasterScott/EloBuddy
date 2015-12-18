using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using Settings = VodkaJanna.Config.ModesMenu.LaneClear;
using SettingsPrediction = VodkaJanna.Config.PredictionMenu;
using SettingsMana = VodkaJanna.Config.ManaManagerMenu;

namespace VodkaJanna.Modes
{
    public sealed class LaneClear : ModeBase
    {
        public override bool ShouldBeExecuted()
        {

            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear);
        }

        public override void Execute()
        {
            {
                if (Settings.UseQ && QCastable() && PlayerMana >= SettingsMana.MinQMana)
                {
                    var minions =
                        EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy,
                            Player.Instance.Position, Q.Range).Where(m => m.IsValidTarget());
                    var farmPos = EntityManager.MinionsAndMonsters.GetLineFarmLocation(minions, Q.Width, (int) Q.Range);
                    if (farmPos.HitNumber >= Settings.MinQTargets)
                    {
                        Q.Cast(farmPos.CastPosition);
                    }
                }
                if (Settings.UseW && W.IsReady() && PlayerMana >= SettingsMana.MinWMana)
                {
                    var minion =
                        EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy,
                            Player.Instance.Position, W.Range).Where(m => m.IsValidTarget()).OrderBy(m => m.Health).FirstOrDefault();
                    if (minion != null)
                    {
                        W.Cast(minion);
                    }
                }
            }
        }
    }
}
