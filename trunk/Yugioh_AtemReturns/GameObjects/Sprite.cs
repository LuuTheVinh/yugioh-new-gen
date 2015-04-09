using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;

namespace Yugioh_AtemReturns.GameObjects
{
    public class Sprite
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
        private float m_Timer = 0;
        //for Transforms
        private Vector2 m_oldPosition;                                              //Move
        private bool isMoveDone = true;                                     
        private Vector2 m_Velocity;                                     
        private float m_moveDistance;
        private bool isScaleDone = true;                                            //Scale
        private Vector2 speedScale;
        private float scaleRatio;
        private float scaleValue;
        private bool isRotateDone = true;                                           //Rotate
        private float oldRotation;
        private float rotateValue;
        private float speedRotate;
        #endregion
        
        #region Properties

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
                m_Bound.X = (int)value.X - (int)Origin.X;
                m_Bound.Y = (int)value.Y - (int)Origin.Y;
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
                //m_origin.X = m_origin.X * value.X;
                //m_origin.Y = m_origin.Y * value.Y;
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
            set { m_Frame = value;
            this.MaxFrameH = this.m_texture.Height / value.Height;
            this.MaxFrameW = this.m_texture.Width / value.Width;
            this.m_Bound.Width = value.Width;
            this.m_Bound.Height = value.Height;
            }
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
            set { 
                m_origin = value;
                m_Bound.X = (int)Position.X - (int)value.X;
                m_Bound.Y = (int)Position.Y - (int)value.Y;
            }
        }
        public Rectangle Bound
        {
            get { return m_Bound; }
            set { m_Bound = value; }
        }

        public bool IsDone {
            get { return (isMoveDone && isScaleDone); } 
        }
        #endregion

        public Sprite(ContentManager contentManager, String address)
        {
            Texture = LoadContent(contentManager, address);
            Color = Color.White;
            Depth = 1.0f;
            Size = new Point(Texture.Bounds.Width, Texture.Bounds.Height);
            Scale = Vector2.One;
            Effect = SpriteEffects.None;
            Frame = new Rectangle(0, 0, Texture.Width, Texture.Height);
            MaxFrameH = 1;
            MaxFrameW = 1;
        }

        public Sprite(Sprite sprite)
        {
            Texture = sprite.Texture;
            Bound = sprite.Bound;
            Color = sprite.Color;
            CurFrameH = sprite.CurFrameH;
            CurFrameW = sprite.CurFrameW;
            Depth = sprite.Depth;
            Effect = sprite.Effect;
            Frame = sprite.Frame;
            MaxFrameH = sprite.MaxFrameH;
            MaxFrameW = sprite.MaxFrameW;
            Origin = sprite.Origin;
            Position = sprite.Position;
            Rotation = sprite.Rotation;
            Scale = sprite.Scale;
            Size = sprite.Size;
        }

        private Texture2D LoadContent(ContentManager contentManager, String address)
        {
            return contentManager.Load<Texture2D>(address);
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

        public virtual void Update(GameTime gameTime)
        {
            
        }

        #region Sprite Transforms
        public void MoveTo(GameTime gameTime, float time, Vector2 nextposition)
        {
            if(isMoveDone)
            {
                isMoveDone = false;
                var velocityX = (nextposition.X - Position.X) / (time * 60); //60 là fps
                var velocityY = (nextposition.Y - Position.Y) / (time * 60);
                m_Velocity = new Vector2(velocityX, velocityY);
                
                m_moveDistance = (float)Math.Sqrt(
                                            (nextposition.X - Position.X) * (nextposition.X - Position.X) +
                                            (nextposition.Y - Position.Y) * (nextposition.Y - Position.Y)
                                        );
                m_oldPosition = Position;
            }

            var distance = (float)Math.Sqrt(((Position.X - m_oldPosition.X) * (Position.X - m_oldPosition.X) +
                                        (Position.Y - m_oldPosition.Y) * (Position.Y - m_oldPosition.Y)));
            
            if (m_moveDistance >= distance && !isMoveDone)
            {
                //Kiểm tra lần di chuyển cuối cùng có vượt ra khỏi vị trí đích hay không
                if ((float)Math.Sqrt(m_Velocity.X * m_Velocity.X + m_Velocity.Y * m_Velocity.Y) > (m_moveDistance - distance))
                {
                    //Nếu có thì gán vận tốc bằng quãng đường còn lại
                    m_Velocity.X = nextposition.X - Position.X;
                    m_Velocity.Y = nextposition.Y - Position.Y;
                }
                Position = new Vector2(Position.X + m_Velocity.X, Position.Y + m_Velocity.Y);
            }
            else
            {
                isMoveDone = true;
                m_oldPosition = Position;
            }
        }
        
        public void MoveBy(GameTime gameTime, float time, Vector2 distance)
        {
            var nextposition = Position + distance;
            MoveTo(gameTime, time, nextposition);
        }

        public void ScaleTo(GameTime gameTime, float time, Vector2 newscale)
        {
            if(isScaleDone)
            {
                speedScale.X = (newscale.X - Scale.X) / (time * 60);
                speedScale.Y = (newscale.Y - Scale.Y) / (time * 60);

                scaleRatio = (float)Math.Sqrt(
                                             (newscale.X - Scale.X) * (newscale.X - Scale.X) +
                                             (newscale.Y - Scale.Y) * (newscale.Y - Scale.Y)
                                             );

                isScaleDone = false;
            }

            scaleValue += (float)Math.Sqrt(speedScale.X * speedScale.X + speedScale.Y*speedScale.Y);

            if(scaleRatio >= scaleValue && !isScaleDone)
            {
                if((Math.Abs(Scale.X - newscale.X) < speedScale.X) && (Math.Abs(Scale.Y - newscale.Y) < speedScale.Y))
                {
                    speedScale.X = newscale.X - Scale.X;
                    speedScale.Y = newscale.Y - Scale.Y;
                }
                Scale = new Vector2(Scale.X + speedScale.X, Scale.Y + speedScale.Y);
            }
            else
            {
                isScaleDone = true;
            }
        }

        public void ScaleBy(GameTime gameTime, float time, Vector2 scaleby)
        {
            Vector2 newscale = Scale + scaleby;
            ScaleTo(gameTime, time, newscale);
        }

        public void RotateTo(GameTime gameTime, float time, float rotateto)
        {
            if (isRotateDone)
            {
                isRotateDone = false;
                oldRotation = Rotation;
                speedRotate = (rotateto - Rotation) / (time * 60);
                rotateValue = rotateto - Rotation;
            }

            float rotated = Rotation - oldRotation;

            if (rotateValue >= rotated && !isRotateDone)
            {
               if(Math.Abs(rotateto - rotated) < speedRotate)
               {
                   speedRotate = rotateto - Rotation;
               }
               Rotation += speedRotate;
            }
            else
            {
                isRotateDone = true;
            }
        }

        public void RotateBy(GameTime gameTime, float time, float rotateby)
        {
            var rotateto = Rotation + rotateby;
            RotateTo(gameTime, time, rotateto);
        }

        #endregion
    }
}
