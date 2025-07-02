using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Template.UI;
using UnityEngine.UI;
using TMPro;
using System;
public class PopupRewardSpin : Window
{
    [Header("Ui reward")]
    [SerializeField] Image imgItem;
    [SerializeField] TextMeshProUGUI textTotal;

    public void ShowUi(Sprite spriteItem, int total , bool isSkin)
    {
        imgItem.sprite = spriteItem;
        if (isSkin == true) textTotal.gameObject.SetActive(false);
        else
        {
            textTotal.gameObject.SetActive(true);
            textTotal.text = total.ToString();
        }
        Show();
    }

    public void ClosePopup()
    {
        Hide();
    }
}
