using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using SharpDX;
using Settings = VodkaXinZhao.Config.ModesMenu.Flee;
using SettingsMana = VodkaXinZhao.Config.ManaManagerMenu;

namespace VodkaXinZhao.Modes
{
    public sealed class Flee : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee);
        }

        public override void Execute()
        {
            
        }
    }
}
