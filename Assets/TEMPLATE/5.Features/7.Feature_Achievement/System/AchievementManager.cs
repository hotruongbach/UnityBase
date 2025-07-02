using Template.User;
using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : FeatureBase
{
    [SerializeField, DisplayInspector] AchievementLibrary library;
    [SerializeField] List<AchievementData> achievementData = new List<AchievementData>();

    Dictionary<AchievementTrigger, List<AchievementData>> AchievementTriggerDict = new Dictionary<AchievementTrigger, List<AchievementData>>();

    protected override void SaveData()
    {
        AchievementDataWrapper wrapper = new AchievementDataWrapper();
        wrapper.achievementDatas = this.achievementData;

        string json = JsonUtility.ToJson(wrapper);
        PlayerPrefs.SetString(PlayerPrefsKey.ACHIEVEMENT, json);
    }

    public override IEnumerator LoadData()
    {
        foreach (AchievementSO achievementSO in library.achievementSOs)
        {
            var data = achievementSO;
            achievementData.Add(new AchievementData(data.ID, data.UnlockLevel, 0));
        }

        LoadFromPlayerPref();
        
        yield return null;

    }

    public override IEnumerator CheckNewDays(bool isNewDay)
    {
        yield return null;
    }

    public override IEnumerator StartFeature()
    {
        InitListener();
        yield return null;
    }

    private void LoadFromPlayerPref()
    {
        string json = PlayerPrefs.GetString(PlayerPrefsKey.ACHIEVEMENT, "");
        if (json.IsNullOrEmpty() == false)
        {
            AchievementDataWrapper wrapper = JsonUtility.FromJson<AchievementDataWrapper>(json);

            achievementData.Clear();
            achievementData = wrapper.achievementDatas;
        }
    }

    private void InitListener()
    {
        foreach(var _achievementData in achievementData)
        {
            AchievementTrigger triggerEvent = library.achievementSOs.Find(x => x.ID == _achievementData.ID).Trigger;
            if (!AchievementTriggerDict.ContainsKey(triggerEvent))
            {
                AchievementTriggerDict[triggerEvent] = new List<AchievementData> { _achievementData };
            }
            else
            {
                AchievementTriggerDict[triggerEvent].Add(_achievementData);
            }
        }
    }

    public void OnAchievementTrigger(AchievementTrigger triggerEvent, int triggerAmount)
    {
        foreach(var achievement in AchievementTriggerDict[triggerEvent])
        {
            if(UserManager.Level >= achievement.UnlockLevel)
            {
                achievement.Progress += triggerAmount;
            }
        }
        SaveData();
    }
}
