using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yugioh_AtemReturns.GameObjects;

namespace Yugioh_AtemReturns.Manager
{
    public class SceneManager
    {
        private List<Scene> m_ListScene = new List<Scene>();
        private static SceneManager m_Instance;
        private ContentManager m_Content;

        public ContentManager Content {
            get { return m_Content; }
            set { m_Content = value; }
        }
        private SceneManager()
        {

        }

        public static SceneManager getInstance()
        {
            if(m_Instance == null)
            {
                m_Instance = new SceneManager();
            }
            return m_Instance;
        }

        public void Init()
        { 
            if(m_ListScene.Count() != 0)
            {
                m_ListScene[m_ListScene.Count() - 1].Init(this.Content);
            }
        }
        public void Update(GameTime gametime)
        {
            if (m_ListScene.Count() != 0)
            {
                m_ListScene[m_ListScene.Count() - 1].Update(gametime);
            }
        }
        public void Render(SpriteBatch spriteBatch)
        {
            if (m_ListScene.Count() != 0)
            {
                m_ListScene[m_ListScene.Count() - 1].Render(spriteBatch);
            }
        }

        public void addScene(Scene scene)
        {
            m_ListScene.Add(scene);
            m_ListScene[m_ListScene.Count() - 1].Init(m_Content);
        }

        public void removeScene()
        {
            if(m_ListScene.Count() != 0)
            {
                m_ListScene.RemoveAt(m_ListScene.Count() - 1);
            }
        }
        public void replaceScene(Scene scene)
        {
            if(m_ListScene.Count() != 0)
            {
                //add scene transation later...
                //

                this.removeScene();
                this.addScene(scene);
            }
        }
    }
}
