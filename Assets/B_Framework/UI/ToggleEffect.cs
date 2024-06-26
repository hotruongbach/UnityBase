using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEditor;

public class ToggleEffect : MonoBehaviour
{
    [SerializeField] RectTransform Handle;
    [SerializeField] Sprite panOff;
    [SerializeField] Sprite panOn;
    [SerializeField] Toggle _toggle;
    [SerializeField] KeySetting typeSetting;
    float wHandle;
    // Start is called before the first frame update
    void Start()
    {

        _toggle = GetComponent<Toggle>();
        _toggle.onValueChanged.AddListener(Switch);
        wHandle = Handle.rect.width / 2;
        if (typeSetting == KeySetting.Music)
        {
            _toggle.onValueChanged.AddListener(ChangeMusic);
            if (PlayerPrefsManager.Music == true)
            {
                Switch(true);
            }
            else
            {
                Switch(false);
            }
        }
        else if (typeSetting == KeySetting.Vibration)
        {
            _toggle.onValueChanged.AddListener(ChangeVibration);
            if (PlayerPrefsManager.Vibration == true)
            {
                Switch(true);
            }
            else
            {
                Switch(false);
            }
        }
    }

    public void Switch(bool off)
    {
        Handle.DOAnchorPos(off ? new Vector2(- wHandle, 0) : new Vector2(wHandle, 0), .4f).SetEase(Ease.InOutBack);
        if (off)
        {
            _toggle.targetGraphic.GetComponent<Image>().sprite = panOff;
        }
        else
        {
            _toggle.targetGraphic.GetComponent<Image>().sprite = panOn;
        }
    }
    public void ChangeMusic(bool isOn)
    {
        if (PlayerPrefsManager.Music == isOn)
        {
            PlayerPrefsManager.Music = !isOn;
            Switch(!isOn);
        }
        else
        {
            PlayerPrefsManager.Music = isOn;
            Switch(isOn);
        }
    }
    public void ChangeVibration(bool isOn)
    {
        if (PlayerPrefsManager.Vibration == isOn)
        {
            PlayerPrefsManager.Vibration = !isOn;
            Switch(!isOn);
        }
        else
        {
            PlayerPrefsManager.Vibration = isOn;
            Switch(isOn);
        }
    }
}
