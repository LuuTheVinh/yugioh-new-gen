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
    delegate void CardLeftClickEventHandle(Card sender, EventArgs e);
    delegate void CardHoveredEventHandle(Card sender, EventArgs e);
    abstract class Card : MyObject
    {
        public Sprite s_BackSide = null;

        private eCardType cardType;
        private bool isFaceUp;
        private Button button;

        public event CardLeftClickEventHandle LeftClick;
        public event CardLeftClickEventHandle RightClick;
        public event CardHoveredEventHandle Hovered;
        public event CardHoveredEventHandle OutHovered;

        public virtual void OnLeftClick(EventArgs e)
        {
            if (LeftClick != null)
                LeftClick(this, e);
        }
        public virtual void OnRightClick(EventArgs e)
        {
            if (RightClick != null)
                RightClick(this, e);
        }
        public virtual void OnHovered(EventArgs e)
        {
            if (Hovered != null)
                Hovered(this, e);
        }
        public virtual void outHovered(EventArgs e)
        {
            if (OutHovered != null)
                OutHovered(this, e);
        }
        #region Property
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

        public Vector2 POSITION
        {
            get { return base.Sprite.Position; }
            set
            {
                base.Sprite.Position = value;
                this.s_BackSide.Position = value;
            }
        }
        public Vector2 SCALE
        {
            get { return Sprite.Scale; }
            set
            {
                Sprite.Scale = value;
                s_BackSide.Scale = value;
            }
        }
        public  float ROTATION
        {
            get { return Sprite.Rotation; }
            set
            {
                Sprite.Rotation = value;
                s_BackSide.Rotation = value;
            }
        }
        public Vector2 ORIGIN
        {
            get { return Sprite.Origin; }
            set
            {
                Sprite.Origin = value;
                s_BackSide.Origin = value;
            }
        }

        public Button Button
        {
            get { return button; }
            set { button = value; }
        }
        public STATUS STATUS
        {
            get { return base.Status; }
            set
            {
                base.Status = value;
                if (value == STATUS.DEF)
                    this.IsFaceUp = false;
                if (value == STATUS.ATK)
                    this.isFaceUp = true;
            }
        }
        #endregion
        public Card(ContentManager _content, ID _id, SpriteID _spriteId, eCardType _cardType)
            : base(_content, _id, _spriteId)
        {
            this.s_BackSide = new Sprite(SpriteManager.getInstance(_content).GetSprite(SpriteID.CBackSide));
            this.IsFaceUp = true;
            this.CardType = _cardType;
            this.Button = new Button(this.Sprite);
            this.Button.Position = this.Position;
            this.Button.ButtonEvent += new Action(Button_DoActionClick);
            this.Button.RightClick += new Action(Button_OnRightClick);
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch _spritebatch)
        {
            if (this.IsFaceUp == true)
                base.Draw(_spritebatch);
            else
                this.s_BackSide.Draw(_spritebatch);
        }
        public override void Update(GameTime gameTime)
        {
            
            this.Button.Update(gameTime);

            if (this.Button.Hovered)
            {
                this.OnHovered(EventArgs.Empty);
            }
            else
            {
                this.outHovered(EventArgs.Empty);
            }
        }

        private void Button_DoActionClick()
        {
            this.OnLeftClick(EventArgs.Empty);
        }
        private void Button_OnRightClick()
        {
            this.OnRightClick(EventArgs.Empty);
        }
    }
}
