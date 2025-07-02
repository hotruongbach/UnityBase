using DG.Tweening;
using MyBox;
using TMPro;
using UnityEngine;

public class PushNotification : MonoBehaviour
{
    [Separator("REFERENCES")]
    [SerializeField] RectTransform rectTF;
    [SerializeField] TMP_Text text; 
    
    [Separator("CONFIG")]
    [Tooltip("Time to this notification to animate show. If = 0, this will show immidiately")]
    [InitializationField,SerializeField] float showDuration = 0.1f;

    [Tooltip("Time to this notification to stay visible before hide")]
    [InitializationField, SerializeField] float visibleDuration = 3f;

    [Tooltip("Time to this notification to animate hide")]
    [InitializationField, SerializeField] float hideDuration = 0.15f;

    [Tooltip("Height of noti")]
    [InitializationField, SerializeField] float height = 100;

    private void OnValidate()
    {
        //auto cache rect transform
        rectTF = GetComponent<RectTransform>();
    }

    public void Init(float showDuration, float visibleDuration, float hideDuration,float height)
    {
        this.showDuration = showDuration;
        this.visibleDuration = visibleDuration;
        this.hideDuration = hideDuration;
        this.height = height;
    }

    public void Show(string message)
    {
        this.StopAllCoroutines();

        text.text = message;
        this.transform.SetAsLastSibling();

        OnShow();

        //auto hide after visible time
        this.DelayedAction(visibleDuration, () =>
        {
            Hide();
        }, true);
    }

    private void OnShow()
    {
        this.gameObject.SetActive(true);

        Vector2 newSize = new Vector2(rectTF.sizeDelta.x, height);
        rectTF.DOSizeDelta(newSize, showDuration);
    }

    private void Hide()
    {
        Vector2 newSize = new Vector2(rectTF.sizeDelta.x, 0);
        rectTF.DOSizeDelta(newSize, hideDuration).OnComplete(() =>
        {
            this.gameObject.SetActive(false);
        }); ;
    }
}
