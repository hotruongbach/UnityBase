using Template.Utilities;
using MyBox;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class OnlineGiftLayout : MonoBehaviour
{
    [SerializeField] OnlineGiftLibrary OnlineGiftLibrary;
    [SerializeField] Image DotPrefab;
    [SerializeField] Image ConnectorPrefab;
    [SerializeField] OnlineGiftReward RewardPrefab;
    [Separator]
    [SerializeField] Transform DotRoot;
    [SerializeField] Transform RewardRoot;
    [SerializeField] int RewardStageCount => OnlineGiftLibrary.OnlineGiftReward.onlineStageRewards.Count;

    [SerializeField] List<OnlineGiftReward> Rewards = new List<OnlineGiftReward>();

    public void UpdateStateAll()
    {
        foreach (var item in Rewards)
        {
            item.UpdateClaimState();
        }
    }

    #region EDITOR TOOL

#if UNITY_EDITOR
    [ButtonMethod] public void Spawn()
    {
        SpawnRewardLayout();
        SpawnDotLayout();
    }

    private void SpawnDotLayout()
    {
        for (int i = 0; i < RewardStageCount - 1; i++)
        {
            Vector2 anchorMax = new Vector2(0.5f, 1 - i * ((float)1 / (RewardStageCount - 1)));
            Vector2 anchorMin = new Vector2(0.5f, 1 - (i + 1) * ((float)1 / (RewardStageCount - 1)));
            var connector = PrefabUtility.InstantiatePrefab(ConnectorPrefab, DotRoot).GetComponent<OnlineGiftConnector>();
            var connectorRect = connector.GetComponent<RectTransform>();
            connectorRect.anchorMin = anchorMin;
            connectorRect.anchorMax = anchorMax;
            connectorRect.anchoredPosition = Vector3.zero;

            var dot = PrefabUtility.InstantiatePrefab(DotPrefab, DotRoot).GetComponent<OnlineGiftDot>();
            var dotRect = dot.GetComponent<RectTransform>();
            dotRect.anchorMin = anchorMax;
            dotRect.anchorMax = anchorMax;
            dotRect.anchoredPosition = Vector3.zero;

            Rewards[i].BindDot(dot);
            Rewards[i + 1].BindConnector(connector);
        }

        //last dot 
        var lastDot = PrefabUtility.InstantiatePrefab(DotPrefab, DotRoot).GetComponent<OnlineGiftDot>();
        var lastDotRect = lastDot.GetComponent<RectTransform>();
        lastDotRect.anchorMin = new Vector2(0.5f, 0);
        lastDotRect.anchorMax = new Vector2(0.5f, 0);
        lastDotRect.anchoredPosition = Vector3.zero;

        Rewards.Last().BindDot(lastDot);
    }

    private void SpawnRewardLayout()
    {
        for (int i = 0; i < RewardStageCount; i++)
        {
            Vector2 anchorMin = new Vector2(0, 1 - i * ((float)1 / (RewardStageCount - 1)));
            Vector2 anchorMax = new Vector2(1, 1 - i * ((float)1 / (RewardStageCount - 1)));

            var newReward = PrefabUtility.InstantiatePrefab(RewardPrefab, RewardRoot).GetComponent<OnlineGiftReward>();
            var rewardRect = newReward.GetComponent<RectTransform>();

            rewardRect.anchorMin = anchorMin;
            rewardRect.anchorMax = anchorMax;
            rewardRect.anchoredPosition = Vector3.zero;

            newReward.SetData(i, OnlineGiftLibrary.GetStageReward(i).Reward);

            newReward.OnClaimComplete.AddListener(UpdateStateAll);

            Rewards.Add(newReward);
        }
    }

    [ButtonMethod] public void DestroyAllChildren()
    {
        for (int i = DotRoot.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(DotRoot.transform.GetChild(i).gameObject);
        }
        for (int i = RewardRoot.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(RewardRoot.transform.GetChild(i).gameObject);
        }
        Rewards.Clear();
    }
#endif
#endregion
}
