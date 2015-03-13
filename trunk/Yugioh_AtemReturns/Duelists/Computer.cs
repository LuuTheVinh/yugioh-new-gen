using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yugioh_AtemReturns.Decks;

namespace Yugioh_AtemReturns.Duelists
{
    class Computer : Duelist
    {
        public Computer() : base(ePlayerId.COMPUTER) { }
        public override void Init()
        {
            base.Init();

        }
        public override void IntiMainDeck(Microsoft.Xna.Framework.Content.ContentManager _content)
        {
            base.IntiMainDeck(_content);

        }
        public override void Update(Microsoft.Xna.Framework.GameTime gametime)
        {
            base.Update(gametime);

        }
        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch _spriteBatch)
        {
            base.Draw(_spriteBatch);
        }
    }
}
