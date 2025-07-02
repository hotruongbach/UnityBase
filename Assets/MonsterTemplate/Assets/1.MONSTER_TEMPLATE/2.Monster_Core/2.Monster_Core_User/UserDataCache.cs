using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UserDataCache : MonoBehaviour
{
    static int coin;
    static int heart;
    public static int Coin
    {
        get => coin;
        set
        {
            coin = value;
            PlayerPrefsManager.Coin = value;
            MonsterEventManager.OnChangeCoin.Post(null, coin);
        }
    }
    public static int Heart
    {
        get => heart;
        set
        {
            heart = value;
            PlayerPrefsManager.Heart = value;
            MonsterEventManager.OnChangeHeart.Post(null, heart);
        }
    }

    private void Start()
    {
        Coin = PlayerPrefsManager.Coin;
        Heart = PlayerPrefsManager.Heart;
    }
    public static void SaveData()
    {
        PlayerPrefs.Save();
    }
}
