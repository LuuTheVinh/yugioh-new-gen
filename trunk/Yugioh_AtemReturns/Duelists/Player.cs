using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yugioh_AtemReturns.Decks;
using Microsoft.Xna.Framework;
using Yugioh_AtemReturns.Cards;
using Yugioh_AtemReturns.GameObjects;
using Yugioh_AtemReturns.Manager;
using Yugioh_AtemReturns.Scenes;
using Microsoft.Xna.Framework.Input;

namespace Yugioh_AtemReturns.Duelists
{

    delegate void UpdatePhase(GameTime gametime);


    partial class Player : Duelist
    {
        protected int tribute;
        protected int requireTribute;


        private InputController input;

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
                        Hand.SendTo(Hand.RemoveCard(SummonBuffer), eDeckId.MONSTERFIELD);
                        this.Status = ePlayerStatus.IDLE;
                        break;
                    default:
                        break;
                }
            }
        }
           
        public event UpdatePhase UpdatePhase;
        protected virtual void OnUpdatePhase(GameTime gametime)
        {
            if (UpdatePhase != null)
                UpdatePhase(gametime);
        }

        public Player()
            : base(ePlayerId.PLAYER)
        { }

        public override void Init()
        {
            base.Init();
            this.MainDeck.Position = GlobalSetting.Default.PlayerMain;
            this.GraveYard.Position = GlobalSetting.Default.PlayerGrave;
            this.MonsterField.Position = GlobalSetting.Default.PlayerMonF;
            this.SpellField.Position = GlobalSetting.Default.PlayerSpellF;
            this.Hand.Position = GlobalSetting.Default.PlayerHand;

            //Graveyard

            this.GraveYard.CardAdded += new CardAddedEventHandler(Graveyard_CardAdded_SetPosition);
                //this.MonsterField.CardAdded += new CardAddedEventHandler(Graveyard_CardAdded);
                //this.MonsterField.CardRemoved += new CardRemoveEventHandler(Graveyard_CardRemove);

            // MonsterField
            this.MonsterField.CardAdded += new CardAddedEventHandler(MonsterField_CardAdded_SetPosition);
            this.MonsterField.CardAdded += new CardAddedEventHandler(MonsterField_CardAdded);
            this.MonsterField.CardRemoved += new CardRemoveEventHandler(MonsterField_CardRemove);

            // Spell Field
            this.SpellField.CardAdded += new CardAddedEventHandler(SpellField_CardAdded_SetPosition);
            this.SpellField.CardAdded += new CardAddedEventHandler(SpellField_CardAdded);
            this.SpellField.CardRemoved += new CardRemoveEventHandler(SpellField_CardRemove);

            // Hand            
            this.Hand.CardAdded += new CardAddedEventHandler(Hand_CardAdded_ScaleCard);
            this.Hand.CardAdded += new CardAddedEventHandler(Hand_SetPosition);
            this.Hand.CardAdded += new CardAddedEventHandler(Hand_CardAdded);
            this.Hand.CardRemoved += new CardRemoveEventHandler(Hand_CardRemove);
            this.Hand.CardRemoved += new CardRemoveEventHandler(Hand_CardRemoved_ScaleCard);
            this.Hand.CardRemoved += new CardRemoveEventHandler(Hand_SetPosition);

            Phase = ePhase.STARTUP;
            isTurn = true;
            this.input = new InputController();

        }
        public override void Init(Microsoft.Xna.Framework.Content.ContentManager _content)
        {
            base.Init(_content);
            this.MainDeck.CreateDeck(_content, "1");
        }

        public override void Update(Microsoft.Xna.Framework.GameTime _gameTime)
        {

            if (SummonBuffer != null)
            {
                if (PlayScene.YNDialog.IsShow)
                {
                    return;
                }
                else
                {
                    if (PlayScene.YNDialog.Result)
                    {

                    }
                    else
                    {
                        this.Status = ePlayerStatus.IDLE;
                    }
                }
            }


            base.Update(_gameTime);
            switch (Phase)
            {
                case ePhase.STARTUP:
                    if (this.Hand.IsAction == true)
                        break;
                    if (this.isTurn == true)
                    {
                        if (Hand.Count == 5)
                        {
                            Phase = ePhase.STANDBY;
                            isTurn = false;
                        }
                        else
                            MainDeck.DrawCard();
                    }
                    break;
                case ePhase.STANDBY:
                    if (this.isTurn == true)
                    {
                        Phase = ePhase.DRAW;
                        this.CurNormalSummon = this.MaxNormalSummon;
                    }
                    break;
                case ePhase.DRAW:
                    MainDeck.DrawCard();
                    Phase = ePhase.MAIN1;
                    break;
                case ePhase.MAIN1:
                    break;
                case ePhase.BATTLE:
                    break;
                case ePhase.MAIN2:
                    break;
                case ePhase.END:
                    Phase = ePhase.STANDBY;
                    isTurn = false;
                    break;
                case ePhase.TEST:
                    break;
                default:
                    break;
            }
            this.input.Begin();
            if (input.isKeyPress(Keys.Escape))
            {
                if (this.Status == ePlayerStatus.WAITFORTRIBUTE)
                {
                    PlayScene.YNDialog.Show();
                }
            }
            this.input.End();
        }
        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch _spriteBatch)
        {
            base.Draw(_spriteBatch);
        }

    }
}