using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Constants;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using System;
using System.Drawing;
using System.Linq;

namespace VodkaJanna.Shielder
{
    internal class Shielder
    {
        public static bool Initialized { get; private set; }
        public static Menu ShielderMenu { get; private set; }
        public static void Initialize(Menu subMenu)
        {
            if (subMenu == null)
            {
                Initialized = false;
                Chat.Print("Can't initialize Shielder for VodkaJanna!", Color.Red);
            }

            InitializeMenuConfig(subMenu);
            Obj_AI_Base.OnProcessSpellCast += OnProcessSpellCast;
            Obj_AI_Base.OnBasicAttack += OnBasicAttack;
            Initialized = true;

        }

        static void InitializeMenuConfig(Menu subMenu)
        {
            if (Initialized)
            {
                return;
            }
            ShielderMenu = subMenu;
            subMenu.AddGroupLabel("Auto Shielder");
            subMenu.AddLabel("This submenu is responsible for auto-shielding.");
            subMenu.AddLabel("It takes into account player mana based on Mana Manager settings.");
            subMenu.AddGroupLabel("Basic Autoattacks");
            subMenu.Add("ShielderOnEnemyAA", new CheckBox("Shield against enemy AA"));
            subMenu.Add("ShielderOnEnemyTurretAA", new CheckBox("Shield against turret attacks"));
            subMenu.Add("ShielderOnAllyAA", new CheckBox("Boost ally AA damage"));
            subMenu.Add("ShielderOnAllyTurretAA", new CheckBox("Boost turret AA damage against heroes"));
            subMenu.AddGroupLabel("Spell Casts");
            subMenu.Add("ShielderOnEnemySpell", new CheckBox("Shield against enemy spells"));
            subMenu.Add("ShielderOnAllySpell", new CheckBox("Boost ally spell damage"));
            subMenu.AddGroupLabel("Delay");
            subMenu.Add("ShielderDelay", new Slider("Delay shielding by {0} ms", 50, 0, 500));
            subMenu.Add("ShielderDelayRandom", new Slider("Randomize delay by adding up to {0} ms", 50, 0, 500));
            subMenu.AddGroupLabel("Ally Whitelist");
            subMenu.AddLabel("Choose which allies to shield.");
            foreach (var ally in EntityManager.Heroes.Allies)
            {
                subMenu.Add("ShielderAllyWhitelist" + ally.ChampionName, new CheckBox(ally.ChampionName));
            }
            subMenu.AddGroupLabel("Ally Spell Whitelist");
            subMenu.AddLabel("Choose which allied spells to boost with shield.");
            foreach (var ally in EntityManager.Heroes.Allies)
            {
                var allySpells =
                    SpellDatabase.DamageBoostSpells.Where(
                        s => s.Champion.Equals(ally.ChampionName, StringComparison.CurrentCultureIgnoreCase));
                if (allySpells.ToList().Count > 0)
                {
                    subMenu.AddLabel(ally.ChampionName);
                    foreach (var spell in allySpells)
                    {
                        subMenu.Add("ShielderAllySpellWhitelist" + spell.SpellName, new CheckBox(spell.SpellName));
                    }
                }
            }
            subMenu.AddGroupLabel("Enemy Spell Whitelist");
            subMenu.AddLabel("Choose which enemy spells to shield against.");
            foreach (var enemy in EntityManager.Heroes.Enemies)
            {
                var enemySpells =
                    SpellDatabase.TargetedSpells.Where(
                        s => s.Champion.Equals(enemy.ChampionName, StringComparison.CurrentCultureIgnoreCase));
                if (enemySpells.ToList().Count > 0)
                {
                    subMenu.AddLabel(enemy.ChampionName);
                    foreach (var spell in enemySpells)
                    {
                        subMenu.Add("ShielderEnemySpellWhitelist" + spell.SpellName, new CheckBox(spell.SpellName));
                    }
                }
            }
        }

        private static void OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (!SpellManager.E.IsReady() || Player.Instance.ManaPercent < Config.ManaManagerMenu.MinEMana || SpellManager.IsUlting())
            {
                return;
            }
            if (sender is AIHeroClient)
            {
                // Boost ally spell dmg
                if (sender.IsAlly && sender.IsValidTarget(SpellManager.E.Range)
                    && ShielderMenu["ShielderAllyWhitelist" + (sender as AIHeroClient).ChampionName].Cast<CheckBox>().CurrentValue)
                {
                    var spellCheckbox = ShielderMenu["ShielderAllySpellWhitelist" + args.SData.Name];
                    if (spellCheckbox != null && spellCheckbox.Cast<CheckBox>().CurrentValue)
                    {
                        CastE((Obj_AI_Base) sender, args.SData.Name, true);
                        var enemyName = "enemy";
                        if (args.Target != null && args.Target is AIHeroClient)
                        {
                            enemyName = (args.Target as AIHeroClient).ChampionName;
                        } else if (args.Target != null && args.Target is Obj_AI_Base)
                        {
                            enemyName = (args.Target as Obj_AI_Base).BaseSkinName;
                        }
                        Debug.WriteChat("Boosting {0} spell damage against {1}. Spell - {2}", (sender as AIHeroClient).ChampionName, enemyName , args.SData.Name);
                        return;
                    }
                }
                // Protect ally from enemy spell
                if (sender.IsEnemy && args.Target != null && args.Target is AIHeroClient && (args.Target as AIHeroClient).IsAlly
                    && ShielderMenu["ShielderAllyWhitelist" + (args.Target as AIHeroClient).ChampionName].Cast<CheckBox>().CurrentValue && (args.Target as AIHeroClient).IsValidTarget(SpellManager.E.Range))
                {
                    var spellCheckbox = ShielderMenu["ShielderEnemySpellWhitelist" + args.SData.Name];
                    if (spellCheckbox != null && spellCheckbox.Cast<CheckBox>().CurrentValue)
                    {
                        CastE((Obj_AI_Base) args.Target, args.SData.Name, false);
                        Debug.WriteChat("Protecting {0} from spell by {1}. Spell - {2}", (args.Target as AIHeroClient).ChampionName, (sender as AIHeroClient).ChampionName, args.SData.Name);
                        return;
                    }
                }
            }
        }

        private static void OnBasicAttack(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (!SpellManager.E.IsReady() || Player.Instance.ManaPercent < Config.ManaManagerMenu.MinEMana || SpellManager.IsUlting())
            {
                return;
            }
            if (sender is AIHeroClient)
            {
                // Boost ally AA
                if (sender.IsAlly && args.SData.IsAutoAttack() && ShielderMenu["ShielderOnAllyAA"].Cast<CheckBox>().CurrentValue && args.Target != null
                    && (args.Target is AIHeroClient || args.Target is Obj_AI_Turret) && ShielderMenu["ShielderAllyWhitelist" + (sender as AIHeroClient).ChampionName].Cast<CheckBox>().CurrentValue
                    && (sender as AIHeroClient).IsValidTarget(SpellManager.E.Range))
                {
                    CastE((Obj_AI_Base)sender, null, false);
                    Debug.WriteChat("Boosting {0} AA damage against {1}", (sender as AIHeroClient).ChampionName, (args.Target as AIHeroClient).ChampionName);
                    return;
                }
                // Protect ally from enemy AA
                if (sender.IsEnemy && args.SData.IsAutoAttack() && ShielderMenu["ShielderOnEnemyAA"].Cast<CheckBox>().CurrentValue && args.Target != null
                    && args.Target is AIHeroClient && ShielderMenu["ShielderAllyWhitelist" + (args.Target as AIHeroClient).ChampionName].Cast<CheckBox>().CurrentValue
                    && (args.Target as AIHeroClient).IsValidTarget(SpellManager.E.Range))
                {
                    CastE((Obj_AI_Base)args.Target, null, false);
                    Debug.WriteChat("Protecting {0} from autoattack by {1}", (args.Target as AIHeroClient).ChampionName, (sender as AIHeroClient).ChampionName);
                    return;
                }
            }
            if (sender is Obj_AI_Turret)
            {
                // Boost ally turret AA
                if (sender.IsAlly && ShielderMenu["ShielderOnAllyTurretAA"].Cast<CheckBox>().CurrentValue && args.Target != null
                    && args.Target is AIHeroClient && (sender as Obj_AI_Turret).IsValidTarget(SpellManager.E.Range))
                {
                    CastE((Obj_AI_Base)sender, null, false);
                    Debug.WriteChat("Boosting turret {0} AA damage against {1}", (sender as Obj_AI_Turret).BaseSkinName, (args.Target as AIHeroClient).ChampionName);
                    return;
                }
                // Protect ally from enemy turret AA
                if (sender.IsEnemy && ShielderMenu["ShielderOnEnemyTurretAA"].Cast<CheckBox>().CurrentValue && args.Target != null
                    && args.Target is AIHeroClient && ShielderMenu["ShielderAllyWhitelist" + (args.Target as AIHeroClient).ChampionName].Cast<CheckBox>().CurrentValue
                    && (args.Target as AIHeroClient).IsValidTarget(SpellManager.E.Range))
                {
                    CastE((Obj_AI_Base)args.Target, null, false);
                    Debug.WriteChat("Protecting {0} from autoattack by turret {1}", (args.Target as AIHeroClient).ChampionName, (sender as Obj_AI_Turret).BaseSkinName);
                    return;
                }
            }
        }

        private static void CastE(Obj_AI_Base target, string spellName, bool isDamageBoost)
        {
            var spellDelay = 0;
            if (spellName != null)
            {
                if (isDamageBoost)
                {
                    var spellData = SpellDatabase.GetDamageBoostSpellData(spellName);
                    if (spellData != null)
                        spellDelay = spellData.Delay;
                }
                else
                {
                    var spellData = SpellDatabase.GetTargetedSpellData(spellName);
                    if (spellData != null)
                        spellDelay = spellData.Delay;
                }
            }
            var delay = ShielderMenu["ShielderDelay"].Cast<Slider>().CurrentValue;
            var delayRandom = ShielderMenu["ShielderDelayRandom"].Cast<Slider>().CurrentValue;
            Random r = new Random(Environment.TickCount);
            var randomizedDelay = delayRandom > 0 ? r.Next(0, delayRandom + 1) : 0;
            Core.DelayAction(() => { SpellManager.E.Cast(target); }, spellDelay + delay + randomizedDelay);
        }
    }
}
