using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yugioh_AtemReturns.Cards;
using Microsoft.Xna.Framework;
using Yugioh_AtemReturns.Cards.Monsters;

namespace Yugioh_AtemReturns.Duelists.AI_Logics
{
    class YamiYugiDeck_AI : AI
    {
        LinkedList<Card> monsterLvLow; // LV4 or lowwer
        LinkedList<Card> monsterLvMid; //LV 5 - LV 6
        LinkedList<Card> monsterLvHigh; // LV 7 or higher
        LinkedList<Card> monster;
        LinkedList<Card> trap;
        LinkedList<Card> spell;

        public YamiYugiDeck_AI()
        {
            monsterLvLow = new LinkedList<Card>();
            monsterLvMid = new LinkedList<Card>();
            monsterLvHigh = new LinkedList<Card>();
            monster = new LinkedList<Card>();
            trap = new LinkedList<Card>();
            spell = new LinkedList<Card>();
        }

        public Card Summon(Player _player, Computer _computer)
        {
            int maxLevelSummon;
            if (_computer.MonsterField.Count >= 2)
                maxLevelSummon = 12;
            if (_computer.MonsterField.Count == 1)
                maxLevelSummon = 6;
            if (_computer.MonsterField.Count == 0)
                maxLevelSummon = 4;
            return this.Summon_Monster_LevelLow(_player, _computer);
        }

        private void Summon_Monster_LevelHigh(Player _player, Computer _computer)
        {
            if (this.monsterLvHigh.Count == 0)
                return;
            int index = Rnd.Rand(0, this.monsterLvHigh.Count - 1);

        }
        private Card Summon_Monster_LevelLow(Player _player, Computer _computer)
        {
            if (this.monsterLvLow.Count == 0)
                return null;
            int index = Rnd.Rand(0, this.monsterLvLow.Count - 1);
            return  monsterLvLow.ElementAt(index);

        }
        public void Update(Player _player, Computer _computer)
        {
            monster.Clear();
            monsterLvLow.Clear();
            monsterLvHigh.Clear();
            monsterLvMid.Clear();
            trap.Clear();
            spell.Clear();
            foreach (var card in _computer.Hand.ListCard)
            {
                if (card.CardType == eCardType.MONSTER)
                {
                    monster.AddLast(card);
                    if ((card as Monster).Level >= 7)
                    {
                        this.monsterLvHigh.AddLast(card);
                    }
                    if ((card as Monster).Level == 5 || (card as Monster).Level == 6)
                    {
                        this.monsterLvMid.AddLast(card);
                    }
                    if ((card as Monster).Level <=4)
                    {
                        this.monsterLvLow.AddLast(card);
                        monsterLvLow.Distinct();
                    }
                }
            }
          
        }
                
        public Card SetTrap(Player _player, Computer _computer)
        {
            return null;
        }
        public Card ActiveTrap(Player _player, Computer _computer)
        {
            return null;
        }
        public Card ActiveSpell(Player _player, Computer _computer)
        {
            return null;
        }
        public ePhase SelectPhase(Player _player, Computer _computer)
        {
            throw new Exception();
        }

    }
}
