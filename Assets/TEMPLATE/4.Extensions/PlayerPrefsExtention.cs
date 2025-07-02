using System.Collections;
using UnityEngine;

public static class PlayerPrefsExtention
{
    public static void SetBoolean(string Prefs, bool _Value)
    {
        PlayerPrefs.SetInt(Prefs, _Value ? 1 : 0);
    }
    public static bool GetBoolean(string Prefs, bool _defaultValue = false)
    {
        if (!PlayerPrefs.HasKey(Prefs))
        {
            SetBoolean(Prefs, _defaultValue);
        }
        if (PlayerPrefs.GetInt(Prefs) == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public static void SetBoolArray(string Prefs, bool[] _Value)
    {
        string Value = "";
        for (int y = 0; y < _Value.Length; y++)
        {
            Value += _Value[y].ToString() + "|";
        }
        PlayerPrefs.SetString(Prefs, Value);
    }
    public static bool[] GetBoolArray(string Prefs)
    {
        string[] tmp = PlayerPrefs.GetString(Prefs).Split("|"[0]);
        if (tmp.Length != 0)
        {
            bool[] myBool = new bool[tmp.Length - 1];
            for (int i = 0; i < tmp.Length - 1; i++)
            {
                myBool[i] = bool.Parse(tmp[i]);
            }
            return myBool;
        }
        return new bool[0];
    }

    public static void SetIntArray(string Prefs, int[] _Value)
    {
        string Value = "";
        for (int y = 0; y < _Value.Length; y++) { Value += _Value[y].ToString() + "|"; }
        PlayerPrefs.SetString(Prefs, Value);
    }
    public static void SetIntArray(string Prefs, ArrayList _Value)
    {
        string Value = "";
        for (int y = 0; y < _Value.Count; y++) { Value += _Value[y].ToString() + "|"; }
        PlayerPrefs.SetString(Prefs, Value);
    }

    public static int[] GetIntArray(string Prefs)
    {
        string[] tmp = PlayerPrefs.GetString(Prefs).Split("|"[0]);
        int[] myInt = new int[tmp.Length - 1];
        for (int i = 0; i < tmp.Length - 1; i++)
        {
            myInt[i] = int.Parse(tmp[i]);
        }
        return myInt;
    }
}
