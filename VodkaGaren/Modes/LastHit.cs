using EloBuddy;
using EloBuddy.SDK;
using Settings = VodkaGaren.Config.ModesMenu.LastHit;

namespace VodkaGaren.Modes
{
    public sealed class LastHit : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            // Only execute this mode when the orbwalker is on lasthit mode
            //return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LastHit);
            return false;
        }

        public override void Execute()
        {
           
        }
    }
}
