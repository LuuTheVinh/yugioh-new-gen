using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yugioh_AtemReturns.Manager
{
    class TextFormater
    {
        private static TextFormater m_Instance;

        private TextFormater()
        { }

        public static TextFormater getIntance()
        {
            if(m_Instance == null)
            {
                m_Instance = new TextFormater();
            }
            return m_Instance;
        }

        public string WordWrap(SpriteFont spritefont, string text, float maxWidth)
        {
            string[] words = text.Split(' ');

            StringBuilder stringbuilder = new StringBuilder();
            float linewidth = 0.0f;
            float spacewidth = spritefont.MeasureString(" ").X;

            foreach (var word in words)
            {
                Vector2 size = spritefont.MeasureString(word);

                if (linewidth + size.X < maxWidth)
                {
                    stringbuilder.Append(word + " ");
                    linewidth += size.X + spacewidth;
                }
                else
                {
                    stringbuilder.Append("\n" + word + " ");
                    linewidth = size.X + spacewidth;
                }
            }

            return stringbuilder.ToString();
        }
    }
}
