using System;
using System.Drawing;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;

namespace VodkaAzir
{
    public static class Program
    {
        public const string ChampName = "Azir";

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
            Chat.Print("Vodka{0} Loaded. Have a splendid game!", Color.LightGoldenrodYellow, ChampName);
            Chat.Print("===============================", Color.Red);
            Chat.Print("THIS ADDON IS NOT CURRENTLY SUPPORTED. IT HAS NOT BEEN UPDATED SINCE 5.24 (December 2015). Use at your own risk.");
            Chat.Print("===============================", Color.Red);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Vodka{0} Loaded. Have a splendid game!", ChampName);
            Console.ResetColor();
        }
    }
}
