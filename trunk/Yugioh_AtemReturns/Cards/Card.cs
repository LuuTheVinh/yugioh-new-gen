using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Yugioh_AtemReturns.GameObjects;
using Yugioh_AtemReturns.Manager;
using Microsoft.Xna.Framework;

namespace Yugioh_AtemReturns.Cards
{

    abstract class Card : MyObject
    {
        public Sprite s_BackSide = null;

        private eCardType cardType;
        private bool isFaceUp;

        public bool IsFaceUp
        {
            get { return isFaceUp; }
            set { isFaceUp = value; }
        }
        public eCardType CardType
        {
            get { return cardType; }
            set { cardType = value; }
        }

        public Vector2 Postion
        {
            get { return Sprite.Position; }
            set
            {
                Sprite.Position = value;
                s_BackSide.Position = value;
            }
        }
        public Vector2 Scale
        {
            get { return Sprite.Scale; }
            set
            {
                Sprite.Scale = value;
                s_BackSide.Scale = value;
            }
        }
        public float Rotation
        {
            get { return Sprite.Rotation; }
            set
            {
                Sprite.Rotation = value;
                s_BackSide.Rotation = value;
            }
        }

        public Card(ContentManager _content, ID _id, SpriteID _spriteId, eCardType _cardType)
            : base(_content, _id, _spriteId)
        {
            this.s_BackSide = new Sprite(SpriteManager.getInstance(_content).GetSprite(SpriteID.CBackSide));
            this.IsFaceUp = true;
            this.CardType = _cardType;
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch _spritebatch)
        {
            if (this.IsFaceUp == true)
                base.Draw(_spritebatch);
            else
                this.s_BackSide.Draw(_spritebatch);
                
        }
    }
}
