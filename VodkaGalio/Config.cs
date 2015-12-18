using System;
using EloBuddy;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

// ReSharper disable InconsistentNaming
// ReSharper disable MemberHidesStaticFromOuterClass

namespace VodkaGalio
{
    public static class Config
    {
        private const string MenuName = "VodkaGalio";

        private static readonly Menu Menu;

        static Config()
        {
            Menu = MainMenu.AddMenu(MenuName, MenuName.ToLower());
            Menu.AddGroupLabel("Welcome to VodkaGalio");
            Menu.AddLabel("Created by Haker");
            Menu.AddLabel("Feel free to send me any suggestions you might have.");
            ModesMenu.Initialize();
            PredictionMenu.Initialize();
            var shielderSubMenu = Config.Menu.AddSubMenu("Shielder");
            Shielder.Shielder.Initialize(shielderSubMenu);
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
                    _useE = MenuModes.Add("harassUseE", new CheckBox("Use E", false));
                }

                public static void Initialize()
                {
                }
            }

            public static class LaneClear
            {
                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useE;
                private static readonly Slider _minQTargets;
                private static readonly Slider _minETargets;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
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

                static LaneClear()
                {
                    MenuModes.AddGroupLabel("LaneClear");
                    _useQ = MenuModes.Add("laneUseQ", new CheckBox("Use Q"));
                    _useE = MenuModes.Add("laneUseE", new CheckBox("Use E"));
                    _minQTargets = MenuModes.Add("minQTargetsLC", new Slider("Minimum targets for Q", 4, 1, 10));
                    _minETargets = MenuModes.Add("minETargetsLC", new Slider("Minimum targets for E", 4, 1, 10));
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
                    MenuModes.AddGroupLabel("LastHit");
                    _useQ = MenuModes.Add("lastHitUseQ", new CheckBox("Use Q"));
                    _useE = MenuModes.Add("lastHitUseE", new CheckBox("Use E"));
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
                    MenuModes.AddGroupLabel("JungleClear");
                    _useQ = MenuModes.Add("jungleUseQ", new CheckBox("Use Q"));
                    _useW = MenuModes.Add("jungleUseW", new CheckBox("Use W"));
                    _useE = MenuModes.Add("jungleUseE", new CheckBox("Use E"));
                    _minQTargets = MenuModes.Add("minQTargetsJC", new Slider("Minimum targets for Q", 2, 1, 10));
                    _minETargets = MenuModes.Add("minETargetsJC", new Slider("Minimum targets for E", 2, 1, 10));
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
            private static readonly CheckBox _antigapcloserQ;
            private static readonly CheckBox _autoR;
            private static readonly CheckBox _potion;
            private static readonly CheckBox _ksQ;
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
            public static bool AntigapcloserUseQ
            {
                get { return _antigapcloserQ.CurrentValue; }
            }
            public static bool AutoR
            {
                get { return _autoR.CurrentValue; }
            }
            public static bool KsQ
            {
                get { return _ksQ.CurrentValue; }
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
                MenuMisc.AddGroupLabel("Anti Gapcloser");
                _antigapcloserQ = MenuMisc.Add("antigapcloserUseQ", new CheckBox("Use Q against gapclosers"));
                MenuMisc.AddGroupLabel("KillSteal");
                _ksQ = MenuMisc.Add("ksQ", new CheckBox("KillSteal Q"));
                _ksE = MenuMisc.Add("ksE", new CheckBox("KillSteal E"));
                _ksR = MenuMisc.Add("ksR", new CheckBox("KillSteal R", false));
                _ksIgnite = MenuMisc.Add("ksIgnite", new CheckBox("KillSteal Ignite", false));
                MenuMisc.AddGroupLabel("Auto R usage");
                _autoR = MenuMisc.Add("autoR", new CheckBox("Use R automatically when X enemies around"));
                _autoRMinHP = MenuMisc.Add("autoRMinHP", new Slider("Minimum HP % to ult automatically", 50));
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
                _minQMana = MenuManaManager.Add("minQMana", new Slider("Minimum mana % to use Q", 30, 0, 100));
                _minWMana = MenuManaManager.Add("minWMana", new Slider("Minimum mana % to use W", 60, 0, 100));
                _minEMana = MenuManaManager.Add("minEMana", new Slider("Minimum mana % to use E", 30, 0, 100));
                _minRMana = MenuManaManager.Add("minRMana", new Slider("Minimum mana % to use R", 20, 0, 100));
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
            public static bool DrawOnlyReady
            {
                get { return _drawOnlyReady.CurrentValue; }
            }

            static DrawingMenu()
            {
                MenuDrawing = Config.Menu.AddSubMenu("Drawing");
                _drawQ = MenuDrawing.Add("drawQ", new CheckBox("Draw Q"));
                _drawW = MenuDrawing.Add("drawW", new CheckBox("Draw W", false));
                _drawE = MenuDrawing.Add("drawE", new CheckBox("Draw E"));
                _drawR = MenuDrawing.Add("drawR", new CheckBox("Draw R"));
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

        public static class PredictionMenu
        {
            private static readonly Menu MenuPrediction;
            private static readonly Slider _minQHCCombo;
            private static readonly Slider _minQHCHarass;
            private static readonly Slider _minQHCLastHit;
            private static readonly Slider _minQHCKillSteal;
            private static readonly Slider _minQHCFlee;
            private static readonly Slider _minEHCCombo;
            private static readonly Slider _minEHCHarass;
            private static readonly Slider _minEHCLastHit;
            private static readonly Slider _minEHCKillSteal;

            public static HitChance MinQHCCombo
            {
                get { return Util.GetHCSliderHitChance(_minQHCCombo); }
            }

            public static HitChance MinQHCHarass
            {
                get { return Util.GetHCSliderHitChance(_minQHCHarass); }
            }
            
            public static HitChance MinQHCLastHit
            {
                get { return Util.GetHCSliderHitChance(_minQHCLastHit); }
            }

            public static HitChance MinQHCKillSteal
            {
                get { return Util.GetHCSliderHitChance(_minQHCKillSteal); }
            }

            public static HitChance MinQHCFlee
            {
                get { return Util.GetHCSliderHitChance(_minQHCFlee); }
            }

            public static HitChance MinEHCCombo
            {
                get { return Util.GetHCSliderHitChance(_minEHCCombo); }
            }

            public static HitChance MinEHCHarass
            {
                get { return Util.GetHCSliderHitChance(_minEHCHarass); }
            }

            public static HitChance MinEHCLastHit
            {
                get { return Util.GetHCSliderHitChance(_minEHCLastHit); }
            }

            public static HitChance MinEHCKillSteal
            {
                get { return Util.GetHCSliderHitChance(_minEHCKillSteal); }
            }

            static PredictionMenu()
            {
                MenuPrediction = Config.Menu.AddSubMenu("Prediction");
                MenuPrediction.AddLabel("Here you can control the minimum HitChance to cast skills.");
                MenuPrediction.AddGroupLabel("Q Prediction");
                MenuPrediction.AddGroupLabel("Combo");
                _minQHCCombo = Util.CreateHCSlider("comboMinQHitChance", "Combo", HitChance.Medium, MenuPrediction);
                MenuPrediction.AddGroupLabel("Harass");
                _minQHCHarass = Util.CreateHCSlider("harassMinQHitChance", "Harass", HitChance.High, MenuPrediction);
                MenuPrediction.AddGroupLabel("Last Hit");
                _minQHCLastHit = Util.CreateHCSlider("lastHitMinQHitChance", "Last Hit", HitChance.Medium, MenuPrediction);
                MenuPrediction.AddGroupLabel("Kill Steal");
                _minQHCKillSteal = Util.CreateHCSlider("killStealMinQHitChance", "Kill Steal", HitChance.Medium, MenuPrediction);
                MenuPrediction.AddGroupLabel("Flee");
                _minQHCFlee = Util.CreateHCSlider("fleeMinQHitChance", "Flee", HitChance.Low, MenuPrediction);

                MenuPrediction.AddSeparator();
                MenuPrediction.AddGroupLabel("E Prediction");
                MenuPrediction.AddGroupLabel("Combo");
                _minEHCCombo = Util.CreateHCSlider("comboMinEHitChance", "Combo", HitChance.Medium, MenuPrediction);
                MenuPrediction.AddGroupLabel("Harass");
                _minEHCHarass = Util.CreateHCSlider("harassMinEHitChance", "Harass", HitChance.High, MenuPrediction);
                MenuPrediction.AddGroupLabel("Last Hit");
                _minEHCLastHit = Util.CreateHCSlider("lastHitMinEHitChance", "Last Hit", HitChance.High, MenuPrediction);
                MenuPrediction.AddGroupLabel("Kill Steal");
                _minEHCKillSteal = Util.CreateHCSlider("killStealMinEHitChance", "Kill Steal", HitChance.Medium, MenuPrediction);
            }

            public static void Initialize()
            {

            }
        }
    }
}
