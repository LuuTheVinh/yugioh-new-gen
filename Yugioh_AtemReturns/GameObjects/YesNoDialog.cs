﻿using System;
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

        private InputController inputControls;

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
                messageText = TextFormater.getIntance().WordWrap(font, messageText, Sprite.Size.X - 60);
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

        public bool IsShow { get; set; }

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
            messageText = TextFormater.getIntance().WordWrap(font, message, Sprite.Bound.Width - 60);

            Origin = new Vector2(Sprite.Bound.Width / 2, Sprite.Bound.Height / 2);

            float textheight = font.MeasureString(messageText).Y;
            float ratio = (Sprite.Bound.Height + textheight) / Sprite.Bound.Height;
            
            Sprite.Scale = new Vector2(1, ratio);

            oldMousePos = new Point(0, 0);

            IsShow = false;

            inputControls = new InputController();
            
        }

        public override void Update(GameTime gameTime)
        {
            if (this.IsShow == false)
                return; //
            base.Update(gameTime);
            UpdateChangedValues();
            yesButton.Update(gameTime);
            noButton.Update(gameTime);

            CheckMouseUpdate();
        }

        public override void Draw(SpriteBatch _spritebatch)
        {
            if(IsShow)
            {

            base.Draw(_spritebatch);

            Sprite.Draw(_spritebatch);
            yesButton.Draw(_spritebatch);
            noButton.Draw(_spritebatch);

            _spritebatch.DrawString(font, messageText, messagePosition, Color.White);

            }
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
            Hide();
        }

        private void NoBtn_Clicked()
        {
            resultChoice = false;
            Hide();
        }

        public void Show()
        {
            IsShow = true;
        }
        public void Show(string _message)
        {
            IsShow = true;
            this.MessageText = _message;
        }
        public void Hide()
        {
            IsShow = false;
        }

        //MOUSE UPDATE
        bool isInside = false;
        private void CheckMouseUpdate()
        {
            inputControls.Begin();

            if (Sprite.Bound.Contains(inputControls.MousePosition) && !isInside)
            {
                isInside = true;
            }

            if (inputControls.IsLeftPress() && isInside)
            {
                if (oldMousePos != new Point(0, 0) && oldMousePos != inputControls.MousePosition)
                {
                    var dir = new Point(inputControls.MousePosition.X - oldMousePos.X, inputControls.MousePosition.Y - oldMousePos.Y);
                    Position = new Vector2(Position.X + dir.X, Position.Y + dir.Y);
                }
                oldMousePos = inputControls.MousePosition;
            }
            else
            {
                oldMousePos = new Point(0, 0);
                isInside = false;
            }

            inputControls.End();
        }
    }
}
