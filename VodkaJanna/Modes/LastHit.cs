using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using Settings = VodkaJanna.Config.ModesMenu.LastHit;
using SettingsPrediction = VodkaJanna.Config.PredictionMenu;
using SettingsMana = VodkaJanna.Config.ManaManagerMenu;

namespace VodkaJanna.Modes
{
    public sealed class LastHit : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LastHit);
        }

        public override void Execute()
        {
            if (Settings.UseW && W.IsReady() && PlayerMana >= SettingsMana.MinWMana)
            {
                var minion =
                        EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy,
                            Player.Instance.Position, W.Range).Where(m => m.IsValidTarget() && m.Health <= Damages.WDamage(m)).OrderByDescending(m => m.Health).FirstOrDefault();
                if (minion != null)
                {
                    Debug.WriteChat("Casting W in Last Hit, Target: {0}, Distance: {1}, Target HP: {2}", minion.Name, ""+minion.Distance(Player.Instance), ""+minion.Health);
                    W.Cast(minion);
                }
            }
        }
    }
}
