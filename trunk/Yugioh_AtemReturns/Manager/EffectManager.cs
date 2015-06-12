using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yugioh_AtemReturns.Manager
{
    class EffectManager
    {
        private static EffectManager m_instance;
        private List<SoundEffectInstance> list_effectInstant;
        private List<SoundEffect> list_soundeffect;
        private EffectManager(ContentManager _content)
        {
            list_effectInstant = new List<SoundEffectInstance>();
            list_soundeffect = new List<SoundEffect>();
            for (int i = 0; i < Enum.GetValues(typeof(eSoundId)).Length; i++)//lấy tất cả enum để load file hình. nếu không tìm thấy thì bỏ qua
            {
                var soundeffect = _content.Load<SoundEffect>(@"Sound\" + ((eSoundId)i).ToString());
                list_soundeffect.Add(soundeffect);
                list_effectInstant.Add(soundeffect.CreateInstance());
            }
        }
        public static void Init(ContentManager _content)
        {
            if (m_instance == null)
                m_instance = new EffectManager(_content);
        }
        public static EffectManager GetInstance()
        {
			if (m_instance == null)
                m_instance = new EffectManager(_content);
            return m_instance;
        }
        public SoundEffectInstance GetSoundEffectInstance(eSoundId _id)
        {
            return list_effectInstant[(int)_id];
        }
        public void Play(eSoundId _id)
        {
            list_effectInstant[(int)_id].Play();
        }
        public void Stop(eSoundId _id)
        {
            list_effectInstant[(int)_id].Stop();
        }
        public SoundState GetState(eSoundId _id)
        {
            return list_effectInstant[(int)_id].State;
        }
        public int GetDuration(eSoundId _id)
        {
            return list_soundeffect[(int)_id].Duration.Milliseconds;
        }
    }
}
