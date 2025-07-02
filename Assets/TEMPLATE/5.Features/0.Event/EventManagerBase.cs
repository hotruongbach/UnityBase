using Template;
using Template.User;
using Template.Utilities;
using MyBox;
using System;
using System.Collections;
using UnityEngine;

public abstract class EventManagerBase : FeatureBase
{
    #region PROPERTIES
    [SerializeField] protected string EventName;
    [DisplayInspector][SerializeField] protected EventTimeConfig TimeConfig;
    [DisplayInspector][SerializeField] protected EventDataBase EventData;

    #endregion

    #region MONOBEHAVIOR

    public override IEnumerator LoadData()
    {
        yield return null;
        EventData.Clear();
        string json = PlayerPrefs.GetString(EventName, string.Empty);

        if (json == string.Empty)
        {
            EventData.Default();
            SaveData();
        }
        else
        {
            EventData.FromJson(json);
        }
    }
    public override IEnumerator StartFeature()
    {
        yield return null;
        if(UserManager.Level >= TimeConfig.UnlockLevel)
        {
            Clock.Tick += OnTimeElapse;
            AddEventListener();
        }
        else
        {
            //GameService.AddListener(EventID.LevelUp, OnLevelUp);

            TemplateEventManager.LeveUpEvent.AddListener(this, OnLevelUpEvent);
        }

    }

    private void OnLevelUpEvent(int obj)
    {
        if (UserManager.Level >= TimeConfig.UnlockLevel)
        {
            Clock.Tick += OnTimeElapse;
            AddEventListener();

            this.StartChain().
                Play(RemoveLevelUpListener());
        }
    }

    protected virtual void OnLevelUp(Component component, object arg2)
    {
        if(UserManager.Level >= TimeConfig.UnlockLevel)
        {
            Clock.Tick += OnTimeElapse;
            AddEventListener();

            this.StartChain().
                Play(RemoveLevelUpListener());
        }
    }

    private IEnumerator RemoveLevelUpListener()
    {
        yield return null;
        //GameService.RemoveListener(EventID.LevelUp, OnLevelUp);
        TemplateEventManager.LeveUpEvent.RemoveListener(this, OnLevelUpEvent);
    }

    /// <summary>
    /// Subcribe to game events that affect competitors(bot)
    /// </summary>
    protected abstract void AddEventListener();

    /// <summary>
    /// Call every second if subcribed to Clock
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected abstract void OnTimeElapse(object sender, EventArgs e);

    protected override void SaveData()
    {
        string json = EventData.ToJson();
        PlayerPrefs.SetString(EventName, json);
    }

    /// <summary>
    /// Save the time this event is activated. Change event state to Prepareing.
    /// Start countdown for user preparing to join
    /// If time run out but user not join, event ended directly
    /// </summary>
    protected virtual void OnEventActivated()
    {
        ChangeState(EventState.Preparing);
        EventData.Time.EventActivatedDateTime = DateTime.Now.ToDetailedString();
        EventData.Time.EventPrepareTimeRemaining = TimeConfig.EventPrepareTime.ToSeconds();
        SaveData();
    }

    /// <summary>
    /// Change state to Processing. Active all competitors (bots)
    /// If time run out, event change to reward stage
    /// </summary>
    protected virtual void OnEventJoined()
    {
        ChangeState(EventState.Processing);
        EventData.Time.EventJoinedDateTime = DateTime.Now.ToDetailedString();
        EventData.Time.EventRuntimeRemaining = TimeConfig.EventRunningTime.ToSeconds();
        SaveData();
    }

    /// <summary>
    /// Change state to Complete. Deactive all competitors(bots).
    /// Give user some time to grab reward.
    /// If time runout, reward are dismissed, event ended
    /// </summary>
    protected virtual void OnEventComplete()
    {
        ChangeState(EventState.Completed);
        EventData.Time.EventCompleteDateTime = DateTime.Now.ToDetailedString();
        EventData.Time.EventClaimTimeRemaining = TimeConfig.EventClaimTime.ToSeconds();
        SaveData();
    }

    /// <summary>
    /// Change state to Deactivated. Countdown to next event reactivate
    /// </summary>
    protected virtual void OnEventEnded()
    {
        ChangeState(EventState.Deactivated);
        EventData.Time.EventEndedDateTime = DateTime.Now.ToDetailedString();
        EventData.Time.EventReactiveTimeRemaning = TimeConfig.EventResetTime.ToSeconds();
        SaveData();
    }

    protected virtual void ChangeState(EventState state)
    {
        EventData.State = state;
    }

    #endregion
}
