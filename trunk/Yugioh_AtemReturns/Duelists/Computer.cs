using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yugioh_AtemReturns.Decks;
using Yugioh_AtemReturns.Cards;
using Yugioh_AtemReturns.Cards.Monsters;
using Microsoft.Xna.Framework;
using Yugioh_AtemReturns.Duelists.AI_Logics;
using Yugioh_AtemReturns.Scenes;
using Yugioh_AtemReturns.GameObjects;

namespace Yugioh_AtemReturns.Duelists
{
    enum eDeckName
    {
        YAMIYUGI
    }
    class Computer : Duelist
    {
        private eDeckName m_deckName;
        private string m_deckId_dbo;
        private AI m_Ai_logics;
        LinkedList<Card> tributemonster; // là danh sách monster bị tribute
        public eDeckName DeckName
        {
            get { return m_deckName; }
            private set {
                m_deckName = value;
                switch (m_deckName)
                {
                    case eDeckName.YAMIYUGI:
                        m_deckId_dbo = "2";
                        m_Ai_logics = new YamiYugiDeck_AI();
                        break;
                    default:
                        break;
                }
            }
        }
        public Card TributeMonster { get; set; }
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

            //GraveYard            
            this.GraveYard.CardAdded += new CardAddedEventHandler(GraveYard_CardAdded);
            this.GraveYard.CardRemoved += new CardRemoveEventHandler(GraveYard_CardRemove);


            // Monster Field
            this.MonsterField.CardAdded += new CardAddedEventHandler(MonsterField_CardAdded_SetPosition);
            this.MonsterField.CardAdded += new CardAddedEventHandler(MonsterField_CardAdded);
            this.MonsterField.CardRemoved += new CardRemoveEventHandler(MonsterField_CardRemove);


            // Hand            
            this.Hand.CardAdded += new CardAddedEventHandler(Hand_CardAdded_ScaleCard);
            this.Hand.CardAdded += new CardAddedEventHandler(Hand_SetPosition);
            this.Hand.CardAdded += new CardAddedEventHandler(Hand_CardAdded);
            this.Hand.CardRemoved += new CardRemoveEventHandler(Hand_CardRemove);
            this.Hand.CardRemoved += new CardRemoveEventHandler(Hand_CardRemoved_ScaleCard);
            this.Hand.CardRemoved += new CardRemoveEventHandler(Hand_SetPosition);
        }
        public override void Init(Microsoft.Xna.Framework.Content.ContentManager _content)
        {
            base.Init(_content);
           // MonsterField.AddTop(new Monster(_content, "1002"));

            this.DeckName =(eDeckName) Rnd.Rand(0, Enum.GetValues(typeof(eDeckName)).Length - 1);
            this.MainDeck.Init(_content, m_deckId_dbo);
            this.GraveYard.Init(_content);
            this.LifePoint = GlobalSetting.Default.MaxLP;
            this.m_numsprite.Position = ComputerSetting.Default.LifePointNum;
            this.Lp_change.Position = ComputerSetting.Default.LP_Change;
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gametime)
        {
            base.Update(gametime);
            switch (Phase)
            {
                case ePhase.STARTUP:
                    this.StartupPhase();
                    break;
                case ePhase.DRAW:
                    if (this.IsTurn == false)
                        break;
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
                    if (this.Hand.IsAction == true)
                        break;

                    if (this.Status == ePlayerStatus.IDLE)
                    {
                        if (CurNormalSummon != 0)
                        {
                            this.SummonBuffer = this.m_Ai_logics.Summon(PlayScene.Player, this);
                        }
                        else
                        {
                            if (this.MonsterField.ListCard.Any(card => (card as Monster).CanATK))
                            {
                                if (this.IsAction == false)
                                this.Phase = ePhase.BATTLE;
                            }else
                            this.Phase = ePhase.END;
                            break;
                        }
                        if (SummonBuffer == null)
                        {
                            this.Phase = ePhase.END;
                            break;
                        }
                        int tri = RequireTributer[(SummonBuffer as Monster).Level - 1];

                        tributemonster = new LinkedList<Card>(this.m_Ai_logics.Tribute(this, tri));

                        if (tri > 0)
                            this.Status = ePlayerStatus.WAITFORTRIBUTE;
                        else
                        {
                            this.Status = ePlayerStatus.SUMONNING;
                            break;
                        }
                    }
                    if (this.Status == ePlayerStatus.WAITFORTRIBUTE)
                    {
                        if (tributemonster.Any() == false)
                        {
                            this.Status = ePlayerStatus.SUMONNING;
                            break;
                        }
                        tributemonster.First().STATUS = STATUS.TRIBUTE;
                        (tributemonster.First() as Monster).CanATK = false;
                        this.Tribute(tributemonster.First());
                        tributemonster.RemoveFirst();
                    }
                    tribute = 0;

                    break;
                case ePhase.BATTLE:
                    if (this.MonsterField.IsAction == true) break;
                    if (this.MonsterField.ListCard.Any(card => (card as Monster).CanATK))
                    {
                        if (PlayScene.battlePhase.Step == eBattleStep.ENDPHASE) 
                            PlayScene.battlePhase.Begin(PlayScene.Player, this, ePlayerId.COMPUTER);
                    }
                    else{
                        this.Phase = ePhase.END;
                    }
                    break;
                case ePhase.MAIN2:
                    break;
                case ePhase.END:
                    Phase = ePhase.DRAW;
                    IsTurn = false;
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
            //switch (e.Card.STATUS)
            //{
            //    case STATUS.DEF:
            switch ((e.Card as Monster).BattlePosition)
            {
                case eBattlePosition.DEF:

                    e.Card.Position = new Vector2(
                        sender.Position.X - ((MonsterField)sender).CurrentSlot * ComputerSetting.Default.FieldSlot.X + 15,
                        this.MonsterField.Position.Y + 23);
                    ((MonsterField)sender).CurrentSlot++;
                    e.Card.Sprite.Rotation = (float)(Math.PI / 2);
                    e.Card.Sprite.Origin = new Vector2(e.Card.Sprite.Texture.Bounds.Center.X, e.Card.Sprite.Texture.Bounds.Center.Y);
                    e.Card.Sprite.Position += new Vector2(e.Card.Sprite.Bound.Width / 2, e.Card.Sprite.Bound.Width / 2);

                    break;
                case eBattlePosition.ATK:

                    e.Card.AddMoveTo(new MoveTo(0.3f,new Vector2(
                        sender.Position.X - ((MonsterField)sender).CurrentSlot * ComputerSetting.Default.FieldSlot.X + 13,
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
        }


        private void MonsterField_CardLeftClick(Card sender, EventArgs e)
        {
            switch (PlayScene.Player.Status)
            {
                case ePlayerStatus.IDLE:
                    break;
                case ePlayerStatus.WAITFORTRIBUTE:
                    break;
                case ePlayerStatus.SUMONNING:
                    break;
                case ePlayerStatus.ATTACKING:
                    //PlayScene.Player.MonsterBeAttack = sender;
                    break;
                default:
                    break;
            }
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
        private void Hand_SetPosition(Deck sender, CardEventArgs e)
        {
            int count = sender.Count;
            if (count == 0) return;

            Vector2[] _position = new Vector2[sender.Count];
            if (count <= 6)
            {
                _position[0] = new Vector2(
                        GlobalSetting.Default.CenterField.X - (sender.ListCard.First.Value.Sprite.Bound.Width + GlobalSetting.Default.HandDistance.X) * count / 2,
                        sender.Position.Y - sender.ListCard.First.Value.Sprite.Bound.Height);
                for (int i = 1; i < _position.Length; i++)
                {
                    _position[i] = new Vector2(
                        _position[i - 1].X + sender.ListCard.First.Value.Sprite.Bound.Width + GlobalSetting.Default.HandDistance.X,
                        sender.Position.Y - sender.ListCard.First.Value.Sprite.Bound.Height);
                }
            }
            else
            {
                _position[0] = new Vector2(297, ComputerSetting.Default.Hand.Y - Hand.ListCard.First.Value.Sprite.Bound.Height);
                int dis = 410 / (count - 1);
                for (int i = 1; i < count ; i++)
                {
                    _position[i] = new Vector2(
                        _position[i - 1].X + dis,
                        _position[i - 1].Y);
                }
            }//else
            e.Card.Sprite.WaitFor(this.MainDeck.Backside);
            sender.ListCard.Last.Value.AddMoveTo(new MoveTo(0.5f, _position[0]));
            int j = 1;
            
            LinkedListNode<Card> node = sender.ListCard.Last.Previous;
            while (node != null)
            {
                node.Value.AddMoveTo(new MoveTo(0.5f,_position[j++]));                
                node = node.Previous;
            }
        }
        private void Hand_CardAdded_ScaleCard(Deck sender, CardEventArgs e)
        {
            float dep = (float)1 / this.Hand.Count;
            int i = 0;
            foreach (var card in Hand.ListCard)
            {
                card.Sprite.Depth = dep * i++;
            }
            e.Card.Scale = new Vector2(1.5f);
        }
        private void Hand_CardRemoved_ScaleCard(Deck sender, CardEventArgs e)
        {
            e.Card.Sprite.Depth = 1.0f;
            e.Card.AddScaleTo(new ScaleTo(0.5f, new Vector2(1.0f)));
        }

        private void Hand_CardLeftClick(Card sender, EventArgs e)
        {
        }
        private void Hand_CardRightClick(Card sender, EventArgs e)
        {
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
                    }
            }
            if (sender.IsAction)
                return; 
            sender.Position = new Vector2(
                x: sender.Position.X,
                y: ComputerSetting.Default.Hand.Y + 30 - Hand.ListCard.First.Value.Sprite.Bound.Height);
        }
        private void Hand_CardOutHover(Card sender, EventArgs e)
        {
            sender.Position = new Vector2(
                x: sender.Position.X,
                y: ComputerSetting.Default.Hand.Y - Hand.ListCard.First.Value.Sprite.Bound.Height);
        }

        #endregion //Hand Implement Event

        #region GraveYard Implement Event
        private void GraveYard_CardAdded(Deck sender, CardEventArgs e)
        {
            if ((e.Card as Monster).BattlePosition == eBattlePosition.DEF)
                e.Card.Sprite.Rotation = 0f;
            if (e.Card.STATUS == STATUS.TRIBUTE)
            {
                e.Card.Sprite.Origin = new Vector2(e.Card.Sprite.Size.X, e.Card.Sprite.Size.Y);
                e.Card.Sprite.Position += e.Card.Sprite.Origin;
            }
            (e.Card as Monster).BattlePosition = eBattlePosition.ATK;

        }
        private void GraveYard_CardRemove(Deck sender, CardEventArgs e)
        {

        }
        #endregion



        public void SelectMonsterATK(BattlePhase _battlephase)
        {
            this.m_Ai_logics.SelectATK(_battlephase,PlayScene.Player, this);
        }
    }
}
