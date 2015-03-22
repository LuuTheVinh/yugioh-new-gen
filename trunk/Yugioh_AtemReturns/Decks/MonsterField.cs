using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yugioh_AtemReturns.Cards;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Yugioh_AtemReturns.Cards.Monsters;

namespace Yugioh_AtemReturns.Decks
{
    class MonsterField : Deck
    {
        private int curSlot;
        public int CurrentSlot
        {
            get { return curSlot; }
            set {
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
        public MonsterField(ePlayerId _id)
            : base(_id, eDeckId.MONSTERFIELD)
        {
        }

        sealed protected override void Init()
        {
            base.Init();

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
        }

    }
}
