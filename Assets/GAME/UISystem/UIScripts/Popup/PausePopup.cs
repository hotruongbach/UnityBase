
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Template.UI
{
    public class PausePopup : Window
    {
        [SerializeField] Transform mainPause;
        [SerializeField] Transform comfirm;
        [SerializeField] Transform uiHeart;

        [SerializeField] Button btnHome;
        [SerializeField] Button btnClose1;

        [SerializeField] Button btnLeave;
        [SerializeField] Button btnClose2;
        private void Start()
        {
            btnHome.onClick.AddListener(ButtonHome);
            btnLeave.onClick.AddListener(ButtonLeave);
            btnClose1.onClick.AddListener(ClosePopup);
            btnClose2.onClick.AddListener(ClosePopup);
        }
        void ButtonLeave()
        {
            GameService.CloseAllPopup();
            SceneManager.LoadSceneAsync(SceneName.HOME);
        }
        void ButtonHome()
        {
            mainPause.gameObject.SetActive(false);
            uiHeart.gameObject.SetActive(true);
            comfirm.gameObject.SetActive(true);
        }
        void ClosePopup()
        {
            uiHeart.gameObject.SetActive(false);
            comfirm.gameObject.SetActive(false);
            GameService.ClosePopup<PausePopup>();
        }
        public override void Initialize()
        {
            //TODO: 
        }

        public override void Show(Action onComplete = null, object param = null)
        {
            mainPause.gameObject.SetActive(true);
            uiHeart.gameObject.SetActive(false);
            comfirm.gameObject.SetActive(false);
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