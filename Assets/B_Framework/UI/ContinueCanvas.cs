using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using B_Framework.DesignPatterns.Observers;

public class ContinueCanvas : BasePopup
{
    float time = 5;
    [SerializeField] Image fill;
    [SerializeField] Image imageContinue;
    bool isShow;

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
        OpenPopup();
        amountRevive++;
    }
    public void Skip()
    {
        isShow = false;
        this.PostEvent(EventID.LoseGame);
        ClosePopup();
        amountRevive = 0;

    }
    Coroutine coroutine;
    public void Continue()
    {
        time = 5;
        isShow = false;
        mainPanel.transform.DOScale(Vector3.zero, 0.5f);
        imageContinue.gameObject.SetActive(true);
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
