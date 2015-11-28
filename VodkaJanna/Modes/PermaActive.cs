using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using Settings = VodkaJanna.Config.MiscMenu;
using SettingsPrediction = VodkaJanna.Config.PredictionMenu;

namespace VodkaJanna.Modes
{
    public sealed class PermaActive : ModeBase
    {
        static Item HealthPotion;
        static Item CorruptingPotion;
        static Item RefillablePotion;
        static Item TotalBiscuit;

        static PermaActive()
        {
            HealthPotion = new Item(2003, 0);
            TotalBiscuit = new Item(2010, 0);
            CorruptingPotion = new Item(2033, 0);
            RefillablePotion = new Item(2031, 0);
        }

        public override bool ShouldBeExecuted()
        {
            return !Player.Instance.IsDead;
        }

        public override void Execute()
        {
            foreach (
                var enemy in
                    EntityManager.Heroes.Enemies.Where(
                        e => e.IsValidTarget()))
            {
                if (Settings.KsQ && SpellManager.Q.IsReady() && Damages.QDamage(enemy) > enemy.Health &&
                    SpellManager.Q.IsInRange(enemy))
                {
                    if (enemy.HasBuffOfType(BuffType.SpellImmunity) || enemy.HasBuffOfType(BuffType.SpellShield))
                    {
                        continue;
                    }
                    var pred = Q.GetPrediction(enemy);
                    if (pred.HitChance >= SettingsPrediction.MinQHCKillSteal)
                    {
                        Debug.WriteChat("Casting Q in KillSteal on {0}, who has {1} HP", enemy.ChampionName,
                            "" + enemy.Health);
                        Q.Cast(pred.CastPosition);
                        break;
                    }
                }

                if (Settings.KsW && SpellManager.W.IsReady() && Damages.WDamage(enemy) > enemy.Health &&
                    SpellManager.W.IsInRange(enemy))
                {
                    if (enemy.HasBuffOfType(BuffType.SpellImmunity) || enemy.HasBuffOfType(BuffType.SpellShield))
                    {
                        continue;
                    }
                    Debug.WriteChat("Casting W in KillSteal on {0}, who has {1} HP", enemy.ChampionName,
                        "" + enemy.Health);
                    W.Cast(enemy);
                    break;
                }

                if (SpellManager.Ignite != null && Settings.KsIgnite && SpellManager.Ignite.IsReady() &&
                    Damages.IgniteDmg(enemy) > enemy.Health && SpellManager.Ignite.IsInRange(enemy))
                {
                    Debug.WriteChat("Casting Ignite in KillSteal on {0}, who has {1} HP", enemy.ChampionName, "" + enemy.Health);
                    SpellManager.Ignite.Cast(enemy);
                    break;
                }

            }

            // Automatic ult usage
            if (Settings.AutoR && R.IsReady() && !Player.Instance.IsRecalling() && !Player.Instance.IsInShopRange())
            {
                var woundedAround =
                    EntityManager.Heroes.Allies.Count(a => !a.IsDead && !a.IsRecalling() && R.IsInRange(a) && a.HealthPercent <= Settings.AutoRMinHP);
                var enemiesAround = EntityManager.Heroes.Enemies.Count(e => e.IsValid() && !e.IsRecalling() && e.Distance(Player.Instance.Position) < 1600);
                if (woundedAround > 0 && enemiesAround >= Settings.AutoRMinEnemies)
                {
                    Debug.WriteChat("AutoCasting R, Wounded allies: {0}, Enemies near in 1600 range: {1}", "" + woundedAround, "" + enemiesAround);
                    R.Cast();
                }
            }

            // Potion manager
            if (Settings.Potion && !Player.Instance.IsInShopRange())
            {
                if (Player.Instance.HealthPercent <= Settings.potionMinHP && !(Player.Instance.HasBuff("RegenerationPotion") || Player.Instance.HasBuff("ItemMiniRegenPotion") || Player.Instance.HasBuff("ItemCrystalFlask") || Player.Instance.HasBuff("ItemDarkCrystalFlask")))
                {
                    if (Item.HasItem(HealthPotion.Id) && Item.CanUseItem(HealthPotion.Id))
                    {
                        Debug.WriteChat("Using HealthPotion because below {0}% HP - have {1}% HP", String.Format("{0}", Settings.potionMinHP), String.Format("{0:##.##}", Player.Instance.HealthPercent));
                        HealthPotion.Cast();
                        return;
                    }
                    if (Item.HasItem(TotalBiscuit.Id) && Item.CanUseItem(TotalBiscuit.Id))
                    {
                        Debug.WriteChat("Using TotalBiscuitOfRejuvenation because below {0}% HP - have {1}% HP", String.Format("{0}", Settings.potionMinHP), String.Format("{0:##.##}", Player.Instance.HealthPercent));
                        TotalBiscuit.Cast();
                        return;
                    }
                    if (Item.HasItem(RefillablePotion.Id) && Item.CanUseItem(RefillablePotion.Id))
                    {
                        Debug.WriteChat("Using RefillablePotion because below {0}% HP - have {1}% HP", String.Format("{0}", Settings.potionMinHP), String.Format("{0:##.##}", Player.Instance.HealthPercent));
                        RefillablePotion.Cast();
                        return;
                    }
                    if (Item.HasItem(CorruptingPotion.Id) && Item.CanUseItem(CorruptingPotion.Id))
                    {
                        Debug.WriteChat("Using CorruptingPotion because below {0}% HP - have {1}% HP", String.Format("{0}", Settings.potionMinHP), String.Format("{0:##.##}", Player.Instance.HealthPercent));
                        CorruptingPotion.Cast();
                        return;
                    }
                }
                if (Player.Instance.ManaPercent <= Settings.potionMinMP && !(Player.Instance.HasBuff("RegenerationPotion") || Player.Instance.HasBuff("ItemMiniRegenPotion") || Player.Instance.HasBuff("ItemCrystalFlask") || Player.Instance.HasBuff("ItemDarkCrystalFlask")))
                {
                    if (Item.HasItem(CorruptingPotion.Id) && Item.CanUseItem(CorruptingPotion.Id))
                    {
                        Debug.WriteChat("Using HealthPotion because below {0}% MP - have {1}% MP", String.Format("{0}", Settings.potionMinMP), String.Format("{0:##.##}", Player.Instance.ManaPercent));
                        CorruptingPotion.Cast();
                        return;
                    }
                }
            }
        }
    }
}
