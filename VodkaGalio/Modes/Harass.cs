using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using Settings = VodkaGalio.Config.ModesMenu.Harass;
using SettingsPrediction = VodkaGalio.Config.PredictionMenu;
using SettingsMana = VodkaGalio.Config.ManaManagerMenu;

namespace VodkaGalio.Modes
{
    public sealed class Harass : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
           
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass);
        }

        public override void Execute()
        {
            if (Settings.UseQ && Q.IsReady() && PlayerMana >= SettingsMana.MinQMana)
            {
                var target = TargetSelector.GetTarget(Q.Range, DamageType.Magical);
                if (target == null)
                {
                    return;
                }
                var pred = Q.GetPrediction(target);
                if (pred.HitChance >= SettingsPrediction.MinQHCHarass)
                {
                    Debug.WriteChat("Casting Q in Harass, Target: {0}, Distance: {1}, Prediction: {2}", target.ChampionName, "" + target.Distance(Player.Instance), pred.HitChance.ToString());
                    Q.Cast(pred.CastPosition);
                }

            }
            if (Settings.UseE && E.IsReady() && PlayerMana >= SettingsMana.MinEMana)
            {
                var target = TargetSelector.GetTarget(E.Range, DamageType.Magical);
                if (target == null)
                {
                    return;
                }
                var pred = E.GetPrediction(target);
                if (pred.HitChance >= SettingsPrediction.MinEHCHarass)
                {
                    Debug.WriteChat("Casting E in Harass, Target: {0}, Distance: {1}, HitChance: {2}", target.ChampionName, "" + target.Distance(Player.Instance), pred.HitChance.ToString());
                    E.Cast(pred.CastPosition);
                }
            }
        }
    }
}
