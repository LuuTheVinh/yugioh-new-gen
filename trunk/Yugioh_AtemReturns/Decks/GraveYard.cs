using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Yugioh_AtemReturns.Cards;

namespace Yugioh_AtemReturns.Decks
{
    class GraveYard : Deck
    {
        public GraveYard(ePlayerId _id)
            : base(_id, eDeckId.GRAVEYARD)
        {
        }

        protected override void Init()
        {
            base.Init();

        }

        public override void Draw(SpriteBatch spritebatch)
        {
            if (ListCard.Count == 0)
                return;
            LinkedListNode<Card> node = ListCard.Last;
            int count = 0;
            do
            {
                node.Value.Draw(spritebatch);
                node = node.Previous;
                count++;
            }
            while (count < ListCard.Count);
        }
    }
}
