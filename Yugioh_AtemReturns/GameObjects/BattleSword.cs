using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Yugioh_AtemReturns.Manager;
using Yugioh_AtemReturns.Duelists;
using Yugioh_AtemReturns.Cards;
using Yugioh_AtemReturns.Decks;

namespace Yugioh_AtemReturns.GameObjects
{

    class BattleSword :MyObject
    {
        private bool m_isShow;
        private Timer m_timer;

        public bool IsShow
        {
            get
            {
                return m_isShow;
            }
            set
            {
                if (value == true)
                {
                }
                else
                {
                    this.Sprite.Rotation = 0.0f;
                }
                m_isShow = value;                
            }
        }
        public bool IsAction
        {
            get
            {
                return Sprite.IsAction;
            }
        }
        public override STATUS Status
        {
            get
            {
                return base.Status;
            }
            set
            {
                base.Status = value;
                if (value == STATUS.SWORD_FULL)
                    this.Sprite.Frame = new Rectangle(0, 0, 35, 70);
                if (value == STATUS.SWORD_HALF)
                    this.Sprite.Frame = new Rectangle(35, 0, 35, 70);     
            }
        }
        public BattleSword(ContentManager _content)
            :base(_content,ID.BATTLE_SWORD,SpriteID.atk_sword)
        {
            this.Sprite.Frame = new Rectangle(0, 0, 35, 70);
            this.Origin = new Vector2(this.Sprite.Bound.Center.X, this.Sprite.Bound.Center.Y);
            this.m_timer = new Timer();
            this.Status = STATUS.SWORD_FULL;
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (this.IsShow == false)
                return;
            //if (this.Sprite.IsAction == false)
            //    this.IsShow = false;
            base.Update(gameTime);
            this.m_timer.Update(gameTime);
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch _spritebatch)
        {
            if (this.IsShow == false)
                return;
            _spritebatch.Begin();
            base.Draw(_spritebatch);
            _spritebatch.End();
        }
        public void Show(Card _card)
        {
            this.Position = new Vector2(_card.Sprite.Bound.Center.X, _card.Sprite.Bound.Center.Y);
            this.Sprite.Frame = new Rectangle(0, 0, 35, 70);
            this.IsShow = true;
            this.Status = STATUS.SWORD_FULL;
        }
        public void Update(Player _player)
        {
            if (this.Sprite.IsAction == true)
                return;
            if (this.Status == STATUS.SWORD_HALF)
                return;
            Point mouse = _player.Input.MousePosition;
            this.Sprite.Rotation = this.SetRotation(mouse);
        }
        public void AttackTo(Card _card)
        {
            this.Rotation = this.SetRotation(_card.Sprite.Bound.Center);
            this.AddMoveTo(
                new MoveTo(0.5f, new Vector2(
                      _card.Sprite.Bound.Center.X,
                      _card.Sprite.Bound.Center.Y)));
        }
        public void DirectAtk(Duelist _duelist)
        {
            Vector2 point_vt = GlobalSetting.Default.CenterField +
                   ((_duelist.DuelistID == ePlayerId.PLAYER) ? new Vector2(0, 210) : new Vector2(0, -210));
            Point p = new Point((int)point_vt.X,(int)point_vt.Y);
            this.Rotation =this.SetRotation(p);
            this.AddMoveTo(
                new MoveTo(0.6f, point_vt));
                    
        }

        public void Hide()
        {
            this.IsShow = false;
        }

        private float SetRotation(Point _pointForward)
        {
            Point sword = new Point((int)this.Sprite.Position.X, (int)this.Sprite.Position.Y);

            if (sword.X == _pointForward.X)
            {
                if (sword.Y >= _pointForward.Y)
                {
                    return  0.0f;
                }
                else
                {
                    return (float)Math.PI;
                }
            }
            if (sword.Y == _pointForward.Y)
            {
                if (sword.X >= _pointForward.X)
                {
                    return (float)-Math.PI / 2;
                }
                else
                {
                    return (float)Math.PI / 2;
                }
            }
            float k = (float)(sword.Y - _pointForward.Y) / (sword.X - _pointForward.X);
            if (sword.X > _pointForward.X)
                return (float)Math.Atan(k) + ((sword.Y > _pointForward.Y) ? 0 : 0) - (float)Math.PI / 2;
            else
                return (float)Math.Atan(k) + ((sword.Y > _pointForward.Y) ? 0 : 0) + (float)Math.PI / 2;
        }
    }
}
