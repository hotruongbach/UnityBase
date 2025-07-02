using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class OnlineGiftService
{
    public static OnlineGiftManager Manager;

    public static void Start(OnlineGiftManager Instance)
    {
        Manager = Instance;
    }

    public static void ClaimFreeGift()
    {
        Manager.ClaimFreeOnlineGift();
    }

    public static void ClaimVideoGift()
    {
        Manager.ClaimVideoOnlineGift();
    }

    public static int CurrentVideoStage()
    {
        return Manager.CurrentVideoStage;
    }

    public static int FreeResetTimeRemaining()
    {
        return Manager.FreeTimeResetRemaining;
    }
}
