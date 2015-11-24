using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Rendering;
using SharpDX;
using SettingsMisc = VodkaDrMundo.Config.MiscMenu;
using SettingsModes = VodkaDrMundo.Config.ModesMenu;
using SettingsDrawing = VodkaDrMundo.Config.DrawingMenu;
using SettingsHealth = VodkaDrMundo.Config.HealthManagerMenu;

namespace VodkaDrMundo
{
    public static class Events
    {
        private static float PlayerHealth
        {
            get { return Player.Instance.HealthPercent; }
        }

        static Events()
        {
            Gapcloser.OnGapcloser += GapcloserOnOnGapcloser;
            Orbwalker.OnPostAttack += OrbwalkerOnPostAttack;
            Drawing.OnDraw += OnDraw;
        }

        private static void GapcloserOnOnGapcloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs e)
        {
            if (SettingsMisc.GapcloserQ && sender.IsEnemy && SpellManager.Q.IsReady() && sender.IsValidTarget() &&
                e.End.Distance(Player.Instance) < 300)
            {
                var pred = SpellManager.Q.GetPrediction(sender);
                if (pred.HitChance >= HitChance.Low)
                {
                    SpellManager.Q.Cast(pred.CastPosition);
                }
            }
        }

        private static void OrbwalkerOnPostAttack(AttackableUnit target, EventArgs args)
        {
            // Use E
            // No sense in checking if W is off cooldown
            if (!SpellManager.E.IsReady())
            {
                return;
            }
            // Check if we should use E to attack heroes
            if ((SettingsModes.Combo.UseE && Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo)) ||
                (Orbwalker.LaneClearAttackChamps && SettingsModes.LaneClear.UseE &&
                 Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear)))
            {
                if (target is AIHeroClient && PlayerHealth >= SettingsHealth.MinEHealth)
                {
                    Debug.WriteChat("Casting W, because attacking enemy in Combo or Harras");
                    SpellManager.E.Cast();
                    Orbwalker.ResetAutoAttack();
                    Player.IssueOrder(GameObjectOrder.AttackUnit, target);
                    return;
                }
            }
            // Check if we should use W to attack minions/monsters/turrets
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear) ||
                Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear))
            {
                if (target is Obj_AI_Minion && PlayerHealth >= SettingsHealth.MinEHealth)
                {
                    if (SettingsModes.JungleClear.UseE && target.Team == GameObjectTeam.Neutral)
                    {
                        Debug.WriteChat("Casting E, because attacking monster in JungleClear");
                        SpellManager.E.Cast();
                        Orbwalker.ResetAutoAttack();
                        Player.IssueOrder(GameObjectOrder.AttackUnit, target);
                    }
                    else if (SettingsModes.LaneClear.UseW && target.Team != GameObjectTeam.Neutral)
                    {
                        Debug.WriteChat("Casting E, because attacking minion in LaneClear");
                        SpellManager.E.Cast();
                        Orbwalker.ResetAutoAttack();
                        Player.IssueOrder(GameObjectOrder.AttackUnit, target);
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
                    Circle.Draw(Color.LightBlue, SettingsModes.Combo.MaxQDistance, Player.Instance.Position);
                }
            }
            if (SettingsDrawing.DrawW)
            {
                if (!(SettingsDrawing.DrawOnlyReady && !SpellManager.W.IsReady()))
                {
                    Circle.Draw(Color.Orange, SpellManager.W.Range, Player.Instance.Position);
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
    }
}
