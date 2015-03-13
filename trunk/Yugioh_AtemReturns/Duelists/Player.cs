using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yugioh_AtemReturns.Decks;

namespace Yugioh_AtemReturns.Duelists
{
    class Player :Duelist
    {

        public Player()
            : base(ePlayerId.PLAYER)
        { }

        public override void Init()
        {
            base.Init();
            this.MainDeck.Position = GlobalSetting.Default.PlayerMain;
            this.GraveYard.Position = GlobalSetting.Default.PlayerGrave;
            this.MonsterField.Position = GlobalSetting.Default.PlayerMonF;
            this.SpellField.Position = GlobalSetting.Default.PlayerSpellF;
            this.Hand.Position = GlobalSetting.Default.PlayerHand;
        }
        public override void IntiMainDeck(Microsoft.Xna.Framework.Content.ContentManager _content)
        {
            base.IntiMainDeck(_content);
            this.MainDeck.CreateDeck(_content, "1");
        }
        public override void Update(Microsoft.Xna.Framework.GameTime gametime)
        {
            base.Update(gametime);
            MainDeck.DrawCard();
        }
        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch _spriteBatch)
        {
            base.Draw(_spriteBatch);
        }

    }
}
