using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Rendering;
using SharpDX;
using SettingsMisc = VodkaJanna.Config.MiscMenu;
using SettingsModes = VodkaJanna.Config.ModesMenu;
using SettingsDrawing = VodkaJanna.Config.DrawingMenu;
using SettingsMana = VodkaJanna.Config.ManaManagerMenu;

namespace VodkaJanna
{
    public static class Events
    {
        private static bool canAntiGapR = true;
        private static bool canInterruptR = true;

        private static float PlayerMana
        {
            get { return Player.Instance.ManaPercent; }
        }

        static Events()
        {
            Interrupter.OnInterruptableSpell += InterrupterOnOnInterruptableSpell;
            Gapcloser.OnGapcloser += GapcloserOnOnGapcloser;
            Orbwalker.OnPostAttack += OrbwalkerOnOnPostAttack;
            Drawing.OnDraw += OnDraw;
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
                    Circle.Draw(Color.Cyan, SpellManager.Q.Range, Player.Instance.Position);
                }
            }
            if (SettingsDrawing.DrawW)
            {
                if (!(SettingsDrawing.DrawOnlyReady && !SpellManager.W.IsReady()))
                {
                    Circle.Draw(Color.Magenta, SpellManager.W.Range, Player.Instance.Position);
                }
            }
            if (SettingsDrawing.DrawE)
            {
                if (!(SettingsDrawing.DrawOnlyReady && !SpellManager.E.IsReady()))
                {
                    Circle.Draw(Color.White, SpellManager.E.Range, Player.Instance.Position);
                }
            }
            if (SettingsDrawing.DrawR)
            {
                if (!(SettingsDrawing.DrawOnlyReady && !SpellManager.R.IsReady()))
                {
                    Circle.Draw(Color.Yellow, SpellManager.R.Range, Player.Instance.Position);
                }
            }
        }

        private static void InterrupterOnOnInterruptableSpell(Obj_AI_Base sender, Interrupter.InterruptableSpellEventArgs interruptableSpellEventArgs)
        {
            if (!sender.IsEnemy || !(sender is AIHeroClient) || SpellManager.IsUlting() || Player.Instance.IsRecalling())
            {
                return;
            }
            if (SettingsMisc.InterrupterUseQ && SpellManager.Q.IsReady() && sender.IsEnemy && SpellManager.Q.IsInRange(sender))
            {
                Debug.WriteChat("Interrupting with Q, Target: {0}, Distance: {1}", ((AIHeroClient)sender).ChampionName, "" + sender.Distance(Player.Instance));
                canInterruptR = false;
                SpellManager.Q.Cast(sender);
                Core.DelayAction(() => { SpellManager.Q.Cast(sender); }, 1);
                Core.DelayAction(() => { canInterruptR = true; }, 1000);
                return;
            }
            if (SettingsMisc.InterrupterUseR && SpellManager.R.IsReady() && sender.IsEnemy && SpellManager.R.IsInRange(sender) && canInterruptR)
            {
                Debug.WriteChat("Interrupting with R, Target: {0}, Distance: {1}", ((AIHeroClient)sender).ChampionName, "" + sender.Distance(Player.Instance));
                SpellManager.R.Cast();
            }
        }

        private static void GapcloserOnOnGapcloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs gapcloserEventArgs)
        {
            if (!sender.IsEnemy || Player.Instance.IsRecalling())
            {
                return;
            }
            if (SettingsMisc.AntigapcloserUseQ && SpellManager.Q.IsReady() && gapcloserEventArgs.End.Distance(Player.Instance) < 200)
            {
                Debug.WriteChat("AntiGapcloser with Q, Target: {0}, Distance: {1}, GapcloserSpell: {2}", sender.ChampionName, "" + sender.Distance(Player.Instance), gapcloserEventArgs.SpellName);
                canAntiGapR = false;
                if (gapcloserEventArgs.Type == Gapcloser.GapcloserType.Targeted &&
                    gapcloserEventArgs.End.Distance(Player.Instance.Position) < 50)
                {
                    SpellManager.Q.Cast(sender);
                    Core.DelayAction(() => { SpellManager.Q.Cast(sender); }, 1);
                    Core.DelayAction(() => { canAntiGapR = true; }, 200);
                }
                else
                {
                    SpellManager.Q.Cast(gapcloserEventArgs.End);
                    Core.DelayAction(() => { SpellManager.Q.Cast(gapcloserEventArgs.End); }, 1);
                    Core.DelayAction(() => { canAntiGapR = true; }, 200);
                }
                return;
            }
            if (SettingsMisc.AntigapcloserUseR && !SpellManager.R.IsOnCooldown && SpellManager.R.IsInRange(gapcloserEventArgs.End) && canAntiGapR)
            {
                Debug.WriteChat("AntiGapcloser with R, Target: {0}, Distance: {1}, GapcloserSpell: {2}", sender.ChampionName, "" + sender.Distance(Player.Instance), gapcloserEventArgs.SpellName);
                SpellManager.R.Cast();
            }
        }

        private static void OrbwalkerOnOnPostAttack(AttackableUnit target, EventArgs args)
        {
            // No sense in checking if E is off cooldown
            if (!SpellManager.E.IsReady())
            {
                return;
            }
            // Check if we should use E to attack minions/monsters/turrets
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear) ||
                Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear))
            {
                if (target is Obj_AI_Minion && PlayerMana >= SettingsMana.MinEMana)
                {
                    if (SettingsModes.JungleClear.UseE && target.Team == GameObjectTeam.Neutral)
                    {
                        Debug.WriteChat("Casting E, because attacking monster in JungleClear");
                        SpellManager.E.Cast(Player.Instance);
                    }
                    else if (SettingsModes.LaneClear.UseE && target.IsEnemy)
                    {
                        Debug.WriteChat("Casting E, because attacking minion in LaneClear");
                        SpellManager.E.Cast(Player.Instance);
                    }
                }

            }
        }
    }
}
