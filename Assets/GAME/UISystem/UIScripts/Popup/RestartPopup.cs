
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Monster.UI
{
    public class RestartPopup : Window
    {
        [SerializeField] Button btnRestart;
        [SerializeField] Button btnCancel;

        private void Start()
        {
            btnRestart.onClick.AddListener(EventRestart);
            btnCancel.onClick.AddListener(EventCancel);
        }
        void EventRestart()
        {

        }
        void EventCancel()
        {
            GameService.ClosePopup<RestartPopup>();
        }
        public override void Initialize()
        {
            //TODO: 
        }

        public override void Show(Action onComplete = null, object param = null)
        {
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