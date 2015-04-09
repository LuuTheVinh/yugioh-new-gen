using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yugioh_AtemReturns.GameObjects;
using Microsoft.Xna.Framework.Content;
using Yugioh_AtemReturns.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Yugioh_AtemReturns.Database;

namespace Yugioh_AtemReturns.Decks
{

    class MainDeck : Deck
    {
        private Sprite m_sprite;
        //private SpriteFont m_font;

        public Sprite Sprite
        {
            get { return m_sprite; }
            set { m_sprite = value; }
        }

        public MainDeck( ePlayerId _id)
            : base(_id, eDeckId.MAINDECK)
        {
            //this.m_font = FontManager.GetInstance(_game).GetFont(FontManager.FONT.MYFONT);
            //this.Sprite.Scale = GlobalSetting.MainDeckScale;
         

        }

        sealed protected override void Init()
        {
            base.Init();

        }

        public void CreateDeck(ContentManager _content, string _deckId)
        {
            this.ListCard = LoadDeck.GetInstance().GetDeck(_content, _deckId);
            this.Shuffle();
            if (ListCard.Count != 0)
                this.Sprite = ListCard.First.Value.s_BackSide;
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            // spritebatch.End();
            
            spritebatch.Begin();
            if (ListCard.Count != 0)
            {
                if (Sprite != null)
                    this.Sprite = ListCard.First.Value.s_BackSide;
                for (int i = 0; i <= Math.Min(ListCard.Count / 2, 20); i++)
                {
                    spritebatch.Draw(
                        this.Sprite.Texture,
                        this.Position - new Vector2(i * 1, i * 1),
                        null,
                        Color.White,
                        Sprite.Rotation,
                        Sprite.Origin, Sprite.Scale,
                        SpriteEffects.None, Sprite.Depth);
                }
            }
            //spritebatch.DrawString(m_font, Convert.ToString(ListCard.Count), Sprite.Position, Color.White);
            spritebatch.End();
        }

        public void DrawCard()
        {
            this.MoveTopToDeck(eDeckId.HAND);
        }


        // For Player
        //public void Update(KeyboardState _keyboardState)
        //{
        //    if (_keyboardState.IsKeyDown(Keys.D))
        //    {
        //        this.MoveToDeck(DECKID.HAND);
        //    }
        //}

        // For AI
        //public void Update()
        //{
        //    this.MoveToDeck(DECKID.HAND);
        //}



        //protected override void PlayerUpdate(Duel.Player _player)
        //{
        //    throw new NotImplementedException();
        //}
        //protected override void AIUpdate(Duel.Computer _com)
        //{
        //    throw new NotImplementedException();
        //}

    }
}
