using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Yugioh_AtemReturns.GameObjects;
using Microsoft.Xna.Framework.Graphics;

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
        public int BattlePoint
        {
            get
            {

                if (this.BattlePosition == eBattlePosition.ATK)
                {
                    return this.atk;
                }
                else
                {
                    if (this.IsFaceUp == false)
                        return 0;
                    return this.def;
                }
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
        private static Color unusePointColor = new Color(181, 181, 181);
        Num num_atk;
        Num num_slash;
        public Num Num_atk
        {
            get { return num_atk; }
            set { num_atk = value; }
        }
        Num num_def;
        public Num Num_def
        {
            get { return num_def; }
            set { num_def = value; }
        }

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

            this.num_atk = new Num(_content, Vector2.Zero, SpriteID.font_710_whitenum);
            this.num_def = new Num(_content, Vector2.Zero, SpriteID.font_710_whitenum);
            this.num_slash = new Num(_content, Vector2.Zero, SpriteID.font_710_whitenum);
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

            this.num_atk = new Num(_content, Vector2.Zero, SpriteID.font_710_whitenum);
            this.num_def = new Num(_content, Vector2.Zero, SpriteID.font_710_whitenum);
            this.num_slash = new Num(_content, Vector2.Zero, SpriteID.font_710_whitenum);
            this.num_slash.FrameWidth = 10;

        }
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);
            if (this.IsAction == false)
            {
                if (this.battlePosition == eBattlePosition.DEF) return;
                if (this.Origin != Vector2.Zero)
                {
                    this.Position -= this.Origin;
                    this.Origin = Vector2.Zero;
                }
            }
        }
        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch _spritebatch)
        {
            base.Draw(_spritebatch);
        }
        public void DrawNum(SpriteBatch _spritebatch)
        {
            switch (this.BattlePosition)
            {
                case eBattlePosition.ATK:
                    this.num_atk.Sprite.Color = Color.White;
                    this.num_def.Sprite.Color = unusePointColor;
                    break;
                case eBattlePosition.DEF:
                    this.num_atk.Sprite.Color = unusePointColor;
                    this.num_def.Sprite.Color = Color.White;
                    break;
                default:
                    break;
            }
            if (Num_atk.IsShow == true)
                Num_atk.Draw(_spritebatch);
            if (Num_def.IsShow == true)
                Num_def.Draw(_spritebatch);
            _spritebatch.Begin();
            if (this.num_slash.IsShow == true)
                _spritebatch.Draw(num_slash.Sprite.Texture, num_slash.Position, num_slash.Sprite.Frame,unusePointColor);
            _spritebatch.End();
        }
        public void ShowBattlePoint(Vector2 _position)
        {
            this.Num_atk.Show(this.Atk, _position);
            this.Num_def.Show(this.Def, _position + new Vector2(40,0));
            this.num_slash.Position = _position + 30 * Vector2.UnitX;
            this.num_slash.Show();
        }
        public void ClearBattlePoint()
        {
            this.Num_atk.IsShow = false;
            this.Num_def.IsShow = false;
            this.num_slash.IsShow = false;
        }
        public void SwitchToDef()
        {
            this.BattlePosition = eBattlePosition.DEF;
            this.SwitchBattlePosition = false;
            this.Origin = new Vector2(this.Sprite.Size.X / 2, this.Sprite.Size.Y / 2);
            this.Position += this.Origin;
            this.AddRotateTo(new RotateTo(0.5f, 90));
        }

        public void SwitchToAtk()
        {
            this.BattlePosition = eBattlePosition.ATK;
            this.SwitchBattlePosition = false;
            //this.Position  -= this.Origin;
            //this.Origin = Vector2.One;
            this.AddRotateTo(new RotateTo(0.5f, 0));
            if (this.IsFaceUp == false)
                this.Flip();

        }
        public void Flip()
        {
            this.IsFaceUp = true;
        }

    }

}
