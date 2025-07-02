using Monster.Quests;
using Monster.UI;
using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public enum RewardSpin
{
    Coin,
    Diamond,
    Bomb,
    Skin
}
[System.Serializable]
public class ItemSpin
{
    public RewardSpin type;
    public Sprite img;
    [ConditionalField(nameof(type), false, RewardSpin.Coin, RewardSpin.Diamond, RewardSpin.Bomb)] public int total;
}
[CreateAssetMenu(fileName = "DataSpin", menuName = "Data/DataSpin")]
public class DataSpin : ScriptableObject
{
    public int amountItem;
    public List<ItemSpin> ListItemSpins = new List<ItemSpin>();
}
