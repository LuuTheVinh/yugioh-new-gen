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

        /// <summary>
        /// Kiểm tra sự kiện click chuột lên các nút chuyển phase
        /// </summary>
        /// <param name="_gameTime"></param>
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

        public void Update(Duelist _duelist)
        {
            if (_duelist.IsTurn == false)
                return;
            if (_duelist.DuelistID == Decks.ePlayerId.COMPUTER)
            {
                foreach (var bt in buttons)
                {
                    bt.Hovered = false;
                }
            }
            int frameY = 75 * Convert.ToInt32(_duelist.DuelistID);
            foreach (var button in buttons)
            {
                button.NormalImage.Frame = new Rectangle(0,frameY,25,75);
            }
            try
            {
                buttons[Convert.ToInt32(_duelist.Phase) - 1].NormalImage.Frame = new Rectangle(40, frameY, 25, 75);
            }
            catch { }
            
        }

    }
}
