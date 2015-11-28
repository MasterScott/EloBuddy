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
                foreach (var m in monsters)
                {
                    var pred = Q.GetPrediction(m);
                    if (pred.HitChance < SettingsPrediction.MinQHCJungleClear)
                    {
                        continue;
                    }
                    var cols = pred.CollisionObjects.Count(t => t.IsValidTarget());
                    if (cols >= Settings.MinQTargets - 1)
                    {
                        Debug.WriteChat("Casting Q in JungleClear, Target: {0}, Distance: {1}, Collisions: {2}", m.Name, "" + m.Distance(Player.Instance), "" + (cols + 1));
                        Q.Cast(pred.CastPosition);
                        Core.DelayAction(() => { Q.Cast(pred.CastPosition); }, 10);
                        return;
                    }
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
