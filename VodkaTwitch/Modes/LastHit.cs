using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using SettingsMana = VodkaTwitch.Config.ManaManagerMenu;

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
