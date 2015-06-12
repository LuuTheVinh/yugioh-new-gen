using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Yugioh_AtemReturns.Duelists;
using Yugioh_AtemReturns.GameObjects;
using Yugioh_AtemReturns.Manager;

namespace Yugioh_AtemReturns.Scenes
{
    class ChosenScene : Scene
    {
        private List<Button> _playerButtons = new List<Button>();
        private int _playerChoose;
        private int _playerChooseIndex;
        private int _goFirstPlayer;                                     //0 : draw | 1: player | 2: bot/player 2
       
        private float _timer;
        private List<Button> _botButtons = new List<Button>();          //Player 2
        private int _botChoose;
        private int _botChooseIndex;

        private ContentManager _contentManager;
        private float _leftOffset;
        private float _rightOffset;
        private float _speedMove;

        private Sprite _selectHandTitle;
        private Sprite _background;

        private Button _playerGoBtn;

        private Timer _myTimer;

        private bool _start;

        public override void Init(Game game)
        {
            base.Init(game);

            _contentManager = game.Content;
            _goFirstPlayer = 0;
            _playerChoose = -1;
            _botChoose = -1;
            _playerChooseIndex = -1;
            _botChooseIndex = -1;
            _goFirstPlayer = -1;

            _rightOffset = 800 + 200*2; //200 là width của 1 nút
            _leftOffset = 0 - 200 * 2;
            _speedMove = 100;

            _timer = 0.0f;

            //Add 4 Button Player
            for (int i = 0; i < 9; i++)
            {
                _playerButtons.Add(RandomHandButton());
                _playerButtons.Last().Position = new Vector2(_leftOffset + i * (_playerButtons.Last().Sprite.Bound.Width), 350);
                float time = (_playerButtons.Last().Position.X - _leftOffset + _playerButtons.First().Sprite.Bound.Width) / _speedMove;

                _playerButtons.Last().AddMoveTo(new MoveTo(time, new Vector2(_leftOffset + (-1) * _playerButtons.First().Sprite.Bound.Width, 350)));
                _playerButtons.Last().ButtonEventWithSender += OnButtonEventWithSender;
            }

            for (int i = 0; i < 9; i++)
            {
                _botButtons.Add(new Button(new Sprite(_contentManager, "Chosen\\jya_eye_1"), new Sprite(_contentManager, "Chosen\\jya_eye_3"), new Sprite(_contentManager, "Chosen\\jya_eye_2")));
                _botButtons.Last().Position = new Vector2(_rightOffset - i * (_botButtons.Last().Sprite.Bound.Width), 50);
                float time = (_rightOffset - _botButtons.Last().Position.X) / _speedMove;
                _botButtons.Last().Origin = new Vector2(_botButtons.Last().Sprite.Bound.Width, _botButtons.Last().Sprite.Bound.Height);
                _botButtons.Last().Rotation = (float)Math.PI;
                
                _botButtons.Last().AddMoveTo(new MoveTo(time, new Vector2(_rightOffset, 50)));
            }

            //Select your hand
            _selectHandTitle = new Sprite(_contentManager, "Chosen\\bg_mid_m2");
            _selectHandTitle.Position = new Vector2(400 - _selectHandTitle.Bound.Width / 2, 300 - _selectHandTitle.Bound.Height / 2);

            _background = new Sprite(_contentManager, "Menu\\MenuBackground");

            //Timer
            _myTimer = new Timer();
            _myTimer.ResetStopWatch();

            _start = true;
        }

        private void OnButtonEventWithSender(object sender)
        {
            EffectManager.GetInstance().Play(eSoundId.attack);

            foreach (var playerButton in _playerButtons)
            {
                playerButton.Sprite.ClearAllAnimation();
            }

            foreach (var botButton in _botButtons)
            {
                botButton.Sprite.ClearAllAnimation();
            }

            var button = sender as Button;
            if (button == null) return;

            _playerChoose = int.Parse(button.Tag);
            _playerChooseIndex = _playerButtons.IndexOf(button);

            PlayerChosenAnimation(button);
            BotRandomHand();
            CheckPlayerWin();

            if (_goFirstPlayer == 1)
            {
                //Player 1 di trc
                _playerGoBtn = new Button(new Sprite(_contentManager, "Chosen\\start_first_1"), new Sprite(_contentManager, "Chosen\\start_first_2"));
                _playerGoBtn.Position = new Vector2(400, 350 + _playerGoBtn.Sprite.Bound.Height / 2);
                _playerGoBtn.ButtonEvent += PlayerGoBtnOnButtonEvent;
                _playerGoBtn.Origin = new Vector2(_playerGoBtn.Sprite.Bound.Width / 2, _playerGoBtn.Sprite.Bound.Height / 2);
                _playerGoBtn.Scale = new Vector2(1.2f, 1.2f);
            }
            else if(_goFirstPlayer == 2)
            {
                //Player 2 đi trước / Bot
                _playerGoBtn = new Button(new Sprite(_contentManager, "Chosen\\start_second_1"), new Sprite(_contentManager, "Chosen\\start_second_2"));
                _playerGoBtn.Position = new Vector2(400, 350 + _playerGoBtn.Sprite.Bound.Height / 2);
                _playerGoBtn.ButtonEvent += PlayerGoBtnOnButtonEvent;
                _playerGoBtn.Origin = new Vector2(_playerGoBtn.Sprite.Bound.Width / 2, _playerGoBtn.Sprite.Bound.Height / 2);
                _playerGoBtn.Scale = new Vector2(1.2f, 1.2f);
            }
        }

        private void PlayerGoBtnOnButtonEvent()
        {
            EffectManager.GetInstance().Play(eSoundId.turn_change);
            GotoPlayScene();
        }

        private void BotRandomHand()
        {
            var rand = new Random();
            int randNum = rand.Next(0, 3);

            int randBtn = rand.Next(2, _botButtons.Count - 3);
            _botButtons[randBtn].Selected = true;
            _botButtons[randBtn].Enable = false;
            _botChooseIndex = randBtn;

            BotChosenAnimation(_botButtons[randBtn]);

            _botChoose = randNum;

            switch (_botChoose)
            {
                case 0:
                {
                    var bot = new Sprite(_contentManager, "Chosen\\jya_cyo_2");
                    _botButtons[randBtn].Sprite.Texture = bot.Texture;

                    break;
                }
                case 1:
                {
                    var bot = new Sprite(_contentManager, "Chosen\\jya_gu_2");
                    _botButtons[randBtn].Sprite.Texture = bot.Texture;
                    break;
                }
                case 2:
                {
                    var bot = new Sprite(_contentManager, "Chosen\\jya_pa_2");
                    _botButtons[randBtn].Sprite.Texture = bot.Texture;
                    break;
                }
            }
        }

        private int CheckPlayerWin()
        {
            switch (Math.Abs(_playerChoose - _botChoose))
            {
                case 1:
                {
                    if (_playerChoose > _botChoose)
                    {
                        return _goFirstPlayer = 1;
                    }

                    return _goFirstPlayer = 2;
                }
                case 2:
                {
                    if (_playerChoose > _botChoose)
                    {
                        return _goFirstPlayer = 2;
                    }
                    return _goFirstPlayer = 1;
                }
                default:
                    return _goFirstPlayer = 0;
            }
        }

        private Button RandomHandButton()
        {
            int randNum;

            if (_playerButtons.Count > 0)
            {
                do
                {
                    var rand = new Random();
                    randNum = rand.Next(0, 3);

                } while (randNum.ToString() == _playerButtons.Last().Tag);
            }
            else
            {
                var rand = new Random();
                randNum = rand.Next(0, 3);
            }

            Button button = null;

            switch (randNum)
            {
                case 0:
                {
                    button = new Button(new Sprite(_contentManager, "Chosen\\jya_cyo_1"), new Sprite(_contentManager, "Chosen\\jya_cyo_3"), new Sprite(_contentManager, "Chosen\\jya_cyo_2"));
                    button.Tag = randNum.ToString();
                    break;
                }

                case 1:
                {
                    button = new Button(new Sprite(_contentManager, "Chosen\\jya_gu_1"), new Sprite(_contentManager, "Chosen\\jya_gu_3"), new Sprite(_contentManager, "Chosen\\jya_gu_2"));
                    button.Tag = randNum.ToString();
                    break;
                }

                case 2:
                {
                    button = new Button(new Sprite(_contentManager, "Chosen\\jya_pa_1"), new Sprite(_contentManager, "Chosen\\jya_pa_3"), new Sprite(_contentManager, "Chosen\\jya_pa_2"));
                    button.Tag = randNum.ToString();
                    break;
                }
            }

            return button;
        }

        public override void Update(GameTime gametime)
        {
            if (_start)
            {
                //Play
                EffectManager.GetInstance().Play(eSoundId.m_duel1);

                _start = false;
            }

            base.Update(gametime);

            foreach (var playerButton in _playerButtons)
            {
                playerButton.Update(gametime);
            }

            foreach (var botButton in _botButtons)
            {
                botButton.Update(gametime);
            }

            //Thêm/bớt khi player chưa chọn
            if (true)
            {
                _timer += gametime.ElapsedGameTime.Milliseconds;

                if (_playerButtons.Last().Position.X < _rightOffset + (-1) * _playerButtons.First().Sprite.Bound.Width && _playerChoose == -1)
                {
                    _timer = 0.0f;
                    float lastPos = _playerButtons.Last().Position.X;

                    _playerButtons.Add(RandomHandButton());
                    _playerButtons.Last().Position = new Vector2(lastPos + _playerButtons.Last().Sprite.Bound.Width, 350);
                    //_playerButtons.Last().Position = new Vector2(_rightOffset + (-1) * _playerButtons.First().Sprite.Bound.Width + _playerButtons.Last().Sprite.Bound.Width, 350);
                    //_playerButtons.Last().AddMoveTo(new MoveTo(10.0f, new Vector2((-1)*_playerButtons.First().Sprite.Bound.Width, 350)));
                    float time = (_playerButtons.Last().Position.X - _leftOffset + _playerButtons.First().Sprite.Bound.Width) / _speedMove;

                    _playerButtons.Last().AddMoveTo(new MoveTo(time, new Vector2(_leftOffset + (-1) * _playerButtons.First().Sprite.Bound.Width, 350)));

                    _playerButtons.Last().ButtonEventWithSender += OnButtonEventWithSender;
                }

                if (_playerButtons.Count > 0 && _playerButtons.First().Position.X <= _leftOffset + (-1)*_playerButtons.First().Sprite.Bound.Width)
                {
                    _playerButtons.RemoveAt(0);
                }

                if (_botButtons.Last().Position.X > _leftOffset && _botChoose == -1)
                {
                    float lastPos = _botButtons.Last().Position.X;

                    _botButtons.Add(new Button(new Sprite(_contentManager, "Chosen\\jya_eye_1"),
                        new Sprite(_contentManager, "Chosen\\jya_eye_3"),
                        new Sprite(_contentManager, "Chosen\\jya_eye_2")));
                    //_botButtons.Last().Position = new Vector2(_leftOffset + (-1)*_playerButtons.First().Sprite.Bound.Width, 50);
                    _botButtons.Last().Position = new Vector2(lastPos + (-1) * _playerButtons.First().Sprite.Bound.Width, 50);
                    _botButtons.Last().Origin = new Vector2(_botButtons.Last().Sprite.Bound.Width, _botButtons.Last().Sprite.Bound.Height);
                    _botButtons.Last().Rotation = (float)Math.PI; 

                    float time = (_rightOffset - _botButtons.Last().Position.X) / _speedMove;
                    _botButtons.Last().AddMoveTo(new MoveTo(time, new Vector2(_rightOffset, 50)));

                }

                if (_botButtons.Count > 0 && _botButtons.First().Position.X >= _rightOffset)
                {
                    _botButtons.RemoveAt(0);
                }
            }

            _myTimer.Update(gametime);

            //Go Button
            
            if (_goFirstPlayer ==  0)
            {
                if (_myTimer.StopWatch(1000))
                {
                    _myTimer.ResetStopWatch();
                    ChooseAgain();
                }
            }
            else if (_goFirstPlayer != -1 && _playerGoBtn != null)
            {
                _playerGoBtn.Update(gametime);

                if (_playerChooseIndex != -1)
                {
                    if (_myTimer.StopWatch(2000.0f))
                    {
                        _playerGoBtn.Tag = "Show";
                        //_myTimer.ResetStopWatch();
                        _playerGoBtn.AddScaleTo(new ScaleTo(0.25f, new Vector2(1.0f, 1.0f)));
                        EffectManager.GetInstance().Play(eSoundId.start);
                    }
                }
            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            
            _background.Draw(spriteBatch);
            _selectHandTitle.Draw(spriteBatch);

            foreach (var playerButton in _playerButtons)
            {
                playerButton.Draw(spriteBatch);
            }

            foreach (var botButton in _botButtons)
            {
                botButton.Draw(spriteBatch);
            }

            if (_playerGoBtn != null && _playerGoBtn.Tag == "Show")
            {
                _playerGoBtn.Draw(spriteBatch);
            }
            
            spriteBatch.End();
            base.Draw(spriteBatch);
        }

        private void ChooseAgain()
        {
            EffectManager.GetInstance().Play(eSoundId.turn_change);

            foreach (var playerButton in _playerButtons)
            {
                playerButton.Enable = true;
                playerButton.Selected = false;

                playerButton.Sprite.AddFade(new Fade(0.0f, 0.5f, 1.0f));
                float time = (playerButton.Position.X - _leftOffset + playerButton.Sprite.Bound.Width) / _speedMove;
                playerButton.AddMoveTo(new MoveTo(time, new Vector2(_leftOffset + (-1) * playerButton.Sprite.Bound.Width, 350)));
            }

            
            foreach (var botButton in _botButtons)
            {
                botButton.Sprite.Texture = new Sprite(_contentManager, "Chosen\\jya_eye_1").Texture;

                botButton.Sprite.AddFade(new Fade(0.0f, 0.5f, 1.0f));

                float time = (_rightOffset - botButton.Position.X) / _speedMove;
                botButton.AddMoveTo(new MoveTo(time, new Vector2(_rightOffset, 50)));
            }

            _playerChoose = -1;
            _playerChooseIndex = -1;
            _botChoose = -1;
            _botChooseIndex = -1;
            _goFirstPlayer = -1;
        }

        private void GotoPlayScene()
        {
            switch (_goFirstPlayer)
            {
                case 1:
                {
                    SceneManager.GetInstance().ReplaceScene(new PlayScene(ePlayerId.PLAYER));
                    break;
                }
                case 2:
                {
                    SceneManager.GetInstance().ReplaceScene(new PlayScene(ePlayerId.COMPUTER));
                    break;
                }
            }
        }

        public void PlayerChosenAnimation(Button button)
        {
            float centerPosition = 400 - button.Sprite.Bound.Width/2;
            button.AddMoveTo(new MoveTo(0.5f, new Vector2(centerPosition, button.Position.Y)));

            int chosenIndex = _playerButtons.IndexOf(button);

            foreach (Button playerButton in _playerButtons)
            {
                playerButton.Enable = false;
                if (playerButton != button)
                {
                    int index = _playerButtons.IndexOf(playerButton);
                    playerButton.Sprite.AddFade(new Fade(0.5f, 1.0f, 0.5f));
                    playerButton.Sprite.AddMoveTo(new MoveTo(0.5f, new Vector2(centerPosition + (index - chosenIndex) * button.Sprite.Bound.Width, button.Position.Y)));
                }
            }
        }

        public void BotChosenAnimation(Button button)
        {
            float centerPosition = 400 - button.Sprite.Bound.Width / 2;

            button.AddMoveTo(new MoveTo(0.5f, new Vector2(centerPosition, button.Position.Y)));
            int chosenIndex = _botButtons.IndexOf(button);

            foreach (Button botButton in _botButtons)
            {
                botButton.Enable = false;
                if (botButton != button)
                {
                    int index = _botButtons.IndexOf(botButton);
                    botButton.Sprite.AddFade(new Fade(0.5f, 1.0f, 0.5f));
                    botButton.Sprite.AddMoveTo(new MoveTo(0.5f, new Vector2(centerPosition + (chosenIndex - index) * button.Sprite.Bound.Width, button.Position.Y)));
                }
            }
        }
    }
}
