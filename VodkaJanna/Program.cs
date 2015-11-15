using System;
using System.Drawing;
using EloBuddy;
using EloBuddy.SDK.Events;

namespace VodkaJanna
{
    public static class Program
    {
        public const string ChampName = "Janna";

        public static void Main(string[] args)
        {
            Loading.OnLoadingComplete += OnLoadingComplete;
        }

        private static void OnLoadingComplete(EventArgs args)
        {
            if (Player.Instance.ChampionName != ChampName)
            {
                return;
            }
            Config.Initialize();
            SpellManager.Initialize();
            ModeManager.Initialize();
            Events.Initialize();

            Chat.Print("VodkaJanna Loaded. Have a splendid game!", Color.LightBlue);
        }
    }
}
