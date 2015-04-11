using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Yugioh_AtemReturns.Decks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Yugioh_AtemReturns.Cards;

namespace Yugioh_AtemReturns.Duelists
{
    enum ePhase
    {
        STARTUP, STANDBY, DRAW, MAIN1, BATTLE, MAIN2, END,
        TEST
    }
    enum ePlayerStatus
    {
        IDLE,
        WAITFORTRIBUTE,
        SUMONNING
    }
    enum eBuffer
    {
        MONSTERSUMMON
    }
    class Duelist
    {
        protected Card m_summonBuffer;
        protected ePlayerStatus m_status;

        protected Deck[] duelDisk;
        protected Transfer transfer;
        protected int m_maxNormalSummon;
        protected int m_CurNormalSummon;
        #region Property
        public Transfer Transfer
        {
            get { return transfer; }
            protected set { transfer = value; }
        }
    
        public bool isTurn;
        public Card SummonBuffer
        {
            get
            {
                return m_summonBuffer;
            }
            set
            {
                m_summonBuffer = value;
            }
        }
        public ePhase Phase;

        public MainDeck MainDeck
        {
            get { return (MainDeck)DuelDisk[0]; }
        }
        public Hand Hand
        {
            get { return (Hand)DuelDisk[1]; }
        }
        public GraveYard GraveYard
        {
            get { return (GraveYard)DuelDisk[2]; }
        }
        public MonsterField MonsterField
        {
            get { return (MonsterField)DuelDisk[3]; }
        }
        public SpellField SpellField
        {
            get { return (SpellField)DuelDisk[4]; }
        }

        public Deck[] DuelDisk
        {
            get { return duelDisk; }
            protected set {duelDisk = value;}
        }

        public int MaxNormalSummon
        {
            get { return m_maxNormalSummon; }
            set { m_maxNormalSummon = value; }
        }
        public int CurNormalSummon
        {
            get { return m_CurNormalSummon; }
            set { m_CurNormalSummon = value; }
    }
        #endregion // property

        //test

        public Duelist(ePlayerId _id)
        {
            DuelDisk = new Deck[] {
                new MainDeck( _id),
                new Hand( _id),
                new GraveYard(_id),
                new MonsterField(_id),
                new SpellField(_id)
            };
            this.Init();
            transfer = new Transfer();
        }

        public virtual void Update(GameTime gametime)
        {
            Transfer.Update(this);
            for (int i = 0; i < DuelDisk.Length; i++)
            {
                DuelDisk[i].Update(gametime);
            }
        }

        public virtual void Init()
        {
            this.MaxNormalSummon = 1;
            this.CurNormalSummon = this.MaxNormalSummon;
        }

        public virtual void Init(ContentManager _content)
        {
        }


        public virtual void Draw(SpriteBatch _spriteBatch)
        {
            for (int i = 0; i < DuelDisk.Length; i++)
            {
                DuelDisk[i].Draw(_spriteBatch);
            }
        }

    }
}
