using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yugioh_AtemReturns.Cards.Traps
{
    public enum eTrapType
    {
        NORMAL,
        CONTINUOUS,
        COUNTER,
    }

    class TrapCardData : CardData
    {
        #region Field
        private eTrapType trapType;
        private int spellSpeed;
        #endregion

        #region Property
        public eTrapType TrapType
        {
            get { return trapType; }
            set {
                switch (value)
                {
                    case eTrapType.NORMAL:
                        spellSpeed = 2;
                        break;
                    case eTrapType.CONTINUOUS:
                        spellSpeed = 2;
                        break;
                    case eTrapType.COUNTER:
                        spellSpeed = 3;
                        break;
                    default:
                        break;
                }
                trapType = value; 
            }
        }

        public int SpellSpeed
        {
            get { return spellSpeed; }
            private set { spellSpeed = value; }
        }
        #endregion

        public TrapCardData(TrapCardData _trapCardData)
            : base(_trapCardData.Id, _trapCardData.Name, _trapCardData.Description, eCardType.TRAP)
        {
            this.TrapType = _trapCardData.TrapType;
        }

        public TrapCardData(string _id, string _name, string _description, string _trapType)
            : base (_id,_name, _description,eCardType.TRAP)
        {
            this.TrapType = getAbility( _trapType);
        }


        #region PrivateMethod
        private eTrapType getAbility(string _sTrapType)
        {
            try
            {
                return (eTrapType)Enum.Parse(typeof(eTrapType), _sTrapType);
            }
            catch
            {
                throw new Exception(_sTrapType + "is not monster trap type format. check enum type eTrapType");
            }
        }
        #endregion
    }
}
