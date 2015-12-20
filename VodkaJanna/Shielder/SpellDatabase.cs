using System;
using EloBuddy;
using System.Collections.Generic;
using System.Linq;

namespace VodkaJanna.Shielder
{
    internal class SpellData
    {
        public string Champion { get; set; }
        public SpellSlot Slot { get; set; }
        public string SpellName { get; set; }
        public int Delay { get; set; }
        public bool Default { get; set; }
    }

    internal class SpellDatabase
    {
        public static readonly List<SpellData> DamageBoostSpells = new List<SpellData>
            {
                new SpellData { Champion = "Ashe", SpellName = "Volley", Slot = SpellSlot.E, Delay = 0, Default = true },
                new SpellData { Champion = "Caitlyn", SpellName = "CaitlynPiltoverPeacemaker", Slot = SpellSlot.Q, Delay = 0, Default = true },
                new SpellData { Champion = "Caitlyn", SpellName = "CaitlynAceintheHole", Slot = SpellSlot.R, Delay = 0, Default = true },
                new SpellData { Champion = "Corki", SpellName = "PhosphorusBomb", Slot = SpellSlot.Q, Delay = 0, Default = true },
                new SpellData { Champion = "Corki", SpellName = "GGun", Slot = SpellSlot.E, Delay = 0, Default = true },
                new SpellData { Champion = "Corki", SpellName = "MissileBarrage", Slot = SpellSlot.R, Delay = 0, Default = true },
                new SpellData { Champion = "Draven", SpellName = "DravenSpinning", Slot = SpellSlot.Q, Delay = 0, Default = true },
                new SpellData { Champion = "Draven", SpellName = "DravenDoubleShot", Slot = SpellSlot.E, Delay = 0, Default = true },
                new SpellData { Champion = "Draven", SpellName = "DravenRCast", Slot = SpellSlot.R, Delay = 0, Default = true },
                new SpellData { Champion = "Ezreal", SpellName = "EzrealMysticShot", Slot = SpellSlot.Q, Delay = 0, Default = true },
                new SpellData { Champion = "Ezreal", SpellName = "EzrealTrueshotBarrage", Slot = SpellSlot.R, Delay = 0, Default = true },
                new SpellData { Champion = "Graves", SpellName = "GravesClusterShot", Slot = SpellSlot.Q, Delay = 0, Default = true },
                new SpellData { Champion = "Graves", SpellName = "GravesChargeShot", Slot = SpellSlot.R, Delay = 0, Default = true },
                new SpellData { Champion = "Jinx", SpellName = "JinxW", Slot = SpellSlot.W, Delay = 0, Default = true },
                new SpellData { Champion = "Jinx", SpellName = "JinxRWrapper", Slot = SpellSlot.R, Delay = 0, Default = true },
                new SpellData { Champion = "KogMaw", SpellName = "KogMawLivingArtillery", Slot = SpellSlot.R, Delay = 0, Default = true },
                new SpellData { Champion = "Lucian", SpellName = "LucianQ", Slot = SpellSlot.Q, Delay = 0, Default = true },
                new SpellData { Champion = "Lucian", SpellName = "LucianW", Slot = SpellSlot.W, Delay = 0, Default = true },
                new SpellData { Champion = "Lucian", SpellName = "LucianR", Slot = SpellSlot.R, Delay = 0, Default = true },
                new SpellData { Champion = "MissFortune", SpellName = "MissFortuneRicochetShot", Slot = SpellSlot.Q, Delay = 0, Default = true },
                new SpellData { Champion = "MissFortune", SpellName = "MissFortuneBulletTime", Slot = SpellSlot.R, Delay = 0, Default = true },
                new SpellData { Champion = "Quinn", SpellName = "QuinnQ", Slot = SpellSlot.Q, Delay = 0, Default = true },
                new SpellData { Champion = "Quinn", SpellName = "QuinnE", Slot = SpellSlot.E, Delay = 0, Default = true },
                new SpellData { Champion = "Quinn", SpellName = "QuinnR", Slot = SpellSlot.R, Delay = 0, Default = false },
                new SpellData { Champion = "Sivir", SpellName = "SivirQ", Slot = SpellSlot.Q, Delay = 0, Default = true },
                new SpellData { Champion = "Twitch", SpellName = "Expunge", Slot = SpellSlot.E, Delay = 0, Default = true },
                new SpellData { Champion = "Twitch", SpellName = "FullAutomatic", Slot = SpellSlot.R, Delay = 0, Default = true },
                new SpellData { Champion = "Urgot", SpellName = "UrgotHeatseekingMissile", Slot = SpellSlot.Q, Delay = 0, Default = true },
                new SpellData { Champion = "Urgot", SpellName = "UrgotPlasmaGrenade", Slot = SpellSlot.E, Delay = 0, Default = true },
                new SpellData { Champion = "Varus", SpellName = "VarusQ", Slot = SpellSlot.Q, Delay = 0, Default = true },
                new SpellData { Champion = "Varus", SpellName = "VarusE", Slot = SpellSlot.E, Delay = 0, Default = true },
                new SpellData { Champion = "Vayne", SpellName = "VayneTumble", Slot = SpellSlot.Q, Delay = 0, Default = false },
                new SpellData { Champion = "Vayne", SpellName = "VayneCondemn", Slot = SpellSlot.E, Delay = 0, Default = true },
                new SpellData { Champion = "Vayne", SpellName = "VayneInquisition", Slot = SpellSlot.R, Delay = 0, Default = true },
                new SpellData { Champion = "LeeSin", SpellName = "BlindMonkQOne", Slot = SpellSlot.Q, Delay = 0, Default = true },
                new SpellData { Champion = "LeeSin", SpellName = "BlindMonkRKick", Slot = SpellSlot.R, Delay = 0, Default = true },
                new SpellData { Champion = "Nasus", SpellName = "NasusQ", Slot = SpellSlot.Q, Delay = 0, Default = true },
                new SpellData { Champion = "Nocturne", SpellName = "NocturneParanoia", Slot = SpellSlot.R, Delay = 0, Default = true },
                new SpellData { Champion = "Shaco", SpellName = "TwoShivPoison", Slot = SpellSlot.E, Delay = 0, Default = true },
                new SpellData { Champion = "Trundle", SpellName = "TrundleTrollSmash", Slot = SpellSlot.Q, Delay = 0, Default = true },
                new SpellData { Champion = "Vi", SpellName = "ViQ", Slot = SpellSlot.Q, Delay = 0, Default = true },
                new SpellData { Champion = "Vi", SpellName = "ViE", Slot = SpellSlot.E, Delay = 0, Default = true },
                new SpellData { Champion = "Vi", SpellName = "ViR", Slot = SpellSlot.R, Delay = 0, Default = true },
                new SpellData { Champion = "XinZhao", SpellName = "XenZhaoComboTarget", Slot = SpellSlot.Q, Delay = 0, Default = true },
                new SpellData { Champion = "XinZhao", SpellName = "XenZhaoParry", Slot = SpellSlot.R, Delay = 0, Default = true },
                new SpellData { Champion = "Khazix", SpellName = "KhazixQ", Slot = SpellSlot.Q, Delay = 0, Default = true },
                new SpellData { Champion = "Khazix", SpellName = "KhazixW", Slot = SpellSlot.W, Delay = 0, Default = true },
                new SpellData { Champion = "MasterYi", SpellName = "AlphaStrike", Slot = SpellSlot.Q, Delay = 0, Default = true },
                new SpellData { Champion = "MasterYi", SpellName = "WujuStyle", Slot = SpellSlot.E, Delay = 0, Default = true },
                new SpellData { Champion = "Talon", SpellName = "TalonNoxianDiplomacy", Slot = SpellSlot.Q, Delay = 0, Default = true },
                new SpellData { Champion = "Talon", SpellName = "TalonShadowAssault", Slot = SpellSlot.R, Delay = 0, Default = true },
                new SpellData { Champion = "Pantheon", SpellName = "PantheonQ", Slot = SpellSlot.Q, Delay = 0, Default = true },
                new SpellData { Champion = "Yasuo", SpellName = "YasuoQW", Slot = SpellSlot.Q, Delay = 0, Default = true },
                new SpellData { Champion = "Yasuo", SpellName = "yasuoq2w", Slot = SpellSlot.Q, Delay = 0, Default = true },
                new SpellData { Champion = "Yasuo", SpellName = "yasuoq3w", Slot = SpellSlot.Q, Delay = 0, Default = true },
                new SpellData { Champion = "Yasuo", SpellName = "YasuoRKnockUpComboW", Slot = SpellSlot.R, Delay = 0, Default = true },
                new SpellData { Champion = "Zed", SpellName = "ZedShuriken", Slot = SpellSlot.Q, Delay = 0, Default = true },
                new SpellData { Champion = "Zed", SpellName = "ZedPBAOEDummy", Slot = SpellSlot.E, Delay = 0, Default = true },
                new SpellData { Champion = "Zed", SpellName = "zedult", Slot = SpellSlot.R, Delay = 0, Default = true },
                new SpellData { Champion = "Aatrox", SpellName = "AatroxW", Slot = SpellSlot.W, Delay = 0, Default = true },
                new SpellData { Champion = "Darius", SpellName = "DariusExecute", Slot = SpellSlot.R, Delay = 0, Default = true },
                new SpellData { Champion = "Gangplank", SpellName = "Parley", Slot = SpellSlot.Q, Delay = 0, Default = true },
                new SpellData { Champion = "Garen", SpellName = "GarenQ", Slot = SpellSlot.Q, Delay = 0, Default = true },
                new SpellData { Champion = "Garen", SpellName = "GarenE", Slot = SpellSlot.E, Delay = 0, Default = true },
                new SpellData { Champion = "Jayce", SpellName = "JayceToTheSkies", Slot = SpellSlot.R, Delay = 0, Default = true },
                new SpellData { Champion = "Jayce", SpellName = "jayceshockblast", Slot = SpellSlot.Q, Delay = 0, Default = true },
                new SpellData { Champion = "Renekton", SpellName = "RenektonCleave", Slot = SpellSlot.Q, Delay = 0, Default = true },
                new SpellData { Champion = "Renekton", SpellName = "RenektonPreExecute", Slot = SpellSlot.W, Delay = 0, Default = true },
                new SpellData { Champion = "Renekton", SpellName = "RenektonSliceAndDice", Slot = SpellSlot.E, Delay = 0, Default = true },
                new SpellData { Champion = "Rengar", SpellName = "RengarQ", Slot = SpellSlot.Q, Delay = 0, Default = true },
                new SpellData { Champion = "Rengar", SpellName = "RengarE", Slot = SpellSlot.E, Delay = 0, Default = true },
                new SpellData { Champion = "Rengar", SpellName = "RengarR", Slot = SpellSlot.R, Delay = 0, Default = true },
                new SpellData { Champion = "Riven", SpellName = "RivenFengShuiEngine", Slot = SpellSlot.R, Delay = 0, Default = true },
                new SpellData { Champion = "MonkeyKing", SpellName = "MonkeyKingDoubleAttack", Slot = SpellSlot.Q, Delay = 0, Default = true },
                new SpellData { Champion = "MonkeyKing", SpellName = "MonkeyKingNimbus", Slot = SpellSlot.E, Delay = 0, Default = true },
                new SpellData { Champion = "MonkeyKing", SpellName = "MonkeyKingSpinToWin", Slot = SpellSlot.R, Delay = 0, Default = true },
                new SpellData { Champion = "Kalista", SpellName = "KalistaMysticShot", Slot = SpellSlot.Q, Delay = 0, Default = true },
                new SpellData { Champion = "Kalista", SpellName = "KalistaExpungeWrapper", Slot = SpellSlot.E, Delay = 0, Default = true },
                new SpellData { Champion = "Warwick", SpellName = "InfiniteDuress", Slot = SpellSlot.R, Delay = 0, Default = true }
            };

        public static readonly List<SpellData> TargetedSpells = new List<SpellData>
            {
                new SpellData { Champion = "Syndra", SpellName = "syndrar", Slot = SpellSlot.R, Default = true },
                new SpellData { Champion = "VeigarR", SpellName = "veigarprimordialburst", Slot = SpellSlot.R, Default = true },
                new SpellData { Champion = "Malzahar", SpellName = "alzaharnethergrasp", Slot = SpellSlot.R, Default = true },
                new SpellData { Champion = "Caitlyn", SpellName = "CaitlynAceintheHole", Slot = SpellSlot.R, Delay = 1000, Default = true },
                new SpellData { Champion = "Caitlyn", SpellName = "CaitlynHeadshotMissile", Slot = SpellSlot.Unknown, Default = true },
                new SpellData { Champion = "Brand", SpellName = "BrandWildfire", Slot = SpellSlot.R, Default = true },
                new SpellData { Champion = "Brand", SpellName = "brandconflagrationmissile", Slot = SpellSlot.E, Default = true },
                new SpellData { Champion = "Kayle", SpellName = "judicatorreckoning", Slot = SpellSlot.Q, Default = true },
                new SpellData { Champion = "Pantheon", SpellName = "PantheonQ", Slot = SpellSlot.Q, Default = true },
                new SpellData { Champion = "Taric", SpellName = "Dazzle", Slot = SpellSlot.Q, Default = true },
                new SpellData { Champion = "TwistedFate", SpellName = "GoldCardAttack", Slot = SpellSlot.W, Default = true },
                new SpellData { Champion = "Viktor", SpellName = "viktorpowertransfer", Slot = SpellSlot.Q, Default = true },
                new SpellData { Champion = "Ahri", SpellName = "ahrifoxfiremissiletwo", Slot = SpellSlot.W, Default = true },
                new SpellData { Champion = "Elise", SpellName = "EliseHumanQ", Slot = SpellSlot.Q, Default = true },
                new SpellData { Champion = "Shaco", SpellName = "TwoShivPoison", Slot = SpellSlot.E, Default = true },
                new SpellData { Champion = "Urgot", SpellName = "UrgotHeatseekingHomeMissile", Slot = SpellSlot.Q, Default = true },
                new SpellData { Champion = "Lucian", SpellName = "LucianPassiveShot", Slot = SpellSlot.Unknown, Default = true },
                new SpellData { Champion = "Baron", SpellName = "BaronAcidBall", Slot = SpellSlot.Unknown, Default = true },
                new SpellData { Champion = "Baron", SpellName = "BaronAcidBall2", Slot = SpellSlot.Unknown, Default = true },
                new SpellData { Champion = "Baron", SpellName = "BaronDeathBreathProj1", Slot = SpellSlot.Unknown, Default = true },
                new SpellData { Champion = "Baron", SpellName = "BaronDeathBreathProj3", Slot = SpellSlot.Unknown, Default = true },
                new SpellData { Champion = "Baron", SpellName = "BaronSpike", Slot = SpellSlot.Unknown, Default = true },
                new SpellData { Champion = "Leblanc", SpellName = "LeblancChaosOrbM", Slot = SpellSlot.Q, Default = true },
                new SpellData { Champion = "Annie", SpellName = "disintegrate", Slot = SpellSlot.Q, Default = true },
                new SpellData { Champion = "Twisted Fate", SpellName = "GoldCardAttack", Slot = SpellSlot.W, Default = true },
                new SpellData { Champion = "Twisted Fate", SpellName = "RedCardAttack", Slot = SpellSlot.W, Default = true },
                new SpellData { Champion = "Twisted Fate", SpellName = "RedCardAttack", Slot = SpellSlot.W, Default = true },
                new SpellData { Champion = "Kassadin", SpellName = "NullLance", Slot = SpellSlot.Q, Default = true },
                new SpellData { Champion = "Teemo", SpellName = "BlindingDart", Slot = SpellSlot.Q, Default = true },
                new SpellData { Champion = "Malphite", SpellName = "SeismicShard", Slot = SpellSlot.Q, Default = true },
                new SpellData { Champion = "Vayne", SpellName = "VayneCondemn", Slot = SpellSlot.E, Default = true },
                new SpellData { Champion = "Nunu", SpellName = "IceBlast", Slot = SpellSlot.E, Default = true },
                new SpellData { Champion = "Tristana", SpellName = "BusterShot", Slot = SpellSlot.R, Default = true },
                new SpellData { Champion = "Cassiopeia", SpellName = "CassiopeiaTwinFang", Slot = SpellSlot.E, Default = true },
                new SpellData { Champion = "Pantheon", SpellName = "Pantheon_Throw", Slot = SpellSlot.Q, Default = true },
                new SpellData { Champion = "Akali", SpellName = "AkaliMot", Slot = SpellSlot.Q, Default = true },
                new SpellData { Champion = "Leblanc", SpellName = "LeblancChaosOrbM", Slot = SpellSlot.Q, Default = true },
                new SpellData { Champion = "Anivia", SpellName = "Frostbite", Slot = SpellSlot.E, Default = true },
                new SpellData { Champion = "Zed", SpellName = "zedult", Slot = SpellSlot.R, Delay = 0, Default = true },
                new SpellData { Champion = "Vi", SpellName = "ViR", Slot = SpellSlot.R, Delay = 0, Default = true },
                new SpellData { Champion = "LeeSin", SpellName = "BlindMonkRKick", Slot = SpellSlot.R, Delay = 0, Default = true },
                new SpellData { Champion = "Warwick", SpellName = "InfiniteDuress", Slot = SpellSlot.R, Delay = 0, Default = true },
                new SpellData { Champion = "Quinn", SpellName = "QuinnE", Slot = SpellSlot.E, Delay = 0, Default = true }
            };

        public static SpellData GetTargetedSpellData(string spellName)
        {
            return
                TargetedSpells.FirstOrDefault(
                    s => s.SpellName.Equals(spellName, StringComparison.CurrentCultureIgnoreCase));
        }

        public static SpellData GetDamageBoostSpellData(string spellName)
        {
            return
                TargetedSpells.FirstOrDefault(
                    s => s.SpellName.Equals(spellName, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}
