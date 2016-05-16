using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using Settings = VodkaXinZhao.Config.MiscMenu;

namespace VodkaXinZhao.Modes
{
    public sealed class PermaActive : ModeBase
    {
        static Item HealthPotion;
        static Item CorruptingPotion;
        static Item RefillablePotion;
        static Item HuntersPotion;
        static Item TotalBiscuit;

        static PermaActive()
        {
            HealthPotion = new Item(2003, 0);
            TotalBiscuit = new Item(2010, 0);
            CorruptingPotion = new Item(2033, 0);
            RefillablePotion = new Item(2031, 0);
            HuntersPotion = new Item(2032, 0);
        }

        public override bool ShouldBeExecuted()
        {
            return !Player.Instance.IsDead;
        }

        public override void Execute()
        {
            // KillSteal
            if (Settings.KsE && E.IsReady())
            {
                var enemy = EntityManager.Heroes.Enemies.FirstOrDefault(e => e.IsValidTarget(E.Range) && e.TotalShieldHealth() < Damages.EDamage(e) && !e.HasBuffOfType(BuffType.SpellImmunity) && !e.HasBuffOfType(BuffType.SpellShield));
                if (enemy != null)
                {
                    E.Cast(enemy);
                    Debug.WriteChat("Casting E in KS on {0}, Enemy HP: {1}", "" + enemy.ChampionName, "" + enemy.Health);
                    return;
                }
            }
            if (Settings.KsR && R.IsReady())
            {
                var enemy =
                    EntityManager.Heroes.Enemies.FirstOrDefault(
                        e =>
                            e.IsValidTarget(R.Range) && e.TotalShieldHealth() < Damages.RDamage(e) && !e.HasBuffOfType(BuffType.SpellImmunity) && !e.HasBuffOfType(BuffType.SpellShield));
                if (enemy != null)
                {
                    R.Cast();
                    Debug.WriteChat("Casting R in KS on {0}, Enemy HP: {1}", "" + enemy.ChampionName, "" + enemy.Health);
                    return;
                }
            }
            if (Settings.KsIgnite && HasIgnite && Ignite.IsReady())
            {
                var enemy =
                    EntityManager.Heroes.Enemies.FirstOrDefault(
                        e =>
                            e.IsValidTarget(Ignite.Range) && e.TotalShieldHealth() < Damages.IgniteDmg(e));
                if (enemy != null)
                {
                    Ignite.Cast(enemy);
                    Debug.WriteChat("Casting Ignite in KS on {0}, Enemy HP: {1}", "" + enemy.ChampionName, "" + enemy.Health);
                    return;
                }
            }


            // Automatic ult usage
            if (Settings.AutoR && R.IsReady() && !Player.Instance.IsRecalling())
            {
                var ultableEnemies = EntityManager.Heroes.Enemies.Count(e => e.IsValidTarget(R.Range));
                if (ultableEnemies >= Settings.AutoRMinEnemies && Player.Instance.HealthPercent >= Settings.AutoRMinHP)
                {
                    Debug.WriteChat("AutoCasting R, Enemies in range: {0}", "" + "" + ultableEnemies);
                    R.Cast();
                    return;
                }
            }

            // Potion manager
            if (Settings.Potion && !Player.Instance.IsInShopRange() && !Player.Instance.IsRecalling() && Player.Instance.HealthPercent <= Settings.potionMinHP && !(Player.Instance.HasBuff("RegenerationPotion") || Player.Instance.HasBuff("ItemCrystalFlaskJungle") || Player.Instance.HasBuff("ItemMiniRegenPotion") || Player.Instance.HasBuff("ItemCrystalFlask") || Player.Instance.HasBuff("ItemDarkCrystalFlask")))
            {
                if (Item.HasItem(HealthPotion.Id) && Item.CanUseItem(HealthPotion.Id))
                {
                    Debug.WriteChat("Using HealthPotion because below {0}% HP - have {1}% HP", String.Format("{0}", Settings.potionMinHP), String.Format("{0:##.##}", Player.Instance.HealthPercent));
                    HealthPotion.Cast();
                    return;
                }
                if (Item.HasItem(HuntersPotion.Id) && Item.CanUseItem(HuntersPotion.Id))
                {
                    Debug.WriteChat("Using HuntersPotion because below {0}% HP - have {1}% HP", String.Format("{0}", Settings.potionMinHP), String.Format("{0:##.##}", Player.Instance.HealthPercent));
                    HuntersPotion.Cast();
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
                if (Item.HasItem(HuntersPotion.Id) && Item.CanUseItem(HuntersPotion.Id))
                {
                    Debug.WriteChat("Using HuntersPotion because below {0}% MP - have {1}% MP", String.Format("{0}", Settings.potionMinMP), String.Format("{0:##.##}", Player.Instance.ManaPercent));
                    HuntersPotion.Cast();
                    return;
                }
            }
        }
    }
}
