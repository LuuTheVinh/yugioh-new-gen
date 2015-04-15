using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Yugioh_AtemReturns.GameObjects;

namespace Yugioh_AtemReturns.Cards.Monsters
{
    enum eBattlePosition
    {
        ATK, DEF
    }
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
        private eBattlePosition battlePosition;
        private bool m_CanAtk;
        #endregion

        #region Property
        public bool SwitchBattlePosition { get; set; }
        public bool CanATK
        {
            get
            {
                return m_CanAtk;
            }
            set
            {
                m_CanAtk = value;
            }
        }
        public eBattlePosition BattlePosition
        {
            get
            {
                return battlePosition;
            }
            set
            {
                battlePosition = value;
                switch (value)
                {
                    case eBattlePosition.ATK:
                    //this.IsFaceUp = true;
                    this.CanATK = true;
                        break;
                    case eBattlePosition.DEF:
                    //this.IsFaceUp = false;
                    this.CanATK = false;
                        break;
                    default:
                        break;
                }
            }
        }
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
            this.CanATK = true;
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
            this.CanATK = true;
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public void SwitchToDef()
        {
            this.BattlePosition = eBattlePosition.DEF;
            this.SwitchBattlePosition = false;
            this.Origin = new Vector2(this.Sprite.Size.X / 2, this.Sprite.Size.Y / 2);
            this.Position = this.Position + this.Origin;
            this.AddRotateTo(new RotateTo(0.5f, 90));
        }

        public void SwitchToAtk()
        {
            this.BattlePosition = eBattlePosition.ATK;
            this.SwitchBattlePosition = false;
            this.Origin = new Vector2(this.Sprite.Size.X / 2, this.Sprite.Size.Y / 2);
            this.AddRotateTo(new RotateTo(0.5f, 0));
        }
        public void Flip()
        {
            this.IsFaceUp = true;
        }
    }
}
