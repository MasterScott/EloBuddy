using EloBuddy.SDK;
using SettingsMana = VodkaTristana.Config.ManaManagerMenu;
using SettingsPrediction = VodkaTristana.Config.PredictionMenu;

namespace VodkaTristana.Modes
{
    public sealed class LastHit : ModeBase
    {

        public override bool ShouldBeExecuted()
        {
            return false;
            //return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LastHit);
        }

        public override void Execute()
        {
           
        }
    }
}
