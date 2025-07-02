using MyBox;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceHolder : MonoBehaviour
{
    [SerializeField] SpriteLibrary library;
    [SerializeField] Image ResourceIcon;
    [SerializeField] TMP_Text AmountText;
    [SerializeField] RectTransform RectTransform;

    public void SetIcon(Sprite resourceSprite)
    {
        ResourceIcon.sprite = resourceSprite;
    }

    public void SetAmount(int amount)
    {
        AmountText.text = amount.ToString();
    }

    public void SetResource(ResourceData data)
    {
        ResourceIcon.sprite = library.GetSprite(data.ResourceType);
        AmountText.text = data.ResourceValue.ToString();
    }

    public void SetResource(ResourceType type, int resourceValue)
    {
        ResourceIcon.sprite = library.GetSprite(type);
        AmountText.text = resourceValue.ToString();
    }


    public void SetSize(float width, float height)
    {
        RectTransform.sizeDelta = new Vector2(width, height);
    }
    [ButtonMethod]
    public void SetDefaultSize()
    {
        SetSize(100, 100);
    }
}
