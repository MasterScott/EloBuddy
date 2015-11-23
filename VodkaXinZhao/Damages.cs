using EloBuddy;
using EloBuddy.SDK;

namespace VodkaXinZhao
{
    class Damages
    {
       public static float QRawDamage()
        {
            return
                (int)
                     ((new int[] { 15, 30, 45, 60, 75 }[SpellManager.Q.Level - 1] +
                     1.2 * (ObjectManager.Player.TotalAttackDamage))*3);
        }

        public static float QDamage(Obj_AI_Base target)
        {
            return Player.Instance.CalculateDamageOnUnit(target, DamageType.Physical, QRawDamage()) *
                   (Player.Instance.HasBuff("SummonerExhaustSlow") ? 0.6f : 1);
        }

        public static float ERawDamage()
        {
            return
                (int)
                    (new int[] { 70, 110, 150, 190, 230 }[SpellManager.E.Level - 1] +
                     0.6 * (ObjectManager.Player.TotalMagicalDamage));
        }

        public static float EDamage(Obj_AI_Base target)
        {
            return Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical, ERawDamage()) *
                   (Player.Instance.HasBuff("SummonerExhaustSlow") ? 0.6f : 1);
        }

        public static float RRawDamage(Obj_AI_Base target)
        {
            return
                (int)
                    (new int[] { 75, 175, 275 }[SpellManager.R.Level - 1] +
                     1.0f * (ObjectManager.Player.TotalAttackDamage) + 0.15f * target.Health);
        }

        public static float RDamage(Obj_AI_Base target)
        {
            return Player.Instance.CalculateDamageOnUnit(target, DamageType.Physical, RRawDamage(target)) *
                   (Player.Instance.HasBuff("SummonerExhaustSlow") ? 0.6f : 1);
        }

        public static float IgniteDmg(Obj_AI_Base target)
        {
            return ObjectManager.Player.GetSummonerSpellDamage(target, DamageLibrary.SummonerSpells.Ignite);
        }
    }
}