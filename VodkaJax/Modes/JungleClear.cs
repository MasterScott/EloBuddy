using EloBuddy.SDK;
using System.Linq;
using Settings = VodkaJax.Config.ModesMenu.JungleClear;
using SettingsMana = VodkaJax.Config.ManaManagerMenu;

namespace VodkaJax.Modes
{
    public sealed class JungleClear : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return false;
            //return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear);
        }

        public override void Execute()
        {
        }
    }
}

