using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Yugioh_AtemReturns.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Yugioh_AtemReturns.Cards;

namespace Yugioh_AtemReturns.GameObjects
{
    class DetailSideBar : MyObject
    {
        private SpriteFont font;
        private ContentManager contentManager;
        private Vector2 descPosition;
        private Rectangle textBoxRect;
        private Button scrollBtn;
        private int scrollBarHeight;
        private float ratioPosition;
        private Button upBtn, downBtn;

        private InputController inputController;

        #region Properties
        public Sprite CardPreview { get; set; }
        public string Description { get; set; }
        public CardData CardData { get; set; }

        #endregion

        public DetailSideBar(ContentManager _content)
        {
            inputController = new InputController();
            contentManager = _content;

            this.Sprite = new Sprite(_content, "General\\detail");
            this.Sprite.Position = new Vector2(0, 0);
            this.CardPreview = new Sprite(SpriteManager.getInstance(_content).GetSprite(SpriteID.BBackSide));
            this.CardPreview.Position = new Vector2(this.Position.X + 5, this.Position.Y + 70);

            font = _content.Load<SpriteFont>("ArialSmall");
            descPosition = new Vector2(this.Position.X + 15, this.Position.Y + 375);
            textBoxRect = new Rectangle((int)descPosition.X, (int)descPosition.Y, 165, 140);

            //CARD DESCRIPTION LAY TRONG CARD DATA
            this.Description = CardData != null ? CardData.Description : "";
            //this.Description = "Dong thu nhat. Hello. Hello. Hello. Hello. Hello. Hello. Hello. Hello. Hello. Hello. Hello. Hello. Hello. Hello. Hello. Hello. Hello. Hello. Hello. Hello. Hello. Hello. Hello. Hello. Hello. Hello. Hello. Hello. Hello. Hello. Hello. Hello. Hello. Hello. Hello. Hello. Hello. Hello. Hello. Hello. Hello. Hello. Hello. Hello. Hello. Hello. Hello. Hello. Hello. Hello. Hello. Hello. Hello. Hello. Hello. Hello. Hello. Hello. Hello. Hello. Hello. Hello. Hello. Hello.";

            this.Description = TextFormater.getIntance().WordWrap(font, this.Description, 170);

            scrollBtn = new Button(new Sprite(_content, "General\\scroll_bar"));
            scrollBtn.Sprite.Frame = new Rectangle(16, 16, 16, 16);

            scrollBtn.Position = new Vector2(this.Position.X + 189, this.Position.Y + 381);
            scrollBarHeight = 129 - scrollBtn.Sprite.Frame.Height;

            var normalup = new Sprite(_content, "General\\scroll_bar");
            normalup.Frame = new Rectangle(16, 0, 16, 16);

            var hoverup = new Sprite(_content, "General\\scroll_bar");
            hoverup.Frame = new Rectangle(32, 0, 16, 16);

            upBtn = new Button(normalup, hoverup);
            upBtn.Position = new Vector2(this.Position.X + 189, this.Position.Y + 365);
            upBtn.ButtonEvent += MoveTextUp;

            var normaldown = new Sprite(_content, "General\\scroll_bar");
            normaldown.Frame = new Rectangle(16, 146, 16, 16);

            var hoverdown = new Sprite(_content, "General\\scroll_bar");
            hoverdown.Frame = new Rectangle(32, 146, 16, 16);

            downBtn = new Button(normaldown, hoverdown);
            downBtn.Position = new Vector2(this.Position.X + 189, this.Position.Y + 511);
            downBtn.ButtonEvent += MoveTextDown;

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            //SCROLL
            if (font.MeasureString(Description).Y > textBoxRect.Height)
            {
                scrollBtn.Update(gameTime);
                upBtn.Update(gameTime);
                downBtn.Update(gameTime);
                MouseUpdate();
                ScrollBtnPress();
            }
        }

        private void MouseUpdate()
        {
            inputController.Begin();

            if(textBoxRect.Contains(inputController.MousePosition))
            {
                var nextY = inputController.DeltaScrollWheelValue * 0.1f;

                //OLD
                //if (nextY >= textBoxRect.Top - (font.MeasureString(Description).Y - textBoxRect.Height)
                //    && nextY <= textBoxRect.Top + 10) //cộng 10 cho nó dư ra chút
                //{
                //    descPosition = new Vector2(descPosition.X, nextY);

                //    //Tính tỉ lệ text di chuyển so với scroll btn di chuyển
                //    float ratio = scrollBarHeight / (font.MeasureString(Description).Y - textBoxRect.Height);

                //    scrollBtn.Position = new Vector2(scrollBtn.Position.X, scrollBtn.Position.Y + inputController.DeltaScrollWheelValue * 0.1f * ratio * (-1));
                //}
                
                
                MoveTextBox(nextY);

                float ratio = scrollBarHeight / (font.MeasureString(Description).Y - textBoxRect.Height);

                MoveScrollButton(inputController.DeltaScrollWheelValue * 0.1f * ratio * (-1));
            }
            
            inputController.End();
        }

        private void MoveTextBox(float value_y)
        {
            float topY = textBoxRect.Top - (font.MeasureString(Description).Y - textBoxRect.Height);
            if ( descPosition.Y + value_y >= topY &&
                 descPosition.Y + value_y <= textBoxRect.Top) //BottomY vì nó di chuyển lên trên
            {
                descPosition = new Vector2(descPosition.X, descPosition.Y + value_y);
            }
            else if (Math.Abs(descPosition.Y - topY) < Math.Abs(value_y))
            {
                descPosition = new Vector2(descPosition.X, descPosition.Y - Math.Abs(descPosition.Y - topY));
            }
            else if (Math.Abs(textBoxRect.Top - descPosition.Y) < Math.Abs(value_y))
            {
                descPosition = new Vector2(descPosition.X, descPosition.Y + Math.Abs(textBoxRect.Top - descPosition.Y));
            }
        }

        private void MoveScrollButton(float value_y)
        {
            if (scrollBtn.Position.Y + value_y >= 381 && scrollBtn.Position.Y + value_y <= 381 + scrollBarHeight)
            {
                scrollBtn.Position = new Vector2(scrollBtn.Position.X, scrollBtn.Position.Y + value_y);
            }
            //Nếu mà khoảng cách thay đổi nhỏ hơn cái value
            else if (Math.Abs(scrollBtn.Position.Y - 381) < Math.Abs(value_y))
            {
                scrollBtn.Position = new Vector2(scrollBtn.Position.X, scrollBtn.Position.Y - Math.Abs(scrollBtn.Position.Y - 381));
            }
            else if (Math.Abs((381 + scrollBarHeight) - scrollBtn.Position.Y) < Math.Abs(value_y))
            {
                scrollBtn.Position = new Vector2(scrollBtn.Position.X, scrollBtn.Position.Y + Math.Abs((381 + scrollBarHeight) - scrollBtn.Position.Y));
            }

        }

        private Point oldMousePos;
        private bool isInside = false;
        private void ScrollBtnPress()
        {
            inputController.Begin();
            float nextY = 0;

            if (scrollBtn.Hovered && !isInside)
            {
                isInside = true;
            }

            if (inputController.IsLeftPress() && isInside)
            {
                if (oldMousePos != new Point(0, 0) && oldMousePos != inputController.MousePosition)
                {
                    nextY = inputController.MousePosition.Y - oldMousePos.Y;
                   
                    //OLD
                    //if (nextY >= 381 && nextY <= 381 + scrollBarHeight)
                    //{
                    //    scrollBtn.Position = new Vector2(scrollBtn.Position.X, nextY);

                    //    float ratio = (font.MeasureString(Description).Y - textBoxRect.Height) / scrollBarHeight;
                    //    descPosition = new Vector2(descPosition.X, descPosition.Y + (inputController.MousePosition.Y - oldMousePos.Y) * ratio * (-1));
                    //}

                    MoveScrollButton(nextY);

                    float ratio = (font.MeasureString(Description).Y - textBoxRect.Height) / scrollBarHeight;

                    MoveTextBox((inputController.MousePosition.Y - oldMousePos.Y) * ratio * (-1));
                }
                oldMousePos = inputController.MousePosition;
            }
            else
            {
                oldMousePos = new Point(0, 0);
                isInside = false;
            }

            inputController.End();
        }

        private void MoveTextUp()
        {
            MoveTextBox(10);
            float ratio = scrollBarHeight / (font.MeasureString(Description).Y - textBoxRect.Height);
            MoveScrollButton(-10 * ratio);
        }

        private void MoveTextDown()
        {
            MoveTextBox(-10);
            float ratio = scrollBarHeight / (font.MeasureString(Description).Y - textBoxRect.Height);
            MoveScrollButton(10 * ratio);
        }

        public override void Draw(SpriteBatch _spritebatch)
        {
            base.Draw(_spritebatch);
            this.CardPreview.Draw(_spritebatch);

            if(font.MeasureString(Description).Y > textBoxRect.Height)
            {
                scrollBtn.Draw(_spritebatch);
                upBtn.Draw(_spritebatch);
                downBtn.Draw(_spritebatch);
            }
                

            ///http://gamedev.stackexchange.com/questions/24697/how-to-clip-cut-off-text-in-a-textbox

            //Copy the current scissor rect so we can restore it after
            Rectangle currentRect = _spritebatch.GraphicsDevice.ScissorRectangle;

            //Set the current scissor rectangle
            _spritebatch.GraphicsDevice.ScissorRectangle = textBoxRect;

            //Draw the text in the scissor rectangle
            _spritebatch.DrawString(font, this.Description, descPosition, Color.Black);

            //Reset scissor rectangle to the saved value
            _spritebatch.GraphicsDevice.ScissorRectangle = currentRect;
        }

        public void SetCardPreview(string cardid)
        {
            SpriteID spriteId;
            if (Enum.TryParse("B" + cardid, false, out spriteId))
            {
                this.CardPreview.Texture = SpriteManager.getInstance(contentManager).GetTexture(spriteId);
            }

            this.CardData = Cards.CardProvider.GetInstance().GetCardById(cardid);

            this.Description = CardData != null ? CardData.Description : "";
            this.Description = TextFormater.getIntance().WordWrap(font, this.Description, 170);

            //Reset Position
            descPosition = new Vector2(this.Position.X + 15, this.Position.Y + 375);
            scrollBtn.Position = new Vector2(this.Position.X + 189, this.Position.Y + 381);
        }

        public void SetDefaultCardPreview()
        {
            this.CardPreview.Texture = SpriteManager.getInstance(contentManager).GetTexture(SpriteID.BBackSide);
            this.Description = "";
        }
    }
}
