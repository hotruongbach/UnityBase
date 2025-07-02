using MyBox;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Monster.FEATURES.DailyGift
{
    public class GiftContainer : MonoBehaviour
    {
        [SerializeField] SpriteLibrary library;
        [SerializeField] Image Icon;
        [SerializeField] TMP_Text ValueText;

        [Separator("FILTERs")]
        [SerializeField] GameObject FilterClaimed;
        [SerializeField] GameObject FilterFocus;

        void _InitGift(Sprite resourceSprite, int value, bool isClaimed, bool isTodayGift)
        {
            Icon.sprite = resourceSprite;
            ValueText.text = $"x{value}";

            FilterClaimed.SetActive(isClaimed);
            FilterFocus.SetActive(!isClaimed && isTodayGift);
        }

        public void InitGift(List<ResourceData> giftData, bool isClaimed, bool isTodayGift)
        {
            var data = giftData[0];

            _InitGift(library.GetSprite(data.ResourceType), data.ResourceValue, isClaimed, isTodayGift);
        }

        public void SetClaimed()
        {
            FilterClaimed.SetActive(true);
            FilterFocus.SetActive(false);
        }
    }
}