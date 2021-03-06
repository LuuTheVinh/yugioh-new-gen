﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yugioh_AtemReturns.Decks;
using Microsoft.Xna.Framework;
using Yugioh_AtemReturns.Cards;
using Yugioh_AtemReturns.GameObjects;
using Yugioh_AtemReturns.Manager;
using Yugioh_AtemReturns.Cards.Monsters;
using Yugioh_AtemReturns.Scenes;

namespace Yugioh_AtemReturns.Duelists
{
    partial class Player
    {
        #region Graveyard Implement Event
        private void Graveyard_CardAdded_SetPosition(Deck sender, CardEventArgs e)
        {
            if (e.Card.STATUS == STATUS.TRIBUTE)
                return;
            if ((e.Card as Monster).BattlePosition == eBattlePosition.DEF)
                e.Card.Sprite.Rotation = 0f;
            (e.Card as Monster).BattlePosition = eBattlePosition.ATK;
            e.Card.STATUS = STATUS.NORMAL;
            e.Card.Sprite.Origin = Vector2.Zero;

        }
        #endregion
        #region Monster Field Implement Event
        private void MonsterField_CardAdded_SetPosition(Deck sender, CardEventArgs e)
        {
            //switch (e.Card.STATUS)
            //{
            //    case STATUS.DEF:
            switch ((e.Card as Monster).BattlePosition)
            {
                case eBattlePosition.DEF:
                    e.Card.Origin = new Vector2(e.Card.Sprite.Texture.Bounds.Center.X, e.Card.Sprite.Texture.Bounds.Center.Y);

                    e.Card.AddMoveTo(new MoveTo(0.3f, new Vector2(
                        sender.Position.X + ((MonsterField)sender).CurrentSlot * GlobalSetting.Default.FieldSlot.X + 15,
                        sender.Position.Y + 15) + e.Card.Origin
                        ));
                    e.Card.AddRotateTo(new RotateTo(0.3f, 90));
                    ((MonsterField)sender).CurrentSlot++;
                    break;
                case eBattlePosition.ATK:
                    e.Card.AddMoveTo(new MoveTo(0.3f, new Vector2(
                        sender.Position.X + ((MonsterField)sender).CurrentSlot * GlobalSetting.Default.FieldSlot.X + 13,
                        sender.Position.Y + 15)));
                    ((MonsterField)sender).CurrentSlot++;
                    break;
                default:
                    break;
            } //switch
        }
        private void MonsterField_CardAdded(Deck sender, CardEventArgs e)
        {

            e.Card.IsFaceUp = ((e.Card as Monster).BattlePosition == eBattlePosition.ATK) ? true : false;

            e.Card.LeftClick += MonsterField_CardLeftClick;
            e.Card.RightClick += MonsterField_CardRightClick;
            e.Card.OutHovered += MonsterField_CardOutHover;
            e.Card.Hovered += MonsterField_CardOnHover;
            (e.Card as Monster).SwitchBattlePosition = false;
        }
        private void MonsterField_CardRemove(Deck sender, CardEventArgs e)
        {
            e.Card.LeftClick -= MonsterField_CardLeftClick;
            e.Card.RightClick -= MonsterField_CardRightClick;
            e.Card.OutHovered -= MonsterField_CardOutHover;
            e.Card.Hovered -= MonsterField_CardOnHover;
            //if (e.Card.Origin != Vector2.Zero)
            //    e.Card.Origin = Vector2.Zero;
        }

        private void MonsterField_CardLeftClick(Card sender, EventArgs e)
        {
            switch (this.Status)
            {
                case ePlayerStatus.IDLE:
                    if ((sender as Monster).SwitchBattlePosition == false)
                        break;
                    if (this.Phase == ePhase.BATTLE)
                        break;
                    switch ((sender as Monster).BattlePosition)
	                {
                        case eBattlePosition.ATK:
                            (sender as Monster).SwitchToDef();
                            break;
                        case eBattlePosition.DEF:
                            (sender as Monster).SwitchToAtk();
                            break;
                        default:
                            break;
	                }
                    break;
                case ePlayerStatus.WAITFORTRIBUTE:
                    sender.STATUS = STATUS.TRIBUTE;

                    this.MonsterField.SendTo(sender, eDeckId.GRAVEYARD);
                    this.tribute++;
                    if (this.tribute == this.RequireTribute)
                    {
                        this.Status = ePlayerStatus.SUMONNING;
                    }
                    break;
                case ePlayerStatus.SUMONNING:
                    break;
                default:
                    break;
            }
        }

        private void MonsterField_CardRightClick(Card sender, EventArgs e)
        {
           // System.Diagnostics.Debug.WriteLine("Right click");
        }
        private void MonsterField_CardOutHover(Card sender, EventArgs e)
        {

        }
        private void MonsterField_CardOnHover(Card sender, EventArgs e)
        {
            PlayScene.DetailSideBar.SetCardPreview(sender.ToString());
        }
        #endregion //Monster Field Implement Event

        #region Spell Field Implement Event
        private void SpellField_CardAdded_SetPosition(Deck sender, CardEventArgs e)
        {
            e.Card.Position = new Vector2(
                sender.Position.X + ((SpellField)sender).CurrentSlot * GlobalSetting.Default.FieldSlot.X + 13,
                sender.Position.Y + 15);
            ((SpellField)sender).CurrentSlot++;
        }
        private void SpellField_CardAdded(Deck sender, CardEventArgs e)
        {
            e.Card.LeftClick += SpellField_CardLeftClick;
            e.Card.RightClick += SpellField_CardRightClick;
            e.Card.Hovered += SpellField_CardOnHover;
            e.Card.OutHovered += SpellField_CardOutHover;
        }
        private void SpellField_CardRemove(Deck sender, CardEventArgs e)
        {
            e.Card.LeftClick -= SpellField_CardLeftClick;
            e.Card.RightClick -= SpellField_CardRightClick;
            e.Card.Hovered -= SpellField_CardOnHover;
            e.Card.OutHovered -= SpellField_CardOutHover;
        }

        private void SpellField_CardLeftClick(Card sender, EventArgs e)
        {
           // System.Diagnostics.Debug.WriteLine("Clicked");
            //this.MonsterField.RemoveCard(sender);
        }
        private void SpellField_CardRightClick(Card sender, EventArgs e)
        {
           // System.Diagnostics.Debug.WriteLine("Right click");
        }
        private void SpellField_CardOutHover(Card sender, EventArgs e)
        {

        }
        private void SpellField_CardOnHover(Card sender, EventArgs e)
        {

        }
        #endregion

        #region Hand Implement Event
        private void Hand_CardAdded(Deck sender, CardEventArgs e)
        {
            e.Card.IsFaceUp = true;
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
        private void Hand_SetPosition(Deck sender, CardEventArgs e)
        {
            int count = sender.Count;
            if (count == 0) return;
            Vector2[] _position = new Vector2[sender.Count];

            if (count <= 6) //if (true)//
            {
                _position[0] = new Vector2(
                    GlobalSetting.Default.CenterField.X + (sender.ListCard.First.Value.Sprite.Bound.Width + GlobalSetting.Default.HandDistance.X) * (count - 2) / 2,
                    sender.Position.Y);
                for (int i = 1; i < _position.Length; i++)
                {
                    _position[i] = new Vector2(
                        _position[i - 1].X - sender.ListCard.First.Value.Sprite.Bound.Width - GlobalSetting.Default.HandDistance.X,
                        sender.Position.Y);
                }
            }//if
            else
            {
                _position[_position.Length-1] = new Vector2(299,555);
                int dis = 410 / (count - 1);
                for (int i = _position.Length - 2; i >= 0; i--)
                {
                    _position[i] = new Vector2(
                        _position[i + 1].X + dis,
                        _position[i + 1].Y);
                }
            }//else
            sender.ListCard.Last.Value.AddMoveTo(new MoveTo(0.5f, _position[0]));
            LinkedListNode<Card> node = sender.ListCard.Last.Previous;
            int j = 1;
            while (node != null)
            {
                node.Value.AddMoveTo(new MoveTo(0.5f, _position[j++]));
                node = node.Previous;
            }//while
        }
        private void Hand_CardAdded_ScaleCard(Deck sender, CardEventArgs e)
        {
            //e.Card.AddScaleTo(new ScaleTo(0.10f, new Vector2(0.2f, 1.5f)));
            float dep = (float)1 / this.Hand.Count;
            int i = 0;
            foreach (var card in Hand.ListCard)
            {
                card.Sprite.Depth = dep * i++;
            }
            sender.ListCard.Last.Value.Sprite.WaitFor(MainDeck.Backside);
            e.Card.Scale = new Vector2(0.0f, 1.5f);
            e.Card.AddScaleTo(new ScaleTo(0.25f, new Vector2(1.5f, 1.5f)));
        }
        private void Hand_CardRemoved_ScaleCard(Deck sender, CardEventArgs e)
        {
            //e.Card.SCALE = new Vector2(1.0f);
            e.Card.Sprite.Depth = 1.0f;
            e.Card.AddScaleTo(new ScaleTo(0.5f,new Vector2(1.0f)));
        }

        private void Hand_CardLeftClick(Card sender, EventArgs e)
        {
            if (this.Status != ePlayerStatus.IDLE)
                return;
            if (IsTurn == false)
                return;
            if (Phase != ePhase.MAIN1 && Phase != ePhase.MAIN2)
                return;
            switch (sender.CardType)
            {
                case eCardType.MONSTER:
                    if (this.CurNormalSummon <= 0)
                        return;
                    if (this.MonsterField.Count == MonsterField.MaxCard)
                        return;
                    this.tribute = 0;
                    this.RequireTribute = 0;
                    if (((Monster)sender).Original.Level >= 5 && this.Status == ePlayerStatus.IDLE)
                    {
                        if (((Monster)sender).Original.Level == 5 || ((Monster)sender).Original.Level == 6)
                        {
                            this.RequireTribute = 1;
                        }
                        else
                        {
                            this.RequireTribute = 2;
                        }
                        if (this.MonsterField.Count < this.RequireTribute)
                            return;
                        this.Status = ePlayerStatus.WAITFORTRIBUTE;
                        if (!PlayScene.YNDialog.IsShow && this.SummonBuffer == null)
                        {
                            PlayScene.YNDialog.Show("Summon this monster require " + this.RequireTribute.ToString() + " monster tribute. Do you want to summon?");
                            this.SummonBuffer = sender;
                            //sender.STATUS = STATUS.ATK;
                            (sender as Monster).BattlePosition = eBattlePosition.ATK;
                            return;
                        }
                    }

                    SummonBuffer = sender;
                    this.Status = ePlayerStatus.SUMONNING;
                    //sender.STATUS = STATUS.ATK;
                    (sender as Monster).BattlePosition = eBattlePosition.ATK;

                    break;
                case eCardType.TRAP:
                    break;
                case eCardType.SPELL:
                    Hand.SendTo(sender, eDeckId.SPELLFIELD);
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
            if ((sender as Card).Button.Hovered == false)
                return;
            if (IsTurn == false)
                return; 
            if (this.Status != ePlayerStatus.IDLE)
                return; 


            if (Phase != ePhase.MAIN1 && Phase != ePhase.MAIN2)
                return;
            switch (sender.CardType)
            {
                case eCardType.MONSTER:
                    if (this.CurNormalSummon <= 0)
                        return;
                    this.tribute = 0;
                    this.RequireTribute = 0;
                    if (((Monster)sender).Original.Level >= 5 && this.Status == ePlayerStatus.IDLE)
                    {
                        if (((Monster)sender).Original.Level == 5 || ((Monster)sender).Original.Level == 6)
                        {
                            this.RequireTribute = 1;
                        }
                        else
                        {
                            this.RequireTribute = 2;
                        }
                        if (this.MonsterField.Count < this.RequireTribute)
                            return;
                        this.Status = ePlayerStatus.WAITFORTRIBUTE;
                        if (PlayScene.YNDialog.IsShow == false && this.SummonBuffer == null)
                        {
                            PlayScene.YNDialog.Show("Summon this monster require " + Convert.ToString(this.RequireTribute) + " monster tribute. Do you want to summon?");
                            this.SummonBuffer = sender;
                            //sender.STATUS = STATUS.DEF;
                            (sender as Monster).BattlePosition = eBattlePosition.DEF;
                            return;
                        }
                    }
                    else
                    {
                        if (this.MonsterField.Count == MonsterField.MaxCard)
                            return;
                    }
                    SummonBuffer = sender;
                    this.Status = ePlayerStatus.SUMONNING;
                    //sender.STATUS = STATUS.DEF;
                    (sender as Monster).BattlePosition = eBattlePosition.DEF;
                    break;
                case eCardType.TRAP:
                    Hand.SendTo(sender, eDeckId.SPELLFIELD);
                    sender.IsFaceUp = false;
                    break;
                case eCardType.SPELL:
                    Hand.SendTo(sender, eDeckId.SPELLFIELD);
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
            if (sender.IsAction)
                return;
            if (this.Hand.Count >= 6)
            {
                var card = this.Hand.ListCard.Find(sender).Previous;
                if (card != null)
                    if (card.Value.Button.Hovered == true)
                    {
                        card.Value.outHovered(EventArgs.Empty);
                        card.Value.Button.Hovered = false;
                    }
            }
            sender.Position = new Vector2(
                x: sender.Position.X,
                y: GlobalSetting.Default.PlayerHand.Y - 30);

        }
        private void Hand_CardOutHover(Card sender, EventArgs e)
        {
            sender.Position = new Vector2(
                x: sender.Position.X,
                y: GlobalSetting.Default.PlayerHand.Y + 0);
        }

        #endregion //Hand Implement Event

    }
}