using System.Collections;
using System.Collections.Generic;
using MyBox;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TemplateSceneManager : Singleton<TemplateSceneManager>
{
    #region NONE STATIC
    private Coroutine loadCoroutine;
    private WaitForSeconds wait = new WaitForSeconds(0);
    private float currentDelay = 0;
    private WaitForSeconds step = new WaitForSeconds(0.1f);

    private void _LoadScene(string name, float extraDelayTime = 1)
    {
        //if (loadCoroutine != null) StopCoroutine(loadCoroutine);

        loadCoroutine = StartCoroutine(_LoadSceneAsync(name, extraDelayTime));
    }

    private IEnumerator _LoadSceneAsync(string sceneName, float delay)
    {
        if (delay != currentDelay)
        {
            wait = new WaitForSeconds(delay);
            currentDelay = delay;
        }
        //Bật cái loading screen lên
        TemplateEventManager.LoadingProgressChange.Post(this, new(0f, 0f));
        // Start the asynchronous operation to load the scene
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);

        // Don't allow the scene to activate until the loading is complete
        asyncOperation.allowSceneActivation = false;
        float progress = 0;
        // While the scene is still loading, update the progress bar
        while (!asyncOperation.isDone)
        {
            progress = asyncOperation.progress;  // Progress stops at 0.9 before scene activation
            TemplateEventManager.LoadingProgressChange.Post(this, new(progress * 100f, 0f));
            yield return step;
            if (progress >= 0.9f)
            {
                asyncOperation.allowSceneActivation = true;
                TemplateEventManager.LoadingProgressChange.Post(this, new(99f, 0f));
                break;
            }
        }
        yield return wait;
        TemplateEventManager.LoadingProgressChange.Post(this, new(100f, 0f));
    }
    #endregion

    #region STATIC
    /// <summary>
    /// Load scene theo const tên từ class SceneName, tự động bật loadding screen
    /// </summary>
    /// <param name="name">Tên scene được khai báo const ở class SceneName</param>
    /// <param name="extraDelayTime">Thời gian chờ sau khi load xong scene, mặc định chờ 1s</param>

    static string nameSceneActive;
    public static void LoadScene(string name, float extraDelayTime = 1)
    {
        nameSceneActive = name;
        Instance._LoadScene(name, extraDelayTime);
    }
    public static void LoadActiveSceneNow(float extraDelayTime = 1)
    {
        Instance._LoadScene(nameSceneActive, extraDelayTime);
    }
    #endregion


    [ButtonMethod]
    void LoadSplash()
    {
        LoadScene(SceneName.SPLASH, 5);
    }

    [ButtonMethod]
    void LoadHome()
    {
        LoadScene(SceneName.HOME);
    }
}

public static class SceneName
{
    public const string SPLASH = "LoadingScene";
    public const string HOME = "HomeScene";
    public const string GAMEPLAY = "GameplayScene";
}
