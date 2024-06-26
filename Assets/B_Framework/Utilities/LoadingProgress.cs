using UnityEngine;

public class LoadingProgress
{
    public float progress;
    public string log;

    public LoadingProgress(float progress, string log)
    {
        this.progress = progress;
        this.log = log;
    }
    public object LogNew(float progress, string log)
    {
        this.progress = progress;
        this.log = log;
        Debug.Log(log);
        return this;
    }
}
