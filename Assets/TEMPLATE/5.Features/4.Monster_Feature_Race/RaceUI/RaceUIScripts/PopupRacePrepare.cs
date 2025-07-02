using Monster;
using Monster.UI;
using MyBox;
using System;
using TMPro;
using UnityEngine;
public class PopupRacePrepare : Window
{
    [Separator("RACING")]
    [SerializeField] RaceData eventSO;
    RaceCustomConfig config => RaceManager.CustomConfig;

    [Separator]
    [SerializeField] TMP_Text description;
    [SerializeField] TimeArea raceEventTime;
    [SerializeField] ButtonBase joinButton;

    private void Start()
    {
        joinButton.AddListener(Join);
    }

    private void Join()
    {
        if (RaceManager.EventState != EventState.Preparing)
        {
            Bug.Log("RACE EVENT ENDED", "red");
        }
        else
        {
            RaceManager.EventJoin();
            this.ShowPopup<PopupRaceProcessing>();
        }
    }

    public override void Show(Action onComplete = null, object param = null)
    {
        //make sure that time text has starting value
        UpdateTime(null, null);
        description.text = $"Be the first to finish {config.RaceLength} levels  and win the grand prize";

        //subscribe to clock
        Clock.Tick += UpdateTime;

        base.Show(onComplete);
    }
    public override void OnReveal(Action onComplete = null, object param = null)
    {
        Bug.Log("Popup update");
        UpdateTime(null, null);
        base.OnReveal(onComplete);
    }
    public override void Hide(Action onComplete = null)
    {
        //unsubscribe to clock
        Clock.Tick -= UpdateTime;

        base.Hide(onComplete);
    }

    private void UpdateTime(object sender, EventArgs e)
    {
        raceEventTime.SetTime(RaceManager.Time.EventPrepareTimeRemaining);
    }
}
