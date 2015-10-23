using System;
using System.Drawing;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Rendering;
using SharpDX;
using Color = SharpDX.Color;
using Font = System.Drawing.Font;

namespace VodkaGaren
{
    public static class Program
    {
        // Change this line to the champion you want to make the addon for,
        // watch out for the case being correct!
        public const string ChampName = "Garen";
        private static Text Text { get; set; }

        public static void Main(string[] args)
        {
            // Wait till the loading screen has passed
            Loading.OnLoadingComplete += OnLoadingComplete;
        }

        private static void OnLoadingComplete(EventArgs args)
        {
            // Verify the champion we made this addon for
            if (Player.Instance.ChampionName != ChampName)
            {
                // Champion is not the one we made this addon for,
                // therefore we return
                return;
            }

            // Initialize the classes that we need
            Config.Initialize();
            SpellManager.Initialize();
            ModeManager.Initialize();

            Text = new Text("", new Font(FontFamily.GenericSansSerif, 12, FontStyle.Bold)) { Color = System.Drawing.Color.ForestGreen };

            // Listen to events we need
            Drawing.OnDraw += OnDraw;
        }

        private static void OnDraw(EventArgs args)
        {
            if (Config.Modes.Drawing.DrawERange)
            {
                Circle.Draw(Color.Yellow, SpellManager.E.Range, Player.Instance.Position);
            }
            if (Config.Modes.Drawing.DrawRRange)
            {
                Circle.Draw(Color.Red, SpellManager.R.Range, Player.Instance.Position);
            }
            if (Config.Modes.Drawing.DrawHPAfterR)
            {
                DrawHPAfterR();
            }
        }

        private static void DrawHPAfterR()
        {
            if (!SpellManager.R.IsLearned)
            {
                return;
            }
            foreach (var enemy in EntityManager.Heroes.Enemies.Where(e => !e.IsDead && e.IsVisible && e.Health > 0))
            {
                int hpAfterR = (int) Math.Floor((double) enemy.Health - Damages.RDamage(enemy));
                Vector2 drawPos = new Vector2(enemy.HPBarPosition.X, enemy.HPBarPosition.Y - 10);
                if (hpAfterR > 0)
                {
                    Text.TextValue = "+" + hpAfterR;
                    Text.Color = System.Drawing.Color.GreenYellow;
                }
                else
                {
                    Text.TextValue = "" + hpAfterR;
                    Text.Color = System.Drawing.Color.Red;
                }
                Text.Position = drawPos;
                Text.Draw();
            }
        }
    }
}
