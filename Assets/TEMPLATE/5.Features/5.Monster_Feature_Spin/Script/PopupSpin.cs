using DG.Tweening;
using Monster.User;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Monster.Spin
{
    public class PopupSpin : FeatureBase
    {
        [Header("Data and Init")]
        [SerializeField] DataSpin dataSpin;
        [SerializeField] InitItemSpin itemPrefab;
        [SerializeField] List<Transform> lstPosItem = new List<Transform>();
        List<InitItemSpin> lstItem = new List<InitItemSpin>();

        [Header("Ui")]
        [SerializeField] GameObject circle;
        [SerializeField] Button btnClose;
        [SerializeField] Button btnSpin;
        [SerializeField] Button btnSpinWatchAds;
        [SerializeField] PopupRewardSpin popupReward;

        int hours;
        int minutes;
        int seconds;

        int idReward;
        SpinPlayerData playerDataSpin;
        private void Awake()
        {
            btnSpin.onClick.AddListener(EventButtonSpin);
        }
        private void Start()
        {
            InitReward();
            TimeCountdown();
            circle.transform.rotation = Quaternion.identity;
        }
        public override IEnumerator LoadData()
        {
            yield return null;
            string json = PlayerPrefs.GetString(PlayerPrefsKey.SPIN, "{}");
            playerDataSpin = JsonUtility.FromJson<SpinPlayerData>(json);
            ActiveButton(true);
        }
        protected override void SaveData()
        {
            string json = JsonUtility.ToJson(playerDataSpin);
            PlayerPrefs.SetString(PlayerPrefsKey.SPIN, json);
        }
        private void Update()
        {
            hours = Mathf.FloorToInt((float)TimeCountdown().TotalHours);
            minutes = Mathf.FloorToInt((float)TimeCountdown().TotalMinutes % 60);
            seconds = Mathf.FloorToInt((float)TimeCountdown().TotalSeconds % 60);
        }
        void InitReward()
        {
            for (int i = 0; i < dataSpin.ListItemSpins.Count; i++)
            {
                InitItemSpin item = Instantiate(itemPrefab, lstPosItem[i]);
                item.Setup(dataSpin.ListItemSpins[i]);
                lstItem.Add(item);
            }
        }
        void ActiveButton(bool isActive)
        {
            btnClose.interactable = isActive;
            if (playerDataSpin.AmountTurnSpin > 0)
            {
                btnSpin.interactable = isActive;
                btnSpin.gameObject.SetActive(isActive);
            }
            else
            {
                btnSpinWatchAds.interactable = isActive;
                btnSpinWatchAds.gameObject.SetActive(isActive);
            }

        }
        public void EventButtonSpin()
        {
            if (playerDataSpin.AmountTurnSpin > 0)
            {
                Spin();
            }
        }
        void Spin()
        {
            ActiveButton(false);
            RotateCircle();
        }
        Coroutine coroutineShow;
        Coroutine coroutineInit;
        WaitForSeconds wait = new WaitForSeconds(2f);
        IEnumerator ShowInitAgain()
        {
            yield return wait;
            InitReward();
        }

        void RotateCircle()
        {
            int amountItem = dataSpin.amountItem;

            int randomPart = UnityEngine.Random.Range(0, amountItem);
            float anglePerPart = 360f / amountItem;
            idReward = randomPart;
            float targetAngle = 0 - (randomPart * anglePerPart);
            int randomRotations = UnityEngine.Random.Range(5, 8);
            float additionalRotations = 360f * randomRotations;
            float finalAngle = targetAngle - additionalRotations;
            int randomDuration = UnityEngine.Random.Range(3, 5);
            circle.transform.DORotate(new Vector3(0, 0, finalAngle), randomDuration, RotateMode.FastBeyond360)
               .SetEase(Ease.InOutQuart)
               .OnComplete(() =>
               {
                   if (coroutineShowpopup != null) StopCoroutine(coroutineShowpopup);
                   coroutineShowpopup = StartCoroutine(ShowPopupRewardSpin());
               });
        }
        Coroutine coroutineShowpopup;
        IEnumerator ShowPopupRewardSpin()
        {
            yield return wait = new WaitForSeconds(0.5f);
            ReciveItem(idReward);
        }
        void ReciveItem(int id)
        {
            if (lstItem[id].itemInfo.type == RewardSpin.Coin)
            {
                int coinSpend = lstItem[id].itemInfo.total;
                //ResourceManager.SpendResource(ResourceType.Coin, lstItem[id].itemInfo.total);
                UserManager.ClaimResource(ResourceType.Coin, lstItem[id].itemInfo.total);
                if (coroutineShow != null) StopCoroutine(coroutineShow);
                coroutineShow = StartCoroutine(ShowSuccessSpin(lstItem[id].itemInfo.img, lstItem[id].itemInfo.total));
            }
            else if (lstItem[id].itemInfo.type == RewardSpin.Diamond)
            {
                int coinSpend = lstItem[id].itemInfo.total;
                //ResourceManager.SpendResource(ResourceType.Diamond, lstItem[id].itemInfo.total);
                //UserManager.ClaimResource(coinSpend);

                Bug.Log("Claim not implement");

                if (coroutineShow != null) StopCoroutine(coroutineShow);
                coroutineShow = StartCoroutine(ShowSuccessSpin(lstItem[id].itemInfo.img, lstItem[id].itemInfo.total));
            }
            else return;
        }
        IEnumerator ShowSuccessSpin(Sprite sprite, int total, bool isSkin = false)
        {
            yield return new WaitForSeconds(0.5f);
            ActiveButton(true);
            circle.transform.rotation = Quaternion.identity;
            popupReward.ShowUi(sprite, total, isSkin);
        }
        //Check time
        private TimeSpan TimeCountdown()
        {
            DateTime now = DateTime.Now; /*DateTime.Parse(UnbiasedTime.Instance.Now().ToString());*/
            DateTime nextDay = now.AddDays(1).Date;
            TimeSpan remainingTime = nextDay - now;
            return remainingTime;
        }

        public override IEnumerator CheckNewDays(bool isNewDay)
        {
            throw new NotImplementedException();
        }

        public override IEnumerator StartFeature()
        {
            throw new NotImplementedException();
        }
    }
}
[Serializable]
public struct SpinPlayerData
{
    public int AmountTurnSpin;
}

