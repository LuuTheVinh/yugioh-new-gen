using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yugioh_AtemReturns.Duelists
{
    interface IPhase
    {
        void Update(Microsoft.Xna.Framework.GameTime _gameTime);

        void Update(Player _player, Computer _computer);

        void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch _spriteBatch);

        /// <summary>
        /// Thêm các event cho các đối tượng
        /// </summary>
        /// <param name="_player"></param>
        /// <param name="_computer"></param>
        void Begin(Player _player, Computer _computer, ePlayerId _id);

        // Gỡ bỏ các event 
        void End(Player _player, Computer _computer);
    }
}
