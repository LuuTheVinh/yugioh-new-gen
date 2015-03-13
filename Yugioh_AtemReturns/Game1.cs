using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Yugioh_AtemReturns.Cards;
using Yugioh_AtemReturns.Cards.Monsters;
using Yugioh_AtemReturns.GameObjects;
using Yugioh_AtemReturns.Manager;
using Yugioh_AtemReturns.Scenes;
using Yugioh_AtemReturns.Cards.Traps;
using Yugioh_AtemReturns.Cards.Spells;
using Yugioh_AtemReturns.Decks;
using Yugioh_AtemReturns.Duelists;

namespace Yugioh_AtemReturns
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Scene introScene;


        //MainDeck maindeck;
        Sprite background;
        //Yugioh_AtemReturns.Cards.Traps.Trap test0;
        Duelist player;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 800;
            this.IsMouseVisible = true;
            Content.RootDirectory = "Content";

            //graphics.ToggleFullScreen();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

            /////////
            introScene = new IntroScene();

            SceneManager.getInstance().addScene(introScene);
            SceneManager.getInstance().Init();

            //maindeck = new MainDeck(ePlayerId.PLAYER);
            //maindeck.CreateDeck(this.Content, "1");
            //maindeck.Position = new Vector2(0, 300);

            background = new Sprite(this.Content, "fie_burn");
            background.Position = new Vector2(0, 0);
            background.Depth = 0.0f;
            player = new Player();
            player.IntiMainDeck(this.Content);
            //maindeck.Position = background.Position + GlobalSetting.Default.PlayerMain;
            //test0 = new Cards.Traps.Trap(this.Content, SpriteID.C3005);
            //test0.Postion = GlobalSetting.Default.PlayerSpellF + new Microsoft.Xna.Framework.Vector2(15, 8) + background.Position;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            SceneManager.getInstance().Content = Content;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            SceneManager.getInstance().Update(gameTime);
            this.player.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            SceneManager.getInstance().Render(spriteBatch);

            background.Draw(spriteBatch);
            //test0.Draw(spriteBatch);
            spriteBatch.End();

            player.Draw(spriteBatch);
            //maindeck.Draw(spriteBatch);

            base.Draw(gameTime);
        }
    }
}
