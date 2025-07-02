using Template.UI;
using Template.Utilities;
using MyBox;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Template.FEATURES.DailyGift
{
    public class PopupDailyGift : Window
    {
        WeekGift ThisWeekGifts => DailyGiftService.ThisWeekGifts;
        [Separator]
        [SerializeField] List<GiftContainer> Gifts = new List<GiftContainer>();
        [Separator]
        [SerializeField] ButtonBase ClaimButton;
        [SerializeField] ButtonBase ClaimBonusButton;

        private GiftContainer TodayGift;

        private void Start()
        {
            ClaimBonusButton.AddListener(ClaimBonusGift);
            ClaimButton.AddListener(ClaimTodayGift);

            EnableButtons(DailyGiftService.IsClaimable);
        }

        private void EnableButtons(bool enabled)
        {
            ClaimButton.gameObject.SetActive(enabled);
            ClaimBonusButton.gameObject.SetActive(enabled);
        }

        private void ClaimTodayGift()
        {
            Bug.Log("Claim");
            //data 
            DailyGiftService.ClaimTodayGift();

            //ui
            TodayGift?.SetClaimed();

            EnableButtons(false);
        }

        private void ClaimBonusGift()
        {
            Bug.Log("Claim bonus");
            DailyGiftService.ClaimTodayGift(2);

            //ui
            TodayGift?.SetClaimed();
            EnableButtons(false);
        }

        public override void Show(Action onComplete = null, object param = null)
        {
            ShowThisWeekGift();
            base.Show(onComplete);
        }

        private void ShowThisWeekGift()
        {
            var weekGifts = DailyGiftService.ThisWeekGifts;
            int GiftClaimedThisWeek = DailyGiftService.GiftClaimed % 7;
            DateTime LastClaimDay = DailyGiftService.LastClaimDay;

            //check if user claim in sunday, just nothing left to claim today
            if(LastClaimDay == DateTime.Now.Date && GiftClaimedThisWeek == 0)
            {
                for (int i = 0; i < weekGifts.dailyGifts.Count; i++)
                {
                    GiftContainer item = Gifts[i];
                    List<ResourceData> data = weekGifts.dailyGifts[i].GiftData;
                    item.InitGift(data, true, false);
                }
                EnableButtons(false);
            }
            else
            {
                for (int i = 0; i < weekGifts.dailyGifts.Count; i++)
                {
                    GiftContainer item = Gifts[i];
                    List<ResourceData> data = weekGifts.dailyGifts[i].GiftData;
                    bool IsClaimed = i < GiftClaimedThisWeek;
                    bool IsTodayGift = weekGifts.dailyGifts[i] == DailyGiftService.TodayGift;
                    item.InitGift(data, IsClaimed, IsTodayGift);
                    if (IsTodayGift) TodayGift = item;
                }
            }
        }
    }
}