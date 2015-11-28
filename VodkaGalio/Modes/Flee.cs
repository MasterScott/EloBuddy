using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using SharpDX;
using Settings = VodkaGalio.Config.ModesMenu.Flee;
using SettingsPrediction = VodkaGalio.Config.PredictionMenu;
using SettingsMana = VodkaGalio.Config.ManaManagerMenu;

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
            if (Settings.UseQ && Q.IsReady() && PlayerMana >= SettingsMana.MinQMana)
            {
                var target = TargetSelector.GetTarget(Q.Range, DamageType.Magical);
                if (target != null)
                {
                    var pred = Q.GetPrediction(target);
                    if (pred.HitChance >= SettingsPrediction.MinQHCFlee)
                    {
                        Debug.WriteChat("Casting Q in Flee, Target: {0}, Distance: {1}, HitChance: {2}",
                            target.ChampionName, "" + target.Distance(Player.Instance), pred.HitChance.ToString());
                        Q.Cast(target);
                        return;
                    }
                }

            }
            if (Settings.UseE && E.IsReady() && PlayerMana >= SettingsMana.MinEMana)
            {
                var cursorPos = Game.CursorPos;
                var playerPos = Player.Instance.Position;
                if (playerPos.Distance(cursorPos) > 300)
                {
                    E.Cast(new Vector3(playerPos.Extend(cursorPos, 450), cursorPos.Z));
                }
            }
        }
    }
}
