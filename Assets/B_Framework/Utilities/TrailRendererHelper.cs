using System.Collections;
using UnityEngine;

public class TrailRendererHelper : MonoBehaviour
{
    [SerializeField] TrailRenderer mTrail;
    [SerializeField] GameConfig gameConfig;
    protected float mTime => gameConfig.TrailLength;
    private void Start()
    {
        mTrail.time = 0;
        mTrail.Clear();
        mTrail.enabled = false;
        Invoke(nameof(ResetTrails), 0.025f);
    }

    void OnEnable()
    {
        mTrail.time = 0;
        mTrail.Clear();
        mTrail.enabled = false;
        Invoke(nameof(ResetTrails),0.025f);
    }

    void ResetTrails()
    {
        mTrail.time = mTime;
        mTrail.enabled = true;
    }
}