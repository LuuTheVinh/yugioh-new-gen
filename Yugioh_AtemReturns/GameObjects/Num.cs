using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Yugioh_AtemReturns.GameObjects
{
    delegate void del_Action();
    class Num :MyObject
    {
        del_Action Action;
        protected Vector2 m_position;
        protected string m_s_number;
        private bool m_isShow;
        
        public bool IsShow
        {
            get { return m_isShow; }
            set { m_isShow = value; }
        }
        public override Vector2 Position
        {
            get
            {
                return this.m_position;
            }
            set
            {
                this.m_position = value;
            }
        }
        public int FrameWidth
        {
            get { return this.Sprite.CurFrameW; }
            set { this.Sprite.CurFrameW = value; }
        }
        public Num(ContentManager _content, Vector2 _position, SpriteID _spriteid)
            :base(_content,ID.NUM,_spriteid)
        {
            this.m_position = _position;
            this.Sprite.Frame = new Rectangle(0, 0, Sprite.Texture.Width / 10, Sprite.Texture.Height);
            this.m_timer = new Timer();
            this.m_s_number = String.Empty;
            Action = null;
        }
        public void Show(int _number, Vector2? _position =null)
        {
            this.m_position = _position.HasValue ? _position.Value : this.Position;
            this.m_s_number =Convert.ToString(_number);
            this.IsShow = true;
            
            //this.Action += MoireMoition;
        }
        public void Show()
        {
            this.IsShow = true;
        }
        public void MoireMoition()
        {
            if (this.m_timer.TimeSlice(66))
            {
                m_a_position = new Vector2[m_a_position.Length];
                for (int i = 0; i < m_a_position.Length; i++)
                {
                    m_a_position[i] = new Vector2(
                        this.m_position.X + this.Sprite.Frame.Width * i,
                       this.m_position.Y + (int)(8 * Math.Cos(80* (m_timer.TotalGameTime / Math.PI)* (i + 1))));
                }
            }
            if (m_timer.StopWatch(1000))
            {
                m_timer.ResetStopWatch();
                m_a_position = null;
                this.Action -= MoireMoition;
            }
        }
        public void CountDown()
        {
            this.m_s_number = Convert.ToString(Int32.Parse(m_s_number) - 100);
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (this.Sprite.IsAction == false)
                Sprite.Color = Color.White;
            //if (this.Action != null)
            //    this.Action();
            //this.MoireMoition();

           // this.Sprite.Position += new Vector2(this.Sprite.Frame.Width, 0);
        }
        const int A = 8;
        private Vector2[] m_a_position;
        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch _spritebatch)
        {
            _spritebatch.Begin();
                for (int i = 0; i < m_s_number.Length; i++)
                {
                    this.Sprite.CurFrameW = Int32.Parse(m_s_number[i].ToString());
                    //this.Sprite.Position = m_a_position[i];
                    if (this.m_a_position != null)
                        this.Sprite.Position = m_a_position[i];
                    else
                    this.Sprite.Position = this.m_position+ Vector2.UnitX *( this.Sprite.Frame.Width * i);

                    this.Sprite.Draw(_spritebatch);
                }
                _spritebatch.End();
        }
    }
}
