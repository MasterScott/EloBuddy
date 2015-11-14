using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using Settings = VodkaJanna.Config.Misc;

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
                bool woundedAllyNear = EntityManager.Heroes.Allies.Where(a => !a.IsDead && !a.IsRecalling() && R.IsInRange(a) && a.HealthPercent <= Config.Misc.AutoRMinHP).Count() > 0;
                bool enemiesNear =
                    EntityManager.Heroes.Enemies.Count(e => !e.IsDead && !e.IsRecalling() && e.Distance(Player.Instance.Position) < 1600) >= Config.Misc.AutoRMinEnemies;
                if (woundedAllyNear && enemiesNear)
                {
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
                        HealthPotion.Cast();
                        return;
                    }
                    if (Item.HasItem(TotalBiscuit.Id) && Item.CanUseItem(TotalBiscuit.Id))
                    {
                        TotalBiscuit.Cast();
                        return;
                    }
                    if (Item.HasItem(RefillablePotion.Id) && Item.CanUseItem(RefillablePotion.Id))
                    {
                        RefillablePotion.Cast();
                        return;
                    }
                    if (Item.HasItem(CorruptingPotion.Id) && Item.CanUseItem(CorruptingPotion.Id))
                    {
                        CorruptingPotion.Cast();
                        return;
                    }
                }
                if (Player.Instance.ManaPercent <= Settings.potionMinMP && !(Player.Instance.HasBuff("RegenerationPotion") || Player.Instance.HasBuff("ItemMiniRegenPotion") || Player.Instance.HasBuff("ItemCrystalFlask") || Player.Instance.HasBuff("ItemDarkCrystalFlask")))
                {
                    if (Item.HasItem(CorruptingPotion.Id) && Item.CanUseItem(CorruptingPotion.Id))
                    {
                        CorruptingPotion.Cast();
                        return;
                    }
                }
            }
        }
    }
}
