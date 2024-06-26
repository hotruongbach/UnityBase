using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using B_Framework.DesignPatterns.Observers;
enum KeySetting
{
    Sound,
    Vibration,
    Music,
    TermOfUse,
    Policy
}
public class ButtonInSetting : MonoBehaviour
{
    [SerializeField] Sprite imageOn, imageOff;
    [SerializeField] KeySetting key;
    [SerializeField] Button button;
    // Start is called before the first frame update
    private void OnEnable()
    {
        AddListen();
    }
    private void OnDisable()
    {
        RemoveListen();
    }
    void Start()
    {
        CheckImage();
    }
    void CheckImage()
    {
        switch (key)
        {
            case KeySetting.Sound:
                if (PlayerPrefsManager.Sound == true)
                {
                    button.image.sprite = imageOn;
                }
                else button.image.sprite = imageOff;
                break;
            case KeySetting.Vibration:
                if (PlayerPrefsManager.Vibration == true)
                {
                    button.image.sprite = imageOn;
                }
                else button.image.sprite = imageOff;
                break;
            case KeySetting.Music:
                if (PlayerPrefsManager.Music == true)
                {
                    button.image.sprite = imageOn;
                }
                else button.image.sprite = imageOff;
                break;
        }
        this.PostEvent(EventID.OnMusicChangeValue, PlayerPrefsManager.Music);
        this.PostEvent(EventID.OnSoundChangeValue, PlayerPrefsManager.Sound);
    }
    void AddListen()
    {
        switch (key)
        {
            case KeySetting.Sound:
                button.onClick.AddListener(Sound);
                break;
            case KeySetting.Vibration:
                button.onClick.AddListener(Vibration);
                break;
            case KeySetting.Music:
                button.onClick.AddListener(Music);
                break;
            case KeySetting.TermOfUse:
                button.onClick.AddListener(TermOfUse);
                break;
            case KeySetting.Policy:
                button.onClick.AddListener(Policy);
                break;
        }
    }
    void RemoveListen()
    {
        switch (key)
        {
            case KeySetting.Sound:
                button.onClick.RemoveAllListeners();
                break;
            case KeySetting.Vibration:
                button.onClick.RemoveAllListeners();
                break;
            case KeySetting.Music:
                button.onClick.RemoveAllListeners();
                break;
        }
    }
    void Sound()
    {
        if (PlayerPrefsManager.Sound == true)
        {
            PlayerPrefsManager.Sound = false;
        }
        else PlayerPrefsManager.Sound = true;
        CheckImage();
    }
    void Vibration()
    {
        if (PlayerPrefsManager.Vibration == true)
        {
            PlayerPrefsManager.Vibration = false;
        }
        else PlayerPrefsManager.Vibration = true;
        CheckImage();
    }
    void Music()
    {
        if (PlayerPrefsManager.Music == true)
        {
            PlayerPrefsManager.Music = false;
        }
        else PlayerPrefsManager.Music = true;
        CheckImage();
    }
    void TermOfUse()
    {

    }
    void Policy()
    {

    }
}
