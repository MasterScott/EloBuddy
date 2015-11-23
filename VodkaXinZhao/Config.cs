using System;
using EloBuddy;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

// ReSharper disable InconsistentNaming
// ReSharper disable MemberHidesStaticFromOuterClass

namespace VodkaXinZhao
{
    public static class Config
    {
        private const string MenuName = "VodkaXinZhao";

        private static readonly Menu Menu;

        static Config()
        {
            Menu = MainMenu.AddMenu(MenuName, MenuName.ToLower());
            Menu.AddGroupLabel("Welcome to VodkaXinZhao");
            Menu.AddLabel("Created by Haker");
            Menu.AddLabel("Feel free to send me any suggestions you might have.");
            ModesMenu.Initialize();
            ManaManagerMenu.Initialize();
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

                //Flee.Initialize(); // TODO Flee Mode
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
                private static readonly CheckBox _useItems;
                private static readonly Slider _minEDistance;
                private static readonly Slider _minRTargets;


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

                public static bool UseItems
                {
                    get { return _useItems.CurrentValue; }
                }

                public static int MinEDistance
                {
                    get { return _minEDistance.CurrentValue; }
                }

                public static int MinRTargets
                {
                    get { return _minRTargets.CurrentValue; }
                }

                static Combo()
                {
                    MenuModes.AddGroupLabel("Combo");
                    _useQ = MenuModes.Add("comboUseQ", new CheckBox("Use Q"));
                    _useW = MenuModes.Add("comboUseW", new CheckBox("Use W"));
                    _useE = MenuModes.Add("comboUseE", new CheckBox("Use E"));
                    _useR = MenuModes.Add("comboUseR", new CheckBox("Use R"));
                    _useItems = MenuModes.Add("comboUseItems", new CheckBox("Use Tiamat/Hydra/BOTRK"));
                    _minEDistance = MenuModes.Add("comboMinEDistance",
                        new Slider("Minimum distance to use E", 175, 0, 550));
                    _minRTargets = MenuModes.Add("comboMinRTargets",
                        new Slider("Minimum enemies in range to ult", 3, 1, 5));
                }

                public static void Initialize()
                {
                }
            }

            public static class Harass
            {
                private static readonly CheckBox _useQ;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }
                
                static Harass()
                {
                    MenuModes.AddGroupLabel("Harass");
                    _useQ = MenuModes.Add("harassUseQ", new CheckBox("Use Q"));
                }

                public static void Initialize()
                {
                }
            }

            public static class LaneClear
            {
                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useW;
                private static readonly CheckBox _useE;
                private static readonly CheckBox _useItems;
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

                public static bool UseItems
                {
                    get { return _useItems.CurrentValue; }
                }

                public static int MinETargets
                {
                    get { return _minETargets.CurrentValue; }
                }

                static LaneClear()
                {
                    MenuModes.AddGroupLabel("LaneClear");
                    _useQ = MenuModes.Add("laneUseQ", new CheckBox("Use Q", false));
                    _useW = MenuModes.Add("laneUseW", new CheckBox("Use W"));
                    _useE = MenuModes.Add("laneUseE", new CheckBox("Use E", false));
                    _useItems = MenuModes.Add("laneUseItems", new CheckBox("Use Tiamat/Hydra/BOTRK"));
                    _minETargets = MenuModes.Add("minETargetsLC", new Slider("Minimum targets for E", 3, 1, 10));
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
                private static readonly CheckBox _useItems;
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

                public static bool UseItems
                {
                    get { return _useItems.CurrentValue; }
                }

                public static int MinETargets
                {
                    get { return _minETargets.CurrentValue; }
                }

                static JungleClear()
                {
                    MenuModes.AddGroupLabel("JungleClear");
                    _useQ = MenuModes.Add("jungleUseQ", new CheckBox("Use Q"));
                    _useW = MenuModes.Add("jungleUseW", new CheckBox("Use W"));
                    _useE = MenuModes.Add("jungleUseE", new CheckBox("Use E", false));
                    _useItems = MenuModes.Add("jungleUseItems", new CheckBox("Use Tiamat/Hydra/BOTRK"));
                    _minETargets = MenuModes.Add("minETargetsJC", new Slider("Minimum targets for E", 2, 1, 10));
                }

                public static void Initialize()
                {
                }


            }

            public static class Flee
            {
                private static readonly CheckBox _useE;
                
                public static bool UseE
                {
                    get { return _useE.CurrentValue; }
                }

                static Flee()
                {
                    MenuModes.AddGroupLabel("Flee");
                    _useE = MenuModes.Add("fleeUseE", new CheckBox("Use E"));
                }

                public static void Initialize()
                {
                }
            }
        }

        public static class MiscMenu
        {
            private static readonly Menu MenuMisc;
            private static readonly CheckBox _interrupterR;
            private static readonly CheckBox _autoR;
            private static readonly CheckBox _potion;
            private static readonly CheckBox _ksE;
            private static readonly CheckBox _ksR;
            private static readonly CheckBox _ksIgnite;
            private static readonly Slider _potionMinHP;
            private static readonly Slider _potionMinMP;
            private static readonly Slider _autoRMinHP;
            private static readonly Slider _autoRMinEnemies;
            
            public static bool InterrupterUseR
            {
                get { return _interrupterR.CurrentValue; }
            }
            public static bool AutoR
            {
                get { return _autoR.CurrentValue; }
            }
            public static bool KsE
            {
                get { return _ksE.CurrentValue; }
            }
            public static bool KsR
            {
                get { return _ksR.CurrentValue; }
            }
            public static bool KsIgnite
            {
                get { return _ksIgnite.CurrentValue; }
            }
            public static int AutoRMinHP
            {
                get { return _autoRMinHP.CurrentValue; }
            }
            public static int AutoRMinEnemies
            {
                get { return _autoRMinEnemies.CurrentValue; }
            }
            public static bool Potion
            {
                get { return _potion.CurrentValue; }
            }
            public static int potionMinHP
            {
                get { return _potionMinHP.CurrentValue; }
            }
            public static int potionMinMP
            {
                get { return _potionMinMP.CurrentValue; }
            }

            static MiscMenu()
            {
                MenuMisc = Config.Menu.AddSubMenu("Misc");
                MenuMisc.AddGroupLabel("Interrupter");
                _interrupterR = MenuMisc.Add("interrupterUseR", new CheckBox("Use R to interrupt spells"));
                MenuMisc.AddGroupLabel("KillSteal");
                _ksE = MenuMisc.Add("ksE", new CheckBox("KillSteal E"));
                _ksR = MenuMisc.Add("ksR", new CheckBox("KillSteal R", false));
                _ksIgnite = MenuMisc.Add("ksIgnite", new CheckBox("KillSteal Ignite", false));
                MenuMisc.AddGroupLabel("Auto R usage");
                _autoR = MenuMisc.Add("autoR", new CheckBox("Use R automatically when X enemies around", false));
                _autoRMinHP = MenuMisc.Add("autoRMinHP", new Slider("Minimum HP % to ult", 50));
                _autoRMinEnemies = MenuMisc.Add("autoRMinEnemies", new Slider("Minimum enemies around to ult", 4, 1, 5));
                MenuMisc.AddGroupLabel("Auto pot usage");
                _potion = MenuMisc.Add("potion", new CheckBox("Use potions"));
                _potionMinHP = MenuMisc.Add("potionminHP", new Slider("Minimum Health % to use potion", 50));
                _potionMinMP = MenuMisc.Add("potionMinMP", new Slider("Minimum Mana % to use potion", 20));
            }

            public static void Initialize()
            {
            }
        }

        public static class ManaManagerMenu
        {
            private static readonly Menu MenuManaManager;
            private static readonly Slider _minQMana;
            private static readonly Slider _minWMana;
            private static readonly Slider _minEMana;
            private static readonly Slider _minRMana;

            public static int MinQMana
            {
                get { return _minQMana.CurrentValue; }
            }
            public static int MinWMana
            {
                get { return _minWMana.CurrentValue; }
            }
            public static int MinEMana
            {
                get { return _minEMana.CurrentValue; }
            }
            public static int MinRMana
            {
                get { return _minRMana.CurrentValue; }
            }

            static ManaManagerMenu()
            {
                MenuManaManager = Config.Menu.AddSubMenu("Mana Manager");
                _minQMana = MenuManaManager.Add("minQMana", new Slider("Minimum mana % to use Q", 20, 0, 100));
                _minWMana = MenuManaManager.Add("minWMana", new Slider("Minimum mana % to use W", 60, 0, 100));
                _minEMana = MenuManaManager.Add("minEMana", new Slider("Minimum mana % to use E", 20, 0, 100));
                _minRMana = MenuManaManager.Add("minRMana", new Slider("Minimum mana % to use R", 30, 0, 100));
            }

            public static void Initialize()
            {
            }
        }

        public static class DrawingMenu
        {
            private static readonly Menu MenuDrawing;
            private static readonly CheckBox _drawE;
            private static readonly CheckBox _drawR;
            private static readonly CheckBox _drawIgnite;
            private static readonly CheckBox _drawOnlyReady;
            
            public static bool DrawE
            {
                get { return _drawE.CurrentValue; }
            }
            public static bool DrawR
            {
                get { return _drawR.CurrentValue; }
            }
            public static bool DrawOnlyReady
            {
                get { return _drawOnlyReady.CurrentValue; }
            }
            public static bool DrawIgnite
            {
                get { return _drawIgnite.CurrentValue; }
            }

            static DrawingMenu()
            {
                MenuDrawing = Config.Menu.AddSubMenu("Drawing");
                _drawE = MenuDrawing.Add("drawE", new CheckBox("Draw E"));
                _drawR = MenuDrawing.Add("drawR", new CheckBox("Draw R"));
                _drawIgnite = MenuDrawing.Add("drawIgnite", new CheckBox("Draw Ignite"));
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
