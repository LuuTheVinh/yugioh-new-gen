using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Yugioh_AtemReturns.Manager;

namespace Yugioh_AtemReturns.GameObjects
{
    public class MyObject
    {
        private Sprite m_Sprite;
        private ID m_Id;
        private STATUS m_Status;
        private Vector2 m_Position;

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
        public STATUS Status
        {
            get { return m_Status; }
            set { m_Status = value; }
        }
        public virtual Vector2 Position 
        {
            get { return m_Position; }
            set 
            { 
                m_Position = value;
                m_Sprite.Position = m_Position;
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
    }
}
