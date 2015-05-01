using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Yugioh_AtemReturns.Manager;

namespace Yugioh_AtemReturns.GameObjects
{
    public abstract class MyObject
    {
        private Sprite m_Sprite;
        private ID m_Id;
        private STATUS m_Status;
        protected Timer m_timer;
        #region Properties
        public Sprite Sprite
        {
            get { return m_Sprite; }
            set { m_Sprite = value; }
        }
        public ID ID
        {
            get { return m_Id; }
            set { m_Id = value; }
        }
        public virtual STATUS Status
        {
            get { return m_Status; }
            set { m_Status = value; }
        }
        public virtual Vector2 Position
        {
            get { return this.Sprite.Position; }
            set
            {
                if (this.Sprite != null)
                    this.Sprite.Position = value;
            }
        }
        public virtual Vector2 Origin
        {
            get { return this.Sprite.Origin; }
            set
            {
                if (this.Sprite != null)
                    this.Sprite.Origin = value;
            }
        }
        public virtual Vector2 Scale
        {
            get { return this.Sprite.Scale; }
            set
            {
                if (this.Sprite != null)
                    this.Sprite.Scale = value;
            }
        }
        public virtual float Rotation
        {
            get { return this.Sprite.Rotation; }
            set
            {
                if (this.Sprite != null)
                    this.Sprite.Rotation = value;
            }
        }
        
        #endregion

        public MyObject(ContentManager _content, ID _id, SpriteID _spriteid)
        {
            Sprite = new Sprite(SpriteManager.getInstance(_content).GetSprite(_spriteid));
            Status = STATUS.NORMAL;
            ID = _id;
            m_timer = new Timer();

        }
        protected MyObject()
        {
            Sprite = null;
            Status = STATUS.NORMAL;
            m_timer = new Timer();
        }
        protected virtual void SetFrame()
        {
            Sprite.Frame = new Rectangle(
                Sprite.CurFrameW * Sprite.Size.X,
                Sprite.CurFrameH * Sprite.Size.Y,
                Sprite.Size.X,
                Sprite.Size.Y);
        }

        public virtual void Update(GameTime gameTime)
        {
            //
            Sprite.Update(gameTime);
            m_timer.Update(gameTime);
        }

        public virtual void Draw(SpriteBatch _spritebatch)
        {
            Sprite.Draw(_spritebatch);
        }

        public virtual void AddMoveTo(MoveTo moveto)
        {
            Sprite.AddMoveTo(moveto);
        }

        public virtual void AddScaleTo(ScaleTo scaleto)
        {
            Sprite.AddScaleTo(scaleto);
        }

        public virtual void AddRotateTo(RotateTo rotateto)
        {
            Sprite.AddRotateTo(rotateto);
        }

    }
}
