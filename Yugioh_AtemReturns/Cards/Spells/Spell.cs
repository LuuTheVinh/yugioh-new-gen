using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace Yugioh_AtemReturns.Cards.Spells
{
    class Spell : Card
    {
        #region Field
        private SpellCardData originalData;

        public SpellCardData Original
        {
            get { return originalData; }
            set { originalData = value; }
        }

        #endregion

        public Spell(ContentManager _content, SpriteID _spriteId)
            : base(_content, ID.CARD, _spriteId, eCardType.SPELL)
        {
            this.Original = new SpellCardData((SpellCardData)CardProvider.GetInstance().GetCardById(_spriteId.ToString()));
        }
        public Spell(ContentManager _content, string _cardId)
            : base(_content, ID.CARD, (SpriteID)(Enum.Parse(typeof(SpriteID), "C" + _cardId)), eCardType.TRAP)
        {
            this.Original = new SpellCardData((SpellCardData)CardProvider.GetInstance().GetCardById("C" + _cardId));
        }
    }
}
