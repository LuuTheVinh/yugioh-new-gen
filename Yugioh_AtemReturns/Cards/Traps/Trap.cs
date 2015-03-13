using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace Yugioh_AtemReturns.Cards.Traps
{

    class Trap : Card
    {
        #region Field
        private TrapCardData originalData;

        public TrapCardData Original
        {
            get { return originalData; }
            set { originalData = value; }
        }

        #endregion

        public Trap(ContentManager _content, SpriteID _spriteId)
            : base(_content, ID.CARD, _spriteId, eCardType.TRAP)
        {
            this.Original = new TrapCardData((TrapCardData)CardProvider.GetInstance().GetCardById(_spriteId.ToString()));
        }
        public Trap(ContentManager _content, string _cardId)
            : base(_content, ID.CARD, (SpriteID) (Enum.Parse(typeof(SpriteID),"C"+_cardId)), eCardType.TRAP)
        {
            this.Original = new TrapCardData((TrapCardData)CardProvider.GetInstance().GetCardById("C" + _cardId));
        }
    }
}
