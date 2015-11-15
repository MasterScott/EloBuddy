using System;
using System.Drawing;
using EloBuddy;
using Settings = VodkaJanna.Config.DebugMenu;

namespace VodkaJanna
{
    class Debug
    {
        private static int lastConsoleMsg = Environment.TickCount; // To prevent msg spam
        private static int lastChatMsg = Environment.TickCount;

        public static void Write(String text)
        {
            WriteChat(text, Color.LightBlue);
            WriteConsole(text, ConsoleColor.Cyan);
        }

        public static void Write(string format, params string[] args)
        {
            WriteChat(String.Format(format, args), Color.LightBlue);
            WriteConsole(String.Format(format, args), ConsoleColor.Cyan);
        }

        public static void WriteChat(string text)
        {
            WriteChat(text, Color.LightBlue);
        }

        public static void WriteChat(string format, params string[] args)
        {
            WriteChat(String.Format(format, args), Color.LightBlue);
        }


        public static void WriteChat(string format, Color color, params string[] args)
        {
            WriteChat(String.Format(format, args), color);
        }

        public static void WriteChat(string text, Color color)
        {
            if (!Settings.DebugChat || Environment.TickCount-lastChatMsg < 50)
            {
                return;
            }
            Chat.Print("[Vodka{0}] {1}", color, Program.ChampName, text);
            lastChatMsg = Environment.TickCount;
        }

        public static void WriteConsole(string text)
        {
            WriteConsole(text, ConsoleColor.Cyan);
        }

        public static void WriteConsole(string format, params string[] args)
        {
            WriteConsole(String.Format(format, args), ConsoleColor.Cyan);
        }
        
        public static void WriteConsole(string format, ConsoleColor color, params string[] args)
        {
            WriteConsole(String.Format(format, args), color);
        }

        public static void WriteConsole(string text, ConsoleColor color)
        {
            if (!Settings.DebugConsole || Environment.TickCount - lastConsoleMsg < 50)
            {
                return;
            }
            Console.ForegroundColor = color;
            Console.WriteLine("[Vodka{0}] {1}", Program.ChampName, text);
            Console.ResetColor();
            lastConsoleMsg = Environment.TickCount;
        }
    }
}
