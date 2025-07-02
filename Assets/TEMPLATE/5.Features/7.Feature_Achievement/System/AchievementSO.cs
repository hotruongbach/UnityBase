using Template.Stamina;
using Template.User;
using MyBox;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CreateAssetMenu(fileName = " new Achievement", menuName = "GAME/ACHIEVEMENT/Achievement")]
public class AchievementSO :ScriptableObject
{
    [ReadOnly] public string ID = "a001";
    public int Progress;

    public AchievementTrigger Trigger;
    public string Title;
    public string Description;
    public Sprite Icon;
    public int UnlockLevel;
    public List<AchievementStage> AchievementStages;

    public bool IsUnlocked => (UserManager.Level >= UnlockLevel);

    private void OnValidate()
    {
        for (int i = 0; i < AchievementStages.Count; i++)
        {
            AchievementStage stage = AchievementStages[i];
            stage.Stage = i;
        }
    }


}
[Serializable]
public class AchievementStage
{
    public int Stage;
    public int Objective;
    public int coinReward;
}

public enum AchievementTrigger
{
    None = 0,

    Win = 1,
    Lose = 2,

    LoginTotal = 3,
    LoginLongestConsecutive = 4,

    CoinCollected = 5,
    CoinSpent = 6,

}
