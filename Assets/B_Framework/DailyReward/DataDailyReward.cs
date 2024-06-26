using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

[System.Serializable]
public class DailyRewardType
{
    public int day;
    [Header("Money")]
    public float money;
}
[CreateAssetMenu(fileName = "DataDailyReward", menuName = "Data/Data Daily Reward")]
public class DataDailyReward : ScriptableObject
{
    public List<DailyRewardType> DailyRewardType = new List<DailyRewardType>();
}
