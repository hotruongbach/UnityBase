using Monster;
using Monster.User;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceManager : EventManagerBase
{
    [SerializeField] RaceCustomConfig customConfig;
    public static RaceData Data => Instance.EventData as RaceData;
    public static RaceManager Instance;
    internal static EventHandler OnStatusChanged;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    protected override void OnTimeElapse(object sender, EventArgs e)
    {
        if (UserManager.Level < TimeConfig.UnlockLevel) return;

        switch (EventData.State)
        {
            case EventState.Deactivated:
                if (EventData.Time.EventReactiveTimeRemaning > 0) EventData.Time.EventReactiveTimeRemaning--;
                else
                {
                    OnEventActivated();
                }

                break;
            case EventState.Preparing:
                if (EventData.Time.EventPrepareTimeRemaining > 0) EventData.Time.EventPrepareTimeRemaining--;
                else
                {
                    OnEventEnded();
                }
                break;
            case EventState.Processing:
                if (EventData.Time.EventRuntimeRemaining > 0) EventData.Time.EventRuntimeRemaining--;
                else
                {
                    OnEventComplete();
                }
                break;
            case EventState.Completed:
                if (TimeConfig.HasClaimTime == false) return;
                if (EventData.Time.EventClaimTimeRemaining > 0) EventData.Time.EventClaimTimeRemaining--;
                else
                {
                    OnEventEnded();
                }
                break;
        }
    }
    public override IEnumerator CheckNewDays(bool isNewDay)
    {
        yield return null;
        //do nothing
    }
    protected override void AddEventListener()
    {
        //GameService.AddListener(EventID.Win, OnPlayerWin);
        //GameService.AddListener(EventID.Lose, OnPlayerLose);

        MonsterEventManager.WinEvent.AddListener(this, OnPlayerWin);
        MonsterEventManager.LoseEvent.AddListener(this, OnPlayerLose);
    }

    private void OnPlayerLose(int param)
    {
        if (UserManager.Level < TimeConfig.UnlockLevel)
        {
            return;
        }
        if (EventData.State == EventState.Processing)
        {
            UpdateBotData();
            SaveData();
        }
    }
    private void OnPlayerWin(int param)
    {
        if (UserManager.Level < TimeConfig.UnlockLevel)
        {
            return;
        }

        if (EventData.State == EventState.Processing)
        {
            Data.player.CurrentStage++;
            if (Data.player.CurrentStage == Data.EndStage)
            {
                Data.ListCompletedCompetitor.Add(Data.player);
                OnEventComplete();
                return;
            }
            UpdateBotData();
            SaveData();
        }
    }
    private void UpdateBotData()
    {
        Data.bot1.BotRandomWin();
        Data.bot2.BotRandomWin();
        Data.bot3.BotRandomWin();
        Data.bot4.BotRandomWin();

        if (Data.bot1.IsCompleted() && !Data.ListCompletedCompetitor.Contains(Data.bot1)) Data.ListCompletedCompetitor.Add(Data.bot1);
        if (Data.bot2.IsCompleted() && !Data.ListCompletedCompetitor.Contains(Data.bot2)) Data.ListCompletedCompetitor.Add(Data.bot2);
        if (Data.bot3.IsCompleted() && !Data.ListCompletedCompetitor.Contains(Data.bot3)) Data.ListCompletedCompetitor.Add(Data.bot3);
        if (Data.bot4.IsCompleted() && !Data.ListCompletedCompetitor.Contains(Data.bot4)) Data.ListCompletedCompetitor.Add(Data.bot4);

        if (Data.ListCompletedCompetitor.Count >= 3)
        {
            OnEventComplete();
        }
    }
    protected override void OnEventJoined()
    {
        Data.Clear();

        Data.StartStage = UserManager.Level;
        Data.EndStage = UserManager.Level + 12;

        Data.player = new RaceEventCompetitor(UserManager.UserName, Data.StartStage, Data.EndStage, 0,true);

        var names = NameCollection.GetRandomNames(4);

        int start = Data.StartStage;
        int end = Data.EndStage;
        int streak = Data.WinStreak;

        Data.bot1 = new RaceEventCompetitor(names[0], start, end, UnityEngine.Random.Range(50 + streak * 10, 70 + streak * 10),false);
        Data.bot2 = new RaceEventCompetitor(names[1], start, end, UnityEngine.Random.Range(30 + streak * 10, 50 + streak * 10), false);
        Data.bot3 = new RaceEventCompetitor(names[2], start, end, UnityEngine.Random.Range(30 + streak * 10, 50 + streak * 10), false);
        Data.bot4 = new RaceEventCompetitor(names[3], start, end, 20 + streak * 10, false);
        base.OnEventJoined();
    }
    protected override void ChangeState(EventState state)
    {
        OnStatusChanged?.Invoke(null,EventArgs.Empty);
        base.ChangeState(state);

    }
    #region STATIC FUNC
    public static EventState EventState => Instance.EventData.State;
    public static EventTimeData Time => Instance.EventData.Time;
    public static RaceCustomConfig CustomConfig => Instance.customConfig;
    public static void ForceSave()
    {
        Instance?.SaveData();
    }
    public static void ForceWin()
    {
        Instance?.OnPlayerWin(0);
    }
    public static void ForceLose()
    {
        Instance?.OnPlayerLose(0);
    }
    public static IEnumerator Init()
    {
        yield return Instance.LoadData();
    }
    public static void EventActivate()
    {
        Instance?.OnEventActivated();
    }
    public static void EventJoin()
    {
        Instance?.OnEventJoined();
    }
    public static void EventComplete()
    {
        Instance?.OnEventComplete();
    }
    public static void ClaimReward()
    {
        //claim some reward
        RaceData data = Data;

        int userPos = data.GetPlayerFinishPosition();
        var reward = new List<ResourceData>();
        switch (userPos)
        {
            case 0: reward = CustomConfig.FirstReward; break;
            case 1: reward = CustomConfig.SecondReward; break;
            case 2: reward = CustomConfig.ThirdReward; break;
        }

        //ResourceManager.ClaimResource(reward);
        UserManager.ClaimResource(reward);

        if (userPos < 3)
        {
            data.WinStreak += 1;
        }
        else
        {
            data.WinStreak -= 2;
            data.WinStreak = Mathf.Max(1, data.WinStreak);
        }

        Instance.OnEventEnded();
    }
    public static void EventDeactive()
    {
        Instance.OnEventEnded();
    }


    #endregion
}
