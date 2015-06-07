using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Yugioh_AtemReturns.Cards;
using Microsoft.Xna.Framework.Content;
using Yugioh_AtemReturns.Duelists;
using Yugioh_AtemReturns.Manager;

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

    delegate void CardAddedEventHandler(Deck sender, CardEventArgs e);
    delegate void CardRemoveEventHandler(Deck sender, CardEventArgs e);
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
        protected bool isListCardChanged;

        public event CardAddedEventHandler CardAdded;

        protected virtual void OnCardAdded(CardEventArgs e)
        {
            isListCardChanged = true;
            if (CardAdded != null)
                CardAdded(this, e);

        }
        public event CardRemoveEventHandler CardRemoved;
        protected virtual void OnCardRemoved(CardEventArgs e)
        {
            isListCardChanged = true;
            if (CardRemoved != null)
                CardRemoved(this, e);
        }

        public bool IsAction
        {
            get
            {
                foreach (var item in ListCard)
                {
                    if (item.IsAction == true)
                        return true;
                }
                return false;
            }
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
        public LinkedList<Card> ListCard
        {
            get { return _listCard; }
            protected set { _listCard = value; }
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
            this.CardAdded +=Deck_CardAddedPlaySound;
        }

        private void Deck_CardAddedPlaySound(Deck sender, CardEventArgs e)
        {
            EffectManager.GetInstance().Play(eSoundId.card_move);
        }

        protected virtual void Init()
        {
            _listCard = new LinkedList<Card>();
            isListCardChanged = false;
        }

        /// <summary>
        /// Add Card to Deck's Top (Last of List)
        /// </summary>
        /// <param name="_card"></param>
        public virtual void AddTop(Card _card)
        {
            ListCard.AddLast(_card);
            OnCardAdded(new CardEventArgs(_card));
        }
        public virtual void AddBot(Card _card)
        {
            ListCard.AddFirst(_card);
            OnCardAdded(new CardEventArgs(_card));
        }

        public virtual void SendTo(Card _card, eDeckId _deck)
        {
            this.OutCard = this.RemoveCard(_card);
            this.DestDeck = _deck;
        }

        public virtual LinkedListNode<Card> RemoveTop()
        {
            if (ListCard.Count == 0)
                return null;
            LinkedListNode<Card> temp = ListCard.Last;

            ListCard.Remove(temp);
            OnCardRemoved(new CardEventArgs(temp.Value));
            return temp;
        }
        public virtual LinkedListNode<Card> RemoveCard(Card _card)
        {
            LinkedListNode<Card> temp = ListCard.Find(_card);
                ListCard.Remove(temp);
            OnCardRemoved(new CardEventArgs(temp.Value));
            return temp;
        }
        public virtual Card RemoveAt(int index)
        {
            var temp = ListCard.ElementAt<Card>(index);
            ListCard.Remove(temp);
            OnCardRemoved(new CardEventArgs(temp));
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

        public virtual void Update(GameTime _gameTime)
        {
            this.isListCardChanged = false;
            foreach (var item in ListCard)
            {
                    item.Update(_gameTime);
                if (this.isListCardChanged == true)
                    break;
            }
        }
        public virtual void Draw(SpriteBatch _spriteBatch)
        {
            foreach (var card in ListCard)
            {
                card.Draw(_spriteBatch);
            }
  
        }
        public override string ToString()
        {
            return this.Count.ToString();
        }
    }
}
