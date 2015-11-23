using System;
using System.Runtime.CompilerServices;
using EloBuddy;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Rendering;
using SharpDX;

namespace VodkaSmite
{
    public static class Program
    {

        public const string ChampName = "Smite";

        public static void Main(string[] args)
        {
            Loading.OnLoadingComplete += OnLoadingComplete;
        }

        private static void OnLoadingComplete(EventArgs args)
        {
            SpellManager.Initialize();
            if (!SpellManager.HasSmite())
            {
                Chat.Print("No smite detected - unloading VodkaSmite.", System.Drawing.Color.Red);
                return;
            }
            Config.Initialize();
            ModeManager.Initialize();
            Events.Initialize();
            WelcomeMsg();
        }

        private static void WelcomeMsg()
        {
            Chat.Print("Vodka{0} Loaded. Have a splendid game!", System.Drawing.Color.LightBlue, ChampName);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Vodka{0} Loaded. Have a splendid game!", ChampName);
            Console.ResetColor();
        }
    }
}
