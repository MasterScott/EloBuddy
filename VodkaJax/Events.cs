using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Rendering;
using SharpDX;
using System;
using System.Linq;
using SettingsDrawing = VodkaJax.Config.DrawingMenu;
using SettingsMana = VodkaJax.Config.ManaManagerMenu;
using SettingsMisc = VodkaJax.Config.MiscMenu;
using SettingsModes = VodkaJax.Config.ModesMenu;

namespace VodkaJax
{
    public static class Events
    {

        static Item Youmuu;
        private static float PlayerMana
        {
            get { return Player.Instance.ManaPercent; }
        }

        static Events()
        {
            Youmuu = new Item(ItemId.Youmuus_Ghostblade);
            Orbwalker.OnPostAttack += OrbwalkerOnOnPostAttack;
            Drawing.OnDraw += OnDraw;
        }

        private static void OrbwalkerOnOnPostAttack(AttackableUnit target, EventArgs args)
        {
            if (SettingsModes.Combo.UseItems && Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo) && CanUseItem(ItemId.Youmuus_Ghostblade))
            {
                Youmuu.Cast();
            }
            // No sense in checking if W is off cooldown
            if (!SpellManager.W.IsReady())
            {
                return;
            }
            // Check if we should use W to attack heroes
            if ((SettingsModes.Combo.UseW && Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo)) ||
                (SettingsModes.Harass.UseW && Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass)) ||
                (Orbwalker.LaneClearAttackChamps && SettingsModes.LaneClear.UseW &&
                 Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear)))
            {
                if (target is AIHeroClient && PlayerMana >= SettingsMana.MinWMana)
                {
                    SpellManager.W.Cast();
                    Orbwalker.ResetAutoAttack();
                    Player.IssueOrder(GameObjectOrder.AttackUnit, target);
                    Debug.WriteChat("Casting W, because attacking enemy hero in Combo or Harras or LaneClear.");
                    return;
                }
            }
            // Check if we should use W to attack minions/monsters/turrets
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear) ||
                Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear))
            {
                if (target is Obj_AI_Minion && PlayerMana >= SettingsMana.MinWMana)
                {
                    if (SettingsModes.JungleClear.UseW && target.Team == GameObjectTeam.Neutral)
                    {
                        SpellManager.W.Cast();
                        Orbwalker.ResetAutoAttack();
                        Player.IssueOrder(GameObjectOrder.AttackUnit, target);
                        Debug.WriteChat("Casting W, because attacking monster in JungleClear");
                    }
                    else if (SettingsModes.LaneClear.UseW && target.IsEnemy)
                    {
                        SpellManager.W.Cast();
                        Orbwalker.ResetAutoAttack();
                        Player.IssueOrder(GameObjectOrder.AttackUnit, target);
                        Debug.WriteChat("Casting W, because attacking minion in LaneClear");
                    }
                }
            }
        }

        public static void Initialize()
        {

        }

        private static void OnDraw(EventArgs args)
        {
            if (SettingsDrawing.DrawQ)
            {
                if (!(SettingsDrawing.DrawOnlyReady && !SpellManager.Q.IsReady()))
                {
                    Circle.Draw(Color.LightBlue, SpellManager.Q.Range, Player.Instance.Position);
                }
            }
            if (SettingsDrawing.DrawE)
            {
                if (!(SettingsDrawing.DrawOnlyReady && !SpellManager.Q.IsReady()))
                {
                    Circle.Draw(Color.LightGreen, SpellManager.E.Range, Player.Instance.Position);
                }
            }
            if (SettingsDrawing.DrawIgnite && SpellManager.HasIgnite())
            {
                if (!(SettingsDrawing.DrawOnlyReady && !SpellManager.Ignite.IsReady()))
                {
                    Circle.Draw(Color.Red, SpellManager.Ignite.Range, Player.Instance.Position);
                }
            }
        }

        private static bool CanUseItem(ItemId id)
        {
            return Item.HasItem(id) && Item.CanUseItem(id);
        }
    }
}
