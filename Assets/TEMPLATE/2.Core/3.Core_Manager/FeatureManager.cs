using Monster.Utilities;
using MyBox;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class FeatureManager : MonoBehaviour
{
    #region FIELD AND PROPERTIES
    [SerializeField] bool IsDebuging = true;
    [Separator]
    [SerializeField] public List<FeatureComponent> AvailableFeature = new List<FeatureComponent>();
    
    private List<FeatureBase> activateFeatures = new List<FeatureBase>();
    #endregion

    #region INITIALIZE

    //Load => Check => Start

    public IEnumerator LoadFeaturesData()
    {
        if(activateFeatures.Count == 0)
        {
            if(IsDebuging) Bug.Log("FEATURE REPORT: ".Colored(Color.yellow) + "No feature activated.");
            yield return null;
        }

        foreach (var feature in activateFeatures)
        {
            Bug.Log("FEATURE REPORT: ".Colored(Color.yellow) + $"{feature.GetType()} loaded");
            yield return feature.LoadData();
        }
    }
    public IEnumerator CheckDayAll(bool IsNewDay)
    {
        if (IsDebuging) Bug.Log("FEATURE REPORT: ".Colored(Color.yellow) + $"IsNewDay {IsNewDay.ToString().SetColor("red")}");

        if (activateFeatures.Count == 0)
        {
            yield return null;
        }

        foreach (var feature in activateFeatures)
        {
            if (IsDebuging) Bug.Log("FEATURE REPORT: ".Colored(Color.yellow) + $"{feature.GetType()} checked");
            yield return feature.CheckNewDays(IsNewDay);
        }
    }
    public IEnumerator StartAllFeature()
    {
        foreach (var feature in activateFeatures)
        {
            if (IsDebuging) Bug.Report("FEATURE REPORT: ",$"{feature.GetType()} started");
            yield return feature.StartFeature();
        }
    }
    #endregion

    #region HELPER
    private void OnValidate()
    {
        CheckActivateFeature();
    }
    [ButtonMethod]
    private void UpdateFeatures()
    {
        AvailableFeature.Clear();
        List<FeatureBase> foundFeature = gameObject.GetComponentsInChildren<FeatureBase>().ToList();

        foreach (var feature in foundFeature)
        {
            string featureName = feature.GetType().ToString().Replace("Manager", "").Trim().Split(".").Last();
            AvailableFeature.Add(new FeatureComponent(feature, $"{featureName}", true));
        }
        CheckActivateFeature();
    }
#if UNITY_EDITOR
    [ButtonMethod]
    private void ActiveAll()
    {
        foreach (var feature in AvailableFeature)
        {
            feature.IsActivate = true;
        }
        EditorUtility.SetDirty(gameObject);
    }

    [ButtonMethod]
    private void DeactiveAll()
    {
        foreach (var feature in AvailableFeature)
        {
            feature.IsActivate = false;
        }
        EditorUtility.SetDirty(gameObject);
    }

#endif
    private void CheckActivateFeature()
    {
        activateFeatures.Clear();
        foreach (var feature in AvailableFeature)
        {
            if (feature.IsActivate) activateFeatures.Add(feature.Feature);
        }
    }
    #endregion
}

[Serializable]
public class FeatureComponent
{
    [HideInInspector]public string Name;
    [ReadOnly]public FeatureBase Feature;
    public bool IsActivate;

    public FeatureComponent(FeatureBase feature, string featureName, bool isActivate)
    {
        Feature = feature;
        Name = featureName;
        IsActivate = isActivate;
    }
}
