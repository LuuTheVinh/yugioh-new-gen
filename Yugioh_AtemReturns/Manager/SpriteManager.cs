using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Yugioh_AtemReturns.GameObjects;

namespace Yugioh_AtemReturns.Manager
{
    class SpriteManager
    {
        private static SpriteManager m_instance;
        private List<Sprite> _listSprite;

        private SpriteManager(ContentManager _content)
        {
            _listSprite = new List<Sprite>();
            LoadContent(_content);

        }

        public static SpriteManager getInstance(ContentManager _content)
        {
            if (m_instance == null)
                m_instance = new SpriteManager(_content);
            return m_instance;
        }

        /// <summary>
        /// <param>Get Sprite thông qua Id </param>
        /// <param>Quy ước Id phải trùng với tên file hình, nếu không tồn tại id nào thì ném ngoại lệ</param>
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public Sprite GetSprite(SpriteID _id)
        {
			try{
            return _listSprite[(int)_id];
			}
			catch
			{
				throw new Exception("Id: \'" + _id + "\' is not exists");
			}
        }
        public Texture2D GetTexture(SpriteID _id)
        {
            try
            {
                return _listSprite[(int)_id].Texture;
            }
            catch
            {
                throw new Exception("Id: \'" + _id + "\' is not exists");
            }
        }
        private void LoadContent(ContentManager _content)
        {
            for (int i = 0; i < Enum.GetValues(typeof(SpriteID)).Length; i++)//lấy tất cả enum để load file hình. nếu không tìm thấy thì bỏ qua
            {
                _listSprite.Add(
                     new Sprite(_content,this.getFormatFileName((SpriteID)i)));
					 // Đoạn này đặt đường dẫn đơn giản nên ràng buộc tất cả hình phải cùng để trong thự mục content
					 // Nếu muốn đặt hình trong thư mục khác thì sửa lại đường dẫn
					 // Nếu muốn đặt các hình ở thư mục khác nhau thì quy ước lấy một vài chữ cái đầu của thư mục làm các chữ cái đầu của IDictionary
					 //rồi sửa đường dẫn bằng các phương thức của strin :))
			}
        }
        private string getFormatFileName(SpriteID _id)
        {
            if (_id.ToString().StartsWith("C")) // quy ước các hình các lá bài bắt đầu bằng chữ C nên đừng sử dụng chữ c cho cái khác nữa
            {
                return ("card\\"+_id.ToString().Substring(1) );
            }
            if (_id.ToString().StartsWith("pha")) // quy ước các hình các lá bài bắt đầu bằng chữ C nên đừng sử dụng chữ c cho cái khác nữa
            {
                return ("phase\\" + _id.ToString());
            }

            if(_id.ToString().StartsWith("B")) //Card Big
            {
                return ("card_big\\" + _id.ToString().Substring(1));
            }
            if (_id.ToString().StartsWith("lp"))
            {
                return ("General\\" + _id.ToString());
            }
            if (_id.ToString().StartsWith("font"))
            {
                return ("font\\" + _id.ToString());
            }
            return _id.ToString();
        }
    }
}
