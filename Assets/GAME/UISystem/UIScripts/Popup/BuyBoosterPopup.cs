
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Template.UI
{
    public class BuyBoosterPopup : Window
    {
        [SerializeField] Text textName;
        [SerializeField] Text textDes;
        [SerializeField] Text textItemFree;
        [SerializeField] Text textItemBuy;
        [SerializeField] Text textPriceBuy;

        [SerializeField] Image imgItemFree;
        [SerializeField] Image imgItemBuy;

        [SerializeField] Button btnFree;
        [SerializeField] Button btnBuy;
        [SerializeField] Button btnClose;

        private void Start()
        {
            btnBuy.onClick.AddListener(ButtonBuyItem);
            btnFree.onClick.AddListener(ButtonBuyItemFree);
            btnClose.onClick.AddListener(ClosePopup);
        }
        void ClosePopup()
        {
            GameService.ClosePopup();
        }
        void ButtonBuyItemFree()
        {
            BuyItem();
        }
        void ButtonBuyItem()
        {
            BuyItem();
        }
        void BuyItem()
        {

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