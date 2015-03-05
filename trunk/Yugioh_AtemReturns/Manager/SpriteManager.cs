using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Yugioh_AtemReturns.Manager
{
    class SpriteManager
    {
        private static SpriteManager m_instance;
        private List<Sprite> _listSprite;
        private int Total;

        private SpriteManager(ContentManager _content)
        {
            _listSprite = new List<Sprite>();
            Total = Enum.GetValues(typeof(ID)).Length; //lấy tất cả enum để load file hình. nếu không tìm thấy thì bỏ qua (xem hàm load)
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
        public Sprite GetSprite(ID _id)
        {
			try{
            return _listSprite[(int)_id];
			}
			catch()
			{
				throw new Exception("Id: \'" + _id + "\' is not exists");
			}
        }

        private void LoadContent(ContentManager _content)
        {
            for(int i = 0; i < Total; i++)
            {
                _listSprite.Add(
                     new Sprite(_content,((ID)i).ToString())); 
					 // Đoạn này đặt đường dẫn đơn giản nên ràng buộc tất cả hình phải cùng để trong thự mục content
					 // Nếu muốn đặt hình trong thư mục khác thì sửa lại đường dẫn
					 // Nếu muốn đặt các hình ở thư mục khác nhau thì quy ước lấy một vài chữ cái đầu của thư mục làm các chữ cái đầu của IDictionary
					 //rồi sửa đường dẫn bằng các phương thức của strin :))
			}
        }
    }
}
