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

            this.MainDeck.Position = ComputerSetting.Default.MainDeck;
            this.GraveYard.Position = ComputerSetting.Default.GraveYard;
            this.MonsterField.Position = ComputerSetting.Default.MonsterField;
            this.SpellField.Position = ComputerSetting.Default.SpellField;
            this.Hand.Position = ComputerSetting.Default.Hand;
     
        
            // Monster Field
            this.MonsterField.CardAdded += new CardAddedEventHandler(MonsterField_CardAdded_SetPosition);
            this.MonsterField.CardAdded += new CardAddedEventHandler(MonsterField_CardAdded);
            this.MonsterField.CardRemoved += new CardRemoveEventHandler(MonsterField_CardRemove);
        }
        public override void IntiMainDeck(Microsoft.Xna.Framework.Content.ContentManager _content)
        {
            base.IntiMainDeck(_content);
            MonsterField.AddTop(new Monster(_content, "1002"));

            
        }
        public override void Update(Microsoft.Xna.Framework.GameTime gametime)
        {
            base.Update(gametime);

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
    }
}
