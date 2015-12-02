using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using Settings = VodkaAzir.Config.ModesMenu.Flee;
using SettingsMana = VodkaAzir.Config.ManaManagerMenu;
using SettingsPrediction = VodkaAzir.Config.PredictionMenu;

namespace VodkaAzir.Modes
{
    public sealed class Flee : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee);
        }

        public override void Execute()
        {
            if (Settings.UseQWE && canQWE())
            {
                var cursorPos = Game.CursorPos;
                if (cursorPos.Distance(_PlayerPos) < 300)
                {
                    return;
                }
                W.Cast(_PlayerPos.Extend(cursorPos, W.Range).To3D());
                var qCastPos = _PlayerPos.Extend(cursorPos, Q.Range).To3D();
                Core.DelayAction(() => {
                    E.Cast();
                }, 200);
                Q.Cast(qCastPos);
                Debug.WriteChat("Casting QWE combo in Flee mode");
            }
            if (Settings.UseR && R.IsReady())
            {
                var target = TargetSelector.GetTarget(R.Range, DamageType.Magical);
                if (target != null)
                {
                    var pred = R.GetPrediction(target);
                    if (pred.HitChance >= HitChance.Low)
                    {
                        R.Cast(pred.CastPosition);
                        Debug.WriteChat("Casting R in Flee mode on {0}", target.ChampionName);
                    }
                }
            }
        }

        public bool canQWE()
        {
            return Q.IsReady() && W.IsReady() && E.IsReady() && PlayerManaExact >= 170;
        }
    }
}
