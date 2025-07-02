using MyBox;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CreateAssetMenu(fileName = "AchievementLibrary", menuName = "GAME/ACHIEVEMENT/Library")]
public class AchievementLibrary : ScriptableObject
{
    [DisplayInspector] public List<AchievementSO> achievementSOs;

    private void OnValidate()
    {
        for (int i = 0; i < achievementSOs.Count; i++)
        {
            achievementSOs[i].ID = $"a_{i.ToString("D3")}";
        }
    }
}
