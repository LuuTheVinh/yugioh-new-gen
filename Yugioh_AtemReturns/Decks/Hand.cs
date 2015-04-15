using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Yugioh_AtemReturns.Cards;
using System;

namespace Yugioh_AtemReturns.Decks
{
    class Hand : Deck
    {
        private int m_maxCard;


        public int MaxCard
        {
            get { return m_maxCard; }
            set { m_maxCard = value; }
        }
        public Hand(ePlayerId _id)
            : base(_id,eDeckId.HAND)
        {
            
        }
        protected override void Init()
        {
            base.Init();
            
        }
        public override void Update(GameTime _gameTime)
        {
            base.Update(_gameTime);

        }
        public override void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
            base.Draw(_spriteBatch);

            
            _spriteBatch.End();

        }

        protected override void OnCardAdded(CardEventArgs e)
        {
            base.OnCardAdded(e);
        }

        #region PublicMethod
        public override void AddTop(Card _card)
        {
            base.AddTop(_card);

        }

        public override Card RemoveAt(int index)
        {
            return base.RemoveAt(index);
        }

        public override LinkedListNode<Card> RemoveCard(Card _card)
        {
            return base.RemoveCard(_card);
        }

        public override LinkedListNode<Card> RemoveTop()
        {
            return base.RemoveTop();
        }


        #endregion // Public Method

    }
}
