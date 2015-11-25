using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Rendering;
using SharpDX;
using SettingsMisc = VodkaTwitch.Config.MiscMenu;
using SettingsModes = VodkaTwitch.Config.ModesMenu;
using SettingsDrawing = VodkaTwitch.Config.DrawingMenu;
using SettingsMana = VodkaTwitch.Config.ManaManagerMenu;

namespace VodkaTwitch
{
    public static class Events
    {
        static Events()
        {
            Gapcloser.OnGapcloser += GapcloserOnOnGapcloser;
            Drawing.OnDraw += OnDraw;
        }

        private static void GapcloserOnOnGapcloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs e)
        {
            var W = SpellManager.W;
            if (SettingsMisc.GapcloserUseW && sender.IsEnemy && W.IsReady() && sender.IsValidTarget() && e.End.Distance(Player.Instance) <= 200.0f)
            {
                var pred = W.GetPrediction(sender);
                if (pred.HitChance >= HitChance.Medium)
                {
                    W.Cast(pred.CastPosition);
                    Debug.WriteChat("Casting W in AntiGapcloser, Target: {0}", sender.ChampionName);
                }
            }
        }

        public static void Initialize()
        {

        }

        private static void OnDraw(EventArgs args)
        {
            if (SettingsDrawing.DrawQ && SpellManager.QActive)
            {
                    var QBuff = Player.Instance.GetBuff("TwitchHideInShadows");
                    if (QBuff != null)
                    {
                        var maxDistance = Player.Instance.MoveSpeed*(QBuff.EndTime - Game.Time) + Player.Instance.BoundingRadius;
                        Circle.Draw(Color.DarkBlue, maxDistance, Player.Instance.Position);
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
                    Circle.Draw(Color.DarkGreen, SpellManager.E.Range, Player.Instance.Position);
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
