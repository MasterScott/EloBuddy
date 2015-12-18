using System;
using System.Linq;
using System.Security.Cryptography;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX;
using Settings = VodkaAzir.Config.MiscMenu;
using SettingsPrediction = VodkaAzir.Config.PredictionMenu;
using SettingsMana = VodkaAzir.Config.ManaManagerMenu;

namespace VodkaAzir.Modes
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
            if (Settings.KsQ || Settings.KsE || (Settings.KsIgnite && HasIgnite))
            {
                var enemies = EntityManager.Heroes.Enemies.Where(e => e.IsValidTarget(1500.0f));
                if (Settings.KsQ && Q.IsReady())
                {
                    var target = enemies.FirstOrDefault(e => Q.IsInRange(e) && e.Health < Damages.QDamage(e));
                    if (target != null)
                    {
                        if (Orbwalker.AzirSoldiers.Count > 0)
                        {
                            foreach (var soldier in Orbwalker.AzirSoldiers) // Q KS
                            {
                                var pred = Prediction.Position.PredictLinearMissile(target, Q.Range, Q.Width,
                                Q.CastDelay, Q.Speed, Int32.MaxValue, soldier.Position, true);
                                if (pred.HitChance >= SettingsPrediction.MinQHCKillSteal)
                                {
                                    Q.Cast(pred.CastPosition.Extend(pred.UnitPosition, 115.0f).To3D());
                                    Debug.WriteChat("Casting Q in KS on {0}, who has {1}HP.", target.ChampionName, target.Health.ToString());
                                    break;
                                }
                            }
                        }
                        else if (Orbwalker.AzirSoldiers.Count == 0 && W.IsReady() && PlayerManaExact >= 110) // WQ KS
                        {
                            var wCastPos = _PlayerPos.Extend(target, W.Range).To3D();
                            var pred = Prediction.Position.PredictLinearMissile(target, Q.Range, Q.Width,
                                Q.CastDelay, Q.Speed, Int32.MaxValue, wCastPos, true);
                            if (pred.HitChance >= SettingsPrediction.MinQHCKillSteal)
                            {
                                W.Cast(wCastPos);
                                Q.Cast(pred.CastPosition.Extend(pred.UnitPosition, 115.0f).To3D());
                                Debug.WriteChat("Casting WQ in KS on {0}, who has {1}HP.", target.ChampionName, target.Health.ToString());
                            }
                        }
                    }
                }
                else if (Settings.KsE && E.IsReady())
                {
                    var target = enemies.FirstOrDefault(e => E.IsInRange(e) && e.Health < Damages.EDamage(e));
                    if (target != null)
                    {
                        if (Orbwalker.AzirSoldiers.Count > 0)
                        {
                            foreach (var soldier in Orbwalker.AzirSoldiers) // E KS
                            {
                                if (target.Position.Between(_PlayerPos, soldier.Position))
                                {
                                    E.Cast();
                                    Debug.WriteChat("Casting E in KS on {0}, who has {1}HP.", target.ChampionName, target.Health.ToString());
                                    break;
                                }
                            }
                        }
                        else if (Orbwalker.AzirSoldiers.Count == 0 && W.IsReady() && _PlayerPos.Distance(target) <= W.Range-50 && PlayerManaExact >= 100) // WE KS
                        {
                            var wCastPos = _PlayerPos.Extend(target, W.Range).To3D();
                            W.Cast(wCastPos);
                            E.Cast();
                            Debug.WriteChat("Casting WE in KS on {0}, who has {1}HP.", target.ChampionName, target.Health.ToString());
                        }
                    }
                }
                else if (Settings.KsIgnite && HasIgnite && Ignite.IsReady())
                {
                    var target = enemies.FirstOrDefault(e => Ignite.IsInRange(e) && e.Health < Damages.IgniteDmg(e));
                    if (target != null)
                    {
                        Ignite.Cast(target);
                        Debug.WriteChat("Casting Ignite in KS on {0}, who has {1}HP.", target.ChampionName, target.Health.ToString());
                    }
                }
            }

            // DashToCursor
            if (Settings.QWEToCursor)
            {
                var cursorPos = Game.CursorPos;
                var distanceToPlayer = cursorPos.Distance(_PlayerPos);
                if (E.IsReady() && distanceToPlayer > Orbwalker.HoldRadius)
                {
                    var soldier =
                        Orbwalker.ValidAzirSoldiers.FirstOrDefault(s => s.Distance(cursorPos) <= 150 && s.Distance(_PlayerPos) < E.Range);
                    if (soldier != null)
                    {
                        E.Cast();
                        Debug.WriteChat("Dashing to existing soldier.");
                    }
                    else if (PlayerManaExact >= 100 && _PlayerPos.Distance(cursorPos) < W.Range && W.IsReady())
                    {
                        W.Cast(cursorPos);
                        E.Cast();
                        Debug.WriteChat("Using WE to dash to location");
                    }
                    else if (PlayerManaExact >= 170 && _PlayerPos.Distance(cursorPos) < Q.Range - 100 &&  W.IsReady() && Q.IsReady())
                    {
                        W.Cast(_PlayerPos.Extend(cursorPos, W.Range).To3D());
                        Core.DelayAction(() => {
                            E.Cast();
                        }, 200);
                        Q.Cast(cursorPos);
                        Debug.WriteChat("Using QWE to dash to location");
                    }
                    else
                    {
                        Orbwalker.MoveTo(cursorPos);
                    }
                }
                else
                {
                    Orbwalker.MoveTo(cursorPos);
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
                }
            }
        }
    }
}
