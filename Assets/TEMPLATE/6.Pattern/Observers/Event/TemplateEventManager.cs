using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemplateEventManager : MonoBehaviour
{
    #region SINGLETON
    protected static TemplateEventManager Instance;
    protected virtual void Awake()
    {
        Instance = this;
    }
    #endregion

    #region EVENTS
    [SerializeField] private TemplateEvent<Tuple<float, float>> loadingProgressChange = new TemplateEvent<Tuple<float, float>>();
    public static TemplateEvent<Tuple<float, float>> LoadingProgressChange => Instance.loadingProgressChange;

    [SerializeField] private TemplateEvent<int> winEvent = new TemplateEvent<int>();
    public static TemplateEvent<int> WinEvent => Instance.winEvent;

    [SerializeField] private TemplateEvent<int> leveUpEvent = new TemplateEvent<int>();
    public static TemplateEvent<int> LeveUpEvent => Instance.leveUpEvent;

    [SerializeField] private TemplateEvent<int> loseEvent = new TemplateEvent<int>();
    public static TemplateEvent<int> LoseEvent => Instance.loseEvent;

    [SerializeField] private TemplateEvent<int> loadingCompletedEvent = new TemplateEvent<int>();
    public static TemplateEvent<int> LoadingCompletedEvent => Instance.loadingCompletedEvent;

    [SerializeField] private TemplateEvent<TempData> tempEvent = new TemplateEvent<TempData>();
    public static TemplateEvent<TempData> TempEvent => Instance.tempEvent;
    [SerializeField] private TemplateEvent<int> onChangeCoin = new TemplateEvent<int>();
    public static TemplateEvent<int> OnChangeCoin => Instance.onChangeCoin;
    [SerializeField] private TemplateEvent<int> onChangeHeart = new TemplateEvent<int>();
    public static TemplateEvent<int> OnChangeHeart => Instance.onChangeHeart;
    #endregion
}

[Serializable]
public class TempData
{
    public int tempInt;
    public float tempFloat;
    public string tempString;

    public TempData(int tempInt, float tempFloat, string tempString)
    {
        this.tempInt = tempInt;
        this.tempFloat = tempFloat;
        this.tempString = tempString;
    }
}
