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
	// for sound
    public class SceneEventArgs
    {
        public SceneEventArgs(Scene _scene)
        {
            this.Scene = _scene;
        }
        public Scene Scene;
    }
    public delegate void SceneAddedEventHandle(SceneManager sender, SceneEventArgs e);
    public delegate void SceneRemovedEventHandle(SceneManager sender, SceneEventArgs e);
    public class SceneManager
    {
		// for sound
        public event SceneAddedEventHandle SceneAdded;
        public event SceneRemovedEventHandle SceneRemoved;
        public virtual void OnSceneAdded(SceneEventArgs sceneE)
        {
            if (SceneAdded != null)
                SceneAdded(this, sceneE);
        }
        public virtual void OnSceneRemoved(SceneEventArgs sceneE)
        {
            if (SceneRemoved != null)
                SceneRemoved(this, sceneE);
        }
		//----
		
        private List<Scene> m_ListScene = new List<Scene>();
        private static SceneManager m_Instance;
        private Game m_game;

        public Game Game
        {
            get { return m_game; }
            set { m_game = value; }
        }   
		//for sound
        private SceneManager()
        {
            this.SceneAdded += SceneManager_SceneAdded;
            this.SceneRemoved +=SceneManager_SceneRemoved;
        }

        private void SceneManager_SceneAdded(SceneManager sender, SceneEventArgs e)
        {
            if (e.Scene is Yugioh_AtemReturns.Scenes.MenuScene)
                EffectManager.GetInstance().Play(eSoundId.m_menu);
            if (e.Scene is Yugioh_AtemReturns.Scenes.PlayScene)
                EffectManager.GetInstance().Play(eSoundId.m_duel1);
        }
        
        private void SceneManager_SceneRemoved(SceneManager sender, SceneEventArgs e)
        {
            if (e.Scene is Yugioh_AtemReturns.Scenes.MenuScene)
                EffectManager.GetInstance().Stop(eSoundId.m_menu);
            if (e.Scene is Yugioh_AtemReturns.Scenes.PlayScene)
                EffectManager.GetInstance().Stop(eSoundId.m_duel1);
        }
		//----


        public static SceneManager GetInstance()
        {
            if(m_Instance == null)
            {
                m_Instance = new SceneManager();
            }
            return m_Instance;
        }

        public void Init(Game game)
        {
            if(m_ListScene.Count() != 0)
            {
                m_ListScene.Last().Init(game);
            }
            SpriteManager.getInstance(game.Content);
        }
        public void Update(GameTime gametime)
        {
            if (m_ListScene.Count() != 0)
            {
                m_ListScene.Last().Update(gametime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (m_ListScene.Count() != 0)
            {
                m_ListScene.Last().Draw(spriteBatch);
            }
        }

        public void AddScene(Scene scene)
        {
            m_ListScene.Add(scene);
            m_ListScene.Last().Init(m_game);
            this.OnSceneAdded(new SceneEventArgs(scene)); // for sound
        }

        public void RemoveScene()
        {
            if(m_ListScene.Count() != 0)
            {
                this.OnSceneRemoved(new SceneEventArgs(m_ListScene.Last()));	//forsound
                m_ListScene.RemoveAt(m_ListScene.Count() - 1);
            }
        }
        public void ReplaceScene(Scene scene)
        {
            if(m_ListScene.Count == 0)
            {
                AddScene(scene);
            }
            else
            {
                RemoveScene();
                AddScene(scene);
            }
        }
    }
}
