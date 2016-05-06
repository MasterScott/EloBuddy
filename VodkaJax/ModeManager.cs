using EloBuddy;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Utils;
using System;
using System.Collections.Generic;
using SharpDX.Direct3D9;
using VodkaJax.Modes;

namespace VodkaJax
{
    public static class ModeManager
    {
        private static List<ModeBase> Modes { get; set; }
        private static int lastTick { get; set; }

        static ModeManager()
        {

            Modes = new List<ModeBase>();


            Modes.AddRange(new ModeBase[]
            {
                new PermaActive(),
                new Combo(),
                new Harass(),
                new LaneClear(),
                new JungleClear(),
                new LastHit(),
                new Flee()
            });

            Game.OnTick += OnTick;
            lastTick = Environment.TickCount;
        }

        public static void Initialize()
        {

        }

        private static void OnTick(EventArgs args)
        {
            if (Environment.TickCount - lastTick > 1000)
            {
                lastTick = Environment.TickCount;
            Chat.Print("State: {0}", Player.Instance.Spellbook.GetSpell(SpellSlot.E).ToggleState + "");
        }

        Modes.ForEach(mode =>
            {
                try
                {

                    if (mode.ShouldBeExecuted())
                    {
                        mode.Execute();
                    }
                }
                catch (Exception e)
                {
                    Logger.Log(LogLevel.Error, "Error executing mode '{0}'\n{1}", mode.GetType().Name, e);
                }
            });
        }
    }
}
