using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

// ReSharper disable InconsistentNaming
// ReSharper disable MemberHidesStaticFromOuterClass

namespace VodkaJax
{
    public static class Config
    {
        private const string MenuName = "VodkaJax";

        private static readonly Menu Menu;

        static Config()
        {
            Menu = MainMenu.AddMenu(MenuName, MenuName.ToLower());
            Menu.AddGroupLabel("Welcome to VodkaJax");
            Menu.AddLabel("Created by Haker");
            Menu.AddLabel("Feel free to send me any suggestions you might have.");
            ModesMenu.Initialize();
            PredictionMenu.Initialize();
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
            public static readonly Menu MenuModes;

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
                private static readonly CheckBox _useItems;
                private static readonly Slider _maxBOTRKHPEnemy;
                private static readonly Slider _maxBOTRKHPPlayer;
                private static readonly Slider _minQDistance;

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

                public static int MaxBOTRKHPPlayer
                {
                    get { return _maxBOTRKHPPlayer.CurrentValue; }
                }

                public static int MaxBOTRKHPEnemy
                {
                    get { return _maxBOTRKHPEnemy.CurrentValue; }
                }

                public static int MinQDistance
                {
                    get { return _minQDistance.CurrentValue; }
                }

                static Combo()
                {
                    MenuModes.AddGroupLabel("Combo");
                    _useQ = MenuModes.Add("comboUseQ", new CheckBox("Use Q"));
                    _useW = MenuModes.Add("comboUseW", new CheckBox("Use W"));
                    _useE = MenuModes.Add("comboUseE", new CheckBox("Use E"));
                    _useR = MenuModes.Add("comboUseR", new CheckBox("Use R"));
                    _minQDistance = MenuModes.Add("comboMinQDistance",
                        new Slider("Only cast Q when distance is above {0}", 300, 0, (int) SpellManager.Q.Range-50));
                    _useItems = MenuModes.Add("comboUseItems", new CheckBox("Use Cutlass/BOTRK/Youmuu"));
                    _maxBOTRKHPPlayer = MenuModes.Add("comboMaxBotrkHpPlayer",
                        new Slider("Max Player HP % to use BOTRK", 80, 0, 100));
                    _maxBOTRKHPEnemy = MenuModes.Add("comboMaxBotrkHpEnemy",
                        new Slider("Max Enemy HP % to use BOTRK", 100, 0, 100));
                }

                public static void Initialize()
                {
                }
            }

            public static class Harass
            {
                private static readonly CheckBox _useW;

                public static bool UseW
                {
                    get { return _useW.CurrentValue; }
                }

                static Harass()
                {
                    MenuModes.AddGroupLabel("Harass");
                    _useW = MenuModes.Add("harassUseW", new CheckBox("Use W"));
                }

                public static void Initialize()
                {
                }
            }

            public static class LaneClear
            {
                private static readonly CheckBox _useW;
                private static readonly CheckBox _useE;

                public static bool UseW
                {
                    get { return _useW.CurrentValue; }
                }

                public static bool UseE
                {
                    get { return _useE.CurrentValue; }
                }

                static LaneClear()
                {
                    MenuModes.AddGroupLabel("LaneClear");
                    _useW = MenuModes.Add("laneUseW", new CheckBox("Use W"));
                    //_useE = MenuModes.Add("laneUseE", new CheckBox("Use E", false));
                }

                public static void Initialize()
                {
                }
            }

            public static class JungleClear
            {
                private static readonly CheckBox _useW;
                private static readonly CheckBox _useE;

                public static bool UseW
                {
                    get { return _useW.CurrentValue; }
                }

                public static bool UseE
                {
                    get { return _useE.CurrentValue; }
                }

                static JungleClear()
                {
                    MenuModes.AddGroupLabel("JungleClear");
                    _useW = MenuModes.Add("jungleUseW", new CheckBox("Use W"));
                    //_useE = MenuModes.Add("jungleUseE", new CheckBox("Use E", false));
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
                    MenuModes.AddGroupLabel("Flee");
                    _useQ = MenuModes.Add("fleeUseQ", new CheckBox("Use Q"));
                    //_useE = MenuModes.Add("fleeUseE", new CheckBox("Use E", false));
                }

                public static void Initialize()
                {
                }
            }
        }

        public static class MiscMenu
        {
            private static readonly Menu MenuMisc;
            private static readonly CheckBox _potion;
            private static readonly CheckBox _ksQW;
            private static readonly CheckBox _ksIgnite;
            private static readonly Slider _potionMinHP;
            private static readonly Slider _potionMinMP;
            
            public static bool KsQW
            {
                get { return _ksQW.CurrentValue; }
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
            public static int potionMinMP
            {
                get { return _potionMinMP.CurrentValue; }
            }

            static MiscMenu()
            {
                MenuMisc = Config.Menu.AddSubMenu("Misc");
                MenuMisc.AddGroupLabel("KillSteal");
                _ksQW = MenuMisc.Add("ksQ", new CheckBox("KillSteal Q/Q+W", false));
                _ksIgnite = MenuMisc.Add("ksIgnite", new CheckBox("KillSteal Ignite"));
                MenuMisc.AddGroupLabel("Auto pot usage");
                _potion = MenuMisc.Add("potion", new CheckBox("Use potions"));
                _potionMinHP = MenuMisc.Add("potionminHP", new Slider("Minimum Health % to use potion", 70));
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
                _minQMana = MenuManaManager.Add("minQMana", new Slider("Minimum mana % to use Q", 15, 0, 100));
                _minWMana = MenuManaManager.Add("minWMana", new Slider("Minimum mana % to use W", 0, 0, 100));
                _minEMana = MenuManaManager.Add("minEMana", new Slider("Minimum mana % to use E", 0, 0, 100));
                _minRMana = MenuManaManager.Add("minRMana", new Slider("Minimum mana % to use R", 25, 0, 100));
            }

            public static void Initialize()
            {
            }
        }

        public static class PredictionMenu
        {
            private static readonly Menu MenuPrediction;

            static PredictionMenu()
            {
                MenuPrediction = Config.Menu.AddSubMenu("Prediction");
                MenuPrediction.AddLabel("Here you can control the minimum HitChance to cast skills.");
                MenuPrediction.AddSeparator();
                MenuPrediction.AddLabel("No skills to use Prediction for :v");
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
            private static readonly CheckBox _drawIgnite;
            private static readonly CheckBox _drawSmite;
            private static readonly CheckBox _drawOnlyReady;
            private static readonly CheckBox _drawLasthittable;

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
            public static bool DrawIgnite
            {
                get { return _drawIgnite.CurrentValue; }
            }
            public static bool DrawSmite
            {
                get { return _drawSmite.CurrentValue; }
            }
            public static bool DrawOnlyReady
            {
                get { return _drawOnlyReady.CurrentValue; }
            }
            public static bool DrawLasthittable
            {
                get { return _drawLasthittable.CurrentValue; }
            }

            static DrawingMenu()
            {
                MenuDrawing = Config.Menu.AddSubMenu("Drawing");
                _drawQ = MenuDrawing.Add("drawQ", new CheckBox("Draw Q"));
                _drawE = MenuDrawing.Add("drawE", new CheckBox("Draw E"));
                _drawR = MenuDrawing.Add("drawR", new CheckBox("Draw R"));
                _drawIgnite = MenuDrawing.Add("drawIgnite", new CheckBox("Draw Ignite"));
                _drawSmite = MenuDrawing.Add("drawSmite", new CheckBox("Draw Smite"));
                _drawOnlyReady = MenuDrawing.Add("drawOnlyReady", new CheckBox("Draw Only Ready Skills"));
                _drawLasthittable = MenuDrawing.Add("drawLasthittable", new CheckBox("Draw minions lasthittable with Q"));
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
