using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using Settings = VodkaAzir.Config.ModesMenu.Combo;
using SettingsMana = VodkaAzir.Config.ManaManagerMenu;
using SettingsPrediction = VodkaAzir.Config.PredictionMenu;

namespace VodkaAzir.Modes
{
    public sealed class Combo : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo);
        }

        public override void Execute()
        {
            if (Settings.UseR && R.IsReady() && PlayerMana >= SettingsMana.MinRMana)
            {
                var target = TargetSelector.GetTarget(R.Range, DamageType.Magical);
                if (target != null)
                {
                    var pred = R.GetPrediction(target);
                    if (pred.HitChance >= SettingsPrediction.MinRHCCombo && target.IsValidTarget())
                    {
                        R.Cast(pred.CastPosition);
                        Debug.WriteChat("Casting R in Combo on {0}", target.ChampionName);
                    }
                }
            }
            if (Orbwalker.AzirSoldiers.Count == 0) // no soldiers
            {
                if (Settings.UseW && W.IsReady() && PlayerMana >= SettingsMana.MinWMana) // so let's make one
                {
                    // make one for Q usage
                    if (Settings.UseQ && Q.IsReady() && PlayerMana >= SettingsMana.MinQMana && PlayerManaExact >= 110)
                    {
                        var target = TargetSelector.GetTarget(Q.Range, DamageType.Magical);
                        if (target != null)
                        {
                            var castPos = _PlayerPos.Extend(target, W.Range).To3D();
                            W.Cast(castPos);
                            Debug.WriteChat("Casting W in Combo on {0} to be able to use Q", target.ChampionName);
                        }
                    }
                    // Make one for autoattacking
                    else
                    {
                        var target = TargetSelector.GetTarget(W.Range + 100, DamageType.Magical);
                        if (target != null)
                        {
                            var castPos = _PlayerPos.Extend(target, W.Range).To3D();
                            W.Cast(castPos);
                            Debug.WriteChat("Casting W in Combo on {0} to autoattack him.", target.ChampionName);
                        }
                    }
                }
            }
            else
            {
                if (Settings.UseQ && Q.IsReady() && PlayerMana >= SettingsMana.MinQMana)
                {
                    var target = TargetSelector.GetTarget(Q.Range, DamageType.Magical);
                    if (target != null)
                    {
                        foreach (var soldier in Orbwalker.AzirSoldiers)
                        {
                            var pred = Prediction.Position.PredictLinearMissile(target, Q.Range, Q.Width, Q.CastDelay,
                                Q.Speed, Int32.MaxValue, soldier.Position, true);
                            if (pred.HitChance >= SettingsPrediction.MinQHCCombo)
                            {
                                Q.Cast(pred.CastPosition.Extend(pred.UnitPosition, 115.0f).To3D());
                                Debug.WriteChat("Casting Q in Combo on {0}", target.ChampionName);
                                break;
                            }
                        }
                    }
                }
                if (Settings.UseE && E.IsReady() && PlayerMana >= SettingsMana.MinEMana) // My name is FireDash
                {
                    var target = TargetSelector.GetTarget(E.Range, DamageType.Magical);
                    if (target != null && target.HealthPercent < Settings.MaxEnemyHPToE)
                    {
                        foreach (var soldier in Orbwalker.AzirSoldiers.Where(s => s.Distance(_Player) < E.Range))
                        {
                            if (target.Position.Between(_PlayerPos, target.Position))
                            {
                                E.Cast();
                                Debug.WriteChat("Casting E in Combo on {0}", target.ChampionName);
                                break;
                            }
                        }
                    }
                }
                if (Settings.UseW && W.IsReady() && PlayerMana >= SettingsMana.MinWMana) // Make some more soldiers
                {
                    var target = TargetSelector.GetTarget(W.Range, DamageType.Magical);
                    if (target != null)
                    {
                        var pred = W.GetPrediction(target);
                        if (pred.HitChance >= HitChance.Low)
                        {
                            W.Cast(pred.CastPosition);
                            Debug.WriteChat("Casting W in Combo on {0}", target.ChampionName);
                        }
                    }
                }
            }
        }
    }
}
