using UnityEngine;

public static class PrefHelper
{
    public static bool GetBool(string str, bool defaultValue)
    {
        return PlayerPrefs.GetInt(str, 0) == 0 ? false : true; ;
    }
    public static void SetBool(string str, bool value)
    {
        PlayerPrefs.SetInt(str, value ? 1 : 0);
    }
}
