using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Yugioh_AtemReturns.GameObjects
{

    class MyObject
    {
        Sprite m_sprite;
        ID m_id;
        STATUS m_status;

        #region Property

        public Sprite Sprite
        {
            get { return m_sprite; }
            set { m_sprite = value; }
        }
        public ID ID
        {
            get { return m_id; }
            set { m_id = value; }
        }
        public STATUS Status
        {
            get { return m_status; }
            set { m_status = value; }
        }
        
        #endregion

        public MyObject(ContentManager _content,ID _id)
        {
            //Sprite = new Sprite(SpriteManager.getInstance(_content).GetSprite(_id));
            Status = STATUS.NORMAL;
            ID = _id;
        }
        protected MyObject()
        {
            Sprite = null;
            Status = STATUS.NORMAL;
        }
        protected virtual void setFrame()
        {
            Sprite.Frame = new Rectangle(
                Sprite.CurFrameW * Sprite.Size.X,
                Sprite.CurFrameH * Sprite.Size.Y,
                Sprite.Size.X,
                Sprite.Size.Y);
        }

        public virtual void Draw(SpriteBatch _spritebatch)
        {
            Sprite.Draw(_spritebatch);
        }
    }
}
