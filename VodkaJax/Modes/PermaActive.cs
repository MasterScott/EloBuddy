using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using System;
using System.Linq;
using Settings = VodkaJax.Config.MiscMenu;
using SettingsCombo = VodkaJax.Config.ModesMenu.Combo;
using SettingsHarass = VodkaJax.Config.ModesMenu.Harass;
using SettingsMana = VodkaJax.Config.ManaManagerMenu;

namespace VodkaJax.Modes
{
    public sealed class PermaActive : ModeBase
    {
        static Item HealthPotion;
        static Item CorruptingPotion;
        static Item RefillablePotion;
        static Item HuntersPotion;
        static Item TotalBiscuit;
        static int lastKSTime; // Prevents spamming abillities in KS mode

        static PermaActive()
        {
            HealthPotion = new Item(2003, 0);
            TotalBiscuit = new Item(2010, 0);
            CorruptingPotion = new Item(2033, 0);
            RefillablePotion = new Item(2031, 0);
            HuntersPotion = new Item(2032, 0);
            lastKSTime = Environment.TickCount;
        }

        public override bool ShouldBeExecuted()
        {
            return !Player.Instance.IsDead;
        }

        public override void Execute()
        {
            // KillSteal
            if (Environment.TickCount - lastKSTime > 500 && ((Settings.KsQW && Q.IsReady()) || (Settings.KsIgnite && HasIgnite)))
            {
                var enemies = EntityManager.Heroes.Enemies.Where(e => e.IsValidTarget(Q.Range-50));
                if (Settings.KsQW && Q.IsReady() && W.IsReady() && PlayerMana >= 95)
                {
                    var target = enemies.FirstOrDefault(e => Q.IsInRange(e) && e.TotalShieldHealth() < (Damages.QDamage(e) + Damages.WDamage(e)) && !e.HasBuffOfType(BuffType.SpellImmunity) && !e.HasBuffOfType(BuffType.SpellShield));
                    if (target != null)
                    {
                        lastKSTime = Environment.TickCount;
                        bool wcast = false;
                        if (target.TotalShieldHealth() > Damages.QDamage(target))
                        {
                            W.Cast();
                            Orbwalker.ResetAutoAttack();
                            wcast = true;
                        }
                        Q.Cast(target);
                        Debug.Write("Casting Q{0} in KS on {1} who has {2}HP.", (wcast ? "+W" : ""), target.ChampionName, target.Health.ToString());
                        return;
                    }
                }
                if (Settings.KsIgnite && HasIgnite && Ignite.IsReady())
                {
                    var target = enemies.FirstOrDefault(e => W.IsInRange(e) && e.TotalShieldHealth() < Damages.IgniteDmg(e));
                    if (target != null)
                    {
                        lastKSTime = Environment.TickCount;
                        Ignite.Cast(target);
                        Debug.Write("Casting Ignite in KS on {0} who has {1}HP.", target.ChampionName, target.Health.ToString());
                        return;
                    }
                }
            }
            // Auto E stun
            if (Player.Instance.HasBuff("JaxCounterStrike") && E.IsReady())
            {
                var countenemies = EntityManager.Heroes.Enemies.Count(e => e.IsValidTarget(E.Range));
                if (countenemies >= SettingsCombo.MinEStunEnemies)
                {
                    E.Cast();
                    Debug.WriteChat("Casting E early to stun "+countenemies+" enemies.");
                }
            }
            // Potion manager
            if (Settings.Potion && !Player.Instance.IsInShopRange() && Player.Instance.HealthPercent <= Settings.potionMinHP && !(Player.Instance.HasBuff("RegenerationPotion") || Player.Instance.HasBuff("ItemCrystalFlaskJungle") || Player.Instance.HasBuff("ItemMiniRegenPotion") || Player.Instance.HasBuff("ItemCrystalFlask") || Player.Instance.HasBuff("ItemDarkCrystalFlask")))
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
            if (Settings.Potion && !Player.Instance.IsInShopRange() && Player.Instance.ManaPercent <= Settings.potionMinMP && !(Player.Instance.HasBuff("RegenerationPotion") || Player.Instance.HasBuff("ItemMiniRegenPotion") || Player.Instance.HasBuff("ItemCrystalFlask") || Player.Instance.HasBuff("ItemDarkCrystalFlask")))
            {
                if (Item.HasItem(CorruptingPotion.Id) && Item.CanUseItem(CorruptingPotion.Id))
                {
                    Debug.WriteChat("Using CorruptingPotion because below {0}% MP - have {1}% MP", String.Format("{0}", Settings.potionMinMP), String.Format("{0:##.##}", Player.Instance.ManaPercent));
                    CorruptingPotion.Cast();
                }
                if (Item.HasItem(HuntersPotion.Id) && Item.CanUseItem(HuntersPotion.Id))
                {
                    Debug.WriteChat("Using HuntersPotion because below {0}% HP - have {1}% HP", String.Format("{0}", Settings.potionMinHP), String.Format("{0:##.##}", Player.Instance.HealthPercent));
                    HuntersPotion.Cast();
                    return;
                }
            }
        }
    }
}
