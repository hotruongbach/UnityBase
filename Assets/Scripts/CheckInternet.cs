using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CheckInternet : MonoBehaviour
{
    [SerializeField] private GameObject noInternetPanel;
    [SerializeField] private float timeInterval;

    public void Start()
    {
        if(PlayerPrefsManager.CurrentLevel > 0)
        {
            StartCoroutine(IECheckInternet());
        } 
    }

    private IEnumerator IECheckInternet()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(timeInterval);
        bool isInternetConnected = false;
        while (true)
        {
            yield return waitForSeconds;
            isInternetConnected = Application.internetReachability != NetworkReachability.NotReachable;
            if (noInternetPanel.activeInHierarchy == isInternetConnected)
            {
                noInternetPanel.SetActive(!isInternetConnected);
            }
        }
    }
}
