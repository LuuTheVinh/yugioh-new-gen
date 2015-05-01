using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yugioh_AtemReturns.GameObjects;
using Yugioh_AtemReturns.Duelists;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Yugioh_AtemReturns.Manager;

namespace Yugioh_AtemReturns.Scenes
{

    class PlayScene : Scene
    {
        public static YesNoDialog YNDialog;
        private Sprite background;
        public static Player Player;
        public static Computer Computer;
        private PhaseSelector phaseSelector;

        RasterizerState _rasterizerState;
        public static DetailSideBar DetailSideBar;
        public static BattlePhase battlePhase;

        ePlayerId first;

        public static int TurnCounter
        {
            get;
            set;
        }
        public PlayScene(ePlayerId _first)
        {
            this.first = _first;
            TurnCounter = 0;
        }
        public override void Init(Game _game)
        {
            base.Init(_game);
            background = new Sprite(Game.Content, "fie_burn");
            background.Position = new Vector2(230, 0);
            background.Depth = 0.0f;


            Player = new Player(_game.Content);
            Player.Init(Game.Content);

            Computer = new Computer();
            Computer.Init(Game.Content);

            switch (first)
            {
                case ePlayerId.PLAYER:
                    Player.IsTurn = true;
                    Computer.IsTurn = false;
                    break;
                case ePlayerId.COMPUTER:
                    Player.IsTurn = false;
                    Computer.IsTurn = true;
                    break;
                default:
                    break;
            }


            YNDialog = new YesNoDialog(_game.Content, "String");
            YNDialog.Position = new Vector2(
                x: this.Game.Window.ClientBounds.Center.X - YNDialog.Sprite.Bound.Width / 2,
                y: this.Game.Window.ClientBounds.Center.Y - YNDialog.Sprite.Bound.Height / 2);

            phaseSelector = new PhaseSelector(_game.Content);
            phaseSelector.DrawPhaseButton.ButtonEvent += new Action(DrawPhaseButton_ButtonEvent);
            phaseSelector.StandbyButton.ButtonEvent += new Action(StandbyButton_ButtonEvent);
            phaseSelector.Main1Button.ButtonEvent +=new Action(Main1Button_ButtonEvent);
            phaseSelector.EndPhaseButton.ButtonEvent += new Action(EndPhaseButton_ButtonEvent);
            phaseSelector.Main2Button.ButtonEvent += new Action(Main2Button_ButtonEvent);
            phaseSelector.BattleButton.ButtonEvent += new Action(BattleButton_ButtonEvent);

            _rasterizerState = new RasterizerState() { ScissorTestEnable = true };
            DetailSideBar = new DetailSideBar(_game.Content);
            battlePhase = new BattlePhase(_game.Content);
        }
        public override void Update(GameTime _gameTime)
        {
            if (this.Game.IsActive == false)
                return;
            if (Player.LifePoint == 0 || Computer.LifePoint == 0)
                SceneManager.GetInstance().AddScene(new MenuScene());
            base.Update(_gameTime);

            YNDialog.Update(_gameTime);
            if (Player.IsTurn == true)
            {
             
                Player.Update(_gameTime); //
                Computer.Update(_gameTime);///s
                
                    
                if (Player.IsTurn == false)
                {
                    Computer.IsTurn = true;
                }

                if (Player.Phase != ePhase.STARTUP)
                {
                    if (Player.Status == ePlayerStatus.IDLE)
                        this.phaseSelector.Update(_gameTime);
                }

            }
            else
            {
                Player.Update(_gameTime);//
                Computer.Update(_gameTime);// 4 chỗ cmt này không được để ở ngoài if
                if (Computer.IsTurn == false)
                {
                    Player.IsTurn = true;
                }
            }

            this.phaseSelector.Update(Player);
            this.phaseSelector.Update(Computer);
            battlePhase.Update(Player, Computer);
            battlePhase.Update(_gameTime);
            DetailSideBar.Update(_gameTime);
        }

        public override void Draw(SpriteBatch _spriteBatch)
        {

       //     _spriteBatch.Begin();

            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend,
                      null, null, _rasterizerState);

            base.Draw(_spriteBatch);
            background.Draw(_spriteBatch);

            DetailSideBar.Draw(_spriteBatch);
            this.phaseSelector.Draw(_spriteBatch);

            _spriteBatch.End();


            Player.Draw(_spriteBatch);
            Computer.Draw(_spriteBatch);
            battlePhase.Draw(_spriteBatch);
            _spriteBatch.Begin();

            PlayScene.YNDialog.Draw(_spriteBatch);
            
            _spriteBatch.End();

        }


        #region Phase

        private void DrawPhaseButton_ButtonEvent()
        {

   //         phaseSelector.DrawPhaseButton.Selected = false;
        }
        private void StandbyButton_ButtonEvent()
        {
      //      phaseSelector.StandbyButton.Selected = false;
        }
        private void Main1Button_ButtonEvent()
        {
      //      phaseSelector.Main1Button.Selected = false;
        }
        private void BattleButton_ButtonEvent()
        {
            if (TurnCounter <= 2)
                return;
           // phaseSelector.BattleButton.Selected = false;
            if (Player.Phase != ePhase.MAIN1)
                return;
            if (Player.MonsterField.CanAttack == false)
                return;
            battlePhase.Begin(Player, Computer,ePlayerId.PLAYER);
            Player.Phase = ePhase.BATTLE;
        }
        private void Main2Button_ButtonEvent()
        {
            if (Player.Phase == ePhase.BATTLE)
            {
                Player.Phase = ePhase.MAIN2;
                return;
            }
  //          phaseSelector.Main2Button.Selected = false;
        }

        private void EndPhaseButton_ButtonEvent()
        {
            Player.Phase = ePhase.END;
        //    phaseSelector.EndPhaseButton.Selected = false;
        }
        #endregion // Phases
    }
}
