using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using Settings = VodkaJanna.Config.MiscMenu;

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
            return true;
        }

        public override void Execute()
        {
            if (Player.Instance.IsDead)
            {
                return;
            }
            // Automatic ult usage
            if (Settings.AutoR && R.IsReady() && !Player.Instance.IsRecalling() && !Player.Instance.IsInShopRange())
            {
                var wounded =
                    EntityManager.Heroes.Allies.Where(a =>!a.IsDead && !a.IsRecalling() && R.IsInRange(a) && a.HealthPercent <= Settings.AutoRMinHP);
                var enemies = EntityManager.Heroes.Enemies.Where(e => !e.IsDead && !e.IsRecalling() && e.Distance(Player.Instance.Position) < 1600);
                if (wounded.Count() > 0 && enemies.Count() >= Settings.AutoRMinEnemies)
                {
                    Debug.WriteChat("AutoCasting R, Wounded allies: {0}, Enemies near: {1}", ""+wounded.Count(), ""+enemies.Count());
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
                        Debug.WriteChat("Using HealthPotion because below {0}% HP - have {1}% HP", ""+Settings.potionMinHP, ""+Player.Instance.HealthPercent);
                        HealthPotion.Cast();
                        return;
                    }
                    if (Item.HasItem(TotalBiscuit.Id) && Item.CanUseItem(TotalBiscuit.Id))
                    {
                        Debug.WriteChat("Using TotalBiscuitOfRejuvenation because below {0}% HP - have {1}% HP", "" + Settings.potionMinHP, "" + Player.Instance.HealthPercent);
                        TotalBiscuit.Cast();
                        return;
                    }
                    if (Item.HasItem(RefillablePotion.Id) && Item.CanUseItem(RefillablePotion.Id))
                    {
                        Debug.WriteChat("Using RefillablePotion because below {0}% HP - have {1}% HP", "" + Settings.potionMinHP, "" + Player.Instance.HealthPercent);
                        RefillablePotion.Cast();
                        return;
                    }
                    if (Item.HasItem(CorruptingPotion.Id) && Item.CanUseItem(CorruptingPotion.Id))
                    {
                        Debug.WriteChat("Using CorruptingPotion because below {0}% HP - have {1}% HP", "" + Settings.potionMinHP, "" + Player.Instance.HealthPercent);
                        CorruptingPotion.Cast();
                        return;
                    }
                }
                if (Player.Instance.ManaPercent <= Settings.potionMinMP && !(Player.Instance.HasBuff("RegenerationPotion") || Player.Instance.HasBuff("ItemMiniRegenPotion") || Player.Instance.HasBuff("ItemCrystalFlask") || Player.Instance.HasBuff("ItemDarkCrystalFlask")))
                {
                    if (Item.HasItem(CorruptingPotion.Id) && Item.CanUseItem(CorruptingPotion.Id))
                    {
                        Debug.WriteChat("Using HealthPotion because below {0}% MP - have {1}% MP", "" + Settings.potionMinMP, "" + Player.Instance.ManaPercent);
                        CorruptingPotion.Cast();
                        return;
                    }
                }
            }
        }
    }
}
