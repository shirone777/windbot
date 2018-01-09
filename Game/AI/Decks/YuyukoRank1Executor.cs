using YGOSharp.OCGWrapper.Enums;
using System.Collections.Generic;
using WindBot;
using WindBot.Game;
using WindBot.Game.AI;

namespace WindBot.Game.AI.Decks
{
    // NOT FINISHED YET
    [Deck("YuyukoRank1", "AI_YuyukoRank1")]
    public class YuyukoRank1Executor : DefaultExecutor
    {
        public class CardId
        {
            public const int MimaL9 = 11011;
            public const int YuyukoL8 = 20032;
            public const int YoumuTheMangetsuYuuki = 20238;
            public const int YoumuTheAkaSakuraYuurei = 20235;
            public const int HatanokokoroL4 = 25081;
            public const int YoumuTheHalfSpirit = 20257;
            public const int YoumuTheBluesky = 20083;
            public const int NueL3 = 26125;
            public const int YoumuTheSweetMaid = 20248;
            public const int YoumuTheHalfSpiritL3 = 20210;
            public const int YoumeiNoKuwa = 20193;
            public const int KokoroCorona = 25127;
            public const int Mougakyou = 20090;
            public const int KanataOfTamaSakura = 20178;
            public const int HiNoOtoko = 25096;
            public const int Hakugyokurou = 20050;
            public const int JigenYuuhei = 10113;

            public const int YoumuF6 = 20245;
            public const int YoumuSanaeF = 19011;
            public const int AyaS8 = 25171;
            public const int YoumuS6 = 20041;
            public const int LoliceR4 = 15059;
            public const int YoumuR4 = 20242;
            public const int LinkYuuka = 25501;
            public const int LinkYuyuko = 20504;
            public const int LinkReimu = 10501;
            public const int LinkReisen = 21501;
        }

        public YuyukoRank1Executor(GameAI ai, Duel duel)
            : base(ai, duel)
        {
            AddExecutor(ExecutorType.SpellSet, DefaultSpellSet);

            AddExecutor(ExecutorType.Activate, CardId.YoumeiNoKuwa);
            AddExecutor(ExecutorType.Activate, CardId.KokoroCorona);
            AddExecutor(ExecutorType.Activate, CardId.Mougakyou);
            AddExecutor(ExecutorType.Activate, CardId.HiNoOtoko);
            AddExecutor(ExecutorType.Activate, CardId.Hakugyokurou);
            AddExecutor(ExecutorType.Activate, CardId.NueL3);
            AddExecutor(ExecutorType.Activate, CardId.YoumuTheSweetMaid);

            AddExecutor(ExecutorType.SpSummon, CardId.MimaL9);
            AddExecutor(ExecutorType.SpSummon, CardId.YuyukoL8);
            AddExecutor(ExecutorType.SpSummon, CardId.YoumuTheAkaSakuraYuurei);
            AddExecutor(ExecutorType.SpSummon, CardId.YoumuTheBluesky);
            AddExecutor(ExecutorType.Summon, CardId.HatanokokoroL4);
            AddExecutor(ExecutorType.SummonOrSet, CardId.HatanokokoroL4);
            AddExecutor(ExecutorType.Summon, CardId.YoumuTheHalfSpirit);
            AddExecutor(ExecutorType.SpSummon, CardId.YoumuTheHalfSpiritL3);

            AddExecutor(ExecutorType.SpSummon, CardId.LinkYuuka);
            AddExecutor(ExecutorType.SpSummon, CardId.LinkYuyuko);
            AddExecutor(ExecutorType.SpSummon, CardId.AyaS8);
            AddExecutor(ExecutorType.SpSummon, CardId.YoumuS6);
            AddExecutor(ExecutorType.SpSummon, CardId.LoliceR4);
            AddExecutor(ExecutorType.SpSummon, CardId.YoumuR4);
            AddExecutor(ExecutorType.SpSummon, CardId.LinkReimu);
            AddExecutor(ExecutorType.SpSummon, CardId.LinkReisen);

            AddExecutor(ExecutorType.Activate, CardId.JigenYuuhei, DefaultUniqueTrap);
            

            AddExecutor(ExecutorType.Repos, DefaultMonsterRepos);
        }
        

        private bool AttackUpEffect()
        {
            ClientCard bestMy = Bot.GetMonsters().GetHighestAttackMonster();
            ClientCard bestEnemyATK = Enemy.GetMonsters().GetHighestAttackMonster();
            ClientCard bestEnemyDEF = Enemy.GetMonsters().GetHighestDefenseMonster();
            if (bestMy == null || (bestEnemyATK == null && bestEnemyDEF == null))
                return false;
            if (bestEnemyATK != null && bestMy.Attack < bestEnemyATK.Attack)
                return true;
            if (bestEnemyDEF != null && bestMy.Attack < bestEnemyDEF.Defense)
                return true;
            return false;
        }
    }
}