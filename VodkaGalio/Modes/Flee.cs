using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using Settings = VodkaGalio.Config.ModesMenu.Flee;

namespace VodkaGalio.Modes
{
    public sealed class Flee : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee);
        }

        public override void Execute()
        {
            if (Settings.UseQ && Q.IsReady())
            {
                var target = TargetSelector.GetTarget(Q.Range, DamageType.Magical);
                if (target == null)
                {
                    return;
                }
                var pred = Q.GetPrediction(target);
                if (pred.HitChance >= HitChance.Medium)
                {
                    Debug.WriteChat("Casting Q in Flee, Target: {0}, Distance: {1}, Prediction: {2}", target.ChampionName, "" + target.Distance(Player.Instance), pred.HitChance.ToString());
                    Q.Cast(target);
                    return;
                }

            }
            if (Settings.UseE && E.IsReady())
            {
                var cursorPos = Game.CursorPos;
                if (Player.Instance.Position.Distance(cursorPos) > 200)
                {
                    E.Cast(cursorPos);
                }
            }
        }
    }
}
