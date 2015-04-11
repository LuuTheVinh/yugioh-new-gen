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
        //MainDeck maindeck;
        private Sprite background;
        //Yugioh_AtemReturns.Cards.Traps.Trap test0;
        public static Player Player;
        public static Computer Computer;

        //Sprite _detail;
        private PhaseSelector phaseSelector;

        RasterizerState _rasterizerState;
        public static DetailSideBar DetailSideBar;
        public override void Init(Game _game)
        {
            base.Init(_game);
            background = new Sprite(Game.Content, "fie_burn");
            background.Position = new Vector2(230, 0);
            background.Depth = 0.0f;

            Player = new Player();
            Player.Init(Game.Content);

            Computer = new Computer();
            Computer.Init(Game.Content);

            Player.isTurn = true;
            Computer.isTurn = false;

            //_detail = new Sprite(SpriteManager.getInstance(_game.Content).GetSprite(SpriteID.detail));

            YNDialog = new YesNoDialog(_game.Content, "String");
            YNDialog.Position = new Vector2(
                x: this.Game.Window.ClientBounds.Center.X - YNDialog.Sprite.Bound.Width / 2,
                y: this.Game.Window.ClientBounds.Center.Y - YNDialog.Sprite.Bound.Height / 2);

            phaseSelector = new PhaseSelector(_game.Content);
            phaseSelector.EndPhaseButton.ButtonEvent += new Action(EndPhaseButton_ButtonEvent);
            phaseSelector.Main2Button.ButtonEvent += new Action(Main2Button_ButtonEvent);
            phaseSelector.BattleButton.ButtonEvent += new Action(BattleButton_ButtonEvent);

            _rasterizerState = new RasterizerState() { ScissorTestEnable = true };
            DetailSideBar = new DetailSideBar(_game.Content);
        }
        public override void Update(GameTime _gameTime)
        {
            if (this.Game.IsActive == false)
                return;
            base.Update(_gameTime);

            YNDialog.Update(_gameTime); 
            if (Player.isTurn == true)
            {
                Player.Update(_gameTime);
                Computer.Update(_gameTime);
                if (Player.isTurn == false)
                {
                    Computer.isTurn = true;
                }
            }
            else
            {
                Player.Update(_gameTime);
                Computer.Update(_gameTime);
                if (Computer.isTurn == false)
                {
                    Player.isTurn = true;
                }
            }
            if (Player.Status == ePlayerStatus.IDLE)
                this.phaseSelector.Update(_gameTime);
            this.phaseSelector.UpdateButton(Player);
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

            _spriteBatch.Begin();
            PlayScene.YNDialog.Draw(_spriteBatch);
            _spriteBatch.End();

        }


        #region Phase
        private void EndPhaseButton_ButtonEvent()
        {
            Player.Phase = ePhase.END;
        }
        private void Main2Button_ButtonEvent()
        {
            if (Player.Phase == ePhase.BATTLE)
                Player.Phase = ePhase.MAIN2;
        }
        private void BattleButton_ButtonEvent()
        {
            foreach (var item in Player.MonsterField.ListCard)
            {
                if (item.STATUS == STATUS.ATK)
                    Player.Phase = ePhase.BATTLE;
            }

        }
        #endregion // Phases
    }
}
