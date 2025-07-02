using System;
using Monster.Audio;
using Monster.UI;

namespace Monster
{
    public static class GameService
    {
        #region AUDIO AND VIBRATION
        public static void PlayVibrate()
        {
            AudioManager.PlayVibrate();
        }

        public static void PlaySound(SoundID soundID)
        {
            AudioManager.PlaySound(soundID);
        }

        public static void PlayMusic(SoundID soundID, bool isLoop = true)
        {
            AudioManager.PlayMusic(soundID, isLoop);
        }

        public static void StopMusic()
        {
            AudioManager.StopMusic();
        }

        public static bool EnableMusic
        {
            get => AudioManager.Instance.EnableMusic;
            set => AudioManager.Instance.EnableMusic = value;
        }
        public static bool EnableSfx
        {
            get => AudioManager.Instance.EnableSfx;
            set => AudioManager.Instance.EnableSfx = value;
        }
        public static bool EnableVibrate
        {
            get => AudioManager.Instance.EnableVibrate;
            set => AudioManager.Instance.EnableVibrate = value;
        }
        public static float MasterVolume
        {
            get => AudioManager.Instance.MasterVolume;
            set => AudioManager.Instance.MasterVolume = value;
        }

        public static void SaveSetting()
        {
            AudioManager.Instance.SaveAudioSetting();
        }
        #endregion

        #region DISPLAY UI
        public static void ShowPopup<T>(Action onComplete = null, object param = null)
            where T : Window
        {
            UIManager.Instance.ShowPopup<T>(onComplete, param);
        }

        public static void ShowView<T>(Action onComplete = null, object param = null)
            where T : Window
        {
            UIManager.Instance.ShowView<T>(onComplete, param);
        }

        /// <summary>
        /// Close all popup
        /// </summary>
        /// <param name="onlyHistory"></param>
        /// <param name="onComplete"></param>
        public static void CloseAllPopup(bool onlyHistory = true, Action onComplete = null)
        {
            UIManager.Instance.CloseAllPopup(onlyHistory, onComplete);
        }

        /// <summary>
        /// Close last popup in history
        /// </summary>
        /// <param name="onComplete"></param>
        public static void ClosePopup(Action onComplete = null)
        {
            UIManager.Instance.ClosePopup(onComplete);
        }


        /// <summary>
        /// Close popup T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="onComplete"></param>
        public static void ClosePopup<T>(Action onComplete = null)
            where T : Window
        {
            UIManager.Instance.ClosePopup<T>(onComplete);
        }

        public static bool IsAnyPopupShowing()
        {
            return UIManager.Instance.IsAnyPopupShowing();
        }

        public static void PushNotify(string message)
        {
            UIManager.Instance.PushNoti(message);
        }
        #endregion
    }
}
