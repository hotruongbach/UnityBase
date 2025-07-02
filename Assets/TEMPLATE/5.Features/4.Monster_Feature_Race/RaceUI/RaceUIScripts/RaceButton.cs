using Monster.UI;
using System;
using TMPro;
using UnityEngine;

public class RaceButton : ButtonBase
{
    EventState raceState => RaceManager.EventState;
    EventTimeData eventTime => RaceManager.Time;

    private void Start()
    {
        OnEventStatusChanged(null, null);
        RaceManager.OnStatusChanged += OnEventStatusChanged;
    }

    protected override void OnClick()
    {
        base.OnClick();
        ShowPopupRace();
    }
    private void ShowPopupRace()
    {
        Bug.Log($"{raceState}");
        if (raceState == EventState.Preparing)
        {
            this.ShowPopup<PopupRacePrepare>();
        }
        if (raceState == EventState.Processing || raceState == EventState.Completed)
        {
            this.ShowPopup<PopupRaceProcessing>();
        }
    }

    public void OnEventStatusChanged(object sender, EventArgs e)
    {
        this.gameObject.SetActive(raceState != EventState.Deactivated);

        //notiTag.SetActive(RaceEventManager.HasNotification);
    }
}
