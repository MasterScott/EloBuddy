using System;
using System.Drawing;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Rendering;
using SharpDX;
using SettingsMisc = VodkaGaren.Config.MiscMenu;
using SettingsModes = VodkaGaren.Config.ModesMenu;
using SettingsDrawing = VodkaGaren.Config.DrawingMenu;
using Color = SharpDX.Color;
using Font = System.Drawing.Font;

namespace VodkaGaren
{
    public static class Events
    {
        private static Text Text;

        static Events()
        {
            Text = new Text("", new Font(FontFamily.GenericSansSerif, 12, FontStyle.Bold)) { Color = System.Drawing.Color.ForestGreen };
            Orbwalker.OnPostAttack += OrbwalkerOnOnPostAttack;
            Obj_AI_Base.OnLevelUp += Obj_AI_Base_OnOnLevelUp;
            Drawing.OnDraw += OnDraw;
        }

        private static void Obj_AI_Base_OnOnLevelUp(Obj_AI_Base sender, Obj_AI_BaseLevelUpEventArgs args)
        {
            if (sender.IsMe && Player.Instance.Level == 6)
            {
                Player.Instance.Spellbook.LevelSpell(SpellSlot.R);
            }
        }

        private static float PlayerMana
        {
            get { return Player.Instance.ManaPercent; }
        }

        private static void AIHeroClientOnOnBuffGain(Obj_AI_Base sender, Obj_AI_BaseBuffGainEventArgs args)
        {
        }

        private static void AIHeroClientOnOnBuffLose(Obj_AI_Base sender, Obj_AI_BaseBuffLoseEventArgs args)
        {
        }

        public static void Initialize()
        {
        }

        private static void OnDraw(EventArgs args)
        {
            var drawOnlyReady = SettingsDrawing.DrawOnlyReady;
            if (SettingsDrawing.DrawE && !(drawOnlyReady && !SpellManager.W.IsReady()))
            {
                Circle.Draw(Color.Orange, SpellManager.E.Range, Player.Instance.Position);
            }
            if (SettingsDrawing.DrawR && !(drawOnlyReady && !SpellManager.R.IsReady()))
            {
                Circle.Draw(Color.Red, SpellManager.R.Range, Player.Instance.Position);
            }
            if (SettingsDrawing.DrawHPAfterR)
            {
                DrawHPAfterR();
            }
        }

        private static void DrawHPAfterR()
        {
            if (!SpellManager.R.IsLearned)
            {
                return;
            }
            foreach (var enemy in EntityManager.Heroes.Enemies.Where(e => !e.IsDead && e.IsVisible && e.Health > 0))
            {
                int hpAfterR = (int)Math.Floor((double)enemy.TotalShieldHealth() - Damages.RDamage(enemy));
                Vector2 drawPos = new Vector2(enemy.HPBarPosition.X, enemy.HPBarPosition.Y - 12);
                if (hpAfterR > 0)
                {
                    Text.TextValue = "+" + hpAfterR;
                    Text.Color = System.Drawing.Color.GreenYellow;
                }
                else
                {
                    Text.TextValue = "" + hpAfterR;
                    Text.Color = System.Drawing.Color.Red;
                }
                Text.Position = drawPos;
                Text.Draw();
            }
        }


        private static void InterrupterOnOnInterruptableSpell(Obj_AI_Base sender, Interrupter.InterruptableSpellEventArgs interruptableSpellEventArgs)
        {
        }

        private static void GapcloserOnOnGapcloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs gapcloserEventArgs)
        {
        }

        private static void OrbwalkerOnOnPostAttack(AttackableUnit target, EventArgs args)
        {
            // No sense in checking if Q is off cooldown
            if (!SpellManager.Q.IsReady())
            {
                return;
            }
            // Check if we should use Q to attack heroes
            if (SettingsMisc.AutoQ)
            {
                if (target is AIHeroClient && target.IsValidTarget(Player.Instance.GetAutoAttackRange()))
                {
                    SpellManager.Q.Cast();
                    Orbwalker.ResetAutoAttack();
                    Player.IssueOrder(GameObjectOrder.AttackUnit, target);
                    Debug.WriteChat("Auto casting Q after autoattacking {0}", ((AIHeroClient)target).ChampionName);
                    return;
                }
            }
        }
    }
}
