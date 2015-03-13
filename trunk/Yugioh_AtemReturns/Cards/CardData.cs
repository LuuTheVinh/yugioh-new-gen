using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yugioh_AtemReturns.Cards
{
    public enum eCardType { MONSTER, TRAP, SPELL, XYZ, SYNCHRO, FUSION };
    class CardData
    {
        private String m_id;
        private eCardType m_cardType;
        private String m_Name;
        private String m_Description;

        #region Property
        public String Id
        {
            get
            {
                return m_id;
            }
            set
            { m_id = value; }
        }
        public String Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }
        public String Description
        {
            get { return m_Description; }
            set { m_Description = value; }
        }
        public eCardType CardType
        {
            get { return m_cardType; }
            set { m_cardType = value; }
        }
        #endregion

        //public CardData()
        //{
        //    m_Name = String.Empty;
        //    m_Description = String.Empty;
        //}
        public CardData(CardData _cardData)
        {
            this.Id = _cardData.Id;
            this.CardType = _cardData.CardType;
            this.Name = _cardData.Name;
            this.Description = _cardData.Description;
        }

        public CardData(string _id, string _name, string _description, string _cardType)
        {
            Id = _id;
            CardType = (eCardType)Enum.Parse(typeof(eCardType), _cardType);
            Name = _name;
            Description = _description;
        }
        public CardData(string _id, string _name, string _description, eCardType _cardType)
        {
            Id = _id;
            CardType = _cardType;
            Name = _name;
            Description = _description;
        }
    }
}
