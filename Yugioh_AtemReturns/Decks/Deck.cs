﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Yugioh_AtemReturns.Cards;

namespace Yugioh_AtemReturns.Decks
{
    public enum eDeckId
    {
        NONE,
        MAINDECK,
        HAND,
        MONSTERFIELD,
        SPELLFIELD,
        GRAVEYARD,
        
    }
    public enum ePlayerId
    {
        PLAYER,
        COMPUTER
    }

    delegate void CardAddedEventHandler(Deck sender, CardAddedEventArgs e);

    abstract class Deck 
    {

        /// <summary>
        /// Deck's Bot is First of List, Deck's Top is Last of List
        /// </summary>
        protected LinkedList<Card> _listCard;
        protected ePlayerId _playerID;
        protected eDeckId m_deckID;
        protected Vector2 m_position;
        protected LinkedListNode<Card> _outCard;
        protected eDeckId _outDest;

        public event CardAddedEventHandler CardAdded;
        protected virtual void OnCardAdded(CardAddedEventArgs e)
        {
            if (CardAdded != null)
                CardAdded(this, e);
        }

        #region Property
        public Vector2 Position
        {
            get { return m_position; }
            set { m_position = value; }
        }
        public int Count
        {
            get { return _listCard.Count; }
        }
        public Card this[int index]
        {
            get { return _listCard.ElementAt<Card>(index); }
        }
        protected LinkedList<Card> ListCard
        {
            get { return _listCard; }
            set { _listCard = value; }
        }
        public ePlayerId PlayerID
        {
            get { return _playerID; }
            protected set { _playerID = value; }
        }
        public eDeckId DeckID
        {
            get { return m_deckID; }
            protected set { m_deckID = value; }
        }
        /// <summary>
        /// Là nơi chứa Card để chuyển đi cho Deck khác
        /// </summary>
        public LinkedListNode<Card> OutCard
        {
            get { return _outCard; }
            set { _outCard = value; }
        }
        public eDeckId DestDeck
        {
            get { return _outDest; }
            set { _outDest = value; }
        }
        #endregion

        public Deck(ePlayerId _playerID, eDeckId _deckid)
        {
            PlayerID = _playerID;
            this.DeckID = _deckid;
            this.Init();
        }

        protected virtual void Init()
        {
            _listCard = new LinkedList<Card>();

        }

        /// <summary>
        /// Add Card to Deck's Top (Last of List)
        /// </summary>
        /// <param name="_card"></param>
        public virtual void AddTop(Card _card)
        {
            ListCard.AddLast(_card);
            OnCardAdded(new CardAddedEventArgs(_card));
        }
        public virtual void AddBot(Card _card)
        {
            ListCard.AddFirst(_card);
            OnCardAdded(new CardAddedEventArgs(_card));
        }

        public virtual LinkedListNode<Card> RemoveTop()
        {
            if (ListCard.Count == 0)
                return null;
            LinkedListNode<Card> temp = ListCard.Last;

            ListCard.Remove(temp);

            return temp;
        }
        public virtual LinkedListNode<Card> RemoveCard(Card _card)
        {
            LinkedListNode<Card> temp = ListCard.Find(_card);
            ListCard.Remove(temp);
            return temp;
        }
        public virtual Card RemoveAt(int index)
        {
            var temp = ListCard.ElementAt<Card>(index);
            ListCard.Remove(temp);
            return temp;
        }
        protected virtual void MoveTopToDeck(eDeckId _id)
        {
            this.OutCard = this.RemoveTop();
            this.DestDeck = _id;
        }

        /// <summary>
        /// Player view all Card in Deck
        /// </summary>
        public virtual void View()
        {
            throw new Exception();
        }

        public void Shuffle()
        {
            Card[] _cards = new Card[ListCard.Count];
            bool[] _index = new bool[ListCard.Count];

            int temp = 0;
            foreach (Card item in ListCard)
            {
                while (true)
                {
                    temp = Rnd.Rand(0, ListCard.Count - 1);
                    if (_index[temp] == false)
                        break;
                }
                _cards[temp] = item;
                _index[temp] = true;
            }
            ListCard.Clear();
            foreach (Card c in _cards)
            {
                ListCard.AddLast(c);
            }
        }

        public virtual void Draw(SpriteBatch _spriteBatch)
        {
            foreach (var card in ListCard)
            {
                card.Draw(_spriteBatch);
            }
        }

    }
}