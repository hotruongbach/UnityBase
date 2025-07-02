using Monster.Utilities;
using MyBox;
using System;
using UnityEngine;

public class NetworkChecker : Singleton<NetworkChecker>
{
    public static bool IsAsia;
    void Awake()
    {
        IsAsia = CheckNetworkInterfaces();
    }

    private bool CheckNetworkInterfaces()
    {
        DateTimeOffset dateTimeOffset = DateTimeOffset.Now;

        TimeSpan offset = dateTimeOffset.Offset;

        int offsetHours = (int)Math.Round(offset.TotalHours);

        if (offsetHours >= 4 && offsetHours <= 10)
        {
            Bug.Log("INTERNET REPORT: ".SetColor(Color.yellow) + "Thiết bị ở khu vực "+"Châu Á".SetColor("yellow") + $" GMT {offsetHours}");
            return true;
        }
        else
        {
            Bug.Log("INTERNET REPORT: ".SetColor(Color.yellow) + "Thiết bị ở khu vực " + "Mỹ (US, UK)".SetColor("yellow") + $" GMT {offsetHours}");
            return false;
        }

    }
}
