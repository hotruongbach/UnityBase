using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class OnlineGiftConnector : MonoBehaviour
{
    [SerializeField] Image connectorFiller;
    public void FillUp(float duration)
    {
        connectorFiller.DOKill();
        connectorFiller.DOFillAmount(1, duration);
    }

    public void Clear()
    {
        connectorFiller.fillAmount = 0;
    }
}
