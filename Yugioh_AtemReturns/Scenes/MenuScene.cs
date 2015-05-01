using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Yugioh_AtemReturns.GameObjects;
using Yugioh_AtemReturns.Manager;
using Button = Yugioh_AtemReturns.GameObjects.Button;
using Yugioh_AtemReturns.Duelists;

namespace Yugioh_AtemReturns.Scenes
{
    class MenuScene : Scene
    {
        private Button duleModeBtn, cardListBtn, deckEditorBtn, optionBtn, quitBtn;
        private Sprite menuBackground;
        private Point offsetButton;
        public override void Init(Game _game)
        {
            base.Init(_game);

            offsetButton = new Point(10, 0);

            //Background
            menuBackground = new Sprite(Game.Content, "Menu\\MenuBackground");

            //Button
            duleModeBtn = new Button(new Sprite(Game.Content, "Menu\\Button\\DuelMode_Normal"), new Sprite(Game.Content, "Menu\\Button\\DuelMode_Hover"));
            //duleModeBtn.Position = new Vector2(0, 0);
            duleModeBtn.Position = Vector2.Zero;
            duleModeBtn.ButtonEvent += gotoDuelScene;

            cardListBtn = new Button(new Sprite(Game.Content, "Menu\\Button\\CardList_Normal"), new Sprite(Game.Content, "Menu\\Button\\CardList_Hover"));
            cardListBtn.Position = new Vector2(duleModeBtn.Position.X + duleModeBtn.Sprite.Size.X + offsetButton.X, duleModeBtn.Position.Y);

            deckEditorBtn = new Button(new Sprite(Game.Content, "Menu\\Button\\DeckEditor_Normal"), new Sprite(Game.Content, "Menu\\Button\\DeckEditor_Hover"));
            deckEditorBtn.Position = new Vector2(cardListBtn.Position.X + cardListBtn.Sprite.Size.X + offsetButton.X, + cardListBtn.Position.Y);

            optionBtn = new Button(new Sprite(Game.Content, "Menu\\Button\\Option_Normal"), new Sprite(Game.Content, "Menu\\Button\\Option_Hover"));
            optionBtn.Position = new Vector2(deckEditorBtn.Position.X + deckEditorBtn.Sprite.Size.X + offsetButton.X, deckEditorBtn.Position.Y);

            quitBtn = new Button(new Sprite(Game.Content, "Menu\\Button\\Quit_Normal"), new Sprite(Game.Content, "Menu\\Button\\Quit_Hover"));
            quitBtn.Position = new Vector2(optionBtn.Position.X + optionBtn.Sprite.Size.X + offsetButton.X, optionBtn.Position.Y);
            quitBtn.ButtonEvent += new Action(quitButton);
            menuBackground.Position = new Vector2(0, 0);

        }

        public override void Update(GameTime gametime)
        {
            if (this.Game.IsActive == false)
                return; 
            base.Update(gametime);

            //Update Buttons
            duleModeBtn.Update(gametime);
            cardListBtn.Update(gametime);
            deckEditorBtn.Update(gametime);
            optionBtn.Update(gametime);
            quitBtn.Update(gametime);

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.Begin();
            //Draw Background
            menuBackground.Draw(spriteBatch);
            
            //Draw Button
            duleModeBtn.Draw(spriteBatch);
            cardListBtn.Draw(spriteBatch);
            deckEditorBtn.Draw(spriteBatch);
            optionBtn.Draw(spriteBatch);
            quitBtn.Draw(spriteBatch);
            
            spriteBatch.End();
        }

        private void gotoDuelScene()
        {
            var playScene = new PlayScene(ePlayerId.PLAYER);
            SceneManager.GetInstance().ReplaceScene(playScene);

        }

        private void quitButton()
        {
            this.Game.Exit();
        }
    }
}
