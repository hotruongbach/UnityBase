using Template.Utilities;
using UnityEngine;

public static class Bug
{
    public static void Log(string message, string color = "")
    {
#if UNITY_EDITOR
        if (color == string.Empty || color == "")
        {
            Debug.Log($"{message}");
        }
        else
        {
            Debug.Log($"<color={color}>{message}</color>");
        }
#endif
    }

    /// <summary>
    /// Log with default color. Reporter string color = yellow, message color = white
    /// </summary>
    /// <param name="reporter"></param>
    /// <param name="message"></param>
    public static void Report(string reporter, string message)
    {
#if UNITY_EDITOR
        Debug.Log($"{reporter.SetColor("yellow")}: {message}");
#endif
    }
}
