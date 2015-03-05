using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Yugioh_AtemReturns.GameObjects;
using Yugioh_AtemReturns.Manager;

namespace Yugioh_AtemReturns.Scenes
{
    public class IntroScene : Scene
    {
        private Texture2D m_Texture;
        private Button testButton;
        private Point visibleSize;
        
        override public void Init(ContentManager content)
        {
            visibleSize = new Point(GraphicsDeviceManager.DefaultBackBufferWidth, GraphicsDeviceManager.DefaultBackBufferHeight);
            testButton = new Button(new Sprite(content, "normal"), new Sprite(content, "hover"));
            testButton.Position = new Vector2(visibleSize.X / 2, visibleSize.Y / 2);
            
            testButton.ButtonEvent += new Action(doSomething);
        }
        override public void Update(GameTime gametime)
        {
            testButton.Update(gametime);
            testButton.Rotation += 0.1f;
        }
        override public void Render(SpriteBatch spriteBatch)
        {
            testButton.Draw(spriteBatch);
            //spriteBatch.Draw(m_Texture, new Vector2(100, 0), Color.White);
        }

        private void doSomething()
        {
            Debug.WriteLine("OK! I'm in.");
        }
    }
}
