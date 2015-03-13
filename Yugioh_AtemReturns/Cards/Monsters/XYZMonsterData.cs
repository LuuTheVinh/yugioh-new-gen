using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yugioh_AtemReturns.Cards.Monsters
{
    class XYZMonsterData : MonsterCardData
    {
        public XYZMonsterData(XYZMonsterData _monster)
            : base(_monster.Id, _monster.Name, _monster.Description, _monster.IsEffect,
            _monster.Attribute.ToString(), _monster.MonsterType.ToString(), _monster.Ability.ToString(), _monster.Atk, _monster.Def, _rank: _monster.Rank)
        {
            this.CardType = eCardType.XYZ;
        }
        public XYZMonsterData(string _id, string _name, string _description, bool _isEffect,
            string _attribute, string _monsterType, string _ability, int _atk, int _def, int _rank)
            : base(_id, _name, _description, _isEffect, _attribute,_monsterType, _ability, _atk, _def, -1,_rank)
        {
            this.CardType = eCardType.XYZ;
        }
    }
}
