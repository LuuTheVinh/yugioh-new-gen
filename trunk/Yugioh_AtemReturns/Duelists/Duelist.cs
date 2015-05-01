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
using Yugioh_AtemReturns.Scenes;
using Yugioh_AtemReturns.GameObjects;
using Yugioh_AtemReturns.Cards.Monsters;

namespace Yugioh_AtemReturns.Duelists
{
    public enum ePlayerId
    {
        PLAYER,
        COMPUTER
    }
    enum ePhase
    {
        STARTUP, DRAW, STANDBY, MAIN1, BATTLE, MAIN2, END,
        TEST
    }
    enum ePlayerStatus
    {
        IDLE,
        WAITFORTRIBUTE,
        SUMONNING,
        ATTACKING
    }
    enum eBuffer
    {
        MONSTERSUMMON
    }
    class Duelist
    {
        public static int[] RequireTributer = { 0, 0, 0, 0, 1, 1, 2, 2, 2, 2, 2, 2 };
        protected Card m_summonBuffer;
        protected ePlayerStatus m_status;

        protected Deck[] duelDisk;
        protected Transfer transfer;
        protected int m_maxNormalSummon;
        protected int m_CurNormalSummon;
        protected ePlayerId m_duelistID;
        protected bool m_isTurn;

        protected int tribute;
        private int requireTribute;

        protected Duelist opponent;
        public int RequireTribute
        {
            get { return requireTribute; }
            set { requireTribute = value; }
        }

        private int m_lifePoint;
        private LP_Change m_lp_change;

        public LP_Change Lp_change
        {
            get { return m_lp_change; }
            set { m_lp_change = value; }
        }

        public int LifePoint
        {
            get { return m_lifePoint; }
            set
            {
                int dif = m_lifePoint - value;

                m_lifePoint = Math.Max(0,value);
                m_lifePoint = Math.Min(m_lifePoint, GlobalSetting.Default.MaxLP);
                if (m_lifePoint == 0)
                {
                    this.IsLose = true;
                }
                if (m_healthbar != null)
                    this.m_healthbar.LPChange(value);
                if (m_numsprite != null)
                    this.m_numsprite.Show(m_lifePoint);
                if (Lp_change != null)
                {
                    if (dif < 0)
                    {
                        Lp_change.Init(-dif, eLPState.PLUS);
                    }
                    if (dif > 0)
                    {
                        Lp_change.Init(dif, eLPState.SUBTRACT);
                    }
                }
            }
        }
        public ePlayerId DuelistID {
            get
            {
                return m_duelistID;
            }
            set
            {
                m_duelistID = value;
            }
        }
        public ePlayerStatus Status
        {
            get
            {
                return m_status;
            }
            set
            {
                m_status = value;
                switch (value)
                {
                    case ePlayerStatus.IDLE:
                        SummonBuffer = null;
                        break;
                    case ePlayerStatus.WAITFORTRIBUTE:

                        break;
                    case ePlayerStatus.SUMONNING:
                        this.CurNormalSummon--;
                        Hand.SendTo(SummonBuffer, eDeckId.MONSTERFIELD);
                        this.Status = ePlayerStatus.IDLE;
                        break;
                    default:
                        break;
                }
            }
        }
        #region Property
        public Transfer Transfer
        {
            get { return transfer; }
            protected set { transfer = value; }
        }

        public bool IsTurn
        {
            get
            {
                return m_isTurn;
            }
            set
            {
                m_isTurn = value;
            }
        }
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
        public ePhase Phase
        {

            get { return m_phase; }
            set
            {
                m_phase = value;
                switch (value)
                {
                    case ePhase.STARTUP:
                        break;
                    case ePhase.DRAW:
                        PlayScene.TurnCounter++;                        
                        break;
                    case ePhase.STANDBY:
                        break;
                    case ePhase.MAIN1:
                        break;
                    case ePhase.BATTLE:
                        break;
                    case ePhase.MAIN2:
                        break;
                    case ePhase.END:
                        break;
                    case ePhase.TEST:
                        break;
                    default:
                        break;
                }
            }
        }

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
        public bool IsAction
        {
            get
            {
                foreach (var item in DuelDisk)
                {
                    if (item.IsAction == true)
                        return true;
                }
                return false;
            }
        }
        #endregion // property

        //test
        HealthBar m_healthbar;
        protected Num m_numsprite;
        private ePhase m_phase;
        public bool IsLose { get; set; }
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
            this.DuelistID = _id;
            MainDeck.CardRemoved+=new CardRemoveEventHandler(MainDeck_CardRemoved);
            MonsterField.CardAdded += MonsterField_CardAdded;
            GraveYard.CardAdded += GraveYard_CardAdded;
        }

        private void GraveYard_CardAdded(Deck sender, CardEventArgs e)
        {
            e.Card.Sprite.Depth = 0.8f;
        }

        private void MonsterField_CardAdded(Deck sender, CardEventArgs e)
        {
            e.Card.Sprite.Depth = 0.5f;
        }

        public virtual void Update(GameTime gametime)
        {
            Transfer.Update(this);
            for (int i = 0; i < DuelDisk.Length; i++)
            {
                DuelDisk[i].Update(gametime);
            }
            switch (this.DuelistID)
            {
                case ePlayerId.PLAYER:
                    this.opponent = PlayScene.Computer;
                    break;
                case ePlayerId.COMPUTER:
                    this.opponent = PlayScene.Player;                    
                    break;
                default:
                    break;
            }
            switch (this.Phase)
            {
                case ePhase.STARTUP:
                    break;
                case ePhase.DRAW:
                    break;
                case ePhase.STANDBY:
                    foreach (var item  in MonsterField.ListCard)
                    {
                        if ((item as Monster).BattlePosition == eBattlePosition.ATK)
                            (item as Monster).CanATK = true; 
                    }
                    break;
                case ePhase.MAIN1:
                    break;
                case ePhase.BATTLE:

                    break;
                case ePhase.MAIN2:
                    break;
                case ePhase.END:
                    break;
                case ePhase.TEST:
                    break;
                default:
                    break;
            }
            this.m_healthbar.Update(gametime);
            this.Lp_change.Update(gametime);
        }

        public virtual void Init()
        {
            this.MaxNormalSummon = 1;
            this.CurNormalSummon = this.MaxNormalSummon;
            this.LifePoint = GlobalSetting.Default.MaxLP;
        }

        public virtual void Init(ContentManager _content)
        {
            this.m_healthbar = new HealthBar(_content, this);
            this.m_lp_change = new LP_Change(_content, GlobalSetting.Default.CenterField);
            this.m_numsprite = new Num(_content, Vector2.Zero, SpriteID.font_2634_dorangenum);
        }


        public virtual void Draw(SpriteBatch _spriteBatch)
        {
            for (int i = 0; i < DuelDisk.Length; i++)
            {
                DuelDisk[i].Draw(_spriteBatch);
            }
            this.m_healthbar.Draw(_spriteBatch);
            this.Lp_change.Draw(_spriteBatch);
            m_numsprite.Draw(_spriteBatch);
        }

        private void MainDeck_CardRemoved(Deck sender, CardEventArgs e)
        {
            if ((sender as MainDeck).Count == 0)
                this.IsLose = true;
        }

        protected void StartupPhase()
        {
            if (this.IsTurn == false)
                return;
            if (this.Hand.IsAction == true)
                return;
            if (Hand.Count == 5)
            {
                Phase = ePhase.END;
            }
            else
                MainDeck.DrawCard();
        }
        protected void Tribute(Card _card)
        {
            this.MonsterField.SendTo(_card, eDeckId.GRAVEYARD);
            this.tribute++;
        }

    }
}
