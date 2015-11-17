using EloBuddy.SDK;
using Settings = VodkaGaren.Config.ModesMenu.JungleClear;

namespace VodkaGaren.Modes
{
    public sealed class JungleClear : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            // Only execute this mode when the orbwalker is on jungleclear mode
            //return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear);
            return false;
        }

        public override void Execute()
        {
            // TODO: Add jungleclear logic here
        }
    }
}
