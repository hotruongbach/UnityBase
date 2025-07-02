using System;
using UnityEngine;

public class MonsterEventManager : MonoBehaviour
{
    #region SINGLETON
    protected static MonsterEventManager instance;
    protected virtual void Awake()
    {
        instance = this;
    }
    #endregion

    #region EVENTS
    [SerializeField] private MonsterEvent<Tuple<float, float>> loadingProgressChange = new MonsterEvent<Tuple<float, float>>();
    public static MonsterEvent<Tuple<float, float>> LoadingProgressChange => instance.loadingProgressChange;

    [SerializeField] private MonsterEvent<int> winEvent = new MonsterEvent<int>();
    public static MonsterEvent<int> WinEvent => instance.winEvent;

    [SerializeField] private MonsterEvent<int> leveUpEvent = new MonsterEvent<int>();
    public static MonsterEvent<int> LeveUpEvent => instance.leveUpEvent;

    [SerializeField] private MonsterEvent<int> loseEvent = new MonsterEvent<int>();
    public static MonsterEvent<int> LoseEvent => instance.loseEvent;

    [SerializeField] private MonsterEvent<int> loadingCompletedEvent = new MonsterEvent<int>();
    public static MonsterEvent<int> LoadingCompletedEvent => instance.loadingCompletedEvent;

    [SerializeField] private MonsterEvent<TempData> tempEvent = new MonsterEvent<TempData>();
    public static MonsterEvent<TempData> TempEvent => instance.tempEvent;
    [SerializeField] private MonsterEvent<int> onChangeCoin = new MonsterEvent<int>();
    public static MonsterEvent<int> OnChangeCoin => instance.onChangeCoin;
    [SerializeField] private MonsterEvent<int> onChangeHeart = new MonsterEvent<int>();
    public static MonsterEvent<int> OnChangeHeart => instance.onChangeHeart;
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