using EloBuddy;
using EloBuddy.SDK;

namespace VodkaAzir
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

        public static float QRawDamage()
        {
            return (new[] { 65.0f, 85.0f, 105.0f, 125.0f, 145.0f }[SpellManager.Q.Level - 1]) + 0.5f * PlayerAP;
        }

        public static float QDamage(Obj_AI_Base target)
        {
            return Player.Instance.CalculateDamageOnUnit(target, DamageType.Physical, QRawDamage()) *
                   (Player.Instance.HasBuff("SummonerExhaustSlow") ? 0.6f : 1);
        }
        
        public static float ERawDamage()
        {
            return (new[] { 60.0f, 90.0f, 120.0f, 150.0f, 180.0f }[SpellManager.E.Level - 1]) + 0.4f * PlayerAP;
        }

        public static float EDamage(Obj_AI_Base target)
        {
            return Player.Instance.CalculateDamageOnUnit(target, DamageType.Physical, ERawDamage()) *
                   (Player.Instance.HasBuff("SummonerExhaustSlow") ? 0.6f : 1);
        }

        public static float RRawDamage()
        {
            return (new[] { 150.0f, 225.0f, 300.0f }[SpellManager.R.Level - 1]) + 0.6f * PlayerAP;
        }

        public static float RDamage(Obj_AI_Base target)
        {
            return Player.Instance.CalculateDamageOnUnit(target, DamageType.Physical, RRawDamage()) *
                   (Player.Instance.HasBuff("SummonerExhaustSlow") ? 0.6f : 1);
        }

        public static float IgniteDmg(Obj_AI_Base target)
        {
            return Player.Instance.GetSummonerSpellDamage(target, DamageLibrary.SummonerSpells.Ignite);
        }
    }
}