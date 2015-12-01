using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

// ReSharper disable InconsistentNaming
// ReSharper disable MemberHidesStaticFromOuterClass

namespace VodkaTristana
{
    public static class Config
    {
        private const string MenuName = "VodkaTristana";

        private static readonly Menu Menu;

        static Config()
        {
            Menu = MainMenu.AddMenu(MenuName, MenuName.ToLower());
            Menu.AddGroupLabel("Welcome to VodkaTristana");
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

                static Combo()
                {
                    MenuModes.AddGroupLabel("Combo");
                    _useQ = MenuModes.Add("comboUseQ", new CheckBox("Use Q"));
                    _useW = MenuModes.Add("comboUseW", new CheckBox("Use W"));
                    _useE = MenuModes.Add("comboUseE", new CheckBox("Use E"));
                    _useR = MenuModes.Add("comboUseR", new CheckBox("Use R (finisher)"));
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
                    MenuModes.AddGroupLabel("Harass");
                    _useQ = MenuModes.Add("harassUseQ", new CheckBox("Use Q"));
                    _useE = MenuModes.Add("harassUseE", new CheckBox("Use E"));
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
                private static readonly Slider _minWTargets;
                private static readonly Slider _minETargets;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }

                public static bool UseE
                {
                    get { return _useE.CurrentValue; }
                }

                public static bool UseW
                {
                    get { return _useW.CurrentValue; }
                }
                
                public static int MinWTargets
                {
                    get { return _minWTargets.CurrentValue; }
                }
                
                public static int MinETargets
                {
                    get { return _minETargets.CurrentValue; }
                }

                static LaneClear()
                {
                    MenuModes.AddGroupLabel("LaneClear");
                    _useQ = MenuModes.Add("laneUseQ", new CheckBox("Use Q"));
                    _useW = MenuModes.Add("laneUseW", new CheckBox("Use W"));
                    _useE = MenuModes.Add("laneUseE", new CheckBox("Use E"));
                    _minWTargets = MenuModes.Add("minWTargetsLC", new Slider("Minimum targets for W", 6, 1, 10));
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
                private static readonly Slider _minWTargets;
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

                public static int MinWTargets
                {
                    get { return _minWTargets.CurrentValue; }
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
                    _useE = MenuModes.Add("jungleUseE", new CheckBox("Use E"));
                    _minWTargets = MenuModes.Add("minWTargetsJC", new Slider("Minimum targets for W", 2, 1, 10));
                    _minETargets = MenuModes.Add("minETargetsJC", new Slider("Minimum targets for E", 2, 1, 10));
                }

                public static void Initialize()
                {
                }
            }

            public static class Flee
            {
                private static readonly CheckBox _useW;
                private static readonly CheckBox _useR;

                public static bool UseW
                {
                    get { return _useW.CurrentValue; }
                }
                
                public static bool UseR
                {
                    get { return _useR.CurrentValue; }
                }

                static Flee()
                {
                    MenuModes.AddGroupLabel("Flee");
                    _useW = MenuModes.Add("fleeUseW", new CheckBox("Use W"));
                    _useR = MenuModes.Add("fleeUseR", new CheckBox("Use R", false));
                }

                public static void Initialize()
                {
                }
            }
        }

        public static class MiscMenu
        {
            private static readonly Menu MenuMisc;
            private static readonly CheckBox _interruptR;
            private static readonly CheckBox _gapcloserR;
            private static readonly CheckBox _potion;
            private static readonly CheckBox _ksW;
            private static readonly CheckBox _ksE;
            private static readonly CheckBox _ksR;
            private static readonly CheckBox _ksIgnite;
            private static readonly KeyBind _WToCursor;
            private static readonly Slider _potionMinHP;
            private static readonly Slider _potionMinMP;

            public static bool InterruptR
            {
                get { return _interruptR.CurrentValue; }
            }
            public static bool GapcloserR
            {
                get { return _gapcloserR.CurrentValue; }
            }
            public static bool KsW
            {
                get { return _ksW.CurrentValue; }
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
            public static bool WToCursor
            {
                get { return _WToCursor.CurrentValue; }
            }

            static MiscMenu()
            {
                MenuMisc = Config.Menu.AddSubMenu("Misc");
                MenuMisc.AddGroupLabel("AntiGapcloser");
                _gapcloserR = MenuMisc.Add("gapcloserR", new CheckBox("Use R to prevent gapclosers", false));
                MenuMisc.AddGroupLabel("Interrupter");
                _interruptR = MenuMisc.Add("interruptR", new CheckBox("Use R to interrupt important spells", false));
                MenuMisc.AddGroupLabel("KillSteal");
                _ksW = MenuMisc.Add("ksW", new CheckBox("KillSteal W", false));
                _ksE = MenuMisc.Add("ksE", new CheckBox("KillSteal E"));
                _ksR = MenuMisc.Add("ksR", new CheckBox("KillSteal R"));
                _ksIgnite = MenuMisc.Add("ksIgnite", new CheckBox("KillSteal Ignite"));
                MenuMisc.AddGroupLabel("Auto pot usage");
                _potion = MenuMisc.Add("potion", new CheckBox("Use potions"));
                _potionMinHP = MenuMisc.Add("potionminHP", new Slider("Minimum Health % to use potion", 70));
                _potionMinMP = MenuMisc.Add("potionMinMP", new Slider("Minimum Mana % to use potion", 20));
                 MenuMisc.AddGroupLabel("Other");
                _WToCursor = MenuMisc.Add("WToCuror",
                    new KeyBind("Jump to cursor", false, KeyBind.BindTypes.HoldActive, 'H'));
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
                _minQMana = MenuManaManager.Add("minQMana", new Slider("Minimum mana % to use Q", 25, 0, 100));
                _minWMana = MenuManaManager.Add("minWMana", new Slider("Minimum mana % to use W", 0, 0, 100));
                _minEMana = MenuManaManager.Add("minEMana", new Slider("Minimum mana % to use E", 35, 0, 100));
                _minRMana = MenuManaManager.Add("minRMana", new Slider("Minimum mana % to use R", 0, 0, 100));
            }

            public static void Initialize()
            {
            }
        }

        public static class PredictionMenu
        {
            private static readonly Menu MenuPrediction;
            private static readonly Slider _minWHCCombo;
            private static readonly Slider _minWHCKillSteal;

            public static HitChance MinWHCCombo
            {
                get { return Util.GetHitChanceSliderValue(_minWHCCombo); }
            }
            public static HitChance MinWHCKillSteal
            {
                get { return Util.GetHitChanceSliderValue(_minWHCKillSteal); }
            }

            static PredictionMenu()
            {
                MenuPrediction = Config.Menu.AddSubMenu("Prediction");
                MenuPrediction.AddLabel("Here you can control the minimum HitChance to cast skills.");
                MenuPrediction.AddGroupLabel("W Prediction");
                MenuPrediction.AddGroupLabel("Combo");
                _minWHCCombo = Util.CreateHitChanceSlider("comboMinWHitChance", "Combo", HitChance.High, MenuPrediction);
                MenuPrediction.AddGroupLabel("Kill Steal");
                _minWHCKillSteal = Util.CreateHitChanceSlider("killStealMinWHitChance", "Kill Steal", HitChance.Medium, MenuPrediction);
            }

            public static void Initialize()
            {

            }
        }

        public static class DrawingMenu
        {
            private static readonly Menu MenuDrawing;
            private static readonly CheckBox _drawW;
            private static readonly CheckBox _drawE;
            private static readonly CheckBox _drawR;
            private static readonly CheckBox _drawIgnite;
            private static readonly CheckBox _drawOnlyReady;
            
            public static bool DrawW
            {
                get { return _drawW.CurrentValue; }
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
            public static bool DrawOnlyReady
            {
                get { return _drawOnlyReady.CurrentValue; }
            }

            static DrawingMenu()
            {
                MenuDrawing = Config.Menu.AddSubMenu("Drawing");
                _drawW = MenuDrawing.Add("drawW", new CheckBox("Draw W"));
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
