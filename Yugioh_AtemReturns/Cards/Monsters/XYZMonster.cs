using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace Yugioh_AtemReturns.Cards.Monsters
{
    class XYZMonster : Monster
    {
        List<Monster> meterial;

        public XYZMonster(ContentManager _content, SpriteID _spriteId)
            :base (_content, _spriteId, eCardType.XYZ)
        {
            meterial = new List<Monster>(5);
        }
        public XYZMonster(ContentManager _content, string _cardId)
            : base(_content, _cardId)
        {
            meterial = new List<Monster>(5);

        }
            
    }
}
