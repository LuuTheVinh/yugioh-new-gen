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
using Microsoft.Xna.Framework.Content;
using Yugioh_AtemReturns.Cards.Monsters;

namespace Yugioh_AtemReturns.Duelists
{

    delegate void UpdatePhase(GameTime gametime);


    partial class Player : Duelist
    {


        private InputController input;

        public InputController Input
        {
            get { return input; }
            set { input = value; }
        }
           
        public event UpdatePhase UpdatePhase;
        protected virtual void OnUpdatePhase(GameTime gametime)
        {
            if (UpdatePhase != null)
                UpdatePhase(gametime);
        }

        public Player(ContentManager _content)
            : base(ePlayerId.PLAYER)
        {
        }


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
            IsTurn = true;
            this.Input = new InputController();

        }
        public override void Init(Microsoft.Xna.Framework.Content.ContentManager _content)
        {
            base.Init(_content);
            this.MainDeck.Init(_content, "1");
            this.GraveYard.Init(_content);
            this.Lp_change.Position = GlobalSetting.Default.LP_Change;
            this.LifePoint = GlobalSetting.Default.MaxLP;
            this.m_numsprite.Position = GlobalSetting.Default.LifePointNum;
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

            //if (this.IsTurn == false)
            //    return;
            base.Update(_gameTime);
            switch (Phase)
            {
                case ePhase.STARTUP:
                    this.StartupPhase();
                    break;
                case ePhase.DRAW:
                    if (this.IsTurn == false)
                        break;
                    if (PlayScene.TurnCounter >2)
                        MainDeck.DrawCard();
                    Phase = ePhase.STANDBY;
                    break;
                case ePhase.STANDBY:
                    if (this.IsTurn == false)
                        break;
                    Phase = ePhase.MAIN1;
                    this.CurNormalSummon = this.MaxNormalSummon;
                    break;
                case ePhase.MAIN1:

                    break;
                case ePhase.BATTLE:
                   // battlePhase.Begin(this, PlayScene.Computer);
                    break;
                case ePhase.MAIN2:
                    break;
                case ePhase.END:
                    foreach (var card in MonsterField.ListCard)
                    {
                        (card as Monster).SwitchBattlePosition = true;
                    }
                    Phase = ePhase.DRAW;
                    IsTurn = false;

                    break;
                case ePhase.TEST:
                    break;
                default:
                    break;
            }
            this.Input.Begin();
            if (Input.isKeyPress(Keys.Escape))
            {
                if (this.Status == ePlayerStatus.WAITFORTRIBUTE)
                {
                    PlayScene.YNDialog.Show();
                }
            }

            this.Input.End();
        }
        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch _spriteBatch)
        {
            base.Draw(_spriteBatch);
        }

    }
}