using System;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

// ReSharper disable InconsistentNaming
// ReSharper disable MemberHidesStaticFromOuterClass
namespace VodkaSmite
{
    public static class Config
    {
        private const string MenuName = "VodkaSmite";

        private static readonly Menu smiterMenu;
        private static readonly CheckBox _smiteEnabled;
        private static readonly KeyBind _smiteEnabledToggle;
        private static readonly CheckBox _smiteEnemies;
        private static readonly CheckBox _drawSmiteStatus;
        private static readonly CheckBox _drawSmiteable;
        private static readonly CheckBox _drawRange;

        public static Menu MainMenu
        {
            get { return smiterMenu; }
        }

        public static bool SmiteEnabled
        {
            get { return _smiteEnabled.CurrentValue; }
        }

        public static bool SmiteEnabledToggle
        {
            get { return _smiteEnabledToggle.CurrentValue; }
        }

        public static bool SmiteEnemies
        {
            get { return _smiteEnemies.CurrentValue; }
        }

        public static bool DrawSmiteStatus
        {
            get { return _drawSmiteStatus.CurrentValue; }
        }

        public static bool DrawSmiteable
        {
            get { return _drawSmiteable.CurrentValue; }
        }

        public static bool DrawSmiteRange
        {
            get { return _drawRange.CurrentValue; }
        }

        static Config()
        {
            smiterMenu = EloBuddy.SDK.Menu.MainMenu.AddMenu(MenuName, MenuName.ToLower());
            smiterMenu.AddGroupLabel("Welcome to VodkaGaren");
            smiterMenu.AddLabel("Created by Haker");
            smiterMenu.AddLabel("Feel free to send me any suggestions you might have.");
            smiterMenu.AddGroupLabel("Smite Status");
            _smiteEnabled = smiterMenu.Add("vSmiteEnabled", new CheckBox("Enabled always"));
            _smiteEnabledToggle = smiterMenu.Add("vSmiteEnabledToggle", new KeyBind("Enabled (Toggle Key)", false, KeyBind.BindTypes.PressToggle, 'K'));
            _smiteEnemies = smiterMenu.Add("vSmiteEnemies", new CheckBox("KS enemies with Smite"));
            smiterMenu.AddGroupLabel("Monsters to smite");
            smiterMenu.AddLabel("Select monsters you want to smite");
            smiterMenu.Add("vSmiteSRU_Baron", new CheckBox("Baron"));
            smiterMenu.Add("vSmiteSRU_Dragon", new CheckBox("Dragon"));
            smiterMenu.Add("vSmiteSRU_Red", new CheckBox("Red"));
            smiterMenu.Add("vSmiteSRU_Blue", new CheckBox("Blue"));
            smiterMenu.Add("vSmiteSRU_Gromp", new CheckBox("Gromp"));
            smiterMenu.Add("vSmiteSRU_Murkwolf", new CheckBox("Murkwolf"));
            smiterMenu.Add("vSmiteSRU_Krug", new CheckBox("Krug"));
            smiterMenu.Add("vSmiteSRU_Razorbeak", new CheckBox("Razorbeak"));
            smiterMenu.Add("vSmiteSru_Crab", new CheckBox("Crab"));
            smiterMenu.Add("vSmiteSRU_RiftHerald", new CheckBox("Rift Herald", false));
            smiterMenu.AddGroupLabel("Drawing");
            _drawSmiteStatus = smiterMenu.Add("vSmiteDrawSmiteStatus", new CheckBox("Draw Smite Status"));
            _drawSmiteable = smiterMenu.Add("vSmiteDrawSmiteable", new CheckBox("Draw Smiteable Monsters"));
            _drawRange = smiterMenu.Add("vSmiteDrawRange", new CheckBox("Draw Smite Range"));
            DebugMenu.Initialize();
        }

        public static void Initialize()
        {
        }

        public static class DebugMenu
        {
            private static readonly Menu MenuDebug;
            private static readonly CheckBox _debugChat;
            private static readonly CheckBox _debugConsole;

            public static bool DebugChat
            {
                get { return _debugChat.CurrentValue; }
            }
            public static bool DebugConsole
            {
                get { return _debugConsole.CurrentValue; }
            }

            static DebugMenu()
            {
                MenuDebug = Config.smiterMenu.AddSubMenu("Debug");
                MenuDebug.AddLabel("This is for debugging purposes only.");
                _debugChat = MenuDebug.Add("debugChat", new CheckBox("Show debug messages in chat", false));
                _debugConsole = MenuDebug.Add("debugConsole", new CheckBox("Show debug messages in console", false));
            }

            public static void Initialize()
            {

            }
        }
    }
}
