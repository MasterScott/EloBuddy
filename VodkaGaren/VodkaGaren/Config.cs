using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using SharpDX;

// ReSharper disable InconsistentNaming
// ReSharper disable MemberHidesStaticFromOuterClass
namespace VodkaGaren
{
    // I can't really help you with my layout of a good config class
    // since everyone does it the way they like it most, go checkout my
    // config classes I make on my GitHub if you wanna take over the
    // complex way that I use
    public static class Config
    {
        private const string MenuName = "VodkaGaren";

        private static readonly Menu Menu;

        static Config()
        {
            // Initialize the menu
            Menu = MainMenu.AddMenu(MenuName, MenuName.ToLower());
            Menu.AddGroupLabel("Welcome to this VodkaGaren!");
            Menu.AddLabel("Created by Haker");

            // Initialize the modes
            Modes.Initialize();
        }

        public static void Initialize()
        {
        }

        public static class Modes
        {
            private static readonly Menu ComboMenu;
            private static readonly Menu KillStealMenu;
            private static readonly Menu DrawingMenu;

            static Modes()
            {
                // Initialize the menu
                ComboMenu = Config.Menu.AddSubMenu("Combo");
                KillStealMenu = Config.Menu.AddSubMenu("KillSteal");
                DrawingMenu = Config.Menu.AddSubMenu("Drawing");


                // Initialize all modes
                // Combo
                Combo.Initialize();
                Menu.AddSeparator();

                // Kill Steal
                KillSteal.Initialize();
                Menu.AddSeparator();

                // Drawing
                Drawing.Initialize();
                Menu.AddSeparator();




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
                    // Initialize the menu values
                    ComboMenu.AddGroupLabel("Combo");
                    _useQ = ComboMenu.Add("comboUseQ", new CheckBox("Use Q"));
                    _useW = ComboMenu.Add("comboUseW", new CheckBox("Use W", false));
                    _useE = ComboMenu.Add("comboUseE", new CheckBox("Use E"));
                    _useR = ComboMenu.Add("comboUseR", new CheckBox("Use R"));
                    _minWEnemies = ComboMenu.Add("minWEnemies", new Slider("Minimum enemies near you to W", 1, 1, 5));
                }

                public static void Initialize()
                {
                }
            }

            public static class KillSteal
            {
                private static readonly CheckBox _useIgnite;
                private static readonly CheckBox _useR;

                public static bool UseR
                {
                    get { return _useR.CurrentValue; }
                }
                public static bool UseIgnite
                {
                    get { return _useIgnite.CurrentValue; }
                }

                static KillSteal()
                {
                    // Initialize the menu values
                    KillStealMenu.AddGroupLabel("KillSteal");
                    _useIgnite = KillStealMenu.Add("ksUseIgnite", new CheckBox("Use Ignite"));
                    _useR = KillStealMenu.Add("ksUseR", new CheckBox("Use R"));
                }

                public static void Initialize()
                {
                }
            }

            public static class Drawing
            {
                private static readonly CheckBox _drawERange;
                private static readonly CheckBox _drawRRange;
                private static readonly CheckBox _drawHPAfterR;

                public static bool DrawERange
                {
                    get { return _drawERange.CurrentValue; }
                }
                public static bool DrawRRange
                {
                    get { return _drawRRange.CurrentValue; }
                }

                public static bool DrawHPAfterR
                {
                    get { return _drawHPAfterR.CurrentValue; }
                }

                static Drawing()
                {
                    // Initialize the menu values
                    DrawingMenu.AddGroupLabel("Drawing");
                    _drawERange = DrawingMenu.Add("drawERange", new CheckBox("Draw E Range", false));
                    _drawRRange = DrawingMenu.Add("DrawRRange", new CheckBox("Draw R Range"));
                    _drawHPAfterR = DrawingMenu.Add("DrawHPAfterR", new CheckBox("Draw Enemy HP After R"));
                }

                public static void Initialize()
                {
                }
            }
        }
    }
}
