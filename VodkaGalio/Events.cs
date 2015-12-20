using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Rendering;
using SharpDX;
using SettingsMisc = VodkaGalio.Config.MiscMenu;
using SettingsModes = VodkaGalio.Config.ModesMenu;
using SettingsDrawing = VodkaGalio.Config.DrawingMenu;
using SettingsMana = VodkaGalio.Config.ManaManagerMenu;
using SettingsPrediction = VodkaGalio.Config.PredictionMenu;

namespace VodkaGalio
{
    public static class Events
    {
        static Events()
        {
            Interrupter.OnInterruptableSpell += InterrupterOnOnInterruptableSpell;
            Gapcloser.OnGapcloser += GapcloserOnOnGapcloser;
            AIHeroClient.OnBuffGain += AIHeroClientOnOnBuffGain;
            AIHeroClient.OnBuffLose += AIHeroClientOnOnBuffLose;
            Drawing.OnDraw += OnDraw;
        }

        private static float PlayerMana
        {
            get { return Player.Instance.ManaPercent; }
        }

        private static void AIHeroClientOnOnBuffGain(Obj_AI_Base sender, Obj_AI_BaseBuffGainEventArgs args)
        {
            if (sender.NetworkId.Equals(Player.Instance.NetworkId) && args.Buff.Name.Equals("GalioIdolOfDurand", StringComparison.CurrentCultureIgnoreCase))
            {
                Debug.WriteChat("Disabling Orbwalker while ulting");
                Orbwalker.DisableAttacking = true;
                Orbwalker.DisableMovement = true;
            }
        }

        private static void AIHeroClientOnOnBuffLose(Obj_AI_Base sender, Obj_AI_BaseBuffLoseEventArgs args)
        {
            if (sender.NetworkId.Equals(Player.Instance.NetworkId) && args.Buff.Name.Equals("GalioIdolOfDurand", StringComparison.CurrentCultureIgnoreCase))
            {
                Debug.WriteChat("Enabling Orbwalker after ulting");
                Orbwalker.DisableAttacking = false;
                Orbwalker.DisableMovement = false;
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
            if (!sender.IsEnemy || !(sender is AIHeroClient) || Player.Instance.IsRecalling() || SpellManager.isUlting() || Player.Instance.IsRecalling())
            {
                Debug.WriteChat("Detected Interruptable spell from {0}, but didn't meet criteria.", sender.Name);
                return;
            }
            Debug.WriteChat("Interruptable Spell from {0}", ((AIHeroClient)sender).ChampionName);
            if (SettingsMisc.InterrupterUseR && SpellManager.R.IsReady() && SpellManager.R.IsInRange(sender))
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
            if (SettingsMisc.AntigapcloserUseQ && SpellManager.Q.IsReady() && PlayerMana >= SettingsMana.MinQMana && gapcloserEventArgs.End.Distance(Player.Instance) < 200)
            {
                Debug.WriteChat("AntiGapcloser with Q, Target: {0}, Distance: {1}, GapcloserSpell: {2}", sender.ChampionName, "" + sender.Distance(Player.Instance), gapcloserEventArgs.SpellName);
                SpellManager.Q.Cast(gapcloserEventArgs.End);
                return;
            }
        }
    }
}
