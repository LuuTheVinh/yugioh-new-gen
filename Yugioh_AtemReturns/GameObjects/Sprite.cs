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
        //Move
        private Vector2 m_oldPosition;                                              
        private Vector2 m_Velocity;                                     
        private float m_moveDistance;                                               
        //Scale
        private Vector2 speedScale;
        private Vector2 scaleTotalValue;
        private Vector2 scaledValue;                                                   
        //Rotate
        private float oldRotation;
        private float rotationTotalValue;
        private float speedRotate;
        //FADE
        private float speedFade;
        private float fadeRatio;
        private float currentFadePercent;
        //
        private List<MoveTo> _moveList = new List<MoveTo>();
        private List<RotateTo> _rotateList = new List<RotateTo>();
        private List<ScaleTo> _scaleList = new List<ScaleTo>();
        private List<Fade> _fadeList = new List<Fade>(); 
        #endregion

        public bool IsAction
        {
            get {
                return (_moveList.Any() || _rotateList.Any() || _scaleList.Any() || _fadeList.Any());
                }
        }//

        LinkedList<Animation> _waitList = new LinkedList<Animation>();

        #region Properties

        public Texture2D Texture
        {
            get{return m_texture;}
            set{m_texture = value;}
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
            set 
            { 
                m_Frame = value;
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
                else
                    m_curFrameW = value;
                this.Frame = new Rectangle(value * this.Frame.Width, this.Frame.Y, this.Frame.Width, this.Frame.Height);
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
                else
                    m_curFrameH = value;
                this.Frame = new Rectangle(this.Frame.X, value * this.Frame.Height, this.Frame.Width, this.Frame.Height);
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
            Depth = sprite.Depth;
            Effect = sprite.Effect;
            Frame = sprite.Frame;
            MaxFrameH = sprite.MaxFrameH;
            MaxFrameW = sprite.MaxFrameW;
            CurFrameH = sprite.CurFrameH;
            CurFrameW = sprite.CurFrameW; 
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

        //TEST
        public void SetSpriteWithName(ContentManager contentManager, String filename)
        {
            Texture = LoadContent(contentManager, filename);
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(
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

		//mới thêm
        public void WaitFor(Sprite _waitsprite)
        {
            if (_waitsprite._fadeList.Any())
                _waitList.Concat(_waitsprite._fadeList);
            if (_waitsprite._moveList.Any())
                _waitList.Concat(_waitsprite._moveList);
            if (_waitsprite._rotateList.Any())
                _waitList.Concat(_waitsprite._rotateList);
            if (_waitsprite._scaleList.Any())
                _waitList.Concat(_waitsprite._scaleList);
        }
		//
        public void WaitFor(Animation _waitanimation)
        {
            _waitList.AddLast(_waitanimation);
        }
        public virtual void Update(GameTime gameTime)
        {
            //THỰC HIỆN TỪNG CÁI ANIMATION TRONG CÁC LIST
            //MOVE TO
            if (_waitList.Any(animation => animation.IsDoing)) // hình như cái này là lamda :D
                return;
            _waitList.Clear();//

            if (_moveList.Any())
            {
                var first = _moveList.First();

                if (first.IsDone)
                {
                    _moveList.Remove(first);
                }
                else
                {
                    this.RunMoveTo(ref first);
                }
            }

            //ROTATE TO
            if (_rotateList.Any())
            {
                var first = _rotateList.First();

                if (first.IsDone)
                {
                    _rotateList.Remove(first);
                }
                else
                {
                    this.RunRotateTo(ref first);
                }
            }

            //SCALE TO
            if (_scaleList.Any())
            {
                var first = _scaleList.First();

                if (first.IsDone)
                {
                    _scaleList.Remove(first);
                }
                else
                {
                    this.RunScaleTo(ref first);
                }
                
            }

            //FADE
            if (_fadeList.Any())
            {
                var first = _fadeList.First();

                if (first.IsDone)
                {
                    _fadeList.Remove(first);
                }
                else
                {
                    this.RunFade(ref first);
                }
            }
        }

        #region Sprite Transforms

        public virtual void AddMoveTo(MoveTo moveto)
        {
            _moveList.Add(moveto);
        }

        private void RunMoveTo(ref MoveTo moveto)
        {
            float time = moveto.Time;
            Vector2 nextposition = moveto.NextPosition;

            if (time == 0)
            {
                Position = nextposition;
                moveto.IsDone = true;
                moveto.IsDoing = false;
                return;
            }

            if(!moveto.IsDoing)
            {
                moveto.IsDoing = true;
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

            if (m_moveDistance >= distance && !moveto.IsDone)
            {
                //Kiểm tra lần di chuyển cuối cùng có vượt ra khỏi vị trí đích hay không
                if ((float)Math.Sqrt(m_Velocity.X * m_Velocity.X + m_Velocity.Y * m_Velocity.Y) >= Math.Abs(m_moveDistance - distance))
                {
                    //Nếu có thì gán vận tốc bằng quãng đường còn lại
                    //m_Velocity.X = nextposition.X - Position.X;
                    //m_Velocity.Y = nextposition.Y - Position.Y;
                    Position = new Vector2(nextposition.X, nextposition.Y);
                    moveto.IsDone = true;
                    moveto.IsDoing = false;
                }
                else
                {
                    Position = new Vector2(Position.X + m_Velocity.X, Position.Y + m_Velocity.Y);
                }
                
            }
            else
            {
                moveto.IsDone = true;
                moveto.IsDoing = false;
            }
        }

        public virtual void AddScaleTo(ScaleTo scaleto)
        {
            
            _scaleList.Add(scaleto);
        }
        
        private void RunScaleTo(ref ScaleTo scaleto)
        {
            var time = scaleto.Time;
            var newscale = scaleto.NextScale;

            if (time == 0)
            {
                this.Scale = newscale;
                scaleto.IsDone = true;
                scaleto.IsDoing = false;
                return;
            }
            
            if(!scaleto.IsDoing)
            {
                scaleto.IsDoing = true;
                speedScale.X = (newscale.X - Scale.X) / (time * 60);
                speedScale.Y = (newscale.Y - Scale.Y) / (time * 60);

                scaleTotalValue.X = Math.Abs(newscale.X - Scale.X);
                scaleTotalValue.Y = Math.Abs(newscale.Y - Scale.Y);

                scaledValue = Vector2.Zero;
            }

            if(!scaleto.IsDone)
            {
                if ((scaleTotalValue.X >= scaledValue.X))
                {
                    if (Math.Abs(scaleTotalValue.X - scaledValue.X) < Math.Abs(speedScale.X))
                    {
                        Scale = new Vector2(newscale.X, Scale.Y);
                    }
                    else
                    {
                        Scale = new Vector2(Scale.X + speedScale.X, Scale.Y);
                        scaledValue.X += Math.Abs(speedScale.X);
                    }

                }

                if (scaleTotalValue.Y >= scaledValue.Y)
                {
                    if (Math.Abs(scaleTotalValue.Y - scaledValue.Y) < Math.Abs(speedScale.Y))
                    {
                        Scale = new Vector2(Scale.X, newscale.Y);
                    }
                    else
                    {
                        Scale = new Vector2(Scale.X, Scale.Y + speedScale.Y);
                        scaledValue.Y += Math.Abs(speedScale.Y);
                    }
                        
                }

                if (Scale.X == newscale.X && Scale.Y == newscale.Y)
                {
                     scaleto.IsDone = true;
                     scaleto.IsDoing = false;
                }
            }
        }

        public virtual void AddRotateTo(RotateTo rotateto)
        {
            _rotateList.Add(rotateto);
        }

        private void RunRotateTo(ref RotateTo newrotation)
        {
            var time = newrotation.Time;
            var rotateto = newrotation.NextRotation * (float)Math.PI / 180;

            if (time == 0)
            {
                this.Rotation = rotateto;
                newrotation.IsDone = true;
                newrotation.IsDoing = false;
                return;
            }

            if (!newrotation.IsDoing)
            {
                newrotation.IsDoing = true;
                oldRotation = Rotation;
                speedRotate = (rotateto - Rotation) / (time * 60);
                rotationTotalValue = Math.Abs(rotateto - Rotation);
            }

            var rotated = Math.Abs(Rotation - oldRotation);

            if (rotationTotalValue >= rotated && !newrotation.IsDone)
            {
                if (Math.Abs(rotationTotalValue - rotated) < Math.Abs(speedRotate))
                {
                    Rotation = rotateto;
                    newrotation.IsDone = true;
                    newrotation.IsDoing = false;
                }
                else
                {
                    Rotation += speedRotate;
                }
            }
            else
            {
                newrotation.IsDone = true;
                newrotation.IsDoing = false;
            }
        }

        public virtual void AddFade(Fade fade)
        {
            _fadeList.Add(fade);
        }

        private void RunFade(ref Fade newfade)
        {
            if (newfade.Time == 0)
            {
                this.Color = Color.White * newfade.ToPercent;
                newfade.IsDone = true;
                newfade.IsDoing = false;
                return;
            }

            if (!newfade.IsDoing)
            {
                currentFadePercent = ((float)this.Color.A / 255);
                if(newfade.FromPercent != currentFadePercent)
                {
                    newfade.FromPercent = currentFadePercent;
                }

                float time = newfade.Time;
                newfade.IsDoing = true;
                speedFade = (newfade.ToPercent - newfade.FromPercent) / (time * 60);
                fadeRatio = (newfade.ToPercent - newfade.FromPercent);
                if (this.Color == new Color(0,0,0,0))
                {
                    this.Color = new Color(1,1,1,1);
                }
            }

            if (Math.Abs(fadeRatio) >= Math.Abs(currentFadePercent - newfade.FromPercent) && !newfade.IsDone)
            {
                if (Math.Abs(newfade.ToPercent - Math.Abs(currentFadePercent)) < Math.Abs(speedFade))
                {
                    this.Color = Color.White * newfade.ToPercent;
                    newfade.IsDone = true;
                    newfade.IsDoing = false;
                }
                else
                {
                    currentFadePercent += speedFade;
                    this.Color = Color.White * (currentFadePercent);
                }
            }
            else
            {
                newfade.IsDone = true;
                newfade.IsDoing = false;
            }
        }

        #endregion
    }
}
