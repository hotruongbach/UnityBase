using MyBox;
using UnityEngine;
[CreateAssetMenu(fileName = "GameConfig", menuName = "Game/GameConfig")]
public class GameConfig : ScriptableObject
{
    internal Vector3 billboardTextOffset;
    internal Vector3 billboardSpriteOffset;

    public float BaseResolution = 2160f / 1080f;
    public float TrailLength = 1;

    [Separator("PLAYER CONFIG")]
    public float PlayerSpeed = 5;
    public float PlayerJumpForce = 10;
    public float TeleportTime = 1f;

    [Separator("TRAP CONFIG")]
    public float SpringForce = 15;

    [Separator("WEIGHT CONFIG")]
    public float RockWeight = 50;
    public float WoodenBoxWeight = 10;
}
