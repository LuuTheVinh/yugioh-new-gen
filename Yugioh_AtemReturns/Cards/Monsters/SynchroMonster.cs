using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace Yugioh_AtemReturns.Cards.Monsters
{
    class SynchroMonster : Monster
    {
          public SynchroMonster(ContentManager _content, SpriteID _spriteId)
            :base (_content, _spriteId, eCardType.SYNCHRO)
        {
        }
          public SynchroMonster(ContentManager _content, string _cardId)
            : base(_content, _cardId)
        {

        }  }
}
