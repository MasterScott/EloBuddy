using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using Settings = VodkaGaren.Config.MiscMenu;
using System;

namespace VodkaGaren.Modes
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
            // Kill Steal
            foreach (var enemy in EntityManager.Heroes.Enemies.Where(e => e.IsEnemy && e.IsVisible && !e.IsDead && !e.IsZombie && e.Health > 0))
            {
                if (Settings.KsR && SpellManager.R.IsLearned && SpellManager.R.IsReady() && Damages.RDamage(enemy) > enemy.TotalShieldHealth() && SpellManager.R.IsInRange(enemy))
                {
                    if (enemy.HasBuffOfType(BuffType.SpellImmunity) || enemy.HasBuffOfType(BuffType.SpellShield))
                    {
                        continue;
                    }
                    SpellManager.R.Cast(enemy);
                    break;
                }

                if (Settings.KsIgnite && SpellManager.Ignite.IsReady() &&
                    Damages.IgniteDmg(enemy) > enemy.Health && SpellManager.Ignite.IsInRange(enemy))
                {
                    SpellManager.Ignite.Cast(enemy);
                    break;
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
            }

        }
    }
}
