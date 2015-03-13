using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Yugioh_AtemReturns.Decks
{
    class SpellField : Deck
    {
        public int MaxCard
        {
            get { return 5; }
        }

        public SpellField(ePlayerId _id)
            : base(_id, eDeckId.SPELLFIELD)
        {

        }

        sealed protected override void Init()
        {
            base.Init();

            this.DeckID = eDeckId.SPELLFIELD;
        }

        public void SetPosition()
        {

        }

        public override void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Begin();
            base.Draw(_spriteBatch);
            _spriteBatch.End();
        }
    }
}
