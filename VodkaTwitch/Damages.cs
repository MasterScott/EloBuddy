using EloBuddy;
using EloBuddy.SDK;

namespace VodkaTwitch
{
    class Damages
    {
        public static float ERawDamage(Obj_AI_Base target)
        {
            var stacks = SpellManager.EStacks(target);
            if (stacks <= 0)
            {
                return 0.0f;
            }
            return
                (int)
                    (new int[] { 20, 35, 50, 65, 80 }[SpellManager.E.Level - 1]) +
                     stacks * (new int[] { 15, 20, 25, 30, 35 }[SpellManager.E.Level - 1] + 0.2f * Player.Instance.TotalMagicalDamage + 0.25f * (Player.Instance.TotalAttackDamage - Player.Instance.BaseAttackDamage));
        }

        public static float EDamage(Obj_AI_Base target)
        {
            return Player.Instance.CalculateDamageOnUnit(target, DamageType.Physical, ERawDamage(target)) *
                   (Player.Instance.HasBuff("SummonerExhaustSlow") ? 0.6f : 1);
        }

        public static float IgniteDmg(Obj_AI_Base target)
        {
            return Player.Instance.GetSummonerSpellDamage(target, DamageLibrary.SummonerSpells.Ignite);
        }
    }
}