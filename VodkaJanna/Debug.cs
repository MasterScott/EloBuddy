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
        private static string lastChatMsgText = "Chat";
        private static string lastConsoleMsgText = "Console";

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
            if (!Settings.DebugChat || (text.Substring(0, 20).Equals(lastChatMsgText, StringComparison.CurrentCultureIgnoreCase) && Environment.TickCount - lastChatMsg < 150))
            {
                return;
            }
            Chat.Print("[Vodka{0}] {1}", color, Program.ChampName, text);
            lastChatMsg = Environment.TickCount;
            lastChatMsgText = text.Substring(0, 20);
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
            if (!Settings.DebugConsole || (text.Substring(0, 20).Equals(lastConsoleMsgText, StringComparison.CurrentCultureIgnoreCase) && Environment.TickCount - lastConsoleMsg < 500))
            {
                return;
            }
            Console.ForegroundColor = color;
            Console.WriteLine("[Vodka{0}] {1}", Program.ChampName, text);
            Console.ResetColor();
            lastConsoleMsg = Environment.TickCount;
            lastConsoleMsgText = text.Substring(0, 20);
        }
    }
}
