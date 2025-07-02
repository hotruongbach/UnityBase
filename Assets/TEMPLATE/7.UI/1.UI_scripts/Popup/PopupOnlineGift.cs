
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Template.UI
{
    public class PopupOnlineGift : Window
    {
        [SerializeField] OnlineGiftLibrary OnlineGiftLibrary;
        [SerializeField] OnlineGiftUIFree freeGift;
        [SerializeField] OnlineGiftLayout OnlineGiftLayout;
        public override void Show(Action onComplete = null, object param = null)
        {
            base.Show(onComplete);
            //TODO: 
            OnlineGiftLayout.UpdateStateAll();
            freeGift.AddListener(OnFreeGiftClaim);

            Clock.Tick += UpdateFreeGiftTime;
        }

        private void UpdateFreeGiftTime(object sender, EventArgs e)
        {
            freeGift.UpdateTime(OnlineGiftService.FreeResetTimeRemaining());
        }

        private void OnFreeGiftClaim()
        {
            OnlineGiftService.ClaimFreeGift();
        }

        public override void OnAnimationComplete(Action OnAdsShowSuccess = null, Action OnAdsShowFailed = null)
        {
            base.OnAnimationComplete(OnAdsShowSuccess, OnAdsShowFailed);
            //TODO: 
        }
        public override void OnReveal(Action onComplete = null, object param = null)
        {
            base.OnReveal(onComplete);
            //TODO: 
        }
        public override void Hide(Action onComplete = null)
        {
            base.Hide(onComplete);
            //TODO: 
            Clock.Tick -= UpdateFreeGiftTime;
        }
    }
}