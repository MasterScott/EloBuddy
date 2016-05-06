using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using Settings = VodkaJax.Config.ModesMenu.Flee;

namespace VodkaJax.Modes
{
    public sealed class Flee : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee);
        }

        public override void Execute()
        {
            if (Settings.UseQ && Q.IsReady() && PlayerManaExact >= 65.0f)
            {
                var jumpPos = Game.CursorPos;
                var target = ObjectManager.Get<Obj_AI_Base>()
                    .FirstOrDefault(o => o.IsAlly && o.Distance(jumpPos) < 100 && o.Distance(Player.Instance) < Q.Range);
                if (target != null)
                    Q.Cast(target);
            }
        }
    }
}
