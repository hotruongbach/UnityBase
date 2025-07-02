
using System;
using System.Collections;
using System.Collections.Generic;
using Template.User;
using UnityEngine;
using UnityEngine.UI;

namespace Template.UI
{
    public class RevivePopup : Window
    {
        [SerializeField] GameObject mainUi;
        [SerializeField] GameObject faileUi;

        [Header("MainPopup")]
        [SerializeField] Button btnReviveCoin;
        [SerializeField] Button btnReviveAds;
        [SerializeField] Button btnGiveup;

        [Header("FailePopup")]
        [SerializeField] Button btnHome;
        [SerializeField] Button btnTryAgain;

        int priceRevive = 100;
        private void Start()
        {
            btnReviveCoin.onClick.AddListener(() => EventButtonRevive(false));
            btnReviveAds.onClick.AddListener(() => EventButtonRevive(true));
            btnGiveup.onClick.AddListener(BtnGiveUp);
        }

        void EventButtonRevive(bool isAds)
        {
            if (isAds == false)
            {
                OnRevive();
            }
            else
            {
                if (UserDataCache.Coin >= priceRevive)
                {
                    OnRevive();
                }
                else
                {
                    GameService.ShowPopup<NotEnoughResourcePopup>();
                }
            }
        }

        void BtnGiveUp()
        {
            mainUi.SetActive(false);
            faileUi.SetActive(true);
        }
        void OnRevive()
        {
            Debug.Log("On Revive");
        }
        public override void Initialize()
        {
            //TODO: 
        }

        public override void Show(Action onComplete = null, object param = null)
        {
            mainUi.SetActive(true);
            faileUi.SetActive(false);
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