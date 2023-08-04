using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using UnityEngine;
using UnityEngine.UI;

public class ContinueCanvas : BasePopup
{
    float time = 5;
    [SerializeField] Image fill;
    [SerializeField] Image imageContinue;
    bool isShow;

    bool isContinue;
    public static int amountRevive;
    // Start is called before the first frame update
    void Start()
    {
        time = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (time > 0 && isShow == true)
        {
            time -= Time.deltaTime;
            fill.fillAmount = time / 5;
        }
        else if (isShow == true && time <= 0)
        {
            time = 5;
            fill.fillAmount = 1;
        }
        else return;
    }
    public void ShowContinue(object param)
    {
        if (isShow == true) return;
        isShow = true;
        isContinue = false;
        OpenPopup();
        amountRevive++;
    }
    public void Skip()
    {

        isShow = false;
        isContinue = false;
        this.PostEvent(EventID.LoseGame);
        ClosePopup();
        amountRevive = 0;

    }
    Coroutine coroutine;
    public void Continue()
    {
        time = 5;
        isContinue = true;
        isShow = false;
        mainPanel.transform.DOScale(Vector3.zero, 0.5f);
        imageContinue.gameObject.SetActive(true);
        SoundManager.instance.PlaySingle(SoundType.Revive);
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(IEContinue());
    }
    IEnumerator IEContinue()
    {
        yield return new WaitForSeconds(2f);
        imageContinue.gameObject.SetActive(false);
        ClosePopup();
    }
}
