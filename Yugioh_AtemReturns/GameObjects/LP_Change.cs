using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Yugioh_AtemReturns.Manager;

namespace Yugioh_AtemReturns.GameObjects
{
    enum eLPState
    {
        PLUS,
        SUBTRACT
    }
    class LP_Change
    {
        private Timer m_timer;
        private  Vector2 m_position;
        private Num m_numsprite;

        private Sprite Sprite
        {
            get
            {
                return this.m_numsprite.Sprite;
            }
        }
        public Vector2 Position
        {
            get
            {
                return m_position;
            }
            set
            {
                m_position = value;
            }
        }
        public LP_Change(ContentManager _content, Vector2 _position)
        {
            m_timer = new Timer();
            this.m_position = _position;
            this.m_numsprite = new Num(_content, _position, SpriteID.font_1640_damage);
        }

        public void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (m_numsprite.IsShow == false)
                return;
            m_timer.Update(gameTime);
            m_numsprite.Update(gameTime);
            if(m_timer.StopWatch(1500))
            {
                this.m_numsprite.Sprite.Color = Color.White;
                this.m_numsprite.IsShow = false;
            }
        }
        public void Init(int _number, eLPState _state)
        {
            if (_state == eLPState.PLUS)
                EffectManager.GetInstance().Play(eSoundId.life_up);
            else
                EffectManager.GetInstance().Play(eSoundId.damage);
            this.m_numsprite.Show(_number, this.m_position);
            this.m_numsprite.Sprite.Frame = new Rectangle(0, 0, 16, 40);
            this.m_numsprite.Sprite.CurFrameH = Convert.ToInt32(_state);
            m_timer.ResetStopWatch();
            ///this.m_numsprite.Sprite.AddFade(new Fade(3,1.0f,0.0f));
        }
        public void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch _spritebatch)
        {
            if (this.m_numsprite.IsShow == false)
                return;
            //base.Draw(_spritebatch);
            _spritebatch.Begin();
            this.Sprite.CurFrameW = 10;
            this.Sprite.Position = this.m_position - new Vector2(this.Sprite.Frame.Width, 0);
            this.Sprite.Draw(_spritebatch);
            this.Sprite.Position = this.m_position;
            _spritebatch.End();
            m_numsprite.Draw(_spritebatch);
        }

    }
}
