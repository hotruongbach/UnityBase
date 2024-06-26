using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager
{
    public const string PREFS_SOUND = "sound";
    public const string PREFS_VIBRATION = "vibration";
    public const string PREFS_MUSIC = "music";
    public const string PREFS_CURRENT_LEVEL = "current_index_level";
    public const string PREFS_CURRENT_MONEY = "current_money";
    public const string PREFS_LEVEL = "current_level";
    public const string PREFS_LEVEL_BONUS = "level_bonus";
    public const string PREFS_MONEY = "money";
    public const string PREFS_LIST_SKIN = "list_skin";
    public const string PREFS_SKIN_USING = "skin_using";
    public const string PREFS_LIST_AMOUNT_VIDEO_SKIN = "list_amount_video_skin";
    public const string PREFS_CURRENT_DAY = "current_day";
    public const string PREFS_RATE_5STAR = "RATE_5STAR";


    public static bool Sound
    {
        get => PlayerPrefs.GetInt(PREFS_SOUND, 1) == 1;
        set => PlayerPrefs.SetInt(PREFS_SOUND, value ? 1 : 0);
    }

    public static bool Vibration
    {
        get => PlayerPrefs.GetInt(PREFS_VIBRATION, 1) == 1;
        set => PlayerPrefs.SetInt(PREFS_VIBRATION, value ? 1 : 0);
    }

    public static bool Music
    {
        get => PlayerPrefs.GetInt(PREFS_MUSIC, 1) == 1;
        set => PlayerPrefs.SetInt(PREFS_MUSIC, value ? 1 : 0);
    }
    public static int CurrentLevel
    {
        get => PlayerPrefs.GetInt(PREFS_LEVEL, 0);
        set
        {
            Debug.Log(value);
            PlayerPrefs.SetInt(PREFS_LEVEL, value);
        }
    }
    public static int CurrentMoney
    {
        get => PlayerPrefs.GetInt(PREFS_CURRENT_MONEY, 100);
        set => PlayerPrefs.SetInt(PREFS_CURRENT_MONEY, value);
    }
    public static int LevelBonus
    {
        get => PlayerPrefs.GetInt(PREFS_LEVEL_BONUS, 0);
        set => PlayerPrefs.SetInt(PREFS_LEVEL_BONUS, value);
    }


    public static int CurrentIndex
    {
        get => PlayerPrefs.GetInt(PREFS_CURRENT_LEVEL, 0);
        set => PlayerPrefs.SetInt(PREFS_CURRENT_LEVEL, value);
    }

    public static float Money
    {
        get => PlayerPrefs.GetFloat(PREFS_MONEY, 0);
        set => PlayerPrefs.SetFloat(PREFS_MONEY, value);
    }
    public static int SkinUsing
    {
        get => PlayerPrefs.GetInt(PREFS_SKIN_USING, 0);
        set => PlayerPrefs.SetInt(PREFS_SKIN_USING, value);
    }
    public static int[] ListSkin
    {
        get => PlayerPrefsBase.GetIntArray(PREFS_LIST_SKIN);
        set => PlayerPrefsBase.SetIntArray(PREFS_LIST_SKIN, value);
    }
    public static int[] ListAmountVideoSkin
    {
        get => PlayerPrefsBase.GetIntArray(PREFS_LIST_AMOUNT_VIDEO_SKIN);
        set => PlayerPrefsBase.SetIntArray(PREFS_LIST_AMOUNT_VIDEO_SKIN, value);
    }
   
    public static int Rate5Star
    {
        get => PlayerPrefs.GetInt(PREFS_RATE_5STAR, 0);
        set => PlayerPrefs.SetInt(PREFS_RATE_5STAR, value);
    }

    private const string PREFS_AMOUT_DAILY_CLAIMED = "amout_daily_claimed";
    private const string PREF_DAY_CLAIMED = "day_claimed";
    private const string PREF_CHECK_CLAIM_DAILY_REWARD = "check_claim_daily_reward";

    #region Daily Check
    public static string CurrentDay
    {
        get => PlayerPrefs.GetString(PREFS_CURRENT_DAY, "");
        set => PlayerPrefs.SetString(PREFS_CURRENT_DAY, value);
    }
    public static int AmoutDailyClaimed
    {
        get => PlayerPrefs.GetInt(PREFS_AMOUT_DAILY_CLAIMED, -1);
        set => PlayerPrefs.SetInt(PREFS_AMOUT_DAILY_CLAIMED, value);
    }
    public static int DayClaimed
    {
        get => PlayerPrefsBase.GetInt(PREF_DAY_CLAIMED);
        set => PlayerPrefsBase.SetInt(PREF_DAY_CLAIMED, value);
    }
    public static bool CheckClaim
    {
        get => PlayerPrefs.GetInt(PREF_CHECK_CLAIM_DAILY_REWARD, 1) == 1;
        set => PlayerPrefs.SetInt(PREF_CHECK_CLAIM_DAILY_REWARD, value ? 1 : 0);
    }
    #endregion
    public const string PREFS_COIN = "coin";
    public static int Coin
    {
        get => PlayerPrefs.GetInt(PREFS_COIN);
        set => PlayerPrefs.SetInt(PREFS_COIN, value);
    }
}