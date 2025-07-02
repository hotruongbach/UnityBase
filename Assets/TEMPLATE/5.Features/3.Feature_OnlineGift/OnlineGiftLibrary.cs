using System;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "OnlineGiftLibrary", menuName = "GAME/ONLINEGIFT/Library")]
public class OnlineGiftLibrary : ScriptableObject
{
    public OnlineGiftByTime OnlineGiftFree;
    public OnlineGiftByVideo OnlineGiftReward;

    public OnlineStageReward GetStageReward(int stage)
    {
        if (stage < 0 || stage >= OnlineGiftReward.onlineStageRewards.Count)
        {
            Bug.Log("Stage not found");
            return null;
        }
        return OnlineGiftReward.onlineStageRewards[stage];
    }
}
[Serializable]
public class OnlineGiftByTime
{
    [Tooltip("Time in seconds")]
    public int ResetTime = 3600;
    public int CoinReward = 15;
}
[Serializable]
public class OnlineGiftByVideo
{
    public List<OnlineStageReward> onlineStageRewards = new List<OnlineStageReward>();
}
[Serializable]
public class OnlineStageReward
{
    public int Stage;
    public ResourceData Reward;
}