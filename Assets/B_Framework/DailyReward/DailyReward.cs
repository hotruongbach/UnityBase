using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DailyReward : MonoBehaviour
{
    public Image Bg;
    public Text textDay;
    public Image imageCoin;
    public TextMeshProUGUI textCoin;

    public Image PanelClaimed;
    public Image IconCheck;

    public Sprite imageCollect;
    public Sprite imageDefault;
    public DailyReward Initialize(int daynumber, float money)
    {
        textDay.gameObject.SetActive(true);
        textDay.text = $"Day {daynumber}";
        if (money > 0)
        {
            imageCoin.gameObject.SetActive(true);
            textCoin.gameObject.SetActive(true);
            textCoin.text = $"{money}";
        }            
        return this;
    }
}
