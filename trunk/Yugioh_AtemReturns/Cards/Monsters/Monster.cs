using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace Yugioh_AtemReturns.Cards.Monsters
{
    class Monster :Card
    {

        #region Field
        private MonsterCardData originalData;
        private bool isEffect;
        private eAttribute attribute;
        private eMonsterType monsterType;
        private int level;
        private int atk;
        private int def;
        private int rank;
        private int pendulumScale;
        private int spellSpeed;
        #endregion

        #region Property

        public MonsterCardData Original
        {
            get { return originalData; }
            set { originalData = value; }
        }
        public bool IsEffect
        {
            get { return isEffect; }
            set
            {
                if (value == false)
                    spellSpeed = 0;
                isEffect = value;
            }
        }
        public eAttribute Attribute
        {
            get { return attribute; }
            set { attribute = value; }
        }
        public eMonsterType MonsterType
        {
            get { return monsterType; }
            set { monsterType = value; }
        }
        public int Level
        {
            get { return level; }
            set
            {
                if (value < 0 || value > 12)
                    throw new Exception("Level Wrong");
                else
                {
                    level = value;
                    if (value != 0)
                        rank = 0;
                }
            }
        }
        public int Atk
        {
            get { return atk; }
            set
            {
                if (value < 0)
                    throw new Exception("ATK Wrong");
                else
                    atk = value;
            }
        }
        public int Def
        {
            get { return def; }
            set
            {
                if (value < 0)
                    throw new Exception("DEF Wrong");
                else
                    def = value;
            }
        }
        public int Rank
        {
            get { return rank; }
            set
            {
                if (value < 0 || value > 12)
                    throw new Exception("Rank wrong");
                else
                {
                    rank = value;
                    if (rank != 0)
                        level = 0;
                }
            }
        }
        public int PendulumScale
        {
            get { return pendulumScale; }
            set { pendulumScale = value; }
        }
        public int SpellSpeed
        {
            get { return spellSpeed; }
            set { spellSpeed = value; }
        }
        #endregion
        
        public Monster(ContentManager _content, SpriteID _spriteId, eCardType _cardType = eCardType.MONSTER)//cái tham số mặc nhiên để tạm
            : base(_content, ID.CARD, _spriteId, _cardType)
        {
            this.Original = new MonsterCardData((MonsterCardData)CardProvider.GetInstance().GetCardById(_spriteId.ToString()));
            this.IsEffect = Original.IsEffect;
            this.Attribute = Original.Attribute;
            this.MonsterType = Original.MonsterType;
            this.Level = Original.Level;
            this.Atk = Original.Atk;
            this.Def = Original.Def;
            this.Rank = Original.Rank;
            this.PendulumScale = Original.PendulumScale;
            this.SpellSpeed = Original.SpellSpeed;
        }
        public Monster(ContentManager _content, string _cardId)
            : base(_content, ID.CARD, (SpriteID)(Enum.Parse(typeof(SpriteID),"C"+ _cardId)), eCardType.MONSTER)
        {
            this.Original = new MonsterCardData((MonsterCardData)CardProvider.GetInstance().GetCardById("C" + _cardId));
            this.IsEffect = Original.IsEffect;
            this.Attribute = Original.Attribute;
            this.MonsterType = Original.MonsterType;
            this.Level = Original.Level;
            this.Atk = Original.Atk;
            this.Def = Original.Def;
            this.Rank = Original.Rank;
            this.PendulumScale = Original.PendulumScale;
            this.SpellSpeed = Original.SpellSpeed;
        }
    }
}
