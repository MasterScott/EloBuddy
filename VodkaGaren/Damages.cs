using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;

namespace VodkaGaren
{
    class Damages
    {
        // Returns Q damage, including Armor, Penetrations and Exhaust.
        public static float QDamage(Obj_AI_Base target)
        {
            return Player.Instance.CalculateDamageOnUnit(target, DamageType.Physical, QRawDamage(target)) *
                   (Player.Instance.HasBuff("SummonerExhaustSlow") ? 0.6f : 1) + (target.HasBuff("garenpassiveenemytarget") ? target.MaxHealth * 0.01f : 0);
        }

        private static float QRawDamage(Obj_AI_Base target)
        {
            return
                (int)
                    (new int[] { 30, 55, 80, 105, 130 }[SpellManager.Q.Level - 1] +
                     1.4 * (Player.Instance.TotalAttackDamage));
        }

        public static float RDamage(Obj_AI_Base target)
        {
            if (target.HasBuff("garenpassiveenemytarget"))
            {
                // Villain Buff
                return Player.Instance.CalculateDamageOnUnit(target, DamageType.True, RRawDamage(target));
            }
            else
            {
                return Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical, RRawDamage(target));
            }
        }

        public static float RRawDamage(Obj_AI_Base target)
        {
            return (new float[] { 175.0f, 350.0f, 525.0f }[SpellManager.R.Level - 1] +
                     new float[] { 0.286f, 0.333f, 0.4f }[SpellManager.R.Level - 1] * (target.MaxHealth - target.Health));
        }

        public static float IgniteDmg(Obj_AI_Base target)
        {
            return Player.Instance.GetSummonerSpellDamage(target, DamageLibrary.SummonerSpells.Ignite);
            
        }
    }
}