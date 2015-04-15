using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yugioh_AtemReturns.Cards;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Yugioh_AtemReturns.Cards.Monsters;
using Yugioh_AtemReturns.Decks;
using Yugioh_AtemReturns.GameObjects;
using Microsoft.Xna.Framework.Content;

namespace Yugioh_AtemReturns.Duelists
{
    enum eBattleStep
    {
        START_STEP,
        BATTLE_STEP,
        START_DS, //Damage Step
        BEFORE_DS, // flip
        DURING_DS, //calculate
        AFTER_DS, // active monster effect
        END_DS,  // destroyed monster to grave
        END_STEP,
        ENDPHASE,
    }
    class BattlePhase :IPhase
    {
        private LinkedList<Card> _list_monsterATK;
        private Monster monsterATK;
        private Monster monsterBeATK;
        private eBattleStep m_step;

        private BattleSword battleSword;
        private Timer m_timer;

        public Monster MonsterATK
        {
            get { return monsterATK; }
            set
            {
                monsterATK = value;
                if (value != null)
                {
                    this.battleSword.Show(value);
                }
                else
                {
                    this.battleSword.Hide();
                }
            }
        }
        public bool IsAction
        {
            get
            {
                if (MonsterATK.IsAction == true)
                    return true;
                if (MonsterBeATK.IsAction == true)
                    return true;
                if (battleSword.IsAction == true)
                    return true;
                return false;
            }
        }
        public Monster MonsterBeATK
        {
            get { return monsterBeATK; }
            set
            {
                monsterBeATK = value;
                if (value != null)
                {
                    //PlayScene.BattleSword.AttackTo(value);
                    if (this.battleSword != null)
                    {
                        this.battleSword.AttackTo(value);
                    }
                }
                else
                {
                }
            }
        }
        public Monster LoseMonster
        {
            get;
            set;
        }
        public BattlePhase(ContentManager _content)
        {
            _list_monsterATK = new LinkedList<Card>();
            battleSword = new BattleSword(_content);
            m_timer = new Timer();
            this.Step = eBattleStep.ENDPHASE;
        }
        public void Update(GameTime _gameTime)
        {
            battleSword.Update(_gameTime);
            m_timer.Update(_gameTime);
        }
        public void Update(Player _player, Computer _computer)
        {
            if (this.Step == eBattleStep.ENDPHASE)
                return;

            if (_player.Phase != ePhase.BATTLE)
                this.Step = eBattleStep.ENDPHASE;

            System.Diagnostics.Debug.WriteLine(this.Step.ToString());
            switch (this.Step)
            {
                case eBattleStep.START_STEP:
                    // Active card effect
                    this.MonsterATK = null;
                    this.MonsterBeATK = null;
                    this.LoseMonster = null;
                    this.Step = eBattleStep.BATTLE_STEP;
                    m_timer.ResetStopWatch();
                    break;
                case eBattleStep.BATTLE_STEP:
                    if (MonsterATK == null)
                        break;
                    battleSword.Update(_player);

                    if (_computer.MonsterField.Count == 0) // Trường hợp đối thủ không có bài trên sân
                    {
                        battleSword.DirectAtk(_computer);
                        this.Step = eBattleStep.START_DS;
                        break;
                    }

                    if (MonsterBeATK != null && MonsterBeATK != null) // Trường hợp chọn được lá bị tấn công
                    {
                        this.Step = eBattleStep.START_DS;
                        break;
                    }
                    break;
                case eBattleStep.START_DS:
                    if (battleSword.IsAction == false)
                    {
                        //Chạy một số animation
                        battleSword.Status = STATUS.SWORD_HALF;
                        if (m_timer.StopWatch(500))
                        {
                            battleSword.Hide();
                            this.Step = eBattleStep.BEFORE_DS;
                        }
                    }
                    // active card effect
                    break;
                case eBattleStep.BEFORE_DS:
                    // flip face down monster
                    if (this.MonsterBeATK != null)
                    {
                        if (this.MonsterBeATK.IsFaceUp == false)
                            this.MonsterBeATK.IsFaceUp = true;
                    }
                    this.Step = eBattleStep.DURING_DS;
                    break;
                case eBattleStep.DURING_DS:
                    if (MonsterBeATK != null)
                    {
                        if (this.MonsterBeATK.BattlePosition == eBattlePosition.ATK)
                        //if (this.MonsterBeATK.STATUS == STATUS.ATK)
                        {

                            if (MonsterATK.Atk < monsterBeATK.Atk)
                                this.LoseMonster = MonsterATK;
                            if (MonsterATK.Atk > MonsterBeATK.Atk)
                                this.LoseMonster = MonsterBeATK;
                        }
                    }

                    //else
                    //{
                    //    this.LoseMonster = (MonsterATK.Atk < MonsterBeATK.Def) ? MonsterATK : MonsterBeATK;
                    //}
                    this.Step = eBattleStep.AFTER_DS;
                    break;
                case eBattleStep.AFTER_DS:
                    //active card effect
                    this.Step = eBattleStep.END_DS;
                    break;
                case eBattleStep.END_DS:
                    if (this.LoseMonster == null)
                    {
                        this.Step = eBattleStep.END_STEP;
                        break;
                    }
                    if (this.LoseMonster.IsAction == true)
                        break;

                    if (object.ReferenceEquals(this.LoseMonster,this.MonsterATK))
                    {
                        this.LoseMonster.LeftClick -= new CardLeftClickEventHandle(player_card_LeftClick);
                        _player.MonsterField.SendTo(this.LoseMonster, eDeckId.GRAVEYARD);
                        this.LoseMonster.AddMoveTo(new MoveTo(0.5f, GlobalSetting.Default.PlayerGrave));
                        this.LoseMonster = null;
                        this.Step = eBattleStep.END_STEP;
                    }
                    if (object.ReferenceEquals(this.LoseMonster, this.MonsterBeATK))
                    {
                        this.LoseMonster.LeftClick -= new CardLeftClickEventHandle(player_card_LeftClick);
                        _computer.MonsterField.SendTo(this.LoseMonster, eDeckId.GRAVEYARD);
                        this.LoseMonster.AddMoveTo(new MoveTo(0.5f,ComputerSetting.Default.GraveYard));
                        this.LoseMonster = null;
                        this.Step = eBattleStep.END_STEP;
                    }
                    break;
                case eBattleStep.END_STEP:
                    this.MonsterATK.LeftClick -= player_card_LeftClick;
                    this._list_monsterATK.Remove(this.MonsterATK);
                    this.MonsterATK.SwitchBattlePosition = false;
                    this.Step = eBattleStep.START_STEP;

                    break;
                case eBattleStep.ENDPHASE:
                    //active some card effect
                    this.End(_player, _computer);
                    break;
                default:
                    break;
            }
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            battleSword.Draw(_spriteBatch);
        }
        public void Begin(Player _player, Computer _computer)
        {
            this.Step = eBattleStep.START_STEP;
            foreach (var card in _player.MonsterField.ListCard)
            {
                if ((card as Monster).CanATK)
                {
                    _list_monsterATK.AddLast(card);
                    card.LeftClick += new CardLeftClickEventHandle(player_card_LeftClick);
                }
            }
            foreach (var card in _computer.MonsterField.ListCard)
            {
                card.LeftClick += new CardLeftClickEventHandle(computer_card_LeftClick);
            }
        }
        public void End(Player _player, Computer _computer)
        {
            this.Step = eBattleStep.ENDPHASE;
            foreach (var card in _player.MonsterField.ListCard)
            {
                card.LeftClick -= new CardLeftClickEventHandle(player_card_LeftClick);
            }
            foreach (var card in _computer.MonsterField.ListCard)
            {
                card.LeftClick -= new CardLeftClickEventHandle(computer_card_LeftClick);
            }
            _list_monsterATK.Clear();
        }
                
        private void player_card_LeftClick(Card sender, EventArgs e)
        {
            if (this.Step != eBattleStep.BATTLE_STEP)
                return;
            if (this.MonsterATK == null)
                this.MonsterATK = (sender as Monster);
        }

        private void computer_card_LeftClick(Card sender, EventArgs e)
        {
            if (this.Step != eBattleStep.BATTLE_STEP)
                return;
            if (this.MonsterATK != null && this.MonsterBeATK == null)
            {
                this.MonsterBeATK = (sender as Monster);
            }
        }

        public eBattleStep Step
        {
            get
            {
                return m_step;
            }
            set
            {
                m_step = value;
            }
        }
    }
}
