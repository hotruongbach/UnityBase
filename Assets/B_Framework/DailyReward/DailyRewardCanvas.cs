using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DailyRewardCanvas : BasePopup
{
    public List<DailyReward> lstDaily = new List<DailyReward>();
    public TextMeshProUGUI timeTitle;
    public TextMeshProUGUI timeLeft;
    public GameObject BtnClaim;
    public GameObject BtnClaimx2;

    [SerializeField] Transform targetCoin;
    [SerializeField] GameObject coinClaim;
    [SerializeField] Color textColor;
    public Image NotiIcon;
    private int HoursNow;
    private int MinutesNow;
    private int SecondsNow;
    public void InitDailyReward(List<DailyRewardType> DailyRewardType)
    {
        for (int i = 0; i < DailyRewardType.Count; i++)
        {
            DailyRewardType Day = DailyRewardType[i];
            lstDaily[i].Initialize(Day.day, Day.money);

        }
    }
    public void Update()
    {
        CheckTimeUnlockReward();
    }
    public void CheckTimeUnlockReward()
    {
        if (PlayerPrefsManager.CheckClaim == true)
        {
            ButtonClaim(true);
        }
        else
        {
            HoursNow = int.Parse(UnbiasedTime.Instance.Now().ToString("HH"));
            MinutesNow = int.Parse(UnbiasedTime.Instance.Now().ToString("mm"));
            SecondsNow = int.Parse(UnbiasedTime.Instance.Now().ToString("ss"));
            ButtonClaim(false);
            timeLeft.text = $"{24 - HoursNow}:{59 - MinutesNow}:{59 - SecondsNow}";
        }
    }
    public void ButtonClaim(bool isShow)
    {
        NotiIcon.gameObject.SetActive(isShow);
        BtnClaim.SetActive(isShow);
        BtnClaimx2.SetActive(isShow);
        timeTitle.gameObject.SetActive(!isShow);
        timeLeft.gameObject.SetActive(!isShow);
    }
    public void ResetUI()
    {
        for (int i = 0; i < lstDaily.Count; i++)
        {
            lstDaily[i].Bg.sprite = lstDaily[i].imageDefault;
            lstDaily[i].textDay.color = textColor;
            lstDaily[i].PanelClaimed.gameObject.SetActive(false);
            lstDaily[i].IconCheck.gameObject.SetActive(false);
        }
    }
    public void CheckUI()
    {
        for (int i = 0; i <= PlayerPrefsManager.AmoutDailyClaimed; i++)
        {
            if (i == PlayerPrefsManager.AmoutDailyClaimed && PlayerPrefsManager.CheckClaim == true)
            {
                lstDaily[i].Bg.sprite = lstDaily[i].imageCollect;
                lstDaily[i].textDay.color = Color.white;
            }
            else
            {
                lstDaily[i].Bg.sprite = lstDaily[i].imageDefault;
                lstDaily[i].textDay.color = textColor;
                lstDaily[i].PanelClaimed.gameObject.SetActive(true);
                lstDaily[i].IconCheck.gameObject.SetActive(true);
            }
        }
    }
}
