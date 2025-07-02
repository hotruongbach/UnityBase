using System;
using DG.Tweening;
using Template;
using Template.Utilities;
using MyBox;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField]
    CanvasGroup canvas;

    [SerializeField]
    Slider loadingSlider;

    [SerializeField]
    private float delayBeforeHiding = 1;

    [SerializeField]
    private float crossFadeDuration = -0.5f;

    [SerializeField]
    private float defaultLoadingStep = 0.2f;

    private void Start()
    {
        //GameService.AddListener(EventID.LoadingProgressChanged, OnLoadingProgressChanged);
        TemplateEventManager.LoadingProgressChange.AddListener(this, OnLoadingProgressChanged);
    }

    private void OnLoadingProgressChanged(Tuple<float, float> param)
    {
        var percentage = param.Item1;
        var timeLoad = param.Item2 < defaultLoadingStep ? defaultLoadingStep : param.Item2;
        Bug.Report($"LOADING REPORT: ", $"{percentage}%-{timeLoad}");
        if (percentage == 100)
        {
            //just fake loading in 1 second
            if (!this.gameObject.activeSelf)
            {
                Show();
            }
            loadingSlider.DOKill();
            loadingSlider.DOValue(percentage, timeLoad);
            Invoke(nameof(Hide), delayBeforeHiding);
        }
        else
        {
            //some loading processing
            if (!this.gameObject.activeSelf)
            {
                Show();
            }

            loadingSlider.DOKill();
            loadingSlider.DOValue(percentage, timeLoad);
        }
    }

    //private void OnLoadingProgressChanged(Component component, object param)
    //{
    //    float percentage = (float)param;
    //    Bug.Log($"Loading progress {percentage.ToString().SetColor("magenta")}");
    //    if (percentage == 100)
    //    {
    //        //just fake loading in 1 second
    //        if (!this.gameObject.activeSelf)
    //        {
    //            Show();
    //        }
    //        loadingSlider.DOKill();
    //        loadingSlider.DOValue(percentage, 0.15f);
    //        Invoke(nameof(Hide), 1);
    //    }
    //    else
    //    {
    //        //some loading processing
    //        if (!this.gameObject.activeSelf)
    //        {
    //            Show();
    //        }

    //        loadingSlider.DOKill();
    //        loadingSlider.DOValue(percentage, 0.15f);
    //    }
    //}

    private void Hide()
    {
        canvas
            .DOFade(0, 0.25f)
            .OnComplete(() =>
            {
                gameObject.SetActive(false);
                //GameService.PostEvent(EventID.LoadingCompleted);
                TemplateEventManager.LoadingCompletedEvent.Post(this, 0);
            });
    }

    private void Show()
    {
        gameObject.SetActive(true);
        canvas.alpha = 1f;
        loadingSlider.value = 0;
    }
}
