using YGOSharp.OCGWrapper.Enums;
using System.Collections.Generic;
using WindBot;
using WindBot.Game;
using WindBot.Game.AI;

namespace WindBot.Game.AI.Decks
{
    // NOT FINISHED YET
    [Deck("YuyukoRank3", "AI_YuyukoRank3")]
    public class YuyukoRank3Executor : DefaultExecutor
    {
        public class CardId
        {
            public const int Liesun = 200119;
            public const int YoumuN5 = 20252;
            public const int Bing = 20046;
            public const int MimaL9 = 11011;
            public const int YuyukoL8 = 2003200;
            public const int YoumuTheMangetsuYuuki = 20238;
            public const int YoumuTheAkaSakuraYuurei = 20237;
            public const int HatanokokoroL4 = 25081;
            public const int YoumuTheHalfSpirit = 20257;
            public const int YoumuTheBluesky = 20084;
            public const int NueL3 = 26125;
            public const int YoumuTheSweetMaid = 20248;
            public const int YoumuTheHalfSpiritL3 = 20212;
            public const int YoumeiNoKuwa = 20193;
            public const int KokoroCorona = 25127;
            public const int Mougakyou = 20090;
            public const int KanataOfTamaSakura = 20179;
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

        public YuyukoRank3Executor(GameAI ai, Duel duel)
            : base(ai, duel)
        {
            AddExecutor(ExecutorType.SpellSet, DefaultSpellSet);

            AddExecutor(ExecutorType.Activate, CardId.Liesun);
            AddExecutor(ExecutorType.Activate, CardId.YoumuN5);
            AddExecutor(ExecutorType.Activate, CardId.YoumeiNoKuwa);
            AddExecutor(ExecutorType.Activate, CardId.KokoroCorona, KokoroCoronaEffect);
            AddExecutor(ExecutorType.Activate, CardId.HiNoOtoko);
            AddExecutor(ExecutorType.Activate, CardId.Mougakyou, MougakyouEffect);
            AddExecutor(ExecutorType.Activate, CardId.YoumuTheSweetMaid, YoumuTheSweetMaidEffect);
            AddExecutor(ExecutorType.Activate, CardId.Hakugyokurou, HakugyokurouEffect);
            AddExecutor(ExecutorType.SpSummon, CardId.YoumuTheBluesky, YoumuTheBlueskySummon);
            AddExecutor(ExecutorType.Activate, CardId.NueL3, NueL3Effect);
            AddExecutor(ExecutorType.Activate, CardId.MimaL9);
            AddExecutor(ExecutorType.Activate, CardId.Bing);
            AddExecutor(ExecutorType.Activate, CardId.YoumuTheMangetsuYuuki);
            AddExecutor(ExecutorType.Activate, CardId.YoumuTheHalfSpirit);
            AddExecutor(ExecutorType.Activate, CardId.YoumuTheHalfSpiritL3);
            AddExecutor(ExecutorType.Activate, CardId.YoumuSanaeF);
            AddExecutor(ExecutorType.Activate, CardId.YoumuR4);
            AddExecutor(ExecutorType.Activate, CardId.YoumuSanaeF);
            AddExecutor(ExecutorType.Activate, CardId.YoumuTheAkaSakuraYuurei);
            AddExecutor(ExecutorType.Activate, CardId.YoumuF6);
            AddExecutor(ExecutorType.Activate, CardId.AyaS8);
            AddExecutor(ExecutorType.Activate, CardId.YoumuS6);
            AddExecutor(ExecutorType.Activate, CardId.LoliceR4);
            AddExecutor(ExecutorType.Activate, CardId.LinkReimu);
            AddExecutor(ExecutorType.Activate, CardId.LinkReisen);

            AddExecutor(ExecutorType.SpSummon, CardId.MimaL9, MimaL9Effect);
            AddExecutor(ExecutorType.SpSummon, CardId.YuyukoL8, YuyukoL8Summon);
            AddExecutor(ExecutorType.SpSummon, CardId.YoumuTheAkaSakuraYuurei);
            AddExecutor(ExecutorType.Summon, CardId.HatanokokoroL4);
            AddExecutor(ExecutorType.SummonOrSet, CardId.HatanokokoroL4);
            AddExecutor(ExecutorType.Summon, CardId.YoumuTheHalfSpirit);
            AddExecutor(ExecutorType.SummonOrSet, CardId.YoumuTheSweetMaid);
            AddExecutor(ExecutorType.SpSummon, CardId.YoumuTheMangetsuYuuki);
            AddExecutor(ExecutorType.SpSummon, CardId.YoumuTheHalfSpiritL3);
            AddExecutor(ExecutorType.Activate, CardId.HatanokokoroL4);

            AddExecutor(ExecutorType.SpSummon, CardId.LinkYuyuko, LinkYuyukoSummon);
            AddExecutor(ExecutorType.Activate, CardId.LinkYuyuko, LinkYuyukoEffect);
            AddExecutor(ExecutorType.SpSummon, CardId.LinkYuuka);
            AddExecutor(ExecutorType.SpSummon, CardId.YoumuS6);
            AddExecutor(ExecutorType.SpSummon, CardId.LoliceR4, DefaultCastelTheSkyblasterMusketeerSummon);
            AddExecutor(ExecutorType.SpSummon, CardId.AyaS8);
            AddExecutor(ExecutorType.SpSummon, CardId.LinkReimu, LinkSummon);
            AddExecutor(ExecutorType.SpSummon, CardId.LinkReisen, LinkSummon);

            AddExecutor(ExecutorType.Activate, CardId.YuyukoL8, YuyukoL8Effect);
            AddExecutor(ExecutorType.SpSummon, CardId.YoumuR4, YoumuR4Summon);

            AddExecutor(ExecutorType.Activate, CardId.JigenYuuhei, DefaultUniqueTrap);
            

            AddExecutor(ExecutorType.Repos, DefaultMonsterRepos);
        }

        bool CardOfDemiseUsed = false;

        public override IList<ClientCard> OnSelectCard(IList<ClientCard> cards, int min, int max, int hint, bool cancelable)
        {
            Logger.DebugWriteLine("OnSelectCard " + cards.Count + " " + min + " " + max);
            if (hint == 504 && cards[0].Location == CardLocation.Hand)
            {
                IList<ClientCard> result = new List<ClientCard>();
                AI.Utils.SelectPreferredCards(result, new[] {
                    CardId.YoumuTheMangetsuYuuki,
                    CardId.YoumuTheAkaSakuraYuurei,
                    CardId.YoumuTheBluesky,
                    CardId.MimaL9,
                    CardId.KanataOfTamaSakura,
                    CardId.HatanokokoroL4,
                    CardId.YoumuTheHalfSpiritL3,
                    CardId.YoumeiNoKuwa,
                    CardId.YoumuTheSweetMaid,
                    CardId.KokoroCorona
                }, cards, min, max);
                AI.Utils.CheckSelectCount(result, cards, min, max);
                return result;
            }
            return null;
        }

        public override IList<ClientCard> OnSelectPendulumSummon(IList<ClientCard> cards, int max)
        {
            Logger.DebugWriteLine("OnSelectPendulumSummon");
            // select the last cards

            IList<ClientCard> selected = new List<ClientCard>();
            for (int i = 1; i <= max; ++i)
            {
                ClientCard card = cards[cards.Count - i];
                if ((card.Location == CardLocation.Removed) || (card.Location == CardLocation.Grave))
                    selected.Add(card);
            }
            if (selected.Count == 0)
                selected.Add(cards[cards.Count - 1]);

            return selected;
        }


        private bool KokoroCoronaEffect()
        {
            AI.SelectCard(new[]
                {
                    CardId.HatanokokoroL4,
                    CardId.YoumuTheBluesky,
                    CardId.YoumuTheHalfSpiritL3
                });
            return true;
        }

        private bool MougakyouEffect()
        {
            if (ActivateDescription == AI.Utils.GetStringId(CardId.Mougakyou, 3))
            {
                AI.SelectCard(new[]
                    {
                    CardId.YoumuTheMangetsuYuuki,
                    CardId.YoumuTheAkaSakuraYuurei,
                    CardId.YoumuTheHalfSpiritL3,
                    CardId.YoumuTheBluesky,
                    CardId.YoumuTheSweetMaid
                });
                return true;
            }
            else if (ActivateDescription == AI.Utils.GetStringId(CardId.Mougakyou, 0) || ActivateDescription == AI.Utils.GetStringId(CardId.Mougakyou, 2))
            {
                AI.SelectCard(new[]
                    {
                    CardId.YoumuTheBluesky,
                    CardId.YoumuTheSweetMaid,
                    CardId.YoumuTheAkaSakuraYuurei,
                    CardId.YoumuTheHalfSpirit,
                    CardId.YoumuTheHalfSpiritL3
                });
                return true;
            }
            else
            {
                AI.SelectCard(new[]
                    {
                    CardId.Hakugyokurou,
                    CardId.KanataOfTamaSakura,
                    CardId.HiNoOtoko,
                    CardId.Mougakyou,
                    CardId.MimaL9,
                    CardId.HatanokokoroL4,
                    CardId.YoumuF6
                });
                return true;
            }
        }

        private bool HakugyokurouEffect()
        {
            if (Bot.GetGraveyardMonsters().Count !=0)
                return false;
            if (ActivateDescription == AI.Utils.GetStringId(CardId.Hakugyokurou, 1))
            {
                AI.SelectCard(new[]
                    {
                    CardId.Mougakyou
                });
                AI.SelectCard(new[]
                    {
                    CardId.YoumuTheMangetsuYuuki,
                    CardId.YoumuTheAkaSakuraYuurei,
                    CardId.YoumuTheHalfSpiritL3,
                    CardId.YoumuTheBluesky,
                    CardId.MimaL9,
                    CardId.YoumuTheSweetMaid
                });
                return true;
            }
            else if (ActivateDescription == AI.Utils.GetStringId(CardId.Hakugyokurou, 0))
            {
                AI.SelectCard(new[]
                    {
                    CardId.Mougakyou
                });
                List<ClientCard> spells = Enemy.GetSpells();
                if (spells.Count == 0)
                return false;
                ClientCard selected = null;
                {
                foreach (ClientCard card in spells)
                    {
                        if (card.IsFacedown())
                        {
                            selected = card;
                            break;
                        }
                    }
                }
                if (selected == null)
                    return false;
                AI.SelectCard(selected);
                return true;
            }
            else
            {
                AI.SelectCard(new[]
                    {
                    CardId.Mougakyou
                });
                AI.SelectCard(new[]
                    {
                    CardId.YoumuTheAkaSakuraYuurei
                });
                return true;
            }
        }


        private bool YoumuTheSweetMaidEffect()
        {
            AI.SelectCard(new[]
                {
                    CardId.YoumuTheBluesky,
                    CardId.YoumuTheHalfSpirit,
                    CardId.YoumuTheHalfSpiritL3,
                    CardId.YoumuTheAkaSakuraYuurei
                });
            {
                AI.SelectNextCard(new[]
                {
                    CardId.YoumuTheBluesky,
                    CardId.YoumuTheAkaSakuraYuurei,
                    CardId.YoumuTheHalfSpiritL3
                });
            }
            return true;
        }

        private bool YoumuTheBlueskySummon()
        {
            AI.SelectPosition(CardPosition.FaceUpDefence);
            return true;
        }

        private bool NueL3Effect()
        {
            AI.SelectCard(new[]
                {
                    CardId.YoumuF6
                });
            {
                AI.SelectNextCard(new[]
                {
                    CardId.YoumuTheMangetsuYuuki,
                    CardId.YoumuTheSweetMaid,
                    CardId.YoumuTheHalfSpiritL3,
                    CardId.YoumuTheBluesky,
                    CardId.YoumuTheAkaSakuraYuurei
                });
            }
            return true;
        }

        private bool MimaL9Effect()
        {
            AI.SelectCard(new[]
                {
                    CardId.YoumuTheAkaSakuraYuurei,
                    CardId.MimaL9,
                    CardId.YoumuTheHalfSpiritL3,
                    CardId.YoumuTheMangetsuYuuki,
                    CardId.YoumuTheSweetMaid,
                    CardId.YoumuTheBluesky,
                    CardId.HatanokokoroL4
                });
            return true;
        }

        private bool YuyukoL8Summon()
        {
            ClientCard target = AI.Utils.GetBestEnemyCard();
            if (target != null)
            {
                AI.SelectCard(target);
                return true;
            }
            return true;
        }

        private bool YuyukoL8Effect()
        {
            if (Duel.Turn != 1 && ActivateDescription == AI.Utils.GetStringId(CardId.YuyukoL8, 2))
            {
                return false;
            }
            if (ActivateDescription == -1 || ActivateDescription == AI.Utils.GetStringId(CardId.YuyukoL8, 0))
            {
                return true;
            }
            return false;
        }


        private bool LinkYuyukoSummon()
        {
            if (Bot.HasInMonstersZone(CardId.LinkYuyuko))
            {
                return false;
            }
            return true;
        }

        private bool LinkYuyukoEffect()
        {
            ClientCard targets = AI.Utils.GetBestEnemySpell();
            ClientCard target = AI.Utils.GetBestEnemyMonster();
            if (targets != null)
            {
                AI.SelectCard(targets);
                return true;
            }
            else if (target != null)
            {
                AI.SelectCard(target);
                return true;
            }
            return false;
        }

        private bool YoumuR4Summon()
        {
            return (AI.Utils.IsTurn1OrMain2() || AI.Utils.IsOneEnemyBetter());
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

        private bool LinkSummon()
        {
            return (AI.Utils.IsTurn1OrMain2() || AI.Utils.IsOneEnemyBetter())
                && AI.Utils.GetBestAttack(Bot) < Card.Attack;
        }
    }
}