using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Yugioh_AtemReturns.Cards;
using Yugioh_AtemReturns.GameObjects;
using Microsoft.Xna.Framework;
using Yugioh_AtemReturns.Cards.Monsters;

namespace Yugioh_AtemReturns.Decks
{
    class GraveYard : Deck
    {
        public GraveYard(ePlayerId _id)
            : base(_id, eDeckId.GRAVEYARD)
        {
        }

        protected override void Init()
        {
            base.Init();

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
                    card.AddMoveTo(new MoveTo(1.0f, GlobalSetting.Default.PlayerGrave - new Vector2((this.Count - 1) / 2)));
                    card.AddRotateTo(new RotateTo(1.0f, 360));
                }
            }
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Begin();
            base.Draw(spritebatch);
            spritebatch.End();
        }
    }
}
