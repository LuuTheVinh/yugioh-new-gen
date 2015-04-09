using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Yugioh_AtemReturns.Cards;
using Microsoft.Xna.Framework;

namespace Yugioh_AtemReturns.Decks
{
    class SpellField : Deck
    {
        private int curSlot;
        public int CurrentSlot
        {
            get { return curSlot; }
            set
            {
                if (value < 0)
                    throw new Exception("Current Position was assigned with wrong value");
                if (value >= MaxCard)
                    curSlot = 0;
                else
                    curSlot = value;
            }
        }
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

        public override void Update(Microsoft.Xna.Framework.GameTime _gameTime)
        {
            base.Update(_gameTime);
        }
        public override void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Begin();
            base.Draw(_spriteBatch);
            _spriteBatch.End();
        }

    }
}
