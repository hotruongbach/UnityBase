using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;

public class RateUs : MonoBehaviour
{
    public static RateUs Instance;
    private const string PREF_RATE_STAR = "rate_star";
    public static bool isShowRate;
    private int rate
    {
        get
        {
            return PlayerPrefs.GetInt(PREF_RATE_STAR, -1);
        }
        set
        {
            PlayerPrefs.SetInt(PREF_RATE_STAR, value);
        }
    }

    [SerializeField] StarRate[] stars;
    [SerializeField] GameObject ratePopup;

    public void Show()
    {
        gameObject.SetActive(true);
        isShowRate = false;
    }


    public void OnClickStar(int num)
    {
        for (int i = 0; i < 5; i++)
        {
            stars[i].ResetStar();
            if (i < num) stars[i].RateStar();
        }
        rate = num;
    }

    public void OnClickRate()
    {
        if (rate > 0)
        {
            DoRate();
        }  
    }

    void DoRate()
    {
        if (rate > 4)
        {
            HighRateClick();
            PlayerPrefsManager.Rate5Star = 1;
            Close();
        }
        else
        {
            Close();
        }
    }

    public void HighRateClick()
    {
#if UNITY_ANDROID
        if (Application.platform == RuntimePlatform.Android)
        {
            Application.OpenURL("market://details?id=" + Application.identifier);
            Close();
        }
        else
        {
            Close();
        }
#else
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            if (!UnityEngine.iOS.Device.RequestStoreReview())
            {
                //Application.OpenURL($"https://apps.apple.com/app/id{GameSDKSettings.APPLE_APP_ID}?action=write-review");
            }
        }
        Close();
#endif
    }

    public void Close()
    {
        this.gameObject.SetActive(false);
    }

}
