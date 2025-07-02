using Template.User;
using Template.Utilities;
using MyBox;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Template.FEATURES.DailyGift
{
    public class DailyGiftManager : FeatureBase
    {

        [SerializeField] DailyGiftConfig GiftLibrary;
        public DailyGiftData Data;
        [Separator("GIFT INFORMATION")]
        [ReadOnly] public int WeekID = 0;
        [ReadOnly] public DailyGift TodayGift;
        public bool IsClaimable = false;
        public override IEnumerator CheckNewDays(bool isNewDay)
        {
            yield return null;
            DateTime LastClaimDate = DateTime.Now.Date;
            try
            {
                LastClaimDate = Data.LastClaimDay.ToDate().Date;
            }
            catch
            {
                LastClaimDate = (DateTime.Now.AddDays(-1)).Date;
            }

            int TimeFromLastClaim = (int)(DateTime.Now.Date - LastClaimDate).TotalDays;

            IsClaimable = TimeFromLastClaim > 0;
        }

        public override IEnumerator LoadData()
        {
            yield return null;
            string json = PlayerPrefs.GetString(PlayerPrefsKey.DAILY_GIFT, "");

            if (json.IsNullOrEmpty())
            {
                Data = new DailyGiftData();
            }
            else
            {
                Data = JsonUtility.FromJson<DailyGiftData>(json);
            }

        }

        public override IEnumerator StartFeature()
        {
            yield return null;

            //find week count and today gift
            RecalculateWeekGift();

            DailyGiftService.Run(this);
        }

        protected override void SaveData()
        {
            string json = JsonUtility.ToJson(Data);
            PlayerPrefs.SetString(PlayerPrefsKey.DAILY_GIFT, json);
        }

        public void TryClaimGift(int multiply = 1)
        {
            //double check claim condition
            if (IsClaimable)
            {
                //claim gift here
                ClaimTodayGift(TodayGift.GiftData, multiply);
            }
        }

        private void ClaimTodayGift(List<ResourceData> gift, int multiply = 1)
        {
            //ResourceManager.ClaimResource(gift, times);
            UserManager.ClaimResource(gift, multiply);

            Data.NumberOfGiftClaimed++;
            IsClaimable = false;
            Data.LastClaimDay = DateTime.Now.Date.ToDateString();
            SaveData();
        }

        public void RecalculateWeekGift()
        {
            WeekID = Data.NumberOfGiftClaimed / 7 % GiftLibrary.Weeks.Count;
            int dayInWeek = Data.NumberOfGiftClaimed % 7;
            if (!IsClaimable) dayInWeek--;
            if (dayInWeek < 0)
            {
                WeekID = Mathf.Max(WeekID - 1, 0);
                dayInWeek = 6;
            }
            Bug.Log($"Week {WeekID} day {dayInWeek}");
            TodayGift = GiftLibrary.GetGift(WeekID, dayInWeek);
        }
        public WeekGift GetThisWeekListGift()
        {
            return GiftLibrary.Weeks[WeekID];
        }
    }

    public static class DailyGiftService
    {
        static DailyGiftManager Manager;
        public static void Run(DailyGiftManager manager)
        {
            Manager = manager;
        }
        public static void ClaimTodayGift(int times = 1)
        {
            Manager.TryClaimGift(times);
        }
        public static int GiftClaimed => Manager.Data.NumberOfGiftClaimed;
        public static bool IsClaimable => Manager.IsClaimable;
        public static WeekGift ThisWeekGifts => Manager.GetThisWeekListGift();
        public static DailyGift TodayGift => Manager.TodayGift;
        public static DateTime LastClaimDay
        {
            get
            {
                DateTime LastClaimDate = DateTime.Now.Date;
                try
                {
                    LastClaimDate = Manager.Data.LastClaimDay.ToDate().Date;
                }
                catch
                {
                    LastClaimDate = (DateTime.Now.AddDays(-1)).Date;
                }
                return LastClaimDate;
            }
        }
    }
}