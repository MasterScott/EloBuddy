using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using Settings = VodkaTwitch.Config.ModesMenu.Combo;
using SettingsMana = VodkaTwitch.Config.ManaManagerMenu;

namespace VodkaTwitch.Modes
{
    public sealed class Combo : ModeBase
    {
        static Item Cutlass;
        static Item BOTRK;

        static Combo()
        {
            Cutlass = new Item(ItemId.Bilgewater_Cutlass, 450);
            BOTRK = new Item(ItemId.Blade_of_the_Ruined_King, 450);
        }

        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo);
        }

        public override void Execute()
        {
            if (Settings.UseQ && Q.IsReady() && !QActive && PlayerMana >= SettingsMana.MinQMana)
            {
                Q.Cast();
                Debug.WriteChat("Casting Q in Combo");
            }
            if (Settings.UseE && E.IsReady() && PlayerMana >= SettingsMana.MinEMana)
            {
                var enemy =
                    EntityManager.Heroes.Enemies
                        .FirstOrDefault(e => e.IsValidTarget(E.Range) && EStacks(e) >= Settings.MinEStacks);
                if (enemy != null)
                {
                    E.Cast();
                    Debug.WriteChat("Casting E in Combo, Target: {0}, Distance: {1}", enemy.ChampionName,
                        "" + Player.Instance.Distance(enemy));
                }
            }
            if (Settings.UseR && R.IsReady() && PlayerMana >= SettingsMana.MinRMana)
            {
                var enemiesAround =
                    EntityManager.Heroes.Enemies
                        .Count(e => e.IsValidTarget(1000.0f));
                if (enemiesAround >= Settings.MinREnemies)
                {
                    R.Cast();
                    Debug.WriteChat("Casting R in Combo, Enemies in 1500 range: {0}", "" + enemiesAround);
                }
            }
            if (Settings.UseW && W.IsReady() && PlayerMana >= SettingsMana.MinWMana)
            {
                var enemy = TargetSelector.GetTarget(W.Range, DamageType.True);
                if (enemy != null && enemy.IsValidTarget(W.Range))
                {
                    var pred = W.GetPrediction(enemy);
                    if (pred.HitChance >= HitChance.High)
                    {
                        W.Cast(pred.CastPosition);
                        Debug.WriteChat("Casting W in Combo, Target: {0}, Distance: {1}", enemy.ChampionName,
                            "" + Player.Instance.Distance(enemy));
                    }
                }
            }
            // Items
            if (Settings.UseItems)
            {
                var enemy = TargetSelector.GetTarget(BOTRK.Range, DamageType.Physical);
                if (enemy != null)
                {
                    if (CanUseItem(ItemId.Bilgewater_Cutlass))
                    {
                        Cutlass.Cast(enemy);
                        Debug.WriteChat("Using Bilgewater Cutlass");
                    } else if (CanUseItem(ItemId.Blade_of_the_Ruined_King) &&
                               enemy.HealthPercent <= Settings.MaxBOTRKHPEnemy && PlayerHealth <= Settings.MaxBOTRKHPPlayer)
                    {
                        BOTRK.Cast(enemy);
                        Debug.WriteChat("Using BOTRK");
                    }
                }
            }
        }

        private bool CanUseItem(ItemId id)
        {
            return Item.HasItem(id) && Item.CanUseItem(id);
        }
    }
}
