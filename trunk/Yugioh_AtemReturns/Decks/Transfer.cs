using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yugioh_AtemReturns.Cards;
using Yugioh_AtemReturns.Duelists;

namespace Yugioh_AtemReturns.Decks
{


    class Transfer
    {

        private LinkedListNode<Card> m_tranferedCard;
        private eDeckId m_Dest;


        public LinkedListNode<Card> TranferedCard
        {
            get { return m_tranferedCard; }
            private set { m_tranferedCard = value; }
        }

        public eDeckId DestDeck
        {
            get { return m_Dest; }
            private set { m_Dest = value; }
        }

        public Transfer()
        {
            this.Init();
        }

        private void Init()
        {
            DestDeck = eDeckId.NONE;
        }

        public void Update(Duelist _player)
        {
            this.collector(_player.DuelDisk);
            this.distribute(_player.DuelDisk);
        }

        #region Private Method

        private void collector(IEnumerable<Deck> _listDeck)
        {
            foreach (Deck deck in _listDeck)
            {

                if (deck.OutCard != null)
                {
                    this.removeFrom(deck);
                    return;
                }
            }
        }


        private void distribute(IEnumerable<Deck> _listDeck)
        {
            if (this.TranferedCard == null)
                return;
            foreach (Deck deck in _listDeck)
            {
                if (this.DestDeck == deck.DeckID)
                {
                    this.removeTo(deck);
                    return;
                }
            }
        }

        private void activeEffect()
        {
            //if (this.TranferedCard == null)
            //    return;
            //if (this.TranferedCard.Value.IsEffect == true)
            //{
            //    // ACTIVE
            //}
        }

        private void removeTo(Deck _deck)
        {
            _deck.AddTop(this.TranferedCard.Value);
            this.TranferedCard = null;
            this.DestDeck = eDeckId.NONE;
        }

        private void removeFrom(Deck _deck)
        {
            this.DestDeck = _deck.DestDeck;
            _deck.DestDeck = eDeckId.NONE;
            this.TranferedCard = _deck.OutCard;
            _deck.OutCard = null;
        }
        #endregion // Private Method
    }
}
