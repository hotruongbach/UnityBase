using Template;
using UnityEngine;
using UnityEngine.UI;
public class ToggleAnimation : MonoBehaviour
{
    [SerializeField] RectTransform handle;
    [SerializeField] Image toggleBG;
    [SerializeField] private Sprite bgOn;
    [SerializeField] private Sprite bgOff;

    Vector2 AnchorLeft = new Vector2(0, 0.5f);
    Vector2 AnchorRight = new Vector2(1, 0.5f);

    public void OnToggleChanged(bool IsOn)
    {
        if (IsOn)
        {
            HandleToRight();
            ChangeColorOn();
        }
        else
        {
            HandleToLeft();
            ChangeColorOff();
        }
    }

    void HandleToLeft()
    {
        handle.anchorMax = AnchorLeft;
        handle.anchorMin = AnchorLeft;
        handle.pivot = AnchorLeft;
    }

    void HandleToRight()
    {
        handle.anchorMax = AnchorRight;
        handle.anchorMin = AnchorRight;
        handle.pivot = AnchorRight;
    }

    void ChangeColorOn()
    {
        toggleBG.sprite = bgOn;
    }
    void ChangeColorOff()
    {
        toggleBG.sprite = bgOff;
    }

}
