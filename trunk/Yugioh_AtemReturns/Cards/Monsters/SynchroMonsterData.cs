using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yugioh_AtemReturns.Cards.Monsters
{
    class SynchroMonsterData : MonsterCardData
    {
        public SynchroMonsterData(SynchroMonsterData _monster)
            : base(_monster.Id, _monster.Name, _monster.Description, _monster.IsEffect,
            _monster.Attribute.ToString(), _monster.MonsterType.ToString(), _monster.Ability.ToString(), _monster.Atk, _monster.Def, _monster.Level)
        {
            this.CardType = eCardType.SYNCHRO;
        }
        public SynchroMonsterData(string _id, string _name, string _description, bool _isEffect,
            string _attribute, string _monsterType, string _ability, int _atk, int _def, int _level)
            : base(_id, _name, _description, _isEffect, _attribute,_monsterType, _ability, _atk, _def, _level)
        {
            this.CardType = eCardType.SYNCHRO;
            
        }
    }
}
