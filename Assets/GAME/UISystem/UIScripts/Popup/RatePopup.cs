
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Monster.UI
{
    public class RatePopup : Window
    {
        [SerializeField] StarRate[] stars;
        [SerializeField] Button btnRate;
        [SerializeField] Button btnOk;
        private const string PREF_RATE_STAR = "rate_star";
        public static bool isShowRate;

        [SerializeField] GameObject mainUi;
        [SerializeField] GameObject thanksUi;
        private int rate
        {
            get
            {
                return PlayerPrefs.GetInt(PREF_RATE_STAR, -1);
            }
            set
            {
                PlayerPrefs.SetInt(PREF_RATE_STAR, value);
            }
        }
        private void Start()
        {
            btnRate.onClick.AddListener(OnClickRate);
            btnOk.onClick.AddListener(Close);
        }
        public void OnClickStar(int num)
        {
            for (int i = 0; i < 5; i++)
            {
                stars[i].ResetStar();
                if (i < num) stars[i].RateStar();
            }
            rate = num;
        }

        public void OnClickRate()
        {
            if (rate > 0)
            {
                DoRate();
            }
        }

        void DoRate()
        {
            if (rate > 4)
            {
                HighRateClick();
                PlayerPrefsManager.Rate5Star = 1;
            }
            ShowThanks();
        }

        public void HighRateClick()
        {
#if UNITY_ANDROID
            if (Application.platform == RuntimePlatform.Android)
            {
                Application.OpenURL("market://details?id=" + Application.identifier);
            }
            ShowThanks();
#else
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            if (!UnityEngine.iOS.Device.RequestStoreReview())
            {
                //Application.OpenURL($"https://apps.apple.com/app/id{GameSDKSettings.APPLE_APP_ID}?action=write-review");
            }
        }
        ShowThanks();
#endif
        }
        void ShowThanks()
        {
            mainUi.SetActive(false);
            thanksUi.SetActive(true);
        }
        public void Close()
        {
            GameService.ClosePopup<RatePopup>();
        }
        public override void Initialize()
        {
            //TODO: 
        }

        public override void Show(Action onComplete = null, object param = null)
        {
            mainUi.SetActive(true);
            thanksUi.SetActive(false);
            base.Show(onComplete, param);
            //TODO: 
        }

        public override void OnAnimationComplete(Action OnAdsShowSuccess = null, Action OnAdsShowFailed = null)
        {
            base.OnAnimationComplete(OnAdsShowSuccess, OnAdsShowFailed);
            //TODO: 
        }
        public override void OnReveal(Action onComplete = null, object param = null)
        {
            base.OnReveal(onComplete, param);
            //TODO: 
        }
        public override void Hide(Action onComplete = null)
        {
            base.Hide(onComplete);
            //TODO: 
        }
        public override void ResetData()
        {
            //TODO: 
        }
    }
}