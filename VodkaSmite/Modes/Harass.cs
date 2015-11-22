using EloBuddy;
using EloBuddy.SDK;

using Settings = VodkaSmite.Config;

namespace VodkaSmite.Modes
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
