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
            spritebatch.Begin();
            base.Draw(spritebatch);
            spritebatch.End();
        }
    }
}
