using EloBuddy;
using EloBuddy.SDK;

namespace VodkaJanna
{
    class Damages
    {
        
        public static float QDamage(Obj_AI_Base target)
        {
            return Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical, QRawDamage())*
                   (Player.Instance.HasBuff("SummonerExhaustSlow") ? 0.6f : 1);
        }

        public static float QRawDamage()
        {
            return
                (int)
                    (new int[] { 60, 85, 110, 135, 160 }[SpellManager.Q.Level - 1] +
                     0.35 * (Player.Instance.TotalMagicalDamage));
        }

        public static float WRawDamage()
        {
            return
                (int)
                    (new int[] { 60, 115, 170, 225, 280 }[SpellManager.E.Level - 1] +
                     0.5 * (Player.Instance.TotalMagicalDamage));
        }

        public static float WDamage(Obj_AI_Base target)
        {
            return Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical, WRawDamage()) *
                   (Player.Instance.HasBuff("SummonerExhaustSlow") ? 0.6f : 1);
        }

        public static float IgniteDmg(Obj_AI_Base target)
        {
            return Player.Instance.GetSummonerSpellDamage(target, DamageLibrary.SummonerSpells.Ignite);
            
        }
    }
}