using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace VodkaSmite
{
    class Util
    {
        public readonly static string[] MonstersNames =
        {
            "TT_Spiderboss", "TTNGolem", "TTNWolf", "TTNWraith",
            "SRU_Blue", "SRU_Gromp", "SRU_Murkwolf", "SRU_Razorbeak",
            "SRU_Red", "SRU_Krug", "SRU_Dragon", "Sru_Crab", "SRU_Baron", "SRU_RiftHerald"
        };

        public static readonly string[] SmiteNames =
        {
            "summonersmite", "s5_summonersmiteplayerganker", "s5_summonersmiteduel"
        };

        public static Slider CreateHCSlider(string identifier, string displayName, HitChance defaultValue, Menu menu)
        {
            var slider = menu.Add(identifier, new Slider(displayName, (int)defaultValue, 0, 8));
            var hcNames = new[]
            {"Unknown", "Impossible", "Collision", "Low", "AveragePoint", "Medium", "High", "Dashing", "Immobile"};
            slider.DisplayName = hcNames[slider.CurrentValue];
            slider.OnValueChange +=
                delegate (ValueBase<int> sender, ValueBase<int>.ValueChangeArgs changeArgs)
                {
                    sender.DisplayName = hcNames[changeArgs.NewValue];
                };
            return slider;
        }

        public static HitChance GetHCSliderHitChance(Slider slider)
        {
            if (slider == null)
            {
                return HitChance.Impossible;
            }
            var currVal = slider.CurrentValue;
            return (HitChance)currVal;
        }
    }
}
