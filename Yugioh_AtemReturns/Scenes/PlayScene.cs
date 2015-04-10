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
        private Player player;
        private Computer computer;

        //Sprite _detail;
        private PhaseSelector phaseSelector;
        public override void Init(Game _game)
        {
            base.Init(_game);
            background = new Sprite(Game.Content, "fie_burn");
            background.Position = new Vector2(230, 0);
            background.Depth = 0.0f;

            player = new Player();
            player.Init(Game.Content);

            computer = new Computer();
            computer.Init(Game.Content);

            player.isTurn = true;
            computer.isTurn = false;

            //_detail = new Sprite(SpriteManager.getInstance(_game.Content).GetSprite(SpriteID.detail));

            YNDialog = new YesNoDialog(_game.Content, "String");
            YNDialog.Position = new Vector2(
                x: this.Game.Window.ClientBounds.Center.X - YNDialog.Sprite.Bound.Width / 2,
                y: this.Game.Window.ClientBounds.Center.Y - YNDialog.Sprite.Bound.Height / 2);

            phaseSelector = new PhaseSelector(_game.Content);
            phaseSelector.EndPhaseButton.ButtonEvent += new Action(EndPhaseButton_ButtonEvent);
            phaseSelector.Main2Button.ButtonEvent += new Action(Main2Button_ButtonEvent);
            phaseSelector.BattleButton.ButtonEvent += new Action(BattleButton_ButtonEvent);
        }
        public override void Update(GameTime _gameTime)
        {
            if (this.Game.IsActive == false)
                return;
            base.Update(_gameTime);

            YNDialog.Update(_gameTime); 
            if (player.isTurn == true)
            {
                this.player.Update(_gameTime);
                this.computer.Update(_gameTime);
                if (player.isTurn == false)
                {
                    computer.isTurn = true;
                }
            }
            else
            {
                this.player.Update(_gameTime);
                this.computer.Update(_gameTime);
                if (computer.isTurn == false)
                {
                    player.isTurn = true;
                }
            }
            if (this.player.Status == ePlayerStatus.IDLE)
                this.phaseSelector.Update(_gameTime);
            this.phaseSelector.UpdateButton(this.player);
        }

        public override void Draw(SpriteBatch _spriteBatch)
        {

            _spriteBatch.Begin();

            base.Draw(_spriteBatch);
            background.Draw(_spriteBatch);
            //_detail.Draw(_spriteBatch);
            this.phaseSelector.Draw(_spriteBatch);
            //test0.Draw(spriteBatch);
            _spriteBatch.End();

            player.Draw(_spriteBatch);
            computer.Draw(_spriteBatch);

            _spriteBatch.Begin();
            PlayScene.YNDialog.Draw(_spriteBatch);
            _spriteBatch.End();

        }


        #region Phase
        private void EndPhaseButton_ButtonEvent()
        {
            this.player.Phase = ePhase.END;
        }
        private void Main2Button_ButtonEvent()
        {
            if (this.player.Phase == ePhase.BATTLE)
                this.player.Phase = ePhase.MAIN2;
        }
        private void BattleButton_ButtonEvent()
        {
            foreach (var item in player.MonsterField.ListCard)
            {
                if (item.STATUS == STATUS.ATK)
                    this.player.Phase = ePhase.BATTLE;
            }

        }
        #endregion // Phases
    }
}
