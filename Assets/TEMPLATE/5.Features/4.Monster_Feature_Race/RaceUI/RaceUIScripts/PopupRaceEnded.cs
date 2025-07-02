using Monster;
using Monster.UI;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupRaceEnded : Window
{
    RaceData eventData => RaceManager.Data;
    RaceCustomConfig eventConfig => RaceManager.CustomConfig;

    [SerializeField] TMP_Text endQuote; //congratulation or OOPS!
    [SerializeField] TMP_Text endRankText;
    [SerializeField] GameObject rewardRoot;
    [SerializeField] List<RaceRewardUI> raceReward = new List<RaceRewardUI>();
    [SerializeField] ButtonBase claimButton;
    [SerializeField] ButtonBase finishButton;

    private void Start()
    {
        claimButton.AddListener(OnClaim);
        finishButton.AddListener(OnClaim);
    }
    public override void Show(Action onComplete = null, object param = null)
    {
        base.Show(onComplete);
        int pos = eventData.GetPlayerFinishPosition();
        ShowUserEndPosition(pos);
        ShowRaceReward(pos);
    }
    private void ShowUserEndPosition(int pos)
    {
        string posString = "";

        switch (pos)
        {
            case 0:
                posString = "1st";
                endQuote.text = "CONGRATULATION!";
                //confetti.Play();
                break;
            case 1:
                posString = "2nd";
                endQuote.text = "CONGRATULATION!";
                //confetti.Play();
                break;
            case 2:
                posString = "3rd";
                endQuote.text = "CONGRATULATION!";
                //confetti.Play();
                break;
            case 3:
            case 4:
                posString = "4th";
                endQuote.text = "OOPS!";
                break;
        }
        endRankText.text = posString;
    }

    private void ShowRaceReward(int rank)//rank from 0->2
    {
        rewardRoot.SetActive(rank < 3);

        var reward = new List<ResourceData>();
        switch (rank)
        {
            case 0: reward = eventConfig.FirstReward; break;
            case 1: reward = eventConfig.SecondReward; break;
            case 2: reward = eventConfig.ThirdReward; break;
        }

        //raceRewardUIs[0].Init(RaceIconType.Coin, reward.coin);
        //raceRewardUIs[1].Init(RaceIconType.Hammer, reward.hammer);
        //raceRewardUIs[2].Init(RaceIconType.Swap, reward.swap);
        //raceRewardUIs[3].Init(RaceIconType.Reroll, reward.reroll);

        claimButton.gameObject.SetActive(rank < 3);
        finishButton.gameObject.SetActive(rank >= 3);
    }

    private void OnClaim()
    {
        GameService.ClosePopup();
        RaceManager.ClaimReward();
    }
}
