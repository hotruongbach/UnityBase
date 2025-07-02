using MyBox;
using UnityEngine;
[CreateAssetMenu(fileName = "GameConfig", menuName = "Game/GameConfig")]
public class GameConfig : ScriptableObject
{
    [Foldout("UI CONFIG")] public float PopupAnimTime = 0.4f;
}
