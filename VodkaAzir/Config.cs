using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

// ReSharper disable InconsistentNaming
// ReSharper disable MemberHidesStaticFromOuterClass

namespace VodkaAzir
{
    public static class Config
    {
        private const string MenuName = "VodkaAzir";

        private static readonly Menu Menu;

        static Config()
        {
            Menu = MainMenu.AddMenu(MenuName, MenuName.ToLower());
            Menu.AddGroupLabel("Welcome to VodkaAzir");
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
                private static readonly Slider _maxEnemyHPToE;

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

                public static int MaxEnemyHPToE
                {
                    get { return _maxEnemyHPToE.CurrentValue; }
                }

                static Combo()
                {
                    MenuModes.AddGroupLabel("Combo");
                    _useQ = MenuModes.Add("comboUseQ", new CheckBox("Use Q"));
                    _useW = MenuModes.Add("comboUseW", new CheckBox("Use W"));
                    _useE = MenuModes.Add("comboUseE", new CheckBox("Use E"));
                    _useR = MenuModes.Add("comboUseR", new CheckBox("Use R"));
                    _maxEnemyHPToE = MenuModes.Add("comboMaxEnemyHPToE", new Slider("E only enemies below {0}% HP", 30, 0, 100));
                }

                public static void Initialize()
                {
                }
            }

            public static class Harass
            {
                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useW;
                private static readonly CheckBox _useE;
                
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
                
                static Harass()
                {
                    MenuModes.AddGroupLabel("Harass");
                    _useQ = MenuModes.Add("harassUseQ", new CheckBox("Use Q"));
                    _useW = MenuModes.Add("harassUseW", new CheckBox("Use W"));
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
                private static readonly Slider _minQTargets;
                private static readonly Slider _minWTargets;
                private static readonly Slider _maxSoldiersForFarming;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }

                public static bool UseW
                {
                    get { return _useW.CurrentValue; }
                }
                
                public static int MinQTargets
                {
                    get { return _minQTargets.CurrentValue; }
                }
                
                public static int MinWTargets
                {
                    get { return _minWTargets.CurrentValue; }
                }
                
                public static int MaxSoldiersForFarming
                {
                    get { return _maxSoldiersForFarming.CurrentValue; }
                }

                static LaneClear()
                {
                    MenuModes.AddGroupLabel("LaneClear");
                    _useQ = MenuModes.Add("laneUseQ", new CheckBox("Use Q"));
                    _useW = MenuModes.Add("laneUseW", new CheckBox("Use W"));
                    _minQTargets = MenuModes.Add("minQTargetsLC", new Slider("Minimum targets for Q", 4, 1, 10));
                    _minWTargets = MenuModes.Add("minWTargetsLC", new Slider("Minimum targets for W", 3, 1, 10));
                    _maxSoldiersForFarming = MenuModes.Add("maxSoldiersLC", new Slider("Max soldiers to farm with", 3, 1, 3));
                }

                public static void Initialize()
                {
                }
            }

            public static class JungleClear
            {
                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useW;
                private static readonly Slider _minQTargets;
                private static readonly Slider _minWTargets;
                private static readonly Slider _maxSoldiersForFarming;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }

                public static bool UseW
                {
                    get { return _useW.CurrentValue; }
                }

                public static int MinQTargets
                {
                    get { return _minQTargets.CurrentValue; }
                }

                public static int MinWTargets
                {
                    get { return _minWTargets.CurrentValue; }
                }

                public static int MaxSoldiersForFarming
                {
                    get { return _maxSoldiersForFarming.CurrentValue; }
                }

                static JungleClear()
                {
                    MenuModes.AddGroupLabel("JungleClear");
                    _useQ = MenuModes.Add("jungleUseQ", new CheckBox("Use Q"));
                    _useW = MenuModes.Add("jungleUseW", new CheckBox("Use W"));
                    _minQTargets = MenuModes.Add("minQTargetsJC", new Slider("Minimum targets for Q", 2, 1, 10));
                    _minWTargets = MenuModes.Add("minWTargetsJC", new Slider("Minimum targets for W", 1, 1, 10));
                    _maxSoldiersForFarming = MenuModes.Add("maxSoldiersJC", new Slider("Max soldiers to farm with", 3, 1, 3));
                }

                public static void Initialize()
                {
                }
            }

            public static class Flee
            {
                private static readonly CheckBox _useQWE;
                private static readonly CheckBox _useR;

                public static bool UseQWE
                {
                    get { return _useQWE.CurrentValue; }
                }
                
                public static bool UseR
                {
                    get { return _useR.CurrentValue; }
                }

                static Flee()
                {
                    MenuModes.AddGroupLabel("Flee");
                    _useQWE = MenuModes.Add("fleeUseQWE", new CheckBox("Use QWE combo"));
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
            private static readonly CheckBox _potion;
            private static readonly CheckBox _ksQ;
            private static readonly CheckBox _ksE;
            private static readonly CheckBox _ksIgnite;
            private static readonly KeyBind _QWEToCursor;
            private static readonly Slider _potionMinHP;
            private static readonly Slider _potionMinMP;

            public static bool InterruptR
            {
                get { return _interruptR.CurrentValue; }
            }
            public static bool KsQ
            {
                get { return _ksE.CurrentValue; }
            }
            public static bool KsE
            {
                get { return _ksE.CurrentValue; }
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
            public static bool QWEToCursor
            {
                get { return _QWEToCursor.CurrentValue; }
            }

            static MiscMenu()
            {
                MenuMisc = Config.Menu.AddSubMenu("Misc");
                MenuMisc.AddGroupLabel("Interrupter");
                _interruptR = MenuMisc.Add("interruptR", new CheckBox("Use R to interrupt important spells", false));
                MenuMisc.AddGroupLabel("KillSteal");
                _ksQ = MenuMisc.Add("ksQ", new CheckBox("KillSteal Q"));
                _ksE = MenuMisc.Add("ksE", new CheckBox("KillSteal E"));
                _ksIgnite = MenuMisc.Add("ksIgnite", new CheckBox("KillSteal Ignite"));
                MenuMisc.AddGroupLabel("Auto pot usage");
                _potion = MenuMisc.Add("potion", new CheckBox("Use potions"));
                _potionMinHP = MenuMisc.Add("potionminHP", new Slider("Minimum Health % to use potion", 70));
                _potionMinMP = MenuMisc.Add("potionMinMP", new Slider("Minimum Mana % to use potion", 20));
                 MenuMisc.AddGroupLabel("Other");
                _QWEToCursor = MenuMisc.Add("QWEToCuror",
                    new KeyBind("Dash to cursor", false, KeyBind.BindTypes.HoldActive, 'H'));
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
                _minQMana = MenuManaManager.Add("minQMana", new Slider("Minimum mana % to use Q", 0, 0, 100));
                _minWMana = MenuManaManager.Add("minWMana", new Slider("Minimum mana % to use W", 0, 0, 100));
                _minEMana = MenuManaManager.Add("minEMana", new Slider("Minimum mana % to use E", 0, 0, 100));
                _minRMana = MenuManaManager.Add("minRMana", new Slider("Minimum mana % to use R", 0, 0, 100));
            }

            public static void Initialize()
            {
            }
        }

        public static class PredictionMenu
        {
            private static readonly Menu MenuPrediction;
            private static readonly Slider _minQHCCombo;
            private static readonly Slider _minQHCHarass;
            private static readonly Slider _minQHCKillSteal;
            private static readonly Slider _minEHCCombo;
            private static readonly Slider _minEHCHarass;
            private static readonly Slider _minEHCKillSteal;
            private static readonly Slider _minRHCCombo;
            private static readonly Slider _minRHCHarass;
            private static readonly Slider _minRHCKillSteal;

            public static HitChance MinQHCCombo
            {
                get { return Util.GetHitChanceSliderValue(_minQHCCombo); }
            }

            public static HitChance MinQHCHarass
            {
                get { return Util.GetHitChanceSliderValue(_minQHCHarass); }
            }
            public static HitChance MinQHCKillSteal
            {
                get { return Util.GetHitChanceSliderValue(_minQHCKillSteal); }
            }

            public static HitChance MinEHCCombo
            {
                get { return Util.GetHitChanceSliderValue(_minEHCCombo); }
            }

            public static HitChance MinEHCHarass
            {
                get { return Util.GetHitChanceSliderValue(_minEHCHarass); }
            }

            public static HitChance MinEHCKillSteal
            {
                get { return Util.GetHitChanceSliderValue(_minEHCKillSteal); }
            }

            public static HitChance MinRHCCombo
            {
                get { return Util.GetHitChanceSliderValue(_minRHCCombo); }
            }

            public static HitChance MinRHCHarass
            {
                get { return Util.GetHitChanceSliderValue(_minRHCHarass); }
            }

            public static HitChance MinRHCKillSteal
            {
                get { return Util.GetHitChanceSliderValue(_minRHCKillSteal); }
            }

            static PredictionMenu()
            {
                MenuPrediction = Config.Menu.AddSubMenu("Prediction");
                MenuPrediction.AddLabel("Here you can control the minimum HitChance to cast skills.");
                MenuPrediction.AddGroupLabel("Q Prediction");
                MenuPrediction.AddGroupLabel("Combo");
                _minQHCCombo = Util.CreateHitChanceSlider("comboMinQHitChance", "Combo", HitChance.Medium, MenuPrediction);
                MenuPrediction.AddGroupLabel("Harass");
                _minQHCHarass = Util.CreateHitChanceSlider("harassMinQHitChance", "Harass", HitChance.High, MenuPrediction);
                MenuPrediction.AddGroupLabel("Kill Steal");
                _minQHCKillSteal = Util.CreateHitChanceSlider("killStealMinQHitChance", "Kill Steal", HitChance.Medium, MenuPrediction);
                MenuPrediction.AddSeparator();
                MenuPrediction.AddGroupLabel("E Prediction");
                MenuPrediction.AddGroupLabel("Combo");
                _minEHCCombo = Util.CreateHitChanceSlider("comboMinEHitChance", "Combo", HitChance.Medium, MenuPrediction);
                MenuPrediction.AddGroupLabel("Harass");
                _minEHCHarass = Util.CreateHitChanceSlider("harassMinEHitChance", "Harass", HitChance.High, MenuPrediction);
                MenuPrediction.AddGroupLabel("Kill Steal");
                _minEHCKillSteal = Util.CreateHitChanceSlider("killStealMinEHitChance", "Kill Steal", HitChance.Medium, MenuPrediction);
                MenuPrediction.AddSeparator();
                MenuPrediction.AddGroupLabel("R Prediction");
                MenuPrediction.AddGroupLabel("Combo");
                _minRHCCombo = Util.CreateHitChanceSlider("comboMinRHitChance", "Combo", HitChance.Medium, MenuPrediction);
                MenuPrediction.AddGroupLabel("Harass");
                _minRHCHarass = Util.CreateHitChanceSlider("harassMinRHitChance", "Harass", HitChance.High, MenuPrediction);
                MenuPrediction.AddGroupLabel("Kill Steal");
                _minRHCKillSteal = Util.CreateHitChanceSlider("killStealMinRHitChance", "Kill Steal", HitChance.Medium, MenuPrediction);
                MenuPrediction.AddSeparator();
            }

            public static void Initialize()
            {

            }
        }

        public static class DrawingMenu
        {
            private static readonly Menu MenuDrawing;
            private static readonly CheckBox _drawQ;
            private static readonly CheckBox _drawW;
            private static readonly CheckBox _drawE;
            private static readonly CheckBox _drawR;
            private static readonly CheckBox _drawIgnite;
            private static readonly CheckBox _drawOnlyReady;

            public static bool DrawQ
            {
                get { return _drawQ.CurrentValue; }
            }
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
                _drawQ = MenuDrawing.Add("drawQ", new CheckBox("Draw Q"));
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
