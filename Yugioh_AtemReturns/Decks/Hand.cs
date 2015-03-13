using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Yugioh_AtemReturns.Cards;

namespace Yugioh_AtemReturns.Decks
{
    class Hand : Deck
    {
        private int m_maxCard;

        public Rectangle Bound;

        private LinkedListNode<Card> SelectCard;

        public int MaxCard
        {
            get { return m_maxCard; }
            set { m_maxCard = value; }
        }


        public Hand(ePlayerId _id)
            : base(_id,eDeckId.HAND)
        {

        }


        public override void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
            base.Draw(_spriteBatch);
            _spriteBatch.End();

        }

        protected override void OnCardAdded(CardAddedEventArgs e)
        {
            base.OnCardAdded(e);
        }
        protected override void Init()
        {
            base.Init();
            this.CardAdded += new CardAddedEventHandler(Hand_CardAdded);
        }
        private void Hand_CardAdded(Deck sender, CardAddedEventArgs e)
        {
            int count = ListCard.Count;
            ListCard.Last.Value.Postion = new Vector2(
                    GlobalSetting.Default.CenterField.X - (ListCard.First.Value.Sprite.Bound.Width + GlobalSetting.Default.HandDistance.X) * count / 2,
                    this.Position.Y);
            LinkedListNode<Card> node = ListCard.Last.Previous;
            while (node != null)
            {
                node.Value.Postion = new Vector2(
                    node.Next.Value.Sprite.Bound.Right + GlobalSetting.Default.HandDistance.X ,
                    this.Position.Y);
                node = node.Previous;
            }
        }

        #region PublicMethod
        public override void AddTop(Card _card)
        {
            base.AddTop(_card);

        }

        public override Card RemoveAt(int index)
        {
            var temp = ListCard.ElementAt<Card>(index);
            ListCard.Remove(temp);
            return temp;
        }

        public override LinkedListNode<Card> RemoveCard(Card _card)
        {
            LinkedListNode<Card> temp = ListCard.Find(_card);
            ListCard.Remove(temp);
            return temp;
        }

        public override LinkedListNode<Card> RemoveTop()
        {
            if (ListCard.Count == 0)
                return null;
            LinkedListNode<Card> temp = ListCard.Last;
            ListCard.Remove(temp);
            return temp;
        }


        #endregion // Public Method

    }
}
