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
        private Sprite mySprite;
        //private Button testButton;
        //private Point visibleSize;
        //override public void Init(ContentManager content)
        //{
           
        //    visibleSize = new Point(GraphicsDeviceManager.DefaultBackBufferWidth, GraphicsDeviceManager.DefaultBackBufferHeight);
        //    testButton = new Button(new Sprite(content, "normal"), new Sprite(content, "hover"), new Sprite(content, "normal"));
        //    testButton.Position = new Vector2(visibleSize.X / 2, visibleSize.Y / 2);
        //    testButton.Origin = new Vector2(testButton.Sprite.Size.X / 2, testButton.Sprite.Size.Y / 2);
        //    testButton.ButtonEvent += new Action(DoSomething);

        //    mySprite = new Sprite(content, "Cat");
        //    mySprite.Position = new Vector2(400, visibleSize.Y * 2);
        //    mySprite.Origin = new Vector2(mySprite.Size.X / 2, mySprite.Size.Y / 2);
        //    mySprite.Scale = new Vector2(0.2f, 0.2f);
        //}
        //override public void Update(GameTime gametime)
        //{
        //    testButton.Update(gametime);

        //    if (testButton.Selected)
        //    {
        //        mySprite.MoveTo(gametime, 0.5f, new Vector2(400, 100));
        //        mySprite.ScaleTo(gametime, 0.5f, new Vector2(0.5f, 0.5f));
        //        mySprite.RotateTo(gametime, 1.0f, MathHelper.Pi * 4);

        //        testButton.RotateTo(gametime, 1.0f, MathHelper.Pi * 5);
        //    }
        //}
        //override public void Draw(SpriteBatch spriteBatch)
        //{
        //    spriteBatch.Begin();

        //    testButton.Draw(spriteBatch);
        //    mySprite.Draw(spriteBatch);

        //    spriteBatch.End();
        //}

        //private void DoSomething()
        //{
        //    Debug.WriteLine("OK! I'm in.");

        //}

    }
}
