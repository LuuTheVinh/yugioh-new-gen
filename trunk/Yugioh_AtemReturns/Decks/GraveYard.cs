using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Yugioh_AtemReturns.Cards;
using Yugioh_AtemReturns.GameObjects;
using Microsoft.Xna.Framework;
using Yugioh_AtemReturns.Cards.Monsters;
using Microsoft.Xna.Framework.Content;
using Yugioh_AtemReturns.Duelists;

namespace Yugioh_AtemReturns.Decks
{
    class GraveYard : Deck
    {
        Num num_sprite;
        public GraveYard(ePlayerId _id)
            : base(_id, eDeckId.GRAVEYARD)
        {
            
        }

        protected override void Init()
        {
            base.Init();

        }
        public void Init(ContentManager _content)
        {
            num_sprite = new Num(_content, this.Position + new Vector2(20, 80), SpriteID.font_68_whitenum);
            this.CardAdded += new CardAddedEventHandler(UpdateNum);
            this.CardRemoved += new CardRemoveEventHandler(UpdateNum);
        }
        
        public override void Update(Microsoft.Xna.Framework.GameTime _gameTime)
        {
            base.Update(_gameTime);
            foreach (var card in ListCard)
            {
                if (card.IsAction == false && card.Status == STATUS.TRIBUTE)
                {
                    //card.STATUS = STATUS.ATK;
                    card.STATUS = STATUS.NORMAL;
                    (card as Monster).BattlePosition = eBattlePosition.ATK;
                    if (card.Origin != Vector2.Zero)
                        card.Origin = Vector2.Zero;
                    card.Rotation = 0.0f;
                    card.Scale = Vector2.One;
                    if (this.PlayerID == ePlayerId.PLAYER)
                    {
                        card.AddMoveTo(new MoveTo(0.7f, GlobalSetting.Default.PlayerGrave - new Vector2((this.Count - 1) / 2)));
                        //card.AddRotateTo(new RotateTo(0.7f, 360));
                    }
                    else
                    {
                        card.AddMoveTo(new MoveTo(0.7f, ComputerSetting.Default.GraveYard - new Vector2((this.Count - 1) / 2)));
                        //card.AddRotateTo(new RotateTo(0.7f, 360));                                        
                    }

                }
            }
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Begin();

            base.Draw(spritebatch);


            spritebatch.End();
            if (num_sprite != null)
                num_sprite.Draw(spritebatch);
        }

        private void UpdateNum(Deck sender, CardEventArgs e)
        {
            this.num_sprite.Show(this.Count, this.Position + new Vector2(22,76));
        }
    }
}
