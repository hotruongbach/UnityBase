using System.Collections;
using UnityEngine;

public abstract class FeatureBase : MonoBehaviour
{
    protected abstract void SaveData();
    public abstract IEnumerator LoadData();
    public abstract IEnumerator CheckNewDays(bool isNewDay);
    public abstract IEnumerator StartFeature();
}
