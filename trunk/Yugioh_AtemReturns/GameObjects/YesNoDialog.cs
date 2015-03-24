using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Yugioh_AtemReturns.Manager;

namespace Yugioh_AtemReturns.GameObjects
{
    class YesNoDialog : MyObject
    {
        private Button yesButton, noButton;
        private bool resultChoice;
        private string messageText;
        private SpriteFont font;
        private Vector2 messagePosition;

        private Point oldMousePos;

        #region Properties
        public bool Result
        {
            get { return resultChoice; }
        }

        public string MessageText
        {
            get { return messageText; }
            set
            {
                messageText = value;
                messageText = WrapText(font, messageText, Sprite.Size.X - 60);
            }
        }

        public override Vector2 Position
        {
            get { return base.Position; }
            set
            {
                base.Position = value;
                UpdateChangedValues();
            }
        }

        public override Vector2 Origin
        {
            get { return base.Origin; }
            set
            {
                base.Origin = value;
                UpdateChangedValues();
            }
        }

        public override Vector2 Scale
        {
            get { return base.Scale; }
            set
            {
                base.Scale = value;
                UpdateChangedValues();
            }
        }

        #endregion

        public YesNoDialog(ContentManager content, string message)
        {
            Sprite = new Sprite(content, "General\\Dialog_BG");
            
            yesButton = new Button(new Sprite(content, "General\\YesBtn_Normal"), new Sprite(content, "General\\YesBtn_Hover"));
            yesButton.Position = new Vector2(Sprite.Position.X + Sprite.Bound.Width / 2 - yesButton.Sprite.Bound.Width - 30,
                                             Sprite.Position.Y + Sprite.Bound.Height - 45
                                     );
            yesButton.ButtonEvent += YesBtn_Clicked;

            noButton = new Button(new Sprite(content, "General\\NoBtn_Normal"), new Sprite(content, "General\\NoBtn_Hover"));
            noButton.Position = new Vector2(Sprite.Position.X + Sprite.Bound.Width / 2 + 30,
                                             Sprite.Position.Y + Sprite.Bound.Height - 45
                                     );
            noButton.ButtonEvent += NoBtn_Clicked;

            font = content.Load<SpriteFont>("ArialBold");
            messagePosition = new Vector2(Sprite.Position.X, Sprite.Position.Y);
            messageText = WrapText(font, message, Sprite.Bound.Width - 60);

            Origin = new Vector2(Sprite.Bound.Width / 2, Sprite.Bound.Height / 2);

            float textheight = font.MeasureString(messageText).Y;
            float ratio = (Sprite.Bound.Height + textheight) / Sprite.Bound.Height;
            
            Sprite.Scale = new Vector2(1, ratio);

            oldMousePos = new Point(0, 0);
            
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            UpdateChangedValues();
            yesButton.Update(gameTime);
            noButton.Update(gameTime);

            CheckMouseUpdate();
        }

        public override void Draw(SpriteBatch _spritebatch)
        {
            base.Draw(_spritebatch);

            Sprite.Draw(_spritebatch);
            yesButton.Draw(_spritebatch);
            noButton.Draw(_spritebatch);

            _spritebatch.DrawString(font, messageText, messagePosition, Color.White);
            
        }

        private void UpdateChangedValues()
        {
            if (noButton != null && Sprite != null)
            {
                var nobtnpos = new Vector2(Sprite.Position.X + 30 * Scale.X,
                                           Sprite.Position.Y + Sprite.Bound.Height / 2 - 45
                                   );
                if (noButton.Position != nobtnpos)
                {
                    noButton.Position = nobtnpos;
                }
            }

            if (yesButton != null && Sprite != null)
            {
                var yesbtnpos = new Vector2(Sprite.Position.X - yesButton.Sprite.Bound.Width - 30 * Scale.X,
                                            Sprite.Position.Y + Sprite.Bound.Height / 2 - 45
                                    );
                if (yesButton.Position != yesbtnpos)
                {
                    yesButton.Position = yesbtnpos;

                }
            }

            var messpos = new Vector2(Sprite.Position.X - Sprite.Bound.Width / 2 + 30 * Scale.X, Sprite.Position.Y - Sprite.Bound.Height / 2 + 25 * Scale.Y);
            if (messagePosition != messpos)
                messagePosition = messpos;
        }

        private void YesBtn_Clicked()
        {
            resultChoice = true;
        }

        private void NoBtn_Clicked()
        {
            resultChoice = false;
        }

        //MOUSE UPDATE
        bool isInside = false;
        private void CheckMouseUpdate()
        {
            InputController.getInstance().Begin();

            if (Sprite.Bound.Contains(InputController.getInstance().MousePosition) && !isInside)
            {
                isInside = true;
            }

            if (InputController.getInstance().IsLeftPress() && isInside)
            {
                if (oldMousePos != new Point(0, 0) && oldMousePos != InputController.getInstance().MousePosition)
                {
                    var dir = new Point(InputController.getInstance().MousePosition.X - oldMousePos.X, InputController.getInstance().MousePosition.Y - oldMousePos.Y);
                    Position = new Vector2(Position.X + dir.X, Position.Y + dir.Y);
                }
                oldMousePos = InputController.getInstance().MousePosition;
            }
            else
            {
                oldMousePos = new Point(0, 0);
                isInside = false;
            }

            InputController.getInstance().End();
        }

        //WRAP TEXT
        public string WrapText(SpriteFont spritefont, string text, float maxWidth)
        {
            string[] words = text.Split(' ');

            StringBuilder stringbuilder = new StringBuilder();
            float linewidth = 0.0f;
            float spacewidth = spritefont.MeasureString(" ").X;

            foreach (var word in words)
            {
                Vector2 size = spritefont.MeasureString(word);

                if (linewidth + size.X < maxWidth)
                {
                    stringbuilder.Append(word + " ");
                    linewidth += size.X + spacewidth;
                }
                else
                {
                    stringbuilder.Append("\n" + word + " ");
                    linewidth = size.X + spacewidth;
                }
            }

            return stringbuilder.ToString();
        }
    }
}
