using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class SplashSceneManager : MonoBehaviour
{
    //public SDKInitializer m_SDKInitializer;
    public CanvasGroup mainPanel;
    public GameObject inGameDebug;
    AssetBundle bundle;
    GameObject uiContainer;
    public GameObject progressContainer, openAdsContainer;
    public Image progressImage;
    public TextMeshProUGUI progressPercent;
    DateTime startLoadTime;

    public void Awake()
    {
#if !ENV_PROD
        inGameDebug.SetActive(true);
#endif
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(mainPanel.gameObject);
    }

//    void Start()
//    {
//        Init();
//    }

//    private void Init()
//    {
//        Screen.sleepTimeout = SleepTimeout.NeverSleep;
//        startLoadTime = DateTime.Now;
//        if (uiContainer == null)
//        {
//            uiContainer = new GameObject("ObjManagerContainer");
//            Instantiate(uiContainer, transform);
//            DontDestroyOnLoad(uiContainer);
//            //InitUIGame();
//        }
//        LoadCurrentLevel();
//        StartCoroutine(LoadScene("GameplayScene"));
//    }

//    IEnumerator LoadScene(string environmentScene)
//    {
//        AsyncOperation loadEnvironmentAsync = SceneManager.LoadSceneAsync(environmentScene, LoadSceneMode.Single);
//        // Check if the load has finished
//        loadEnvironmentAsync.allowSceneActivation = false;
//        do
//        {
//            yield return new WaitForSeconds(1);
//            progressPercent.text = (loadEnvironmentAsync.progress * 100) + "%";
//            progressImage.fillAmount = loadEnvironmentAsync.progress;
//        }
//        while (loadEnvironmentAsync.progress < .9f);
//        yield return new WaitUntil(()=> loadEnvironmentAsync.progress > 0.89f);

//        DateTime current = DateTime.Now;
//        if ((current - startLoadTime).TotalSeconds < 4)
//        {
//            yield return new WaitForSeconds(5 - (int)(current - startLoadTime).Seconds);
//            progressPercent.text = (100) + "%";
//            progressImage.fillAmount = 1;
//        }

//#if UNITY_EDITOR
//        if (Application.internetReachability != NetworkReachability.NotReachable)
//        {
//            yield return new WaitUntil(() => SDKInitializer.isAllSdkInitialized);
//        }
//#endif
//        yield return CheckOpenAds();


//        yield return new WaitForSeconds(0.5f);
//        Destroy(mainPanel.gameObject);
//        loadEnvironmentAsync.allowSceneActivation = true;
//        yield return new WaitForSeconds(2f);
//        Destroy(gameObject);
//    }

//    IEnumerator CheckOpenAds()
//    {
//        if (PlayerPrefs.GetInt("open_app_count", 0) >= 1 && Application.internetReachability != NetworkReachability.NotReachable)
//        {
//            yield return new WaitUntil(() => AdsController.Instance.IsOpenAdHasLoaded() || Time.realtimeSinceStartup > 10f);
//        }
//        Instantiate(openAdsContainer);
//    }

//    public void LoadCurrentLevel()
//    {
//        PlayerPrefsManager.currentLevelData = LevelDataManager.Instance.GetLevelData(PlayerPrefsManager.CurrentIndex);
        
//    }  
}