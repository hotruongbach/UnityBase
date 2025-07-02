using DG.Tweening;
using Monster.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceChestUI : MonoBehaviour
{
    [SerializeField] RaceCustomConfig config;

    [SerializeField] GameObject detailRoot;

    [SerializeField] RaceRewardUI coin;
    [SerializeField] RaceRewardUI hammer;
    [SerializeField] RaceRewardUI swap;
    [SerializeField] RaceRewardUI reroll;

    [SerializeField] RaceRewardPositionType positionType;

    private void Start()
    {
        var reward = config.FirstReward;
        switch (positionType)
        {
            case RaceRewardPositionType.First:
                break;
            case RaceRewardPositionType.Second:
                reward = config.SecondReward;
                break;
            case RaceRewardPositionType.Third:
                reward = config.ThirdReward;
                break;
        }
        //coin.Init(RaceIconType.Coin, reward.coin);
        //hammer.Init(RaceIconType.Hammer, reward.hammer);
        //swap.Init(RaceIconType.Swap, reward.swap);
        //reroll.Init(RaceIconType.Reroll, reward.reroll);
    }

    public void OntoggleChanged(bool isOn)
    {
        if (isOn)
        {
            //show detail
            ShowDetail();

            //auto close
            this.StartDelayAction(3f, () =>
            {
                HideDetail();
            });
        }
        else
        {
            //hide detail
            HideDetail();
        }
    }

    public void ShowDetail()
    {
        detailRoot.transform.DOKill();
        detailRoot.transform.localScale = Vector3.zero;
        detailRoot.gameObject.SetActive(true);
        detailRoot.transform.DOScale(Vector3.one, 0.25f);
    }
    public void HideDetail()
    {
        this.StopAllCoroutines();

        detailRoot.transform.DOKill();
        detailRoot.transform.DOScale(Vector3.zero, 0.25f).OnComplete(() =>
        {
            detailRoot.gameObject.SetActive(false);
        });
    }
}

public enum RaceRewardPositionType
{
    First = 0,
    Second = 1,
    Third = 2,
}
