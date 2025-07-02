public enum EventState
{
    /// <summary>
    /// Event are not running or in countdown
    /// </summary>
    Deactivated = 0,

    /// <summary>
    /// Event are active, preparing to join
    /// </summary>
    Preparing = 1,

    /// <summary>
    /// Event are processing with bots, players ...
    /// </summary>
    Processing = 2,

    /// <summary>
    /// Event process completed, time to grab some reward
    /// </summary>
    Completed = 3,
}
