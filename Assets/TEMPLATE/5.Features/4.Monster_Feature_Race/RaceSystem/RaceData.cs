using Monster.Stamina;
using Monster.User;
using Monster.Utilities;
using MyBox;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CreateAssetMenu(fileName = "RaceData", menuName = "GAME/FEATURE/RaceData")]
public class RaceData : EventDataBase
{
    [SerializeField] EventTimeConfig TimeConfig;
    [ReadOnly]public int StartStage;
    [ReadOnly]public int EndStage;
    [Separator("COMPETITORs")]
    public RaceEventCompetitor player;
    public RaceEventCompetitor bot1;
    public RaceEventCompetitor bot2;
    public RaceEventCompetitor bot3;
    public RaceEventCompetitor bot4;
    [Separator("FINISH INFORMATION")]
    public int WinStreak;
    [ReadOnly] public List<RaceEventCompetitor> ListCompletedCompetitor;

    [ButtonMethod]
    public override void Clear()
    {
        State = EventState.Deactivated;
        Time = new EventTimeData();
        player.Clear();
        bot1.Clear();
        bot2.Clear();
        bot3.Clear();
        bot4.Clear();
        WinStreak = 0;
        ListCompletedCompetitor.Clear();
        StartStage = 0;
        EndStage = 0;
    }

    public override void Default()
    {
        State = EventState.Deactivated;
        Time = new EventTimeData();

        player.Clear();
        bot1.Clear();
        bot2.Clear();
        bot3.Clear();
        bot4.Clear();
        WinStreak = 0;
        ListCompletedCompetitor.Clear();

        StartStage = UserManager.Level;
        EndStage = UserManager.Level + 12;
    }

    private void OnValidate()
    {
        if (Application.isPlaying)
        {
            Time.EventActivatedDateTime = DateTime.Now.ToDetailedString();
            Time.EventCompleteDateTime = DateTime.Now.ToDetailedString();
            Time.EventEndedDateTime = DateTime.Now.ToDetailedString();
            Time.EventJoinedDateTime = DateTime.Now.ToDetailedString();

            RaceManager.ForceSave();
        }
    }

    public override void FromJson(string json)
    {
        var dataSO = JsonUtility.FromJson<RaceDataSave>(json);
        State = dataSO.state;
        Time = dataSO.Time;

        StartStage = dataSO.StartStage;
        EndStage = dataSO.EndStage;

        player = dataSO.player;
        bot1 = dataSO.bot1;
        bot2 = dataSO.bot2;
        bot3 = dataSO.bot3;
        bot4 = dataSO.bot4;
        WinStreak = dataSO.WinTreak;
        ListCompletedCompetitor = dataSO.ListCompletedCompetitor;

        switch (State)
        {
            case EventState.Deactivated:
                InitTimeFromDeactivated();
                break;
            case EventState.Preparing:
                InitFromActivated();
                break;
            case EventState.Processing:
                InitFromProcessing();
                break;
            case EventState.Completed:
                InitFromCompleted();
                break;
        }
    }

    public override string ToJson()
    {
        RaceDataSave saveData = new RaceDataSave();
        saveData.state = State;

        saveData.StartStage = StartStage;
        saveData.EndStage = EndStage;
        saveData.Time = Time;

        saveData.player = player;
        saveData.bot1 = bot1;
        saveData.bot2 = bot2;
        saveData.bot3 = bot3;
        saveData.bot4 = bot4;

        saveData.WinTreak = WinStreak;
        saveData.ListCompletedCompetitor = ListCompletedCompetitor;

        string json = JsonUtility.ToJson(saveData);
        return json;
    }
    #region INIT TIME FUNCTION
    private void InitTimeFromDeactivated()
    {
        DateTime lastSavedDateTime = DateTime.Now;
        if (Time.EventEndedDateTime.TryGetDateTime(out lastSavedDateTime))
        {
            int timeFromSave = (int)(DateTime.Now - lastSavedDateTime).TotalSeconds;

            if (timeFromSave > TimeConfig.EventResetTime.ToSeconds())
            {
                RaceManager.EventActivate();
            }
            else
            {
                Time.EventReactiveTimeRemaning = TimeConfig.EventResetTime.ToSeconds() - timeFromSave;
            }
        }
    }
    private void InitFromActivated()
    {
        DateTime lastSavedDateTime = DateTime.Now;
        if (Time.EventActivatedDateTime.TryGetDateTime(out lastSavedDateTime))
        {
            int timeFromSave = (int)(DateTime.Now - lastSavedDateTime).TotalSeconds;

            if (timeFromSave > TimeConfig.EventPrepareTime.ToSeconds())
            {
                RaceManager.EventDeactive();
            }
            else
            {
                Time.EventPrepareTimeRemaining = TimeConfig.EventPrepareTime.ToSeconds() - timeFromSave;
            }
        } 
    }
    private void InitFromProcessing()
    {
        DateTime lastSavedDateTime = DateTime.Now;
        if (Time.EventJoinedDateTime.TryGetDateTime(out lastSavedDateTime))
        {
            int timeFromSave = (int)(DateTime.Now - lastSavedDateTime).TotalSeconds;

            if (timeFromSave > TimeConfig.EventRunningTime.ToSeconds())
            {
                RaceManager.EventComplete();
            }
            else
            {
                Time.EventRuntimeRemaining = TimeConfig.EventRunningTime.ToSeconds() - timeFromSave;
            }
        }
    }
    private void InitFromCompleted()
    {
        //infinite time to claim
        if (TimeConfig.HasClaimTime == false) return;

        //LIMITED TIME TO CLAIMDateTime lastSavedDateTime = DateTime.Now;
        DateTime lastSavedDateTime = DateTime.Now;
        if (Time.EventCompleteDateTime.TryGetDateTime(out lastSavedDateTime))
        {
            int timeFromSave = (int)(DateTime.Now - lastSavedDateTime).TotalSeconds;

            if (timeFromSave > TimeConfig.EventClaimTime.ToSeconds())
            {
                RaceManager.EventDeactive();
            }
            else
            {
                Time.EventClaimTimeRemaining = TimeConfig.EventClaimTime.ToSeconds() - timeFromSave;
            }
        }
    }
    #endregion

    /// <summary>
    /// Find position of player in list finisher
    /// </summary>
    /// <returns></returns>
    public int GetPlayerFinishPosition()
    {
        var playerInList = ListCompletedCompetitor.Find(x => x.IsPlayer);

        if (playerInList == null)
        {
            Bug.Log("RACE LOG: PLAYER NOT COMPLETE RACE", "red");
            return 4;
        }
        else
        {
            int pos = ListCompletedCompetitor.IndexOf(playerInList);
            Bug.Log($"RACE LOG: PLAYER POS = {pos}", "red");
            return pos;
        }
    }


}

/// <summary>
/// Use to choose which data to save and load
/// </summary>
public class RaceDataSave
{
    public EventState state;
    public EventTimeData Time;

    public int StartStage;
    public int EndStage;

    public RaceEventCompetitor player;
    public RaceEventCompetitor bot1;
    public RaceEventCompetitor bot2;
    public RaceEventCompetitor bot3;
    public RaceEventCompetitor bot4;

    public int WinTreak;
    public List<RaceEventCompetitor> ListCompletedCompetitor;
}
