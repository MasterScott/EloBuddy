using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using Settings = VodkaTristana.Config.ModesMenu.Harass;
using SettingsMana = VodkaTristana.Config.ManaManagerMenu;
using SettingsPrediction = VodkaTristana.Config.PredictionMenu;

namespace VodkaTristana.Modes
{
    public sealed class Harass : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return false; // Doing it all in Events
            //return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass);
        }

        public override void Execute()
        {
        }
    }
}
