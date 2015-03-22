using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yugioh_AtemReturns.Cards;

namespace Yugioh_AtemReturns.Decks
{
     internal class CardEventArgs
    {
        public Card Card { get; set; }
        public CardEventArgs(Card card)
        {
            this.Card = card;
        }
    }
}
