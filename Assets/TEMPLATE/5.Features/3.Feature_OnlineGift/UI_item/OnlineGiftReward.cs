using Template.UI;
using MyBox;
using UnityEngine;
using UnityEngine.Events;

public class OnlineGiftReward : MonoBehaviour
{
    [SerializeField] ResourceHolder holderPrefab;
    [SerializeField] Transform rewardRoot;
    [SerializeField] ButtonBase ClaimButton;
    [SerializeField] GameObject ClaimedButton;

    [SerializeField] OnlineGiftDot dot;
    [SerializeField] OnlineGiftConnector connector;

    public UnityEvent OnClaimComplete;

    private int CurrentStage => OnlineGiftService.CurrentVideoStage();

    [SerializeField,ReadOnly]private int stage;

    private void Start()
    {
        ClaimButton.AddListener(OnClaimButton);
    }

    public void BindDot(OnlineGiftDot dot)
    {
        this.dot = dot;
    }
    public void BindConnector(OnlineGiftConnector connector)
    {
        this.connector = connector;
    }

    private void OnClaimButton()
    {
        //play reward
        Bug.Log("PLAY VIDEO HERE", "red");

        //on video complete
        OnlineGiftService.ClaimVideoGift();
        OnClaimComplete?.Invoke();
    }

    public void SetData(int stage, ResourceData[] resources)
    {
        foreach (ResourceData data in resources)
        {
            var holder = Instantiate(holderPrefab, rewardRoot);
            holder.SetResource(data);
        }
        this.stage = stage;
    }
    public void SetData(int stage, ResourceData resources)
    {
        var holder = Instantiate(holderPrefab, rewardRoot);
        holder.SetResource(resources);
        this.stage = stage;
    }

    public void UpdateClaimState()
    {
        //claimable
        if (this.stage == CurrentStage)
        {
            ClaimedButton.SetActive(false);
            ClaimButton.gameObject.SetActive(true);
            dot?.Focus();
            connector?.FillUp(0.15f);
        }

        //unclaimable
        if (this.stage > CurrentStage)
        {
            ClaimedButton.SetActive(false);
            ClaimButton.gameObject.SetActive(false);
            dot?.Unfocus();
            connector?.Clear();
        }

        //claimed
        if (this.stage < CurrentStage)
        {
            ClaimedButton.SetActive(true);
            ClaimButton.gameObject.SetActive(false);
            dot?.Unfocus();
            connector?.FillUp(0);
        }
    }
}
