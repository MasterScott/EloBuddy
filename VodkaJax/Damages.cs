using EloBuddy;
using EloBuddy.SDK;

namespace VodkaJax
{
    class Damages
    {
        private static AIHeroClient _Player
        {
            get { return Player.Instance; }
        }

        private static float PlayerAP
        {
            get { return Player.Instance.TotalMagicalDamage; }
        }

        private static float PlayerAD
        {
            get { return Player.Instance.TotalAttackDamage; }
        }

        private static float PlayerBonusAD
        {
            get { return Player.Instance.TotalAttackDamage - Player.Instance.BaseAttackDamage; }
        }

        public static float QRawDamage(Obj_AI_Base target)
        {
            return ((new[] { 70.0f, 110.0f, 150.0f, 190.0f, 230.0f }[SpellManager.Q.Level - 1]) + PlayerAP * 0.6f + PlayerAD);
        }

        public static float QDamage(Obj_AI_Base target)
        {
            return Player.Instance.CalculateDamageOnUnit(target, DamageType.Physical, QRawDamage(target)) *
                   (Player.Instance.HasBuff("SummonerExhaustSlow") ? 0.6f : 1);
        }

        public static float WRawDamage(Obj_AI_Base target)
        {
            return (new[] { 40.0f, 75.0f, 110.0f, 145.0f, 180.0f}[SpellManager.W.Level - 1]) + 0.6f * PlayerAP;
        }
        
        public static float WDamage(Obj_AI_Base target)
        {
            return Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical, WRawDamage(target)) *
                   (Player.Instance.HasBuff("SummonerExhaustSlow") ? 0.6f : 1);
        }

        public static float WAndAutoDamage(Obj_AI_Base target)
        {
            return (Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical, WRawDamage(target)) + Player.Instance.CalculateDamageOnUnit(target, DamageType.Physical, PlayerAD)) *
                   (Player.Instance.HasBuff("SummonerExhaustSlow") ? 0.6f : 1);
        }

        public static float IgniteDmg(Obj_AI_Base target)
        {
            return Player.Instance.GetSummonerSpellDamage(target, DamageLibrary.SummonerSpells.Ignite);
        }

        public static float SmiteDmgHero(AIHeroClient target)
        {
            return Player.Instance.CalculateDamageOnUnit(target, DamageType.True,
                20.0f + Player.Instance.Level * 8.0f);
        }
    }
}