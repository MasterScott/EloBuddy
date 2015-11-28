using SettingsMana = VodkaTwitch.Config.ManaManagerMenu;
using SettingsPrediction = VodkaTwitch.Config.PredictionMenu;

namespace VodkaTwitch.Modes
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
