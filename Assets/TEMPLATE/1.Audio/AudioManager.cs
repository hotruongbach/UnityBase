using MyBox;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Template.Audio
{
    public class AudioManager : Singleton<AudioManager>
    {
        [Serializable]
        class SoundIDClipPair
        {
            [HideInInspector] public string name;
            public SoundID m_SoundID;
            public AudioClip m_AudioClip;
        }
        [SerializeField]
        AudioSource m_MusicSource;
        [SerializeField]
        AudioSource m_EffectSource;
        [SerializeField, Min(0f)]
        float m_MinSoundInterval = 0.1f;
        [SerializeField]
        SoundIDClipPair[] m_Sounds;
        float m_LastSoundPlayTime;
        readonly Dictionary<SoundID, AudioClip> m_Clips = new();
        const string k_AudioSettings = "AudioSettings";
        AudioSettings m_AudioSettings = new();
        public bool EnableMusic
        {
            get => m_AudioSettings.EnableMusic;
            set
            {
                m_AudioSettings.EnableMusic = value;
                m_MusicSource.mute = !value;
            }
        }
        public bool EnableSfx
        {
            get => m_AudioSettings.EnableSfx;
            set
            {
                m_AudioSettings.EnableSfx = value;
                m_EffectSource.mute = !value;
            }
        }
        public bool EnableVibrate
        {
            get => m_AudioSettings.EnableVibrate;
            set
            {
                m_AudioSettings.EnableVibrate = value;
            }
        }
        public float MasterVolume
        {
            get => m_AudioSettings.MasterVolume;
            set
            {
                m_AudioSettings.MasterVolume = value;
                AudioListener.volume = value;
            }
        }

        private void OnValidate()
        {
            foreach (var sound in m_Sounds)
            {
                sound.name = $"{sound.m_SoundID}";
            }
        }

        void Start()
        {
            foreach (var sound in m_Sounds)
            {
                m_Clips.Add(sound.m_SoundID, sound.m_AudioClip);
            }
        }

        void OnEnable()
        {
            var audioSettings = LoadAudioSetting();
            EnableMusic = audioSettings.EnableMusic;
            EnableSfx = audioSettings.EnableSfx;
            EnableVibrate = audioSettings.EnableVibrate;
            MasterVolume = audioSettings.MasterVolume;
        }

        void OnDisable()
        {
            SaveAudioSetting();
        }
        void _PlayMusic(SoundID soundID, bool looping = true)
        {
            if (m_MusicSource.isPlaying)
                return;

            m_MusicSource.clip = m_Clips[soundID];
            m_MusicSource.loop = looping;
            m_MusicSource.Play();
        }

        /// <summary>
        /// Play a music based on its sound ID
        /// </summary>
        /// <param name="soundID">The ID of the music</param>
        /// <param name="looping">Is music looping?</param>
        public static void PlayMusic(SoundID soundID, bool looping = true)
        {
            Instance._PlayMusic(soundID, looping);
        }

        /// <summary>
        /// Stop the current music
        /// </summary>
        public static void StopMusic()
        {
            Instance.m_MusicSource.Stop();
        }

        void _PlaySound(SoundID soundID)
        {
            if (soundID == SoundID.None) return;
            if (!m_Clips.ContainsKey(soundID)) return;
            if (Time.time - m_LastSoundPlayTime >= m_MinSoundInterval)
            {
                m_EffectSource.PlayOneShot(m_Clips[soundID]);
                m_LastSoundPlayTime = Time.time;
            }
        }

        /// <summary>
        /// Play a sound effect based on its sound ID
        /// </summary>
        /// <param name="soundID">The ID of the sound effect</param>
        public static void PlaySound(SoundID soundID)
        {
            Instance._PlaySound(soundID);
        }
        public static void PlayVibrate()
        {
            if (Instance.EnableVibrate)
            {
                Vibration.Vibrate(100);
            }
        }
        public void SaveAudioSetting()
        {
            PlayerPrefs.SetString(k_AudioSettings, JsonUtility.ToJson(m_AudioSettings));
        }
        AudioSettings LoadAudioSetting()
        {
            return PlayerPrefs.HasKey(k_AudioSettings)
                    ? JsonUtility.FromJson<AudioSettings>(PlayerPrefs.GetString(k_AudioSettings))
                    : new AudioSettings();
        }
    }
}