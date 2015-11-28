using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace VodkaDrMundo
{
    class Util
    {

        public static Slider CreateHCSlider(string identifier, string displayName, HitChance defaultValue, Menu menu)
        {
            var slider = menu.Add(identifier, new Slider(displayName, 3, 1, 3));
            var hcNames = new[] { "Low HitChance", "Medium HitChance", "High HitChance" };
            switch (defaultValue)
            {
                case HitChance.Low:
                    slider.CurrentValue = 1;
                    break;
                case HitChance.Medium:
                    slider.CurrentValue = 2;
                    break;
                case HitChance.High:
                    slider.CurrentValue = 3;
                    break;
                default:
                    slider.CurrentValue = 2;
                    break;
            }
            slider.DisplayName = hcNames[slider.CurrentValue-1];

            slider.OnValueChange +=
                delegate (ValueBase<int> sender, ValueBase<int>.ValueChangeArgs changeArgs)
                {
                    sender.DisplayName = hcNames[changeArgs.NewValue-1];
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
            switch (currVal)
            {
                case 1:
                    return HitChance.Low;
                case 2:
                    return HitChance.Medium;
                case 3:
                    return HitChance.High;
                default:
                    return (HitChance) currVal;
            }
        }
    }
}
