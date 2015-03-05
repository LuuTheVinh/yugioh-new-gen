using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace T
{
    class Sprite
    {
        #region Field
        private Texture2D m_texture;
        private Vector2 m_position;
        private Color m_color;
        private float m_depth;
        private Point m_size;
        private Vector2 m_scale;
        private float m_rotation;
        private SpriteEffects m_effect;
        private int m_curFrameW;
        private int m_maxFrameW;
        private int m_curFrameH;
        private int m_maxFrameH;
        private Vector2 m_origin;
        private Rectangle m_Frame;
        private Rectangle m_Bound;
        #endregion
        
        #region Property

        public Texture2D Texture
        {
            get{return m_texture;}
            private set{m_texture = value;}
        }
        public Vector2 Position
        {
            get{return m_position;}
            set{
                m_position = value;
                m_Bound.X = (int)value.X;
                m_Bound.Y = (int)value.Y;
            }
        }
        public Color Color
        {
            get { return m_color; }
            set { m_color = value; }
        }
        public float Depth
        {
            get { return m_depth; }
            set {
                if (m_depth > 1 || m_depth < 0)
                {
                    throw new Exception("Wrong Depth");
                }
                else
                    m_depth = value;
            }
        }
        public Point Size
        {
            get { return m_size; }
            set { m_size = value; }
        }
        public Vector2 Scale
        {
            get { return m_scale; }
            set
            {
                m_scale = value;
                m_Bound.Width =(int) (Texture.Width * value.X);
                m_Bound.Height = (int)(Texture.Height * value.Y);
            }
        }
        public float Rotation
        {
            get { return m_rotation; }
            set { m_rotation = value; }
        }
        public SpriteEffects Effect
        {
            get { return m_effect; }
            set { m_effect = value; }
        }
        public Rectangle Frame
        {
            get { return m_Frame; }
            set { m_Frame = value; }
        }
        public int CurFrameW
        {
            get { return m_curFrameW; }
            set
            {
                if (value == m_maxFrameW)
                    m_curFrameW = 0;
                m_curFrameW = value;
            }
        }
        private int MaxFrameW
        {
            get { return m_maxFrameW; }
            set { m_maxFrameW = value; }
        }
        public int CurFrameH
        {
            get { return m_curFrameH; }
            set
            {
                if (value == m_maxFrameH)
                    m_curFrameH = 0;
                m_curFrameH = value;
            }
        }
        private int MaxFrameH
        {
            get { return m_maxFrameH; }
            set { m_maxFrameH = value; }
        }
        public Vector2 Origin
        {
            get { return m_origin; }
            set { m_origin = value; }
        }
        public Rectangle Bound
        {
            get { return m_Bound; }
            set { m_Bound = value; }
        }
        #endregion

        public Sprite(ContentManager _contentManager, String _address)
        {
            Texture = LoadContent(_contentManager, _address);
            Color = Color.White;
            Depth = 1.0f;
            Size = new Point(Texture.Bounds.Width, Texture.Bounds.Height);
            Scale = Vector2.One;
            Effect = SpriteEffects.None;
            Frame = new Rectangle(0, 0, Texture.Width, Texture.Height);
            MaxFrameH = 1;
            MaxFrameW = 1;
        }

        public Sprite(Sprite _sprite)
        {
            this.Texture = _sprite.Texture;
            this.Bound = _sprite.Bound;
            this.Color = _sprite.Color;
            this.CurFrameH = _sprite.CurFrameH;
            this.CurFrameW = _sprite.CurFrameW;
            this.Depth = _sprite.Depth;
            this.Effect = _sprite.Effect;
            this.Frame = _sprite.Frame;
            this.MaxFrameH = _sprite.MaxFrameH;
            this.MaxFrameW = _sprite.MaxFrameW;
            this.Origin = _sprite.Origin;
            this.Position = _sprite.Position;
            this.Rotation = _sprite.Rotation;
            this.Scale = _sprite.Scale;
            this.Size = _sprite.Size;
        }

        private Texture2D LoadContent(ContentManager _contentManager, String _address)
        {
            return _contentManager.Load<Texture2D>(_address);
        }

        public void Draw(SpriteBatch _spritebatch)
        {
            _spritebatch.Draw(
                Texture,
                Position,
                Frame,
                Color,
                Rotation,
                Origin,
                Scale,
                Effect,
                Depth
            );
        }
    }
}
