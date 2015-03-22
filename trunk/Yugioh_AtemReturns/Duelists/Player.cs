using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yugioh_AtemReturns.Decks;
using Microsoft.Xna.Framework;
using Yugioh_AtemReturns.Cards;

namespace Yugioh_AtemReturns.Duelists
{
    class Player : Duelist
    {

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


            // MonsterField
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
        public override void IntiMainDeck(Microsoft.Xna.Framework.Content.ContentManager _content)
        {
            base.IntiMainDeck(_content);
            this.MainDeck.CreateDeck(_content, "1");
        }

        public override void Update(Microsoft.Xna.Framework.GameTime _gameTime)
        {
            base.Update(_gameTime);

            this.MainDeck.DrawCard();
        }
        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch _spriteBatch)
        {
            base.Draw(_spriteBatch);
        }

        #region Monster Field Implement Event
        private void MonsterField_CardAdded_SetPosition(Deck sender, CardEventArgs e)
        {

            switch (e.Card.STATUS)
            {
                case STATUS.DEF:

                    e.Card.POSITION = new Vector2(
                        sender.Position.X + ((MonsterField)sender).CurrentSlot * GlobalSetting.Default.FieldSlot.X + 15,
                        this.MonsterField.Position.Y + 23);
                    ((MonsterField)sender).CurrentSlot++;
                    e.Card.s_BackSide.Rotation = (float)(Math.PI / 2);
                    e.Card.s_BackSide.Origin = new Vector2(e.Card.s_BackSide.Texture.Bounds.Center.X, e.Card.s_BackSide.Texture.Bounds.Center.Y);
                    e.Card.s_BackSide.Position += new Vector2(e.Card.s_BackSide.Bound.Width / 2, e.Card.s_BackSide.Bound.Width / 2);

                    break;
                case STATUS.ATK:
                    e.Card.POSITION = new Vector2(
                        sender.Position.X + ((MonsterField)sender).CurrentSlot * GlobalSetting.Default.FieldSlot.X + 13,
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

        private void MonsterField_CardOutHover(Card sender, EventArgs e)
        {

        }
        private void MonsterField_CardOnHover(Card sender, EventArgs e)
        {

        }
        #endregion //Monster Field Implement Event

        #region Hand Implement Event
        private void Hand_CardAdded(Deck sender, CardEventArgs e)
        {
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
                    sender.Position.Y);
            LinkedListNode<Card> node = sender.ListCard.Last.Previous;
            while (node != null)
            {
                node.Value.POSITION = new Vector2(
                    node.Next.Value.Sprite.Bound.Right + GlobalSetting.Default.HandDistance.X,
                    sender.Position.Y);
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
            Hand.OutCard = Hand.RemoveCard(sender);
            switch (sender.CardType)
            {
                case eCardType.MONSTER:
                    Hand.DestDeck = eDeckId.MONSTERFIELD;
                    sender.STATUS = STATUS.ATK;
                    break;
                case eCardType.TRAP:
                    Hand.DestDeck = eDeckId.SPELLFIELD;
                    break;
                case eCardType.SPELL:
                    Hand.DestDeck = eDeckId.SPELLFIELD;
                    break;
                case eCardType.XYZ:
                    break;
                case eCardType.SYNCHRO:
                    break;
                case eCardType.FUSION:
                    break;
                default:
                    break;
            } // switch
        }
        private void Hand_CardRightClick(Card sender, EventArgs e)
        {
            Hand.OutCard = Hand.RemoveCard(sender);
            switch (sender.CardType)
            {
                case eCardType.MONSTER:
                    Hand.DestDeck = eDeckId.MONSTERFIELD;
                    sender.STATUS = STATUS.DEF;
                    break;
                case eCardType.TRAP:
                    Hand.DestDeck = eDeckId.SPELLFIELD;
                    sender.IsFaceUp = false;
                    break;
                case eCardType.SPELL:
                    Hand.DestDeck = eDeckId.SPELLFIELD;
                    sender.IsFaceUp = false;
                    break;
                case eCardType.XYZ:
                    break;
                case eCardType.SYNCHRO:
                    break;
                case eCardType.FUSION:
                    break;
                default:
                    break;
            } // switch
        }
        private void Hand_CardOnHover(Card sender, EventArgs e)
        {
            sender.Position = new Vector2(
                x: sender.Position.X,
                y: GlobalSetting.Default.PlayerHand.Y - 15);
        }
        private void Hand_CardOutHover(Card sender, EventArgs e)
        {
            sender.Position = new Vector2(
                x: sender.Position.X,
                y: GlobalSetting.Default.PlayerHand.Y + 15);
        }

        #endregion //Hand Implement Event
    }
}