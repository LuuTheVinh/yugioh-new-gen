using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Yugioh_AtemReturns.Manager;
using Yugioh_AtemReturns.Duelists;
using Microsoft.Xna.Framework;

namespace Yugioh_AtemReturns.GameObjects
{
    class HealthBar:MyObject
    {
        Sprite m_lp_extractor;

        Duelist duelist;
        public HealthBar(ContentManager _content, Duelist _duelist):
            base(_content,ID.HEALTH_BAR,SpriteID.lp_bar)
        {
            duelist = _duelist;
            m_lp_extractor = new Sprite(
                SpriteManager.getInstance(_content).GetSprite(SpriteID.lp_extract));
            switch (_duelist.DuelistID)
            {
                case ePlayerId.PLAYER:
                    m_lp_extractor.Position = GlobalSetting.Default.LPBAR;
                    this.Sprite.Frame = new Rectangle(0, 0, 150, 16);
                    break;
                case ePlayerId.COMPUTER:
                    m_lp_extractor.Position = ComputerSetting.Default.LPBAR;
                    this.Sprite.Frame = new Rectangle(0, 16, 150, 16);
                    break;
                default:
                    break;
            }
            this.Sprite.Position = this.m_lp_extractor.Position + new Vector2(28, 2);
        }
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);
        }
        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch _spritebatch)
        {
            _spritebatch.Begin();
            this.m_lp_extractor.Draw(_spritebatch);
            base.Draw(_spritebatch);
            _spritebatch.End();
        }
        public void LPChange(int _current_lp)
        {
            if (_current_lp == GlobalSetting.Default.MaxLP)
            {
                this.AddScaleTo(new ScaleTo(0.5f, Vector2.One));
            }
            else
            {
                float ratio = (float)_current_lp / GlobalSetting.Default.MaxLP;
                this.AddScaleTo(new ScaleTo(0.5f, new Vector2(ratio, 1)));
            }
        }
    }
}
