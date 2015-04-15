using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Yugioh_AtemReturns.GameObjects
{
    public class Scene
    {
        protected bool isPaused = false;
        protected Game Game;

        virtual public void Init(Game game)
        {
            this.Game = game;
            //this.Game.Window.Title = "Yugioh!!! Atem Returns";
        }
        virtual public void Update(GameTime gametime)
        {

        }
        virtual public void Pause()
        { }
        virtual public void Resume()
        { }
        virtual public void Draw(SpriteBatch spriteBatch)
        { }

    }
}
