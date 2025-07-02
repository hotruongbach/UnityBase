using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EventDataBase : ScriptableObject
{
    [SerializeField] public EventState State;
    [SerializeField] public EventTimeData Time;

    public abstract void FromJson(string json);

    public abstract string ToJson();

    public abstract void Clear();
    public abstract void Default();
}