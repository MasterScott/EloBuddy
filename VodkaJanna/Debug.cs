using System;
using System.Drawing;
using EloBuddy;
using Settings = VodkaJanna.Config.DebugMenu;

namespace VodkaJanna
{
    class Debug
    {
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
            if (!Settings.DebugChat)
            {
                return;
            }
            Chat.Print("[Vodka{0}] {1}", color, Program.ChampName, text);
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
            if (!Settings.DebugConsole)
            {
                return;
            }
            Console.ForegroundColor = color;
            Console.WriteLine("[Vodka{0}] {1}", Program.ChampName, text);
            Console.ResetColor();
        }
    }
}
