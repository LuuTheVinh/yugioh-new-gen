using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yugioh_AtemReturns.Cards;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Yugioh_AtemReturns.Cards.Monsters;
using Yugioh_AtemReturns.Duelists;
using Yugioh_AtemReturns.GameObjects;

namespace Yugioh_AtemReturns.Decks
{
    class MonsterField : Deck
    {
        private int curSlot;
        private bool[] m_aSlot;
        private bool m_CanATK;

        public bool CanAttack
        {
            get
            {
                if (m_CanATK == false)
                    return false;
                foreach (var card in ListCard)
                {
                    if ((card as Monster).CanATK == true)
                        return true;
                }
                return false;
            }
            set
            {
                m_CanATK = true;
            }
        }

        public int CurrentSlot
        {
            get { return curSlot; }
            set {
                if (value < 0)
                    throw new Exception("Current Position was assigned with wrong value");
                if (value >= MaxCard)
                    curSlot = 0;
                else
                {
                    curSlot = value;
                }
            }
        }
        public int MaxCard
        {
            get { return 5; }
        }
        private Card[] m_cardslot;
        public MonsterField(ePlayerId _id)
            : base(_id, eDeckId.MONSTERFIELD)
        {
        }

        sealed protected override void Init()
        {
            base.Init();
            CanAttack = true;
            this.CardAdded += new CardAddedEventHandler(MonsterField_CardAdded);
            this.CardRemoved +=new CardRemoveEventHandler(MonsterField_CardRemoved);
            m_aSlot = new bool[5];
            m_cardslot = new Card[5];
        }
        public override void Update(GameTime _gameTime)
        {
            base.Update(_gameTime);
        }
        public override void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Begin();

            base.Draw(_spriteBatch);
            _spriteBatch.End();
            foreach (Monster item in ListCard)
            {
                if (this.PlayerID == ePlayerId.PLAYER || item.BattlePosition == eBattlePosition.ATK)
                    item.DrawNum(_spriteBatch);
            }
        }

        public bool AnySlot()
        {
            return this.Count < MaxCard;
        }
        private void MonsterField_CardAdded(Deck sender, CardEventArgs e)
        {
            while(m_cardslot[CurrentSlot] != null)
            {
                CurrentSlot++;
            }
            m_cardslot[CurrentSlot] = e.Card;
            Vector2 position = Vector2.Zero;
            if (PlayerID == ePlayerId.PLAYER)
            {
                position = GlobalSetting.Default.PlayerMonF + new Vector2(5, 90)
                    + GlobalSetting.Default.FieldSlot.X * CurrentSlot * Vector2.UnitX;
            }
            else
            {
                position = ComputerSetting.Default.MonsterField + new Vector2(5, 0)
                    - ComputerSetting.Default.FieldSlot.X * CurrentSlot * Vector2.UnitX;
            }
            //if (this.PlayerID != ePlayerId.COMPUTER || (e.Card as Monster).BattlePosition != eBattlePosition.DEF)
                (e.Card as Monster).ShowBattlePoint(position);
        }
        private void MonsterField_CardRemoved(Deck sender, CardEventArgs e)
        {
            for (int i = 0; i < m_cardslot.Length; i++)
            {
                if (object.ReferenceEquals(m_cardslot[i], e.Card) == true)
                {
                    m_cardslot[i] = null;
                    return;
                }
            }
            (e.Card as Monster).ClearBattlePoint();
        }
        //--
    }
}
