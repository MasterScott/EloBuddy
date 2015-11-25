using System;
using System.Drawing;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;

namespace VodkaTwitch
{
    public static class Program
    {
        public const string ChampName = "Twitch";

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
            WelcomeMsg();
        }

        private static void WelcomeMsg()
        {
            Chat.Print("Vodka{0} Loaded. Have a splendid game!", Color.GreenYellow, ChampName);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Vodka{0} Loaded. Have a splendid game!", ChampName);
            Console.ResetColor();
        }
    }
}
