using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yugioh_AtemReturns.Cards.Spells
{
    public enum eSpellType
    {
        NORMAL,
        CONTINUOUS,
        EQUIP,
        QUICKPLAY,
        FIELD,
        RITUAL,
    }

    class SpellCardData : CardData
    {
        #region Field
        private eSpellType spellType;
        private int spellSpeed;
        #endregion

        #region Property
        public eSpellType SpellType
        {
            get { return spellType; }
            set
            {
                switch (value)
                {
                    case eSpellType.NORMAL:
                        spellSpeed = 1;
                        break;
                    case eSpellType.CONTINUOUS:
                        spellSpeed = 1;
                        break;
                    case eSpellType.EQUIP:
                        spellSpeed = 1;
                        break;
                    case eSpellType.QUICKPLAY:
                        spellSpeed = 2;
                        break;
                    case eSpellType.FIELD:
                        spellSpeed = 1;
                        break;
                    case eSpellType.RITUAL:
                        spellSpeed = 1;
                        break;
                    default:
                        break;
                }
                spellType = value;
            }
        }

        public int SpellSpeed
        {
            get { return spellSpeed; }
            private set { spellSpeed = value; }
        }
        #endregion

        public SpellCardData(SpellCardData _spellCardData)
            : base(_spellCardData.Id, _spellCardData.Name, _spellCardData.Description, eCardType.SPELL)
        {
            this.SpellType = _spellCardData.SpellType;
        }

        public SpellCardData(string _id, string _name, string _description, string _trapType)
            : base (_id,_name, _description,eCardType.SPELL)
        {
            this.SpellType = getAbility( _trapType);
        }


        #region PrivateMethod
        private eSpellType getAbility(string _sSpellType)
        {
            try
            {
                return (eSpellType)Enum.Parse(typeof(eSpellType), _sSpellType);
            }
            catch
            {
                throw new Exception(_sSpellType + "is not monster spell type format. check enum type eSpellType");
            }
        }
        #endregion
    }
}
