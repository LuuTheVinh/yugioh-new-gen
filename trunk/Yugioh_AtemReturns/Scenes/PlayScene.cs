using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yugioh_AtemReturns.GameObjects;
using Yugioh_AtemReturns.Duelists;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Yugioh_AtemReturns.Scenes
{
    class PlayScene : Scene
    {

        //MainDeck maindeck;
        private Sprite background;
        //Yugioh_AtemReturns.Cards.Traps.Trap test0;
        private Duelist player;
        private Computer computer;
        public override void Init(Game _game)
        {
            base.Init(_game);
            background = new Sprite(Game.Content, "fie_burn");
            background.Position = new Vector2(230, 0);
            background.Depth = 0.0f;
            player = new Player();
            player.IntiMainDeck(Game.Content);
            computer = new Computer();
            computer.IntiMainDeck(Game.Content);
        } 
        public override void Update(GameTime gametime)
        {
            if (this.Game.IsActive == false)
                return;
            base.Update(gametime);
            this.player.Update(gametime);
            this.computer.Update(gametime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            base.Draw(spriteBatch);
            background.Draw(spriteBatch);

            //test0.Draw(spriteBatch);
            spriteBatch.End();
            player.Draw(spriteBatch);
            computer.Draw(spriteBatch);

        }


    }
}
