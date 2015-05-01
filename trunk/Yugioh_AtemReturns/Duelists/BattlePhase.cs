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

        public LinkedList<Card> List_monsterATK
        {
            get { return _list_monsterATK; }
            set { _list_monsterATK = value; }
        }
        private Monster[] monster = new Monster[2];
        readonly bool[] isdestroy = new bool[2];
        private eBattleStep m_step;
        private ePlayerId m_playerTurn;
        private BattleSword battleSword;
        private Timer m_timer;
        public Monster MonsterATK
        {
            get { return monster[0]; }
            set
            {
                monster[0] = value;
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
            get { return monster[1]; }
            set
            {
                monster[1] = value;
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
            this.checkEndphase(_player, _computer);

#if DEBUG
            System.Diagnostics.Debug.WriteLine(this.Step.ToString());
#endif
            switch (this.Step)
            {
                case eBattleStep.START_STEP:
                    // Active card effect
                    this.MonsterATK = null;
                    this.MonsterBeATK = null;
                    this.LoseMonster = null;
                    this.isdestroy[0] = false;
                    this.isdestroy[1] = false;
                    this.Step = eBattleStep.BATTLE_STEP;
                    m_timer.ResetStopWatch();
                    break;
                case eBattleStep.BATTLE_STEP:

                    if (this.m_playerTurn == ePlayerId.PLAYER)
                    {
                        //wait event click
                        //...
                        if (MonsterATK == null)
                            break;
                        battleSword.Update(_player);

                        if (_computer.MonsterField.Count == 0) // Trường hợp đối thủ không có bài trên sân
                        {
                            battleSword.DirectAtk(_computer);
                            this.Step = eBattleStep.START_DS;
                            break;
                        }

                        if (MonsterBeATK != null) // Trường hợp chọn được lá bị tấn công
                        {
                            this.Step = eBattleStep.START_DS;
                            break;
                        }
                    }
                    else//com turn
                    {
                        if (MonsterATK == null)
                        {
                            //chọn lá tấn công cho computer
                            _computer.SelectMonsterATK(this);
                        }
                        if (this.MonsterATK == null)
                        {
                            _computer.Phase = ePhase.END;
                            this.Step = eBattleStep.ENDPHASE;
                            break;
                        }
                        if (_player.MonsterField.Count == 0) // Trường hợp đối thủ không có bài trên sân
                        {
                            battleSword.DirectAtk(_player);
                        }
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
                        if (this.MonsterBeATK.BattlePosition == eBattlePosition.DEF)
                        {
                            if (MonsterATK.Atk > MonsterBeATK.Def)
                            {
                                this.isdestroy[1] = true;
                            }
                            if (MonsterATK.Atk < MonsterBeATK.Def)
                            {
                                this.LoseMonster = MonsterATK;
                                this.DamageCalculate(_player, _computer,  MonsterBeATK.Def - MonsterATK.Atk);
                                this.LoseMonster = null;
                            }
                        }
                        else
                        {
                            int dif = MonsterATK.Atk - MonsterBeATK.Atk;
                            if (dif < 0)
                            {
                                this.isdestroy[0] = true;
                                this.LoseMonster = MonsterATK;
                            }
                            if (dif > 0)
                            {
                                this.isdestroy[1] = true;
                                this.LoseMonster = MonsterBeATK;
                            }
                            if (dif == 0)
                            {
                                this.isdestroy[0] = true;
                                this.isdestroy[1] = true;
                            }
                            this.DamageCalculate(_player, _computer, dif);
                        }
                    }
                    else
                    {
                        AtkDirectly(_player, _computer);
                    }
                    this.Step = eBattleStep.AFTER_DS;
                    break;
                case eBattleStep.AFTER_DS:
                    //active card effect
                    this.Step = eBattleStep.END_DS;
                    break;
                case eBattleStep.END_DS:

                    for (int i = 0; i < 2; i++)
                    {
                        if (isdestroy[i] == true)
                        {
                            LoseMonster = monster[i];
                        }
                        else
                            continue;
                        if (this.LoseMonster.IsAction == true)
                            break;
                        if (_player.MonsterField.ListCard.Contains(LoseMonster))
                        {
                            this.LoseMonster.LeftClick -= new CardLeftClickEventHandle(player_card_LeftClick);
                            _player.MonsterField.SendTo(this.LoseMonster, eDeckId.GRAVEYARD);
                            this.LoseMonster.AddMoveTo(new MoveTo(0.5f, GlobalSetting.Default.PlayerGrave));
                            this.LoseMonster = null;
                            this.Step = eBattleStep.END_STEP;
                        }
                        if (_computer.MonsterField.ListCard.Contains(LoseMonster))
                        {
                            this.LoseMonster.LeftClick -= new CardLeftClickEventHandle(computer_card_LeftClick);
                            _computer.MonsterField.SendTo(this.LoseMonster, eDeckId.GRAVEYARD);
                            this.LoseMonster.AddMoveTo(new MoveTo(0.5f, ComputerSetting.Default.GraveYard));
                            this.LoseMonster = null;
                            this.Step = eBattleStep.END_STEP;
                        }

                    }

                    this.Step = eBattleStep.END_STEP;
                    break;

                case eBattleStep.END_STEP:
                    //if (this.MonsterATK == null)
                    //{
                    //    this.Step = eBattleStep.START_STEP;
                    //    break;
                    //}
                    if (_player.IsAction || _computer.IsAction)
                        break;
                    this.MonsterATK.CanATK = false;
                    this.MonsterATK.LeftClick -= player_card_LeftClick;
                    this.List_monsterATK.Remove(this.MonsterATK);
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

        private void checkEndphase(Player _player, Computer _computer)
        {
            if (this.m_playerTurn == ePlayerId.PLAYER)
            {
                if (_player.Phase != ePhase.BATTLE)
                    this.Step = eBattleStep.ENDPHASE;
            }
            else
            {
                if (_computer.Phase != ePhase.BATTLE)
                    this.Step = eBattleStep.ENDPHASE;
            }
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            battleSword.Draw(_spriteBatch);
        }

        public void Begin(Player _player, Computer _computer, ePlayerId _id)
        {
            this.Step = eBattleStep.START_STEP;
            this.m_playerTurn = _id;
            if (m_playerTurn == ePlayerId.PLAYER)
            {
                foreach (var card in _player.MonsterField.ListCard)
                {
                    if ((card as Monster).CanATK)
                    {
                        List_monsterATK.AddLast(card);
                        card.LeftClick += new CardLeftClickEventHandle(player_card_LeftClick);
                    }
                }
                foreach (var card in _computer.MonsterField.ListCard)
                {
                    card.LeftClick += new CardLeftClickEventHandle(computer_card_LeftClick);
                }
            }
            else
            {
                foreach (var card in _computer.MonsterField.ListCard)
                {
                    if ((card as Monster).CanATK)
                    {
                        List_monsterATK.AddLast(card);
                    }
                }
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
            List_monsterATK.Clear();
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

        private void DamageCalculate(Player _player, Computer _computer, int _dif)
        {
            if (_player.MonsterField.ListCard.Contains(LoseMonster))
            {
                _player.Lp_change.Position = LoseMonster.Position;
                _player.LifePoint -= Math.Abs(_dif);
            }
            if (_computer.MonsterField.ListCard.Contains(LoseMonster))
            {
                _computer.Lp_change.Position = LoseMonster.Position;
                _computer.LifePoint -= Math.Abs(_dif);
            }
        }

        private void AtkDirectly(Player _player, Computer _computer)
        {
            if (_player.MonsterField.ListCard.Contains(MonsterATK))
            {
                _computer.Lp_change.Position = ComputerSetting.Default.LP_Change;
                _computer.LifePoint -= MonsterATK.Atk;
            }
            if (_computer.MonsterField.ListCard.Contains(MonsterATK))
            {
                _player.Lp_change.Position = GlobalSetting.Default.LP_Change;
                _player.LifePoint -= MonsterATK.Atk;
            }
        }
    }
}
