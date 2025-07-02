using System;
using System.Collections;
using Monster;
using Monster.UI;
using Monster.User;
using Monster.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInitializer : MonoBehaviour
{
    public Clock Clock;

    //public ResourceManager resourceManager;
    public UserManagerBase userManager;
    public FeatureManager featureManager;
    public GameObject openAdsContainer;

    private AsyncOperation loadSceneOperation;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Application.targetFrameRate = 60;
    }

    public virtual void Start()
    {
        //Input.multiTouchEnabled = false;

        this.StartChain()
            .Play(InitClock())
            .Play(PreloadGameScene())
            .Parallel(InitSDK())
            .Play(LoadPlayerData())
            .Play(UpdateLoadingProgress(50, 5))
            //.Play(LoadResourceData())
            .Play(UpdateLoadingProgress(70, 1.5f))
            .Play(FetchRemoteData())
            .Play(UpdateLoadingProgress(90, 1))
            .Play(FetchFeatureData())
            .Play(UpdateLoadingProgress(100))
            .Play(EnterGame());
    }

    public virtual IEnumerator InitClock()
    {
        yield return Clock.Activate();
        Bug.Report("MANAGER REPORT", "Clock initialized!");
    }

    public virtual IEnumerator InitSDK()
    {
#if !UNITY_EDITOR && ADS_ENABLED
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            yield return new WaitUntil(() => SDKInitializer.isAllSdkInitialized);
        }
        UpdateLoadingProgress(25);
        yield return new WaitForSeconds(2f);
#endif
        yield return null;
    }

    public virtual IEnumerator LoadPlayerData()
    {
        //yield return null;
        yield return userManager.LoadData();
        Bug.Report("MANAGER REPORT", "PLAYER LOADED!");
    }

    //public virtual IEnumerator LoadResourceData()
    //{
    //    yield return resourceManager.LoadResourceData();
    //    Bug.Report("MANAGER REPORT", "USER RESOURCE LOADED!");
    //}
    public virtual IEnumerator FetchRemoteData()
    {
        yield return null;
        Bug.Report("MANAGER REPORT", "Remote data not found!");
    }

    public virtual IEnumerator FetchFeatureData()
    {
        yield return featureManager.LoadFeaturesData();
        yield return featureManager.CheckDayAll(IsNewDay());
        yield return featureManager.StartAllFeature();

        Bug.Report("MANAGER REPORT", "All FEATURE LOADED!");
    }

    private IEnumerator PreloadGameScene()
    {
        yield return null;
        if (SceneManager.GetActiveScene().name != SceneName.GAMEPLAY)
        {
            loadSceneOperation = SceneManager.LoadSceneAsync(SceneName.HOME);
            loadSceneOperation.allowSceneActivation = false;
        }
        else
        {
            GameService.ShowView<HomeView>();
        }
    }

    public virtual IEnumerator EnterGame()
    {
        yield return null;
        if (loadSceneOperation != null)
        {
            Bug.Report("MANAGER REPORT", "ENTER GAME!");
            loadSceneOperation.allowSceneActivation = true;
        }
        else
        {
            Bug.Report("MANAGER REPORT", "Load scene fail!");
        }
        GameService.ShowView<HomeView>();
    }

    private IEnumerator UpdateLoadingProgress(float percentage, float timeLoad = 0.2f)
    {
        yield return null;
        //GameService.PostEvent(EventID.LoadingProgressChanged, null, percentage);
        MonsterEventManager.LoadingProgressChange.Post(this, new(percentage, timeLoad));
    }

    private bool IsNewDay()
    {
        DateTime LastLogin = DateTime.Now;
        if (PlayerPrefs.GetString(PlayerPrefsKey.LOGIN_DAY).TryGetDate(out LastLogin) == false)
        {
            PlayerPrefs.SetString(PlayerPrefsKey.LOGIN_DAY, DateTime.Now.Date.ToDateString());
            return true;
        }
        else
        {
            PlayerPrefs.SetString(PlayerPrefsKey.LOGIN_DAY, DateTime.Now.Date.ToDateString());
            var result = LastLogin != DateTime.Now.Date;
            return result;
        }
    }
}
