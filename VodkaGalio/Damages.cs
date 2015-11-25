using EloBuddy;
using EloBuddy.SDK;

namespace VodkaGalio
{
    class Damages
    {
       public static float QRawDamage()
        {
            return
                (int)
                    (new int[] { 80, 135, 190, 245, 300 }[SpellManager.Q.Level - 1] +
                     0.6 * (Player.Instance.TotalMagicalDamage));
        }

        public static float QDamage(Obj_AI_Base target)
        {
            return Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical, QRawDamage()) *
                   (Player.Instance.HasBuff("SummonerExhaustSlow") ? 0.6f : 1);
        }

        public static float ERawDamage()
        {
            return
                (int)
                    (new int[] { 60, 105, 150, 195, 240 }[SpellManager.E.Level - 1] +
                     0.5 * (Player.Instance.TotalMagicalDamage));
        }

        public static float EDamage(Obj_AI_Base target)
        {
            return Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical, ERawDamage()) *
                   (Player.Instance.HasBuff("SummonerExhaustSlow") ? 0.6f : 1);
        }

        public static float RRawDamage()
        {
            return
                (int)
                    (new int[] { 200, 300, 400 }[SpellManager.R.Level - 1] +
                     0.6 * (Player.Instance.TotalMagicalDamage));
        }

        public static float RDamage(Obj_AI_Base target)
        {
            return Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical, RRawDamage()) *
                   (Player.Instance.HasBuff("SummonerExhaustSlow") ? 0.6f : 1);
        }

        public static float IgniteDmg(Obj_AI_Base target)
        {
            return Player.Instance.GetSummonerSpellDamage(target, DamageLibrary.SummonerSpells.Ignite);
            
        }
    }
}