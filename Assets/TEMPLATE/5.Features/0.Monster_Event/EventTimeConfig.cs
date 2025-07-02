using MyBox;
using UnityEngine;
[CreateAssetMenu(fileName = "EventConfig", menuName = "GAME/FEATURE/EventConfig")]
public class EventTimeConfig : ScriptableObject
{
    public int UnlockLevel = 0;
    public bool HasClaimTime = true;

    [Tooltip("Time to player to join this event. If it run out before user joined, event ended immediately")]
    public float EventPrepareTime = 2f;

    [Tooltip("Time to event process. Start when user join. When it run out, event change to ending state")]
    public float EventRunningTime = 10f;

    [Tooltip("Time for user to claim reward. If it run out, user's reward is dismiss or send to mail")]
    [ConditionalField(nameof(HasClaimTime), false, true)][SerializeField] public float EventClaimTime = 2f;

    [Tooltip("Time to re-activate event")]
    public float EventResetTime = 5f;
}

public static class FloatHourExtensions
{
    public static int ToSeconds(this float value)
    {
        return (int)(value * 3600);
    }
}
