using System;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerPrefsManager
{
    #region INTEGER
    static Dictionary<string, int> IntDictionary = new Dictionary<string, int>();

    public static int GetInt(string key, int defaultValue)
    {
        int result = 0;

        if (IntDictionary.ContainsKey(key))
        {
            result = IntDictionary[key];
        }
        else
        {
            result = PlayerPrefs.GetInt(key, defaultValue);
            IntDictionary.Add(key, result);
        }

        return result;
    }

    public static void TryGetInt(string key, int defaultValue, out int outValue)
    {
        int result = 0;

        if (IntDictionary.ContainsKey(key))
        {
            result = IntDictionary[key];
        }
        else
        {
            result = PlayerPrefs.GetInt(key, defaultValue);
            IntDictionary.Add(key, result);
        }

        outValue = result;
    }

    public static void SetInt(string key, int newValue)
    {
        if (IntDictionary.ContainsKey(key))
        {
            IntDictionary[key] = newValue;
        }
        else
        {
            IntDictionary.Add(key, newValue);
        }

        PlayerPrefs.SetInt(key, newValue);
    }
    #endregion

    #region FLOAT
    static Dictionary<string, float> FloatDictionary = new Dictionary<string, float>();

    public static float GetFloat(string key, float defaultValue)
    {
        float result = 0f;

        if (FloatDictionary.ContainsKey(key))
        {
            result = FloatDictionary[key];
        }
        else
        {
            result = PlayerPrefs.GetFloat(key, defaultValue);
            FloatDictionary.Add(key, result);
        }

        return result;
    }

    public static void TryGetFloat(string key, float defaultValue, out float outValue)
    {
        float result = 0f;

        if (FloatDictionary.ContainsKey(key))
        {
            result = FloatDictionary[key];
        }
        else
        {
            result = PlayerPrefs.GetFloat(key, defaultValue);
            FloatDictionary.Add(key, result);
        }

        outValue = result;
    }

    public static void SetFloat(string key, float newValue)
    {
        if (FloatDictionary.ContainsKey(key))
        {
            FloatDictionary[key] = newValue;
        }
        else
        {
            FloatDictionary.Add(key, newValue);
        }

        PlayerPrefs.SetFloat(key, newValue);
    }

    #endregion

    #region BOOL
    static Dictionary<string, bool> BoolDictionary = new Dictionary<string, bool>();

    public static bool GetBool(string key, bool defaultValue)
    {
        bool result = false;

        if (BoolDictionary.ContainsKey(key))
        {
            result = BoolDictionary[key];
        }
        else
        {
            result = PlayerPrefs.GetInt(key, defaultValue ? 1 : 0) == 1;
            BoolDictionary.Add(key, result);
        }

        return result;
    }

    public static void TryGetBool(string key, bool defaultValue, out bool outValue)
    {
        bool result = false;

        if (BoolDictionary.ContainsKey(key))
        {
            result = BoolDictionary[key];
        }
        else
        {
            result = PlayerPrefs.GetInt(key, defaultValue ? 1 : 0) == 1;
            BoolDictionary.Add(key, result);
        }

        outValue = result;
    }

    public static void SetBool(string key, bool newValue)
    {
        if (BoolDictionary.ContainsKey(key))
        {
            BoolDictionary[key] = newValue;
        }
        else
        {
            BoolDictionary.Add(key, newValue);
        }

        PlayerPrefs.SetInt(key, newValue ? 1 : 0);
    }

    #endregion

    #region STRING
    static Dictionary<string, string> StringDictionary = new Dictionary<string, string>();

    public static string GetString(string key, string defaultValue)
    {
        string result = string.Empty;

        if (StringDictionary.ContainsKey(key))
        {
            result = StringDictionary[key];
        }
        else
        {
            result = PlayerPrefs.GetString(key, defaultValue);
            StringDictionary.Add(key, result);
        }

        return result;
    }

    public static void TryGetString(string key, string defaultValue, out string outValue)
    {
        string result = string.Empty;

        if (StringDictionary.ContainsKey(key))
        {
            result = StringDictionary[key];
        }
        else
        {
            result = PlayerPrefs.GetString(key, defaultValue);
            StringDictionary.Add(key, result);
        }

        outValue = result;
    }

    public static void SetString(string key, string newValue)
    {
        if (StringDictionary.ContainsKey(key))
        {
            StringDictionary[key] = newValue;
        }
        else
        {
            StringDictionary.Add(key, newValue);
        }

        PlayerPrefs.SetString(key, newValue);
    }

    #endregion

    #region INT ARRAY
    static Dictionary<string, int[]> IntArrayDictionary = new Dictionary<string, int[]>();

    public static int[] GetIntArray(string key, int[] defaultValue)
    {
        int[] result = null;

        if (IntArrayDictionary.ContainsKey(key))
        {
            result = IntArrayDictionary[key];
        }
        else
        {
            string savedValue = PlayerPrefs.GetString(key);
            if (!string.IsNullOrEmpty(savedValue))
            {
                result = Array.ConvertAll(savedValue.Split(','), int.Parse);
            }
            else
            {
                result = defaultValue;
            }

            IntArrayDictionary.Add(key, result);
        }

        return result;
    }

    public static void TryGetIntArray(string key, int[] defaultValue, out int[] outValue)
    {
        int[] result = null;

        if (IntArrayDictionary.ContainsKey(key))
        {
            result = IntArrayDictionary[key];
        }
        else
        {
            string savedValue = PlayerPrefs.GetString(key);
            if (!string.IsNullOrEmpty(savedValue))
            {
                result = Array.ConvertAll(savedValue.Split(','), int.Parse);
            }
            else
            {
                result = defaultValue;
            }

            IntArrayDictionary.Add(key, result);
        }

        outValue = result;
    }

    public static void SetIntArray(string key, int[] newValue)
    {
        if (IntArrayDictionary.ContainsKey(key))
        {
            IntArrayDictionary[key] = newValue;
        }
        else
        {
            IntArrayDictionary.Add(key, newValue);
        }

        string savedValue = string.Join(",", newValue);
        PlayerPrefs.SetString(key, savedValue);
    }

    #endregion

    #region FLOAT ARRAY
    static Dictionary<string, float[]> FloatArrayDictionary = new Dictionary<string, float[]>();

    public static float[] GetFloatArray(string key, float[] defaultValue)
    {
        float[] result = null;

        if (FloatArrayDictionary.ContainsKey(key))
        {
            result = FloatArrayDictionary[key];
        }
        else
        {
            string savedValue = PlayerPrefs.GetString(key);
            if (!string.IsNullOrEmpty(savedValue))
            {
                result = Array.ConvertAll(savedValue.Split(','), float.Parse);
            }
            else
            {
                result = defaultValue;
            }

            FloatArrayDictionary.Add(key, result);
        }

        return result;
    }

    public static void TryGetFloatArray(string key, float[] defaultValue, out float[] outValue)
    {
        float[] result = null;

        if (FloatArrayDictionary.ContainsKey(key))
        {
            result = FloatArrayDictionary[key];
        }
        else
        {
            string savedValue = PlayerPrefs.GetString(key);
            if (!string.IsNullOrEmpty(savedValue))
            {
                result = Array.ConvertAll(savedValue.Split(','), float.Parse);
            }
            else
            {
                result = defaultValue;
            }

            FloatArrayDictionary.Add(key, result);
        }

        outValue = result;
    }

    public static void SetFloatArray(string key, float[] newValue)
    {
        if (FloatArrayDictionary.ContainsKey(key))
        {
            FloatArrayDictionary[key] = newValue;
        }
        else
        {
            FloatArrayDictionary.Add(key, newValue);
        }

        string savedValue = string.Join(",", newValue);
        PlayerPrefs.SetString(key, savedValue);
    }

    #endregion

    #region Decor Game
    private const string PREFS_CURRENT_LEVEL = "CurrentLevelUnlock";
    private const string PREFS_TUTORIALS = "Tutorials";

    public static int CurrentLevel
    {
        get => GetInt(PREFS_CURRENT_LEVEL, 0);
        set => SetInt(PREFS_CURRENT_LEVEL, value);
    }

    public static bool IsTutorials
    {
        get => PlayerPrefs.GetInt(PREFS_TUTORIALS, 1) > 0;
        set => PlayerPrefs.SetInt(PREFS_TUTORIALS, value ? 1 : 0);
    }
    private const string PREFS_FIRST_LOGIN = "First_Login";
    private const string PREFS_HINTBOOTSER = "HintBooster";
    private const string PREFS_FEEZEBOOTSER = "FreezeBooster";
    private const string PREFS_COMPASS = "CompassBooster";
    private const string PREFS_CLAIMHINT = "ClaimHint";
    private const string PREFS_CLAIMFREEZE = "ClaimFreeze";
    private const string PREFS_CLAIMCOMPASS = "ClaimCompass";

    public static int HintCount
    {
        get => PlayerPrefs.GetInt(PREFS_HINTBOOTSER, 0);
        set => PlayerPrefs.SetInt(PREFS_HINTBOOTSER, value);
    }
    public static int FeezeCount
    {
        get => PlayerPrefs.GetInt(PREFS_FEEZEBOOTSER, 0);
        set => PlayerPrefs.SetInt(PREFS_FEEZEBOOTSER, value);
    }
    public static int CompassCount
    {
        get => PlayerPrefs.GetInt(PREFS_COMPASS, 0);
        set => PlayerPrefs.SetInt(PREFS_COMPASS, value);
    }
    public static int FirstLogin
    {
        get => PlayerPrefs.GetInt(PREFS_FIRST_LOGIN, 0);
        set => PlayerPrefs.SetInt(PREFS_FIRST_LOGIN, value);
    }
    public static bool IsClaimHint
    {
        get => PlayerPrefs.GetInt(PREFS_CLAIMHINT, 0) > 0;
        set => PlayerPrefs.SetInt(PREFS_CLAIMHINT, value ? 1 : 0);
    }

    public static bool IsClaimFreeze
    {
        get => PlayerPrefs.GetInt(PREFS_CLAIMFREEZE, 0) > 0;
        set => PlayerPrefs.SetInt(PREFS_CLAIMFREEZE, value ? 1 : 0);
    }

    public static bool IsClaimCompass
    {
        get => PlayerPrefs.GetInt(PREFS_CLAIMCOMPASS, 0) > 0;
        set => PlayerPrefs.SetInt(PREFS_CLAIMCOMPASS, value ? 1 : 0);
    }
    #endregion

    #region PlayerData
    private const string PREFS_COIN = "Coin";
    private const string PREFS_HEART = "Heart";
    private const string PREFS_RATE_5STAR = "rate_5star";
    public static int Coin
    {
        get => GetInt(PREFS_COIN, 0);
        set => SetInt(PREFS_COIN, value);
    }

    public static int Heart
    {
        get => GetInt(PREFS_HEART, 5);
        set => SetInt(PREFS_HEART, value);
    }
    public static int Rate5Star
    {
        get => PlayerPrefs.GetInt(PREFS_RATE_5STAR, 0);
        set => PlayerPrefs.SetInt(PREFS_RATE_5STAR, value);
    }
    #endregion
}
