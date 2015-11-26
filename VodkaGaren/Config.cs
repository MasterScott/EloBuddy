using System;
using EloBuddy;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

// ReSharper disable InconsistentNaming
// ReSharper disable MemberHidesStaticFromOuterClass

namespace VodkaGaren
{
    public static class Config
    {
        private const string MenuName = "VodkaGaren";

        private static readonly Menu Menu;

        static Config()
        {
            Menu = MainMenu.AddMenu(MenuName, MenuName.ToLower());
            Menu.AddGroupLabel("Welcome to VodkaGaren");
            Menu.AddLabel("Created by Haker");
            Menu.AddLabel("Feel free to send me any suggestions you might have.");
            ModesMenu.Initialize();
            MiscMenu.Initialize();
            DrawingMenu.Initialize();
            DebugMenu.Initialize();
        }

        public static void Initialize()
        {
        }

        public static class ModesMenu
        {
            private static readonly Menu MenuModes;

            static ModesMenu()
            {
                MenuModes = Config.Menu.AddSubMenu("Modes");

                Combo.Initialize();
                MenuModes.AddSeparator();

                Harass.Initialize();
                MenuModes.AddSeparator();

                LaneClear.Initialize();
                MenuModes.AddSeparator();

                JungleClear.Initialize();
                MenuModes.AddSeparator();

                LastHit.Initialize();
                MenuModes.AddSeparator();

                Flee.Initialize();
            }

            public static void Initialize()
            {
            }

            public static class Combo
            {
                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useW;
                private static readonly CheckBox _useE;
                private static readonly CheckBox _useR;
                private static readonly Slider _minWEnemies;


                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }

                public static bool UseW
                {
                    get { return _useW.CurrentValue; }
                }

                public static bool UseE
                {
                    get { return _useE.CurrentValue; }
                }

                public static bool UseR
                {
                    get { return _useR.CurrentValue; }
                }

                public static int MinWEnemies
                {
                    get { return _minWEnemies.CurrentValue; }
                }

                static Combo()
                {
                    MenuModes.AddGroupLabel("Combo");
                    _useQ = MenuModes.Add("comboUseQ", new CheckBox("Use Q"));
                    _useW = MenuModes.Add("comboUseW", new CheckBox("Use W"));
                    _useE = MenuModes.Add("comboUseE", new CheckBox("Use E"));
                    _useR = MenuModes.Add("comboUseR", new CheckBox("Use R"));
                    _minWEnemies = MenuModes.Add("comboMinRTargets",
                        new Slider("Minimum enemies near you to W", 1, 1, 5));
                }

                public static void Initialize()
                {
                }
            }

            public static class Harass
            {
                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useE;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }

                public static bool UseE
                {
                    get { return _useE.CurrentValue; }
                }

                static Harass()
                {
                    //MenuModes.AddGroupLabel("Harass");
                    //_useQ = MenuModes.Add("harassUseQ", new CheckBox("Use Q"));
                    //_useE = MenuModes.Add("harassUseE", new CheckBox("Use E", false));
                }

                public static void Initialize()
                {
                }
            }

            public static class LaneClear
            {
                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useE;
                private static readonly Slider _minETargets;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }

                public static bool UseE
                {
                    get { return _useE.CurrentValue; }
                }

                public static int MinETargets
                {
                    get { return _minETargets.CurrentValue; }
                }

                static LaneClear()
                {
                    //MenuModes.AddGroupLabel("LaneClear");
                    //_useQ = MenuModes.Add("laneUseQ", new CheckBox("Use Q"));
                    //_useE = MenuModes.Add("laneUseE", new CheckBox("Use E"));
                    //_minETargets = MenuModes.Add("minETargetsLC", new Slider("Minimum targets for E", 4, 1, 10));
                }

                public static void Initialize()
                {
                }
            }

            public static class LastHit
            {
                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useE;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }

                public static bool UseE
                {
                    get { return _useE.CurrentValue; }
                }


                static LastHit()
                {
                    // MenuModes.AddGroupLabel("LastHit");
                    //_useQ = MenuModes.Add("lastHitUseQ", new CheckBox("Use Q"));
                    //_useE = MenuModes.Add("lastHitUseE", new CheckBox("Use E"));
                }

                public static void Initialize()
                {
                }
            }

            public static class JungleClear
            {
                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useW;
                private static readonly CheckBox _useE;
                private static readonly Slider _minQTargets;
                private static readonly Slider _minETargets;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }

                public static bool UseW
                {
                    get { return _useW.CurrentValue; }
                }

                public static bool UseE
                {
                    get { return _useE.CurrentValue; }
                }

                public static int MinQTargets
                {
                    get { return _minQTargets.CurrentValue; }
                }

                public static int MinETargets
                {
                    get { return _minETargets.CurrentValue; }
                }

                static JungleClear()
                {
                    //MenuModes.AddGroupLabel("JungleClear");
                    //_useQ = MenuModes.Add("jungleUseQ", new CheckBox("Use Q"));
                    //_useW = MenuModes.Add("jungleUseW", new CheckBox("Use W"));
                    //_useE = MenuModes.Add("jungleUseE", new CheckBox("Use E"));
                    //_minQTargets = MenuModes.Add("minQTargetsJC", new Slider("Minimum targets for Q", 2, 1, 10));
                    //_minETargets = MenuModes.Add("minETargetsJC", new Slider("Minimum targets for E", 2, 1, 10));
                }

                public static void Initialize()
                {
                }


            }

            public static class Flee
            {
                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useE;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }

                public static bool UseE
                {
                    get { return _useE.CurrentValue; }
                }

                static Flee()
                {
                    //MenuModes.AddGroupLabel("Flee");
                    //_useQ = MenuModes.Add("fleeUseQ", new CheckBox("Use Q"));
                    //_useE = MenuModes.Add("fleeUseE", new CheckBox("Use E"));
                }

                public static void Initialize()
                {
                }
            }
        }

        public static class MiscMenu
        {
            private static readonly Menu MenuMisc;
            private static readonly CheckBox _autoQ;
            private static readonly CheckBox _potion;
            private static readonly CheckBox _ksR;
            private static readonly CheckBox _ksIgnite;
            private static readonly Slider _potionMinHP;


            public static bool KsR
            {
                get { return _ksR.CurrentValue; }
            }
            public static bool AutoQ
            {
                get { return _autoQ.CurrentValue; }
            }
            public static bool KsIgnite
            {
                get { return _ksIgnite.CurrentValue; }
            }
            public static bool Potion
            {
                get { return _potion.CurrentValue; }
            }
            public static int potionMinHP
            {
                get { return _potionMinHP.CurrentValue; }
            }

            static MiscMenu()
            {
                MenuMisc = Config.Menu.AddSubMenu("Misc");
                _autoQ = MenuMisc.Add("autoQ", new CheckBox("Auto Q after hitting enemy hero", false));
                MenuMisc.AddGroupLabel("KillSteal");
                _ksR = MenuMisc.Add("ksR", new CheckBox("KillSteal R", false));
                _ksIgnite = MenuMisc.Add("ksIgnite", new CheckBox("KillSteal Ignite", false));
                MenuMisc.AddGroupLabel("Auto pot usage");
                _potion = MenuMisc.Add("potion", new CheckBox("Use potions"));
                _potionMinHP = MenuMisc.Add("potionminHP", new Slider("Minimum Health % to use potion", 50));
            }

            public static void Initialize()
            {
            }
        }

        public static class DrawingMenu
        {
            private static readonly Menu MenuDrawing;
            private static readonly CheckBox _drawQ;
            private static readonly CheckBox _drawE;
            private static readonly CheckBox _drawR;
            private static readonly CheckBox _drawHPAfterR;
            private static readonly CheckBox _drawOnlyReady;

            public static bool DrawQ
            {
                get { return _drawQ.CurrentValue; }
            }
            public static bool DrawE
            {
                get { return _drawE.CurrentValue; }
            }
            public static bool DrawR
            {
                get { return _drawR.CurrentValue; }
            }
            public static bool DrawHPAfterR
            {
                get { return _drawHPAfterR.CurrentValue; }
            }
            public static bool DrawOnlyReady
            {
                get { return _drawOnlyReady.CurrentValue; }
            }

            static DrawingMenu()
            {
                MenuDrawing = Config.Menu.AddSubMenu("Drawing");
                _drawQ = MenuDrawing.Add("drawQ", new CheckBox("Draw Q"));
                _drawE = MenuDrawing.Add("drawE", new CheckBox("Draw E"));
                _drawR = MenuDrawing.Add("drawR", new CheckBox("Draw R"));
                _drawHPAfterR = MenuDrawing.Add("drawHPAfterR", new CheckBox("Draw Enemy HP after R"));
                _drawOnlyReady = MenuDrawing.Add("drawOnlyReady", new CheckBox("Draw Only Ready Skills"));
            }

            public static void Initialize()
            {
            }
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
                MenuDebug = Config.Menu.AddSubMenu("Debug");
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
