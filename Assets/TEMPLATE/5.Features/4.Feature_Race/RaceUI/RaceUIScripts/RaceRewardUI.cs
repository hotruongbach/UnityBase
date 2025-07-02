using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RaceRewardUI : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] TMP_Text amount;

    public void Init(ResourceData resoucre)
    {
        this.amount.text = resoucre.ResourceValue.ToString();
    }
}
