using System;
using UnityEngine;
using B_Framework.DesignPatterns.Observers;

public class DailyRewardController : MonoBehaviour
{
    public DataDailyReward dataDailyReward;
    [SerializeField] DailyRewardCanvas dailyRewardCanvas;

    private void Start()
    {
        UnlockRewardDay();
        InitDailyReward();
    }
    public void UnlockRewardDay()
    {
        if (PlayerPrefsManager.CurrentDay == "")
        {
            PlayerPrefsManager.CurrentDay = UnbiasedTime.Instance.Now().ToString("d");
            PlayerPrefsManager.CheckClaim = true;
        }
        else if (DateTime.Parse(PlayerPrefsManager.CurrentDay).ToString("d") != DateTime.Parse(UnbiasedTime.Instance.Now().ToString("d")).ToString("d"))
        {
            PlayerPrefsManager.CurrentDay = UnbiasedTime.Instance.Now().ToString("d");
            if (PlayerPrefsManager.DayClaimed >= 7)
            {
                PlayerPrefsManager.DayClaimed = 0;
            }
            PlayerPrefsManager.CheckClaim = true;
        }
    }
    public void ShowPopup()
    {
        dailyRewardCanvas.OpenPopup();
        dailyRewardCanvas.CheckUI();
    }

    private void InitDailyReward()
    {
        dailyRewardCanvas.InitDailyReward(dataDailyReward.DailyRewardType);
    }

    public void Claim(int x = 1)
    {
        int i = PlayerPrefsManager.AmoutDailyClaimed;
        if (dataDailyReward.DailyRewardType[i].money > 0)
        {
            PlayerPrefsManager.Coin += (int)dataDailyReward.DailyRewardType[i].money * x;
            EventDispatcher.Instance.PostEvent(EventID.OnCoinChangeValue);
        }
        PlayerPrefsManager.CheckClaim = false;
        PlayerPrefsManager.CurrentDay = UnbiasedTime.Instance.Now().ToString("d");
        PlayerPrefsManager.DayClaimed++;
        dailyRewardCanvas.CheckUI();
        dailyRewardCanvas.ButtonClaim(false);
    }

    public void Claimx2()
    {
        Claim(2);
    }
}

