using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yugioh_AtemReturns.GameObjects
{
    public class Scene
    {
        private bool isPaused = false;
        virtual public void Init(ContentManager content)
        { }
        virtual public void Update(GameTime gametime)
        { }
        virtual public void Pause()
        { }
        virtual public void Resume()
        { }
        virtual public void Draw(SpriteBatch spriteBatch)
        { }
    }
}
