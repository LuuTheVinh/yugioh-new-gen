using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yugioh_AtemReturns.Decks;
using Yugioh_AtemReturns.Cards;
using Yugioh_AtemReturns.Cards.Monsters;
using Microsoft.Xna.Framework;

namespace Yugioh_AtemReturns.Duelists
{
    class Computer : Duelist
    {

        public Computer() : base(ePlayerId.COMPUTER) { }
        public override void Init()
        {
            base.Init();

            Phase = ePhase.STARTUP;

            this.MainDeck.Position = ComputerSetting.Default.MainDeck;
            this.GraveYard.Position = ComputerSetting.Default.GraveYard;
            this.MonsterField.Position = ComputerSetting.Default.MonsterField;
            this.SpellField.Position = ComputerSetting.Default.SpellField;
            this.Hand.Position = ComputerSetting.Default.Hand;
     
            
        
            // Monster Field
            this.MonsterField.CardAdded += new CardAddedEventHandler(MonsterField_CardAdded_SetPosition);
            this.MonsterField.CardAdded += new CardAddedEventHandler(MonsterField_CardAdded);
            this.MonsterField.CardRemoved += new CardRemoveEventHandler(MonsterField_CardRemove);


            // Hand            
            this.Hand.CardAdded += new CardAddedEventHandler(Hand_CardAdded_ScaleCard);
            this.Hand.CardAdded += new CardAddedEventHandler(Hand_CardAdded_SetPosition);
            this.Hand.CardAdded += new CardAddedEventHandler(Hand_CardAdded);
            this.Hand.CardRemoved += new CardRemoveEventHandler(Hand_CardRemove);
            this.Hand.CardRemoved += new CardRemoveEventHandler(Hand_CardRemoved_ScaleCard);
        }
        public override void Init(Microsoft.Xna.Framework.Content.ContentManager _content)
        {
            base.Init(_content);
            MonsterField.AddTop(new Monster(_content, "1002"));

            this.MainDeck.CreateDeck(_content, "2");
            
        }
        public override void Update(Microsoft.Xna.Framework.GameTime gametime)
        {
            base.Update(gametime);
            switch (Phase)
            {
                case ePhase.STARTUP:
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
                    }
                    break;
                case ePhase.DRAW:
                    MainDeck.DrawCard();
                    Phase = ePhase.END;
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

        }
        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch _spriteBatch)
        {
            base.Draw(_spriteBatch);
        }

        #region Monster Field Implement Event
        private void MonsterField_CardAdded_SetPosition(Deck sender, CardEventArgs e)
        {
            e.Card.STATUS = STATUS.DEF; // test
            switch (e.Card.STATUS)
            {
                case STATUS.DEF:

                    e.Card.POSITION = new Vector2(
                        sender.Position.X - ((MonsterField)sender).CurrentSlot * ComputerSetting.Default.FieldSlot.X + 15,
                        this.MonsterField.Position.Y + 23);
                    ((MonsterField)sender).CurrentSlot++;
                    e.Card.s_BackSide.Rotation = (float)(Math.PI / 2);
                    e.Card.s_BackSide.Origin = new Vector2(e.Card.s_BackSide.Texture.Bounds.Center.X, e.Card.s_BackSide.Texture.Bounds.Center.Y);
                    e.Card.s_BackSide.Position += new Vector2(e.Card.s_BackSide.Bound.Width / 2, e.Card.s_BackSide.Bound.Width / 2);

                    break;
                case STATUS.ATK:
                    e.Card.POSITION = new Vector2(
                        sender.Position.X - ((MonsterField)sender).CurrentSlot * ComputerSetting.Default.FieldSlot.X + 13,
                        sender.Position.Y + 15);
                    ((MonsterField)sender).CurrentSlot++;
                    break;
                default:
                    break;
            } //switch
        }
        private void MonsterField_CardAdded(Deck sender, CardEventArgs e)
        {
            e.Card.LeftClick += MonsterField_CardLeftClick;
            e.Card.RightClick += MonsterField_CardRightClick;
            e.Card.OutHovered += MonsterField_CardOutHover;
            e.Card.Hovered += MonsterField_CardOnHover;
        }
        private void MonsterField_CardRemove(Deck sender, CardEventArgs e)
        {
            e.Card.LeftClick -= MonsterField_CardLeftClick;
            e.Card.RightClick -= MonsterField_CardRightClick;
            e.Card.OutHovered -= MonsterField_CardOutHover;
            e.Card.Hovered -= MonsterField_CardOnHover;
        }


        private void MonsterField_CardLeftClick(Card sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Clicked");
        }
        
        private void MonsterField_CardRightClick(Card sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Right click");
        }        
        
        private void MonsterField_CardOnHover(Card sender, EventArgs e)
        {

        }
        private void MonsterField_CardOutHover(Card sender, EventArgs e)
        {

        }
        #endregion
        #region Hand Implement Event
        private void Hand_CardAdded(Deck sender, CardEventArgs e)
        {
            e.Card.IsFaceUp = false;
            e.Card.LeftClick += Hand_CardLeftClick;
            e.Card.RightClick += Hand_CardRightClick;
            e.Card.Hovered += Hand_CardOnHover;
            e.Card.OutHovered += Hand_CardOutHover;
        }
        private void Hand_CardRemove(Deck sender, CardEventArgs e)
        {
            e.Card.LeftClick -= Hand_CardLeftClick;
            e.Card.RightClick -= Hand_CardRightClick;
            e.Card.Hovered -= Hand_CardOnHover;
            e.Card.OutHovered -= Hand_CardOutHover;
        }
        private void Hand_CardAdded_SetPosition(Deck sender, CardEventArgs e)
        {
            int count = sender.Count;
            sender.ListCard.Last.Value.POSITION = new Vector2(
                    GlobalSetting.Default.CenterField.X - (sender.ListCard.First.Value.Sprite.Bound.Width + GlobalSetting.Default.HandDistance.X) * count / 2,
                    sender.Position.Y - sender.ListCard.First.Value.Sprite.Bound.Height);
            LinkedListNode<Card> node = sender.ListCard.Last.Previous;
            while (node != null)
            {
                node.Value.POSITION = new Vector2(
                    node.Next.Value.Sprite.Bound.Right + GlobalSetting.Default.HandDistance.X,
                    sender.Position.Y - sender.ListCard.First.Value.Sprite.Bound.Height);
                node = node.Previous;
            }
        }
        private void Hand_CardAdded_ScaleCard(Deck sender, CardEventArgs e)
        {
             e.Card.SCALE = new Vector2(1.5f);
        }
        private void Hand_CardRemoved_ScaleCard(Deck sender, CardEventArgs e)
        {
            e.Card.SCALE = new Vector2(1.0f);
        }

        private void Hand_CardLeftClick(Card sender, EventArgs e)
        {
        }
        private void Hand_CardRightClick(Card sender, EventArgs e)
        {
        }
        private void Hand_CardOnHover(Card sender, EventArgs e)
        {
            sender.POSITION= new Vector2(
                x: sender.Position.X,
                y: ComputerSetting.Default.Hand.Y + 15 - Hand.ListCard.First.Value.Sprite.Bound.Height);
        }
        private void Hand_CardOutHover(Card sender, EventArgs e)
        {
            sender.POSITION = new Vector2(
                x: sender.Position.X,
                y: ComputerSetting.Default.Hand.Y - 15 - Hand.ListCard.First.Value.Sprite.Bound.Height);

        }

        #endregion //Hand Implement Event
    }
}
