using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;

namespace VodkaSmite.Modes

{
    public sealed class PermaActive : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return !Player.Instance.IsDead;
        }

        public override void Execute()
        {
            if (!Smite.IsReady() || !(Config.SmiteEnabled || Config.SmiteEnabledToggle))
            {
                return;
            }
            if (Config.SmiteEnemies && Smite.Name.Equals("s5_summonersmiteplayerganker"))
            {
                var enemy = EntityManager.Heroes.Enemies.FirstOrDefault(e => Smite.IsInRange(e) && !e.IsDead && e.Health > 0 && !e.IsInvulnerable && e.IsVisible && e.TotalShieldHealth() < Damages.SmiteDmgHero(e));
                if (enemy != null)
                {
                    Debug.WriteChat("Casting Smite on {0}, who has {1}HP", enemy.ChampionName, string.Format("{0}", (int)enemy.Health));
                    Smite.Cast(enemy);
                    return;
                }
            }
            var monsters =
                EntityManager.MinionsAndMonsters.GetJungleMonsters(_PlayerPos, Smite.Range)
                    .Where(e => !e.IsDead && e.Health > 0 && Util.MonstersNames.Contains(e.BaseSkinName) && !e.IsInvulnerable && e.IsVisible && e.Health < Damages.SmiteDmgMonster(e));
            foreach (var m in monsters)
            {
                if (Config.MainMenu["vSmite" + m.BaseSkinName].Cast<CheckBox>().CurrentValue)
                {
                    Debug.WriteChat("Casting Smite on {0}, who has {1}HP", m.Name, string.Format("{0}", (int)m.Health));
                    Smite.Cast(m);
                    return;
                }
            }
        }
    }
}
