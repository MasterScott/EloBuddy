using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using Settings = VodkaJanna.Config.Modes.JungleClear;

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
            if (Settings.UseQ && QCastable())
            {
                var monsters = EntityManager.MinionsAndMonsters.GetJungleMonsters(Player.Instance.Position, Q.Range).Where(t => !t.IsDead && t.IsValid && !t.IsInvulnerable);
                foreach (var m in monsters)
                {
                    if (Q.GetPrediction(m).CollisionObjects.Count(t => !t.IsDead && t.IsValid && !t.IsInvulnerable) >= Settings.MinQTargets - 1)
                    {
                        Q.Cast(m);
                        Core.DelayAction(() => { Q.Cast(m); }, 10);
                        return;
                    }
                }
            }
            if (Settings.UseW && W.IsReady())
            {
                var monster = EntityManager.MinionsAndMonsters.GetJungleMonsters(Player.Instance.Position, W.Range).Where(m => !m.IsDead && m.IsValid && !m.IsInvulnerable).OrderByDescending(m => m.Health).FirstOrDefault();
                if (monster != null)
                {
                    W.Cast(monster);
                }
            }
        }
    }
}
