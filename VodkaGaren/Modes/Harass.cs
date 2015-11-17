using EloBuddy;
using EloBuddy.SDK;
using Settings = VodkaGaren.Config.ModesMenu.Harass;

namespace VodkaGaren.Modes
{
    public sealed class Harass : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            // Only execute this mode when the orbwalker is on harass mode
            //return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass);
            return false;
        }

        public override void Execute()
        {
            
        }
    }
}
