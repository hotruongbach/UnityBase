using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Template.FEATURES.DailyGift
{
    [CreateAssetMenu(fileName = "DailyGiftConfig", menuName = "GAME/DailyGiftConfig")]
    public class DailyGiftConfig : ScriptableObject
    {
        public List<WeekGift> Weeks = new List<WeekGift>();

        public DailyGift GetGift(int week, int day)
        {
            var weekData = Weeks[week];
            var giftData = weekData.dailyGifts[day];

            return giftData;
        }

        private void OnValidate()
        {
            foreach (var week in Weeks)
            {
                week.name = $"Week {Weeks.IndexOf(week)}";
                foreach (var day in week.dailyGifts)
                {
                    day.index = week.dailyGifts.IndexOf(day) + 1;
                    day.name = day.ToString();
                }
            }
        }


    }
    [Serializable]
    public class DailyGift
    {
        [HideInInspector] public string name;
        public int index;
        public List<ResourceData> GiftData;
        public override string ToString()
        {
            string result = $"Day {index}";
            for (int i = 0; i < GiftData.Count; i++)
            {
                var gift = GiftData[i];
                string detail = $" {gift.ResourceType}x{gift.ResourceValue}";
                result += detail;
            }
            return result;
        }
    }
    [Serializable]
    public class WeekGift
    {
        [HideInInspector] public string name;
        public List<DailyGift> dailyGifts;
    }
}