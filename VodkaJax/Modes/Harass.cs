using EloBuddy;
using EloBuddy.SDK;
using Settings = VodkaJax.Config.ModesMenu.Harass;
using SettingsMana = VodkaJax.Config.ManaManagerMenu;

namespace VodkaJax.Modes
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
