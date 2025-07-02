using System;
using System.Collections.Generic;
[Serializable]
public class AchievementData
{
    public string ID;
    public int UnlockLevel;
    public int Progress;

    public AchievementData(string iD, int unlockLevel, int progress)
    {
        ID = iD;
        UnlockLevel = unlockLevel;
        Progress = progress;
    }
}
[Serializable]
public class AchievementDataWrapper
{
    public List<AchievementData> achievementDatas = new List<AchievementData>();
}
