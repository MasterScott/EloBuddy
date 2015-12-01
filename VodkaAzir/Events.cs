using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Rendering;
using SharpDX;
using SettingsMisc = VodkaAzir.Config.MiscMenu;
using SettingsDrawing = VodkaAzir.Config.DrawingMenu;

namespace VodkaAzir
{
    public static class Events
    {
        static Events()
        {
            Interrupter.OnInterruptableSpell += InterrupterOnInterruptableSpell;
            Drawing.OnDraw += OnDraw;
        }

        private static void InterrupterOnInterruptableSpell(Obj_AI_Base sender, Interrupter.InterruptableSpellEventArgs interruptableSpellEventArgs)
        {
            if (SettingsMisc.InterruptR && SpellManager.R.IsReady() && sender.IsEnemy && sender.IsValidTarget(SpellManager.R.Range))
            {
                SpellManager.R.Cast(sender);
                Debug.WriteChat("Interrupting spell from {0}", ((AIHeroClient)sender).ChampionName);
            }
        }

        public static void Initialize()
        {

        }

        private static void OnDraw(EventArgs args)
        {
            if (SettingsDrawing.DrawQ)
            {
                if (!(SettingsDrawing.DrawOnlyReady && !SpellManager.Q.IsReady()))
                {
                    Circle.Draw(Color.LightGreen, SpellManager.Q.Range, Player.Instance.Position);
                }
            }
            if (SettingsDrawing.DrawW)
            {
                if (!(SettingsDrawing.DrawOnlyReady && !SpellManager.W.IsReady()))
                {
                    Circle.Draw(Color.LightGreen, SpellManager.W.Range, Player.Instance.Position);
                }
            }
            if (SettingsDrawing.DrawE)
            {
                if (!(SettingsDrawing.DrawOnlyReady && !SpellManager.E.IsReady()))
                {
                    Circle.Draw(Color.Yellow, SpellManager.E.Range, Player.Instance.Position);
                }
            }
            if (SettingsDrawing.DrawR)
            {
                if (!(SettingsDrawing.DrawOnlyReady && !SpellManager.R.IsReady()))
                {
                    Circle.Draw(Color.Orange, SpellManager.R.Range, Player.Instance.Position);
                }
            }
            if (SettingsDrawing.DrawIgnite && SpellManager.HasIgnite())
            {
                if (!(SettingsDrawing.DrawOnlyReady && !SpellManager.Ignite.IsReady()))
                {
                    Circle.Draw(Color.Red, SpellManager.Ignite.Range, Player.Instance.Position);
                }
            }
        }
    }
}
