using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using Settings = VodkaJanna.Config.ModesMenu.Combo;

namespace VodkaJanna.Modes
{
    public sealed class Combo : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo);
        }

        public override void Execute()
        {
            if (Settings.UseQ && QCastable())
            {
                var target = TargetSelector.GetTarget(Q.Range, DamageType.Magical);
                if (target == null)
                {
                    return;
                }
                var pred = Q.GetPrediction(target);
                if (pred.HitChance >= HitChance.Medium)
                {
                    Debug.WriteChat("Castin Q in Combo, Target: {0}, Distance: {1}, HitChance: {2}", target.ChampionName, ""+target.Distance(Player.Instance), pred.HitChance.ToString());
                    Q.Cast(pred.CastPosition);
                    Core.DelayAction(() => { Q.Cast(pred.CastPosition); }, 10);
                }
                
            }
            if (Settings.UseW && W.IsReady())
            {
                var target = TargetSelector.GetTarget(W.Range, DamageType.Magical);
                if (target == null)
                {
                    return;
                }
                W.Cast(target);
            }
        }
    }
}
