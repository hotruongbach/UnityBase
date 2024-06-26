using System;
using UnityEngine;

public class NetworkChecker : MonoBehaviour
{
    public bool IsAsia => CheckNetworkInterfaces();
    void Start()
    {
        CheckNetworkInterfaces();
    }

    private bool CheckNetworkInterfaces()
    {
        DateTimeOffset dateTimeOffset = DateTimeOffset.Now;

        TimeSpan offset = dateTimeOffset.Offset;

        int offsetHours = (int)Math.Round(offset.TotalHours);

        if (offsetHours >= 4 && offsetHours <= 10)
        {
            Debug.Log($"<color=blue>Thiết bị ở khu vực Châu Á {offsetHours}</color>");
            return true;
        }
        else
        {
            Debug.Log($"<color=blue>Thiết bị ở khu vực Mỹ (US, UK) {offsetHours}</color>");
            return false;
        }

    }
}
