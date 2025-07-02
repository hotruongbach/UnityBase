using MyBox;
using System;

[Serializable]
public class EventTimeData
{
    /// <summary>
    /// The datetime that event activated to user
    /// </summary>
    [ReadOnly] public string EventActivatedDateTime;
    public int EventPrepareTimeRemaining;

    /// <summary>
    /// The datetime that user press join
    /// </summary>
    [ReadOnly] public string EventJoinedDateTime;
    public int EventRuntimeRemaining;

    /// <summary>
    /// The datetime user is eliminate or complete the event
    /// </summary>
    [ReadOnly] public string EventCompleteDateTime;
    public int EventClaimTimeRemaining;

    /// <summary>
    /// The datetime that event totaly ended, start calculateing for next re_activate
    /// </summary>
    [ReadOnly] public string EventEndedDateTime;
    public int EventReactiveTimeRemaning;

    public void Display()
    {
        Bug.Log("EVENT TIME LOG--", "yellow");
        Bug.Log($"Activated time {EventActivatedDateTime} || {EventPrepareTimeRemaining}");
        Bug.Log($"Joined time {EventJoinedDateTime} || {EventRuntimeRemaining}");
        Bug.Log($"Complete time {EventCompleteDateTime} || {EventClaimTimeRemaining}");
        Bug.Log($"Ended time {EventEndedDateTime} || {EventReactiveTimeRemaning}");
    }
}
