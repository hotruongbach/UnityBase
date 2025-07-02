using Template;
using Template.User;
using Template.Utilities;
using MyBox;
using System;
using System.Collections;
using UnityEngine;

public class OnlineGiftManager : FeatureBase
{
    [SerializeField] OnlineGiftLibrary library;

    [SerializeField, ReadOnly] string LastClaimFreeGiftTime = string.Empty;

    [SerializeField, ReadOnly] public int CurrentVideoStage = 0;

    [SerializeField, ReadOnly] public int FreeTimeResetRemaining = 0;


    private void Default()
    {
        LastClaimFreeGiftTime = string.Empty;
        CurrentVideoStage = 0;
        FreeTimeResetRemaining = 0;
    }

    public override IEnumerator LoadData()
    {
        yield return null;
        string json = PlayerPrefs.GetString(PlayerPrefsKey.ONLINEGIFT, "");
        if (json.IsNullOrEmpty())
        {
            Default();
        }
        else
        {
            OnlineGiftDataWrapper dataWrapper = JsonUtility.FromJson<OnlineGiftDataWrapper>(json);
            LastClaimFreeGiftTime = dataWrapper.LastClaimFreeGiftTime;
            CurrentVideoStage = dataWrapper.CurrentVideoStage;

            
            DateTime lastFreeClaimed = DateTime.Now.AddSeconds(-library.OnlineGiftFree.ResetTime);
            try
            {
                lastFreeClaimed = LastClaimFreeGiftTime.ToDetailedDatetime();
            }
            catch
            {
                Bug.Log("Free claim time format is wrong");
            }
            
            FreeTimeResetRemaining = library.OnlineGiftFree.ResetTime - (int)(DateTime.Now - lastFreeClaimed).TotalSeconds;
        }
    }
    public override IEnumerator CheckNewDays(bool isNewDay)
    {
        yield return null;
        if (isNewDay) 
        {
            //reset all reward
            Default();
        }
    }
    public override IEnumerator StartFeature()
    {
        yield return null;
        OnlineGiftService.Start(this);
        Clock.Tick += CalculateTime;
    }

    private void CalculateTime(object sender, EventArgs e)
    {
        if (FreeTimeResetRemaining > 0) FreeTimeResetRemaining -= 1;
    }

    protected override void SaveData()
    {
        OnlineGiftDataWrapper dataWrapper = new OnlineGiftDataWrapper(LastClaimFreeGiftTime, CurrentVideoStage);
        string json = JsonUtility.ToJson(dataWrapper);
        PlayerPrefs.SetString(PlayerPrefsKey.ONLINEGIFT, json);

        Bug.Log("Online gift data saved " + json.Colored(Color.green));
    }

    [ButtonMethod]
    public void ClaimFreeOnlineGift()
    {
        if(FreeTimeResetRemaining <= 0)
        {
            int coinRewardValue = library.OnlineGiftFree.CoinReward;
            //ResourceManager.ClaimResource(ResourceType.Coin, coinRewardValue);

            UserManager.ClaimResource(ResourceType.Coin, coinRewardValue);

            FreeTimeResetRemaining = library.OnlineGiftFree.ResetTime;
            LastClaimFreeGiftTime = DateTime.Now.ToDetailedString();

            SaveData();
        }
        else
        {
            Bug.Log($"Can not claim now, wait {FreeTimeResetRemaining}s");
        }
    }

    [ButtonMethod]
    public void ClaimVideoOnlineGift()
    {
        if (CurrentVideoStage < library.OnlineGiftReward.onlineStageRewards.Count)
        {
            var reward = library.GetStageReward(CurrentVideoStage).Reward;
            //ResourceManager.ClaimResource(reward);
            UserManager.ClaimResource(reward);
            CurrentVideoStage++;
            SaveData();
        }
        else
        {
            Bug.Log($"Stage {CurrentVideoStage} exceeded");
        }
    }
}

public class OnlineGiftDataWrapper
{
    public string LastClaimFreeGiftTime;
    public int CurrentVideoStage;

    public OnlineGiftDataWrapper(string lastClaimFreeGiftTime, int currentVideoStage)
    {
        LastClaimFreeGiftTime = lastClaimFreeGiftTime;
        CurrentVideoStage = currentVideoStage;
    }
}
