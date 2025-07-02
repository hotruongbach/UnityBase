using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MonsterEvent<T>
{
    private List<Action<T>> callbacks = new List<Action<T>>();
    private HashSet<Action<T>> listenersToRemove = new HashSet<Action<T>>();

#if UNITY_EDITOR
    public List<Component> Listeners;
    public List<EventHistory<T>> History = new List<EventHistory<T>>();

    private const int MaxHistorySize = 10;
#endif

    // Post the event to all listeners
    public void Post(Component sender, T param)
    {
        // Now, remove listeners that were marked for removal
        RemoveStagedListeners();

#if UNITY_EDITOR
        // Add the new event to history, ensuring it only keeps the latest 10 events
        History.Add(new EventHistory<T>(sender, param));

        if (History.Count > MaxHistorySize)
        {
            History.RemoveAt(0); // Remove the oldest element if the history exceeds 10 items
        }
#endif
        // Safely iterate over the listeners list and invoke them
        foreach (var listener in callbacks)
        {
            listener?.Invoke(param);
        }
    }

    // Add a listener to the event
    public void AddListener(Component listener, Action<T> callback)
    {
        if (callbacks.Contains(callback)) return;

        callbacks.Add(callback);
#if UNITY_EDITOR
        Listeners.Add(listener);
#endif
    }
    public void AddListener(Action<T> callback, Component listener)
    {
        if (callbacks.Contains(callback)) return;

        callbacks.Add(callback);
#if UNITY_EDITOR
        Listeners.Add(listener);
#endif
    }

    // Remove a listener from the event, but it won't happen until after the event has been posted
    public void RemoveListener(Component listener, Action<T> callback)
    {
        if (callbacks.Contains(callback) == false) return;
        // Staging the removal of the listener
        listenersToRemove.Add(callback);

#if UNITY_EDITOR
        //save reference of listener
        Listeners.Remove(listener);
#endif
    }

    // Perform the actual removal of staged listeners
    private void RemoveStagedListeners()
    {
        foreach (var listener in listenersToRemove)
        {
            callbacks.Remove(listener);
        }

        // Clear the staged removal set
        listenersToRemove.Clear();
    }
}
[Serializable]
public class EventHistory<T>
{
    public Component Sender;
    public T Value;

    public EventHistory(Component sender, T value)
    {
        Sender = sender;
        Value = value;
    }
}
