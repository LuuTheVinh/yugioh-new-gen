using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yugioh_AtemReturns.Cards.Monsters
{
    public enum eMonsterType
    {
        AQUA,
        BEAST,
        BEASTWARRIROR,
        CREATEGOD,
        DINOSAUR,
        DIVINEBEAST,
        DRAGON,
        FAIRY,
        FIEND,
        FISH,
        INSECT,
        MACHINE,
        PLANT,
        PSYCHIC,
        PYRO,
        REPTILE,
        ROCK,
        SEASERPENT,
        SPELLCASTER,
        THUNDER,
        WARRIOR,
        WINGEDBEAST,
        WYRM,
        ZOMBIE
    }
    public enum eAttribute
    {
        DARK,
        DIVINE,
        EARTH,
        FIRE,
        LIGHT,
        WATER,
        WIND
    }
    public enum eAbility
    {
        NORMAL,
        FLIP,
        TOON,
        SPIRIT,
        UNION,
        GEMINI,
        TUNER
    }

    class MonsterCardData : CardData
    {

        //public MonsterCardData() : base()
        //{

        //}

        #region Field
        private bool isEffect;
        private eAttribute attribute;
        private eMonsterType monsterType;
        private eAbility ability;
        private int level;
        private int atk;
        private int def;
        private int rank;
        private int pendulumScale;
        private int spellSpeed;
        #endregion

        #region Property
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
        public eAbility Ability
        {
            get { return ability; }
            set { ability = value; }
        }
        public int Level
        {
            get { return level; }
            set {
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
            set {
                if (value < 0)
                    throw new Exception("ATK Wrong");
                else
                    atk = value; }
        }
        public int Def
        {
            get { return def; }
            set {
                if (value < 0)
                    throw new Exception("DEF Wrong");
                else
                    def = value; }
        }
        public int Rank
        {
            get { return rank; }
            set {
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

        public MonsterCardData(MonsterCardData _monster)
            : base(_monster.Id, _monster.Name, _monster.Description, _monster.CardType)
        {
            this.IsEffect = _monster.IsEffect;
            this.Attribute = _monster.Attribute;
            this.MonsterType = _monster.MonsterType;
            this.Ability = _monster.Ability;
            this.Level = _monster.Level;
            this.Atk = _monster.Atk;
            this.Def = _monster.Def;
            this.Rank = _monster.Rank;
            this.PendulumScale = _monster.PendulumScale;
            this.SpellSpeed = 0;
        }

        public MonsterCardData(string _id, string _name, string _description, bool _isEffect,
            string _attribute, string _monsterType, string _ability, int _atk, int _def, int _level = 0, int _rank = 0, int _pendulumnScale = 0)
            : base(_id, _name, _description, eCardType.MONSTER)
        {
            this.IsEffect = _isEffect;
            this.Attribute = this.getAttribute(_attribute);
            this.MonsterType = this.getMonsterType(_monsterType);
            this.Ability = this.getAbility(_ability);
            this.Atk = _atk;
            this.Def = _def;
            this.Level = _level;
            this.Rank = _rank;
            this.PendulumScale = _pendulumnScale;
            this.SpellSpeed = 0;
        }

        #region Private Method
        private eMonsterType getMonsterType(string _sMonsterType)
        {
            return (eMonsterType)Enum.Parse(typeof(eMonsterType), _sMonsterType);
        }
        private eAttribute getAttribute(string _sAttribute)
        {
            for (int i = 0; i < Enum.GetValues(typeof(eAttribute)).Length; i++)
            {
                if (((eAttribute)i).ToString().Substring(0, 3) == _sAttribute)
                    return (eAttribute)i;
            }
            throw new Exception(_sAttribute + " is not monster attribute format. check enum type eAttribute");
        }
        private eAbility getAbility(string _sAbility)
        {
            try
            {
                return (eAbility)Enum.Parse(typeof(eAbility), _sAbility);
            }
            catch
            {
                throw new Exception(_sAbility + "is not monster ability format. check enum type eAbility");
            }
        }    
        #endregion
    }
}
