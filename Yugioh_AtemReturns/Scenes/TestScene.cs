using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yugioh_AtemReturns.GameObjects;
using Yugioh_AtemReturns.Manager;

namespace Yugioh_AtemReturns.Scenes
{
    class TestScene : Scene
    {
        private Texture2D m_Texture;
        private Vector2 m_Position;
        override public void Init(ContentManager content)
        {
            m_Texture = content.Load<Texture2D>("Cat");
            m_Position = new Vector2(100, 0);
        }
        override public void Update(GameTime gametime)
        {
            m_Position.X++;
            if(gametime.TotalGameTime.TotalMilliseconds >= 5000)
            {
                SceneManager.GetInstance().ReplaceScene(new IntroScene());
            }
        }
        override public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(m_Texture, m_Position, Color.White);
        }
    }
}
