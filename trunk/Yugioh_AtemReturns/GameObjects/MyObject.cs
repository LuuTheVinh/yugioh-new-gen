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

        }
        protected MyObject()
        {
            Sprite = null;
            Status = STATUS.NORMAL;
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
        }

        public virtual void Draw(SpriteBatch _spritebatch)
        {
            Sprite.Draw(_spritebatch);
        }

        public virtual void MoveTo(GameTime gametime, float time, Vector2 position)
        {
            Sprite.MoveTo(gametime, time, position);
        }

        public virtual void MoveBy(GameTime gametime, float time, Vector2 position)
        {
            Sprite.MoveBy(gametime, time, position);
        }

        public virtual void ScaleTo(GameTime gameTime, float time, Vector2 newscale)
        {
            Sprite.ScaleTo(gameTime, time, newscale);
        }

        public virtual void ScaleBy(GameTime gameTime, float time, Vector2 scaleby)
        {
            Sprite.ScaleBy(gameTime, time, scaleby);
        }

        public virtual void RotateTo(GameTime gameTime, float time, float rotateto)
        {
            Sprite.RotateTo(gameTime, time, rotateto);
        }

        public virtual void RotateBy(GameTime gameTime, float time, float rotateby)
        {
            Sprite.RotateBy(gameTime, time, rotateby);
        }

    }
}
