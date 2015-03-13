using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace Yugioh_AtemReturns.Cards.Monsters
{
    class FusionMonster : Monster
    {

        public FusionMonster(ContentManager _content, SpriteID _spriteId)
            : base(_content, _spriteId, eCardType.FUSION)
        {
        }
        public FusionMonster(ContentManager _content, string _cardId)
            : base(_content, _cardId)
        {

        }
    }
}
