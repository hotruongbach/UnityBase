using Monster.UI;
using UnityEngine;
using UnityEngine.Events;

public class OnlineGiftUIFree : MonoBehaviour
{
    [SerializeField] ButtonBase ClaimButton;
    [SerializeField] TimeArea TimeArea;

    public void AddListener(UnityAction OnClaimComplete)
    {
        ClaimButton.AddListener(OnClaimComplete);
    }

    public void UpdateTime(int totalSecond)
    {
        TimeArea.SetTime(totalSecond);

        if (totalSecond <= 0)
        {
            ClaimButton.gameObject.SetActive(true);
            TimeArea.gameObject.SetActive(false);
        }
        else
        {
            ClaimButton.gameObject.SetActive(false);
            TimeArea.gameObject.SetActive(true);
        }
    }
}
