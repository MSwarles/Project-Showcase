using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace SuperMetroid
{
    // TODO: AUDIO - make audio engine more robust
    /// <summary>
    /// Singleton class used to play game audio.
    /// </summary>
    public class AudioManager
    {
        #region Fields
        Game1 gameRef;
        Dictionary<string, SoundEffect> m_soundDict = new Dictionary<string, SoundEffect>();
        private float m_masterVolume;
        private float m_musicVolume;
        private float m_sfxVolume;
        #endregion

        #region Properties
        public float MasterVolume
        {
            get { return m_masterVolume; }
            set { m_masterVolume = MathHelper.Clamp(value, 0f, 1f); }
        }
        public float MusicVolume
        {
            get { return m_musicVolume; }
            set { m_musicVolume = MathHelper.Clamp(value, 0f, 1f); }
        }
        public float SFXVolume
        {
            get { return m_sfxVolume; }
            set { m_sfxVolume = MathHelper.Clamp(value, 0f, 1f); }
        }
        #endregion

        #region Constructor
        public AudioManager(Game game)
        {
            gameRef = (Game1)game;
            MasterVolume = 1f;
            MusicVolume = 1f;
            SFXVolume = 1f;

            LoadContent();
        }
        #endregion

        #region XNA Methods
        protected void LoadContent()
        {
            SoundEffect sound = gameRef.Content.Load<SoundEffect>(@"Audio\shoot");
            m_soundDict.Add("shoot", sound);

            sound = gameRef.Content.Load<SoundEffect>(@"Audio\shootCollision");
            m_soundDict.Add("shotHit", sound);

            sound = gameRef.Content.Load<SoundEffect>(@"Audio\step");
            m_soundDict.Add("step", sound);

            sound = gameRef.Content.Load<SoundEffect>(@"Audio\jump");
            m_soundDict.Add("spinJump", sound);
        }
        #endregion

        public void PlaySound(string soundName)
        {
            if (m_soundDict.ContainsKey(soundName))
            {
                    SoundEffectInstance sound = m_soundDict[soundName].CreateInstance();
                    sound.Volume = m_sfxVolume * m_masterVolume;
                    sound.Play();
            }
        }

        public void PlaySound(string soundName, int volume)
        {
            foreach (KeyValuePair<string, SoundEffect> entry in m_soundDict)
            {
                if (entry.Key == soundName)
                {
                    SoundEffectInstance sound = entry.Value.CreateInstance();
                    sound.Volume = volume;
                    sound.Play();
                    break;
                }
            }
        }
    }
}