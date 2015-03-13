using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yugioh_AtemReturns.Cards;
using Microsoft.Xna.Framework.Graphics;

namespace Yugioh_AtemReturns.Decks
{
    class MonsterField : Deck
    {
        public int MaxCard
        {
            get { return 5; }
        }
        public MonsterField(ePlayerId _id)
            : base(_id, eDeckId.MONSTERFIELD)
        {
        }

        sealed protected override void Init()
        {
            base.Init();

            this.DeckID = eDeckId.MONSTERFIELD;
        }

        public override void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Begin();
            base.Draw(_spriteBatch);
            _spriteBatch.End();
        }

        public override void AddTop(Card _card)
        {
            base.AddTop(_card);

        }

    }
}
