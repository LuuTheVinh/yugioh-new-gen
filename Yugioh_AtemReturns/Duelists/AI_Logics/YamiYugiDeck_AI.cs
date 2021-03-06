﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yugioh_AtemReturns.Cards;
using Microsoft.Xna.Framework;
using Yugioh_AtemReturns.Cards.Monsters;
using Yugioh_AtemReturns.Scenes;

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

        private Card Summon(Computer _com, bool _is_ATK)
        {
            if (this.monster.Any() == false)
                return null;
            Card rtvalue;
            if (_is_ATK == true)
            {
                int atkHandMax = monster.Max(card => (card as Monster).Atk); //tìm lá bài có max atk trên tay

                rtvalue = monster.Where(card => (card as Monster).Atk == atkHandMax).First();
                (rtvalue as Monster).BattlePosition = eBattlePosition.ATK;
            }
            else
            {
                int atkHandMin = monster.Max(card => (card as Monster).Def); //tìm lá bài có max def trên tay

                rtvalue = monster.Where(card => (card as Monster).Def == atkHandMin).First();
                (rtvalue as Monster).BattlePosition = eBattlePosition.DEF;
            }
            if (Duelist.RequireTributer[(rtvalue as Monster).Level - 1] == 0)
                return rtvalue;  // nếu level thấp không cần tribute thì return luôn

            //ngược lại cần tribute thì kiểm tra xem, những lá bài cần tribute có mạnh hơn lá muôn summon không
            // nếu mạnh hơn thì không summon
            if (_com.MonsterField.ListCard.Any())
            {
                var tributemonster = _com.MonsterField.ListCard.OrderBy(
                        card => (card as Monster).BattlePoint).Take(2);
                //nếu không đủ chỗ để summon nữa thì không summon
                if (_com.MonsterField.Count - Duelist.RequireTributer[(rtvalue as Monster).Level - 1]
                           == _com.MonsterField.MaxCard)
                {
                    this.monster.Remove(rtvalue);
                    rtvalue = Summon(_com, _is_ATK); // đệ quy
                }

                // nếu lá bài dùng để hi sinh mạnh hơn lá muốn summon thì bỏ qua
                if ((rtvalue as Monster).BattlePoint < tributemonster.Max(card => (card as Monster).BattlePoint))
                {
                    this.monster.Remove(rtvalue);
                    rtvalue = Summon(_com, _is_ATK);
                }

                //khi summon không thoả điều kiện thì bị remove. và lặp lại vòng đệ quy
                // khi vào vòng đệ quy. lá mạnh nhất đã thay đổi.
                // lại tiếp tục bếu không đủ điều kiện lại remove và vào vòng đệ quy.
                // đến khi thoã điều kiện thì return
            }
            return rtvalue;
        }
        private Card Set_CardDefence(Computer _com)
        {
            //if (this.monster.Any() == false)
                return null;
        }
        public Card Summon(Player _player, Computer _computer)
        {
            //hàm update kiểm tra trường hợp nào có thể summon, trường hợp nào có thể set def
            // không quan tâm summon monster nào, set bài nào

            this.refresh(_computer);
            if (_player.MonsterField.Count == 0) // nếu đối thủ không có bài thì summon
            {
                return this.Summon(_computer, true);
            }
            else
            {
                if (_player.MonsterField.ListCard.All(card => (card as Monster).BattlePosition == eBattlePosition.DEF))
                {
                    return this.Summon(_computer,true);
                }
                if ((_player.MonsterField.ListCard.Min(card => (card as Monster).BattlePoint)
                    <= this.monster.Max(card_ => (card_ as Monster).Atk)))
                {
                    return this.Summon(_computer, true);
                }
            }

            return this.Summon(_computer, false);
            return null;
        }

        public void SelectATK(BattlePhase _battlephase,Player _player, Computer _computer)
        {
            Card monsterbeATK;
            int minBattlePoint;
            Card monsteratk;
            if (_player.MonsterField.ListCard.Any())
            {
                // chọn trước lá bị tấn công
                minBattlePoint = _player.MonsterField.ListCard.Min(card => (card as Monster).BattlePoint);
                monsterbeATK = _player.MonsterField.ListCard.Where(card => (card as Monster).BattlePoint == minBattlePoint).First();
            }
            else
            {
                minBattlePoint = 0;
                monsterbeATK = null;
            }
            if (_battlephase.List_monsterATK.Any())
            {
                LinkedList<Card> list = new LinkedList<Card>(
                    _battlephase.List_monsterATK
                    .Where(card =>  (card as Monster).Atk > minBattlePoint));

                if (list.Any())
                {
                    monsteratk = list
                        .OrderBy(card => (card as Monster).Atk)
                        .First();
                }
                else
                    monsteratk = null;
            }
            else
            {
                monsteratk = null;
            }
            _battlephase.MonsterATK = monsteratk as Monster;
            if (_battlephase.MonsterATK != null)
                _battlephase.MonsterBeATK = monsterbeATK as Monster;
            else
                _battlephase.MonsterBeATK = null;
        } 

        public Card Tribute(Computer _computer)
        {
            int min = _computer.MonsterField.ListCard.Min(card => (card as Monster).BattlePoint);
            Card tributemonster = _computer.MonsterField.ListCard.Where(card => (card as Monster).BattlePoint == min).First();

            return tributemonster;
        }
        public IEnumerable<Card> Tribute(Computer _commputer, int _amount)
        {
            var tributemonster = _commputer.MonsterField.ListCard.OrderBy(
                    card => (card as Monster).BattlePoint)
                    .Select(card => (card as Monster).BattlePoint)
                    .Take(_amount);
            return  _commputer.MonsterField.ListCard
                .Where( card => tributemonster.Contains((card as Monster).BattlePoint));
        }
        /// <summary>
        /// phân loại bài trên tay
        /// </summary>
        /// <param name="_computer"></param>
        private void classify(Computer _computer)
        {
            int count = _computer.MonsterField.Count;
            foreach (var card in _computer.Hand.ListCard)
            {
                if (card.CardType == eCardType.MONSTER)
                {
                    if ((card as Monster).Level >= 7)
                    {
                        if (count >= 2)
                            monster.AddLast(card);
                        this.monsterLvHigh.AddLast(card);
                    }
                    if ((card as Monster).Level == 5 || (card as Monster).Level == 6)
                    {
                        if (count >= 1)
                            monster.AddLast(card);
                        this.monsterLvMid.AddLast(card);
                    }
                    if ((card as Monster).Level <= 4)
                    {
                        if (count >= 0)
                            monster.AddLast(card);
                        this.monsterLvLow.AddLast(card);
                        monsterLvLow.Distinct();
                    }
                }
            }
        }
        /// <summary>
        /// clear dữ liệu cử để cập nhật
        /// </summary>
        private void clear()
        {
            monster.Clear();
            monsterLvLow.Clear();
            monsterLvHigh.Clear();
            monsterLvMid.Clear();
            trap.Clear();
            spell.Clear();
        }
        /// <summary>
        /// refresh mỗi frame một lần
        /// </summary>
        /// <param name="_computer"></param>
        private void refresh(Computer _computer)
        {
            this.clear();
            this.classify(_computer);
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
