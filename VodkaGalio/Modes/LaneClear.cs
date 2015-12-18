using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using Settings = VodkaGalio.Config.ModesMenu.LaneClear;
using SettingsPrediction = VodkaGalio.Config.PredictionMenu;
using SettingsMana = VodkaGalio.Config.ManaManagerMenu;

namespace VodkaGalio.Modes
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
                if (Settings.UseQ && Q.IsReady() && PlayerMana >= SettingsMana.MinQMana)
                {
                    var minions =
                        EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, Player.Instance.Position, Q.Range).Where(
                            m => m.IsValidTarget());
                    var farmPos = EntityManager.MinionsAndMonsters.GetCircularFarmLocation(minions, Q.Width, (int)Q.Range);
                    if (farmPos.HitNumber >= Settings.MinQTargets)
                    {
                        Q.Cast(farmPos.CastPosition);
                    }
                }
                if (Settings.UseE && E.IsReady() && PlayerMana >= SettingsMana.MinEMana)
                {
                    var minions =
                        EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, Player.Instance.Position, E.Range).Where(
                            m => m.IsValidTarget());
                    var farmPos = EntityManager.MinionsAndMonsters.GetLineFarmLocation(minions, E.Width, (int)E.Range);
                    if (farmPos.HitNumber >= Settings.MinETargets)
                    {
                        Q.Cast(farmPos.CastPosition);
                    }
                }
            }
        }
    }
}
