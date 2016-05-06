using EloBuddy.SDK;
using System.Linq;
using Settings = VodkaJax.Config.ModesMenu.LaneClear;
using SettingsMana = VodkaJax.Config.ManaManagerMenu;

namespace VodkaJax.Modes
{
    public sealed class LaneClear : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return false;
            //return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear);
        }

        public override void Execute()
        {
        }
    }
}
