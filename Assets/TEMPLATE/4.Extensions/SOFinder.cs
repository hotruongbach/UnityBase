using Template.FEATURES.DailyGift;
using Template.Quests;
using Template.Stamina;
using UnityEditor;
using UnityEngine;

public static class SOFinder
{
#if UNITY_EDITOR
    #region CORE FUNCTION
    public static T Select<T>(string path, bool isPingAsset = true) where T : ScriptableObject
    {
        T myObject = AssetDatabase.LoadAssetAtPath<T>(path);

        if (myObject != null)
        {
            // Select the ScriptableObject in the Project window
            if (isPingAsset)
            {
                Selection.activeObject = myObject;
                EditorGUIUtility.PingObject(myObject);
            }
        }
        else
        {
            Bug.Report("SO FINDER REPORT", "SO not found! Please update path");
            return null;
        }

        return myObject;
    }
    #endregion

    #region PATH
    static string DAILY_GIFT_CONFIG_PATH = "Assets/1.TEMPLATE/5.Features/1.Feature_DailyGift/GiftSystem/DailyGiftConfig.asset";
    static string DAILY_QUEST_CONFIG_PATH = "Assets/1.TEMPLATE/5.Features/2.Feature_DailyQuests/QuestLibrary.asset";
    static string SPIN_CONFIG_PATH = "Assets/1.TEMPLATE/5.Features/5.Feature_Spin/DataSpin.asset";
    static string ACHIEVEMENT_CONFIG_PATH = "Assets/1.TEMPLATE/5.Features/7.Feature_Achievement/SO/AchievementLibrary.asset";
    static string ONLINE_GIFT_CONFIG_PATH = "Assets/1.TEMPLATE/5.Features/3.Feature_OnlineGift/OnlineGiftLibrary.asset";
    static string RACE_DATA_PATH = "Assets/1.TEMPLATE/5.Features/4.Feature_Race/RaceSystem/RaceData.asset";
    static string RACE_TIME_CONFIG_PATH = "Assets/1.TEMPLATE/5.Features/4.Feature_Race/RaceSystem/RaceTimeConfig.asset";
    static string STAMINA_CONFIG_PATH = "Assets/1.TEMPLATE/5.Features/6.Feature_Stamina/StaminaConfig.asset";
    static string GAME_CONFIG_PATH = "Assets/1.TEMPLATE/10.Config/GameConfig.asset";

    #endregion

    #region FINDER METHODS
    [MenuItem("Tools/SOFinder/DailyGift/Config")]
    public static void FindDailyGift()
    {
        Select<DailyGiftConfig>(DAILY_GIFT_CONFIG_PATH);
    }

    [MenuItem("Tools/SOFinder/DailyQuest/Config")]
    public static void FindDailyQuest()
    {
        Select<QuestLibrary>(DAILY_QUEST_CONFIG_PATH);
    }

    [MenuItem("Tools/SOFinder/Spin/Spin Config")]
    public static void FindSpin()
    {
        Select<DataSpin>(SPIN_CONFIG_PATH);
    }

    [MenuItem("Tools/SOFinder/AchievementLibrary")]
    public static void FindAchievement()
    {
        Select<AchievementLibrary>(ACHIEVEMENT_CONFIG_PATH);
    }

    [MenuItem("Tools/SOFinder/OnlineGift/Config")]
    public static void FindOnlineGift()
    {
        Select<OnlineGiftLibrary>(ONLINE_GIFT_CONFIG_PATH);
    }

    [MenuItem("Tools/SOFinder/Race/RaceDataSO")]
    public static void FindRaceData()
    {
        Select<StaminaConfig>(RACE_DATA_PATH);
    }

    [MenuItem("Tools/SOFinder/Race/RaceTime")]
    public static void FindRaceTime()
    {
        Select<EventTimeConfig>(RACE_TIME_CONFIG_PATH);
    }

    [MenuItem("Tools/SOFinder/Stamina")]
    public static void FindStaminaConfig()
    {
        Select<StaminaConfig>(STAMINA_CONFIG_PATH);
    }

    [MenuItem("Tools/SOFinder/GameConfig")]
    public static GameConfig FindGameConfig()
    {
        return Select<GameConfig>(GAME_CONFIG_PATH,false);
    }
    #endregion
#endif
}
