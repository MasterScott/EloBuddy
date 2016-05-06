using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using Settings = VodkaJax.Config.ModesMenu.Combo;
using SettingsMana = VodkaJax.Config.ManaManagerMenu;

namespace VodkaJax.Modes
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
            // Don't do anything if ulting
            if (_Player.Spellbook.IsChanneling)
            {
                return;
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
                        Debug.WriteChat("Using Bilgewater Cutlass on {0}", enemy.ChampionName);
                    }
                    else if (CanUseItem(ItemId.Blade_of_the_Ruined_King) &&
                             enemy.HealthPercent <= Settings.MaxBOTRKHPEnemy &&
                             PlayerHealthPercent <= Settings.MaxBOTRKHPPlayer)
                    {
                        BOTRK.Cast(enemy);
                        Debug.WriteChat("Using BOTRK on {0}", enemy.ChampionName);
                    }
                }
            }
            // Skills
            if (Settings.UseQ && Q.IsReady() && PlayerMana >= SettingsMana.MinQMana)
            {
                AIHeroClient target = null;
                if (TargetSelector.SeletedEnabled && TargetSelector.SelectedTarget != null)
                {
                    target = TargetSelector.SelectedTarget;
                }
                else
                {
                    target = TargetSelector.GetTarget(R.Range, DamageType.Magical);
                }
                if (target != null)
                {
                    var distance = target.Distance(Player.Instance);
                    if (PlayerManaExact > 65 && distance >= Settings.MinQDistance && distance < Q.Range)
                    {
                        Q.Cast(target);
                        Debug.WriteChat("Casting Q in Combo, distance {0}.", ""+distance);
                    }
                }
            }
            if (Settings.UseE && E.IsReady() && PlayerMana >= SettingsMana.MinEMana)
            {
                AIHeroClient target = null;
                if (TargetSelector.SeletedEnabled && TargetSelector.SelectedTarget != null)
                {
                    target = TargetSelector.SelectedTarget;
                }
                else
                {
                    target =
                        EntityManager.Heroes.Enemies.FirstOrDefault(
                            e => e.IsValidTarget() && e.Distance(Player.Instance) <= 180.0f && !e.HasBuffOfType(BuffType.SpellImmunity) && !e.HasBuffOfType(BuffType.SpellShield));
                }
                if (target != null)
                {
                    E.Cast();
                    Debug.WriteChat("Casting E in Combo on {0}", target.ChampionName);
                }
            }
        }

        private bool CanUseItem(ItemId id)
        {
            return Item.HasItem(id) && Item.CanUseItem(id);
        }
    }
}
