using MyBox;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterSceneManager : Singleton<MonsterSceneManager>
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
        MonsterEventManager.LoadingProgressChange.Post(this, new (0f, 0f));
        // Start the asynchronous operation to load the scene
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);

        // Don't allow the scene to activate until the loading is complete
        asyncOperation.allowSceneActivation = false;
        float progress = 0;
        // While the scene is still loading, update the progress bar
        while (!asyncOperation.isDone)
        {
            progress = asyncOperation.progress;  // Progress stops at 0.9 before scene activation
            MonsterEventManager.LoadingProgressChange.Post(this, new(progress * 100f, 0f));
            yield return step;
            if (progress >= 0.9f)
            {
                asyncOperation.allowSceneActivation = true;
                MonsterEventManager.LoadingProgressChange.Post(this, new(99f, 0f));
                break;
            }
        }
        yield return wait;
        MonsterEventManager.LoadingProgressChange.Post(this, new(100f, 0f));
    }
    #endregion

    #region STATIC
    /// <summary>
    /// Load scene theo const tên từ class SceneName, tự động bật loadding screen
    /// </summary>
    /// <param name="name">Tên scene được khai báo const ở class SceneName</param>
    /// <param name="extraDelayTime">Thời gian chờ sau khi load xong scene, mặc định chờ 1s</param>
    public static void LoadScene(string name, float extraDelayTime = 1)
    {
        Instance._LoadScene(name, extraDelayTime);
    }
    #endregion


    [ButtonMethod]
    void LoadSplash()
    {
        MonsterSceneManager.LoadScene(SceneName.SPLASH, 5);
    }

    [ButtonMethod]
    void LoadHome()
    {
        MonsterSceneManager.LoadScene(SceneName.HOME);
    }
}

public static class SceneName
{
    public const string SPLASH = "LoadingScene";
    public const string HOME = "HomeScene";
    public const string GAMEPLAY = "Test Scene";
}
