using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NotifyCanvasController : BasePopup
{
    [SerializeField] TextMeshProUGUI text;
    bool isShow;
    Coroutine coClose;
    public void Initialize(string messenge)
    {
        if (isShow)
        {
            isShow = false;
        }
        else
        {
            gameObject.SetActive(true);
            OpenPopup();
            isShow = true;
            text.text = messenge;
            if (coClose != null)
            {
                StopCoroutine(coClose);
            }
            coClose = StartCoroutine(CoCloseCanvas());
        }
    }
    IEnumerator CoCloseCanvas()
    {
        yield return new WaitForSeconds(1f);
        ClosePopup();
        isShow = false;
    }
}
