using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using Settings = VodkaXinZhao.Config.ModesMenu.Harass;
using SettingsMana = VodkaXinZhao.Config.ManaManagerMenu;

namespace VodkaXinZhao.Modes
{
    public sealed class Harass : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return false;
            //return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass);
        }

        public override void Execute()
        {
            
        }
    }
}
