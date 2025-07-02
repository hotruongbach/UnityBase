using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class RaceCustomConfig
{
    /// <summary>
    /// How many stage that competitor need to win
    /// </summary>
    [SerializeField] public int RaceLength = 12;
    public List<ResourceData> FirstReward;
    public List<ResourceData> SecondReward;
    public List<ResourceData> ThirdReward;
}
