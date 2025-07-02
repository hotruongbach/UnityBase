using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InitItemSpin : MonoBehaviour
{
    public ItemSpin itemInfo;
    [SerializeField] Image imgIcon;
    [SerializeField] TextMeshProUGUI txtTotal;

    public void Setup(ItemSpin info)
    {
        itemInfo = info;
        if (info.type == RewardSpin.Skin)
        {
            txtTotal.gameObject.SetActive(false);
            imgIcon.sprite = info.img;
        }
        else
        {
            txtTotal.gameObject.SetActive(true);
            txtTotal.text = $"+{info.total}";
            imgIcon.sprite = info.img;
        }
    }
}
