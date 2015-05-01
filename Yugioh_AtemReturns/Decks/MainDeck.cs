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
using Yugioh_AtemReturns.Duelists;

namespace Yugioh_AtemReturns.Decks
{

    class MainDeck : Deck
    {
        private Sprite m_sprite;
        private Num num_sprite;

        private Sprite m_backside;

        public Sprite Backside
        {
            get { return m_backside; }
            set { m_backside = value; }
        }
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

        public void Init(ContentManager _content, string _deckId)
        {
            this.ListCard = LoadDeck.GetInstance().GetDeck(_content, _deckId);
            foreach (var card in ListCard)
            {
                card.Position = this.Position;
                card.IsFaceUp = false;
            }
            this.Shuffle();
            num_sprite = new Num(_content, this.Position + new Vector2(20, 80), SpriteID.font_68_whitenum);
            this.CardAdded += new CardAddedEventHandler(UpdateNum);
            this.CardRemoved += new CardRemoveEventHandler(UpdateNum);
            //if (ListCard.Count != 0)
            //    this.Sprite = ListCard.First.Value.s_BackSide;
            this.Sprite = new Sprite(SpriteManager.getInstance(_content).GetSprite(SpriteID.CBackSide));
            this.Backside = new Sprite(this.Sprite);
            Backside.Origin = new Vector2(Backside.Size.X / 2, 0);
        }


        public override void Update(GameTime _gameTime)
        {
            base.Update(_gameTime);
            this.Backside.Update(_gameTime);
        }
        public override void Draw(SpriteBatch spritebatch)
        {
            // spritebatch.End();
            
            spritebatch.Begin();
            if (ListCard.Count != 0)
            {
                //if (Sprite != null)
                //    this.Sprite = ListCard.First.Value.s_BackSide;
                for (int i = 0; i <= Math.Min(ListCard.Count / 4, 10); i++)
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
            if (Backside.IsAction == true)
                Backside.Draw(spritebatch);
            //spritebatch.DrawString(m_font, Convert.ToString(ListCard.Count), Sprite.Position, Color.White);
            spritebatch.End();
            num_sprite.Draw(spritebatch);
            
        }

        public void DrawCard()
        {
            if (this.Count == 0)
                return;
            this.Backside.Position = this.Position + Backside.Origin;
            this.Backside.Scale = Vector2.One;
            if (this.PlayerID == ePlayerId.PLAYER)
            {
                this.Backside.AddMoveTo(new MoveTo(0.15f, new Vector2(
                    this.Position.X,
                    GlobalSetting.Default.PlayerHand.Y)));
            }
            else
            {
                this.Backside.AddMoveTo(new MoveTo(0.15f, new Vector2(
                    this.Position.X,
                    ComputerSetting.Default.Hand.Y)));
            }
            Backside.AddScaleTo(new ScaleTo(0.20f, new Vector2(0.15f, 1.2f)));
            this.MoveTopToDeck(eDeckId.HAND);
        }
        private void UpdateNum(Deck sender, CardEventArgs e)
        {
            this.num_sprite.Show(this.Count, this.Position + new Vector2(20, 76));
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
