using DG.Tweening;
using Monster;
using Monster.UI;
using Monster.Utilities;
using MyBox;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PopupRaceProcessing : Window
{
    //[SerializeField] RaceEventData eventData;
    //[SerializeField] RaceEventConfig eventConfig;

    [Separator("RACING PHASE")]
    [SerializeField] TimeArea timer;
    [SerializeField] TMP_Text description;
    [SerializeField] List<Slider> competitorProgress = new List<Slider>();
    [SerializeField] List<Avatar> avatars = new List<Avatar>();
    [SerializeField] Button winButton;
    [SerializeField] Button loseButton;

    RaceData eventData => RaceManager.Data;
    RaceCustomConfig eventConfig => RaceManager.CustomConfig;
    private void Start()
    {

#if BUILT_SANDBOX
        winButton.gameObject.SetActive(true);
        endQuote.gameObject.SetActive(true);

        winButton.onClick.AddListener(OnWinButton);
        loseButton.onClick.AddListener(OnLoseButton);
#else
        winButton.gameObject.SetActive(false);
        loseButton.gameObject.SetActive(false);
#endif
    }

    private void OnWinButton()
    {
        RaceManager.ForceWin();
        UpdateCompetitorProgress();
    }
    private void OnLoseButton()
    {
        RaceManager.ForceLose();
        UpdateCompetitorProgress();
    }

    private void UpdateEventTime(object sender, EventArgs e)
    {
        //timeText.text = eventData.RemainingTime.ToTimeFormat();
        if (eventData.State == EventState.Processing)
        {
            timer.SetTime(RaceManager.Time.EventRuntimeRemaining);
        }
    }

    public override void Show(Action onComplete = null, object param = null)
    {

        UpdateEventTime(null, null);

        description.text = $"Be the first to finish {eventConfig.RaceLength} levels and win the grand prize";
        InitCompetitorAvatar();

        Clock.Tick += UpdateEventTime;

        base.Show(onComplete);
    }

    private void ShowEnd()
    {
        GameService.ClosePopup();
        this.ShowPopup<PopupRaceEnded>();
    }
    public override void OnAnimationComplete(Action OnAdsShowSuccess = null, Action OnAdsShowFailed = null)
    {
        base.OnAnimationComplete(OnAdsShowSuccess, OnAdsShowFailed);
        Invoke(nameof(UpdateCompetitorProgress), 0.5f);
        //UpdateCompetitorProgress();
    }

    public override void Hide(Action onComplete = null)
    {
        Clock.Tick -= UpdateEventTime;

        base.Hide(onComplete);
    }

    public override void OnReveal(Action onComplete = null, object param = null)
    {
        //UpdateCompetitorProgress();
        InitCompetitorAvatar();
        base.OnReveal(onComplete);
    }

    private void UpdateCompetitorProgress()
    {
        competitorProgress[0].maxValue = eventConfig.RaceLength;
        competitorProgress[1].maxValue = eventConfig.RaceLength;
        competitorProgress[2].maxValue = eventConfig.RaceLength;
        competitorProgress[3].maxValue = eventConfig.RaceLength;
        competitorProgress[4].maxValue = eventConfig.RaceLength;

        competitorProgress[0].DOValue(eventData.player.CurrentStage - eventData.StartStage, 0.5f);
        competitorProgress[1].DOValue(eventData.bot1.CurrentStage - eventData.StartStage, 0.5f);
        competitorProgress[2].DOValue(eventData.bot2.CurrentStage - eventData.StartStage, 0.5f);
        competitorProgress[3].DOValue(eventData.bot3.CurrentStage - eventData.StartStage, 0.5f);
        competitorProgress[4].DOValue(eventData.bot4.CurrentStage - eventData.StartStage, 0.5f);

        if (eventData.State == EventState.Completed)
        {
            Invoke(nameof(ShowEnd), 1f);
        }
    }

    private void InitCompetitorAvatar()
    {
        //avatars[0].SetAvatar(User.GetUserAvatarIndex(), User.GetUserFrameIndex());
        //avatars[1].SetAvatar(eventData.bot1.Icon, eventData.bot1.Frame);
        //avatars[2].SetAvatar(eventData.bot2.Icon, eventData.bot2.Frame);
        //avatars[3].SetAvatar(eventData.bot3.Icon, eventData.bot3.Frame);
        //avatars[4].SetAvatar(eventData.bot4.Icon, eventData.bot4.Frame);

        //avatars[0].SetName(User.UserName, true);
        //avatars[1].SetName(eventData.bot1.Name, true);
        //avatars[2].SetName(eventData.bot2.Name, true);
        //avatars[3].SetName(eventData.bot3.Name, true);
        //avatars[4].SetName(eventData.bot4.Name, true);

        //avatars[0].ShowPosition(false);
        //avatars[1].ShowPosition(false);
        //avatars[2].ShowPosition(false);
        //avatars[3].ShowPosition(false);
        //avatars[4].ShowPosition(false);
    }
}
