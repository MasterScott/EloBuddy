using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using SettingsMana = VodkaXinZhao.Config.ManaManagerMenu;

namespace VodkaXinZhao.Modes
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
