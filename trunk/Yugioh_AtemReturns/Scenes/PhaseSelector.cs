using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yugioh_AtemReturns.GameObjects;
using Microsoft.Xna.Framework.Content;
using Yugioh_AtemReturns.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Yugioh_AtemReturns.Duelists;

namespace Yugioh_AtemReturns.Scenes
{
    class PhaseSelector
    {
        private Button[] buttons;

        public Button DrawPhaseButton
        {
            get { return buttons[0]; }
            set { buttons[0] = value; }
        }
        public Button StandbyButton
        {
            get { return buttons[1]; }
            set { buttons[1] = value; }
        }
        public Button Main1Button
        {
            get { return buttons[2]; }
            set { buttons[2] = value; }
        }
        public Button BattleButton
        {
            get { return buttons[3]; }
            set { buttons[3] = value; }
        }
        public Button Main2Button
        {
            get { return buttons[4]; }
            set { buttons[4] = value; }
        }
        public Button EndPhaseButton
        {
            get { return buttons[5]; }
            set { buttons[5] = value; }
        }

        public PhaseSelector(ContentManager _content)
        {
            buttons = new Button[]{
                            new Button(new Sprite(SpriteManager.getInstance(_content).GetSprite(SpriteID.pha_s_dr)),new Sprite(SpriteManager.getInstance(_content).GetSprite(SpriteID.pha_s_dr))),
                            new Button(new Sprite(SpriteManager.getInstance(_content).GetSprite(SpriteID.pha_s_st)), new Sprite(SpriteManager.getInstance(_content).GetSprite(SpriteID.pha_s_st))),
                            new Button(new Sprite(SpriteManager.getInstance(_content).GetSprite(SpriteID.pha_s_m1)), new Sprite(SpriteManager.getInstance(_content).GetSprite(SpriteID.pha_s_m1))),
                            new Button(new Sprite(SpriteManager.getInstance(_content).GetSprite(SpriteID.pha_s_ba)), new Sprite(SpriteManager.getInstance(_content).GetSprite(SpriteID.pha_s_ba))),
                            new Button(new Sprite(SpriteManager.getInstance(_content).GetSprite(SpriteID.pha_s_m2)), new Sprite(SpriteManager.getInstance(_content).GetSprite(SpriteID.pha_s_m2))),
                            new Button(new Sprite(SpriteManager.getInstance(_content).GetSprite(SpriteID.pha_s_en)) , (new Sprite(SpriteManager.getInstance(_content).GetSprite(SpriteID.pha_s_en))))
                     };

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].NormalImage.Frame = new Rectangle(0, 0, 25, 75);
                buttons[i].HoverImage.Frame = new Rectangle(40, 0, 25, 75);
                buttons[i].Sprite.Position = GlobalSetting.Default.Phase + GlobalSetting.Default.PhaseSpace * i;
            }
        }
        public void Update(GameTime _gameTime)
        {
            foreach (var item in buttons)
            {
                item.Update(_gameTime);
            }
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            foreach (var item in buttons)
            {
                item.Draw(_spriteBatch);
            }
        }
        public void UpdateButton(Player _player)
        {
            Rectangle CurPhase;
            Rectangle NonCurPhase;
            if (_player.isTurn == true)
            {
                CurPhase = new Rectangle(40, 0, 25, 75);
                NonCurPhase = new Rectangle(0, 0, 25, 75);
            }
            else
            {
                CurPhase = new Rectangle(40, 75, 25, 75);
                NonCurPhase = new Rectangle(0, 75, 25, 75);
            }
            if (_player.Phase == ePhase.STANDBY)
                this.StandbyButton.NormalImage.Frame = CurPhase;
            else
                this.StandbyButton.NormalImage.Frame = NonCurPhase;

            if (_player.Phase == ePhase.DRAW)
                this.DrawPhaseButton.NormalImage.Frame = CurPhase;
            else
                this.DrawPhaseButton.NormalImage.Frame = NonCurPhase;

            if (_player.Phase == ePhase.MAIN1)
                Main1Button.NormalImage.Frame = CurPhase;
            else
                Main1Button.NormalImage.Frame = NonCurPhase;

            if (_player.Phase == ePhase.BATTLE)
                this.BattleButton.NormalImage.Frame = CurPhase;
            else
                this.BattleButton.NormalImage.Frame = NonCurPhase;

            if (_player.Phase == ePhase.MAIN2)
                this.Main2Button.NormalImage.Frame = CurPhase;
            else
                this.Main2Button.NormalImage.Frame = NonCurPhase;

            if (_player.Phase == ePhase.END)
                this.EndPhaseButton.NormalImage.Frame = CurPhase;
            else
                this.EndPhaseButton.NormalImage.Frame = NonCurPhase;
        }
    }
}
