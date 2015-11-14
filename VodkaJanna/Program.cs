using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Constants;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Rendering;
using SharpDX;
using VodkaJanna.Modes;

namespace VodkaJanna
{
    public static class Program
    {
        public const string ChampName = "Janna";

        public static void Main(string[] args)
        {
            Loading.OnLoadingComplete += OnLoadingComplete;
        }

        private static void OnLoadingComplete(EventArgs args)
        {
            if (Player.Instance.ChampionName != ChampName)
            {
                return;
            }

            Config.Initialize();
            SpellManager.Initialize();
            ModeManager.Initialize();

            Interrupter.OnInterruptableSpell += InterrupterOnOnInterruptableSpell;
            Gapcloser.OnGapcloser += GapcloserOnOnGapcloser;
            Orbwalker.OnPostAttack += OrbwalkerOnOnPostAttack;
            Drawing.OnDraw += OnDraw;

            Chat.Print("VodkaJanna loaded");
        }
        
        private static void InterrupterOnOnInterruptableSpell(Obj_AI_Base sender, Interrupter.InterruptableSpellEventArgs interruptableSpellEventArgs)
        {
            if (!sender.IsEnemy)
            {
                return;
            }
            if (Config.Misc.InterrupterUseQ && SpellManager.Q.IsReady() && sender is AIHeroClient && SpellManager.Q.IsInRange(sender))
            {
                SpellManager.Q.Cast(sender);
            }
            if (Config.Misc.InterrupterUseR && !SpellManager.R.IsOnCooldown && sender is AIHeroClient && SpellManager.R.IsInRange(sender))
            {
                SpellManager.R.Cast();
            }
        }

        private static void GapcloserOnOnGapcloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs gapcloserEventArgs)
        {
            if (!sender.IsEnemy)
            {
                return;
            }
            if (Config.Misc.AntigapcloserUseQ && SpellManager.Q.IsReady() && SpellManager.Q.IsInRange(gapcloserEventArgs.End))
            {
                SpellManager.Q.Cast(gapcloserEventArgs.End);
            }
            if (Config.Misc.AntigapcloserUseR && !SpellManager.R.IsOnCooldown && SpellManager.R.IsInRange(sender))
            {
                SpellManager.R.Cast();
            }
        }

        private static void OrbwalkerOnOnPostAttack(AttackableUnit target, EventArgs args)
        {
            // No sense in checking if E is off cooldown
            if (SpellManager.E.IsOnCooldown)
            {
                return;
            }
            // Check if we should use E to attack heroes
            if ((Config.Modes.Combo.UseE && Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo)) ||
                (Config.Modes.Harass.UseE && Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass)) ||
                (Orbwalker.LaneClearAttackChamps && Config.Modes.LaneClear.UseE &&
                 Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear)))
            {
                if (target is AIHeroClient && SpellManager.E.IsReady())
                {
                    SpellManager.E.Cast(Player.Instance);
                    return;
                }
            }
            // Check if we should use E to attack minions/monsters/turrets
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear) ||
                Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear))
            {
                if (SpellManager.E.IsReady())
                {
                    if (target is Obj_AI_Minion && target.Team == GameObjectTeam.Neutral && Config.Modes.JungleClear.UseE)
                    {
                        SpellManager.E.Cast(Player.Instance);
                    } else if (target is Obj_AI_Minion && target.IsEnemy && Config.Modes.LaneClear.UseE)
                    {
                        SpellManager.E.Cast(Player.Instance);
                    }
                }

            }
        }
        
        private static void OnDraw(EventArgs args)
        {
            if (Config.Drawing.DrawQ)
            {
                Circle.Draw(Color.Cyan, SpellManager.Q.Range, Player.Instance.Position);
            }
            if (Config.Drawing.DrawQMax)
            {
                Circle.Draw(Color.Cyan, 1700, Player.Instance.Position);
            }
            if (Config.Drawing.DrawW)
            {
                Circle.Draw(Color.Magenta, SpellManager.W.Range, Player.Instance.Position);
            }
            if (Config.Drawing.DrawE)
            {
                Circle.Draw(Color.White, SpellManager.E.Range, Player.Instance.Position);
            }
            if (Config.Drawing.DrawR)
            {
                Circle.Draw(Color.Yellow, SpellManager.E.Range, Player.Instance.Position);
            }
        }
    }
}
