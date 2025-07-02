using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
namespace Template.Quests
{
    [Serializable]
    public class Quest
    {
        [HideInInspector] public string Name;
        public QuestType QuestType;   
        public string Description;
        public List<QuestObjective> Objectives;
        [HideInInspector]public QuestStatus QuestStatus = QuestStatus.Incomplete; //not changeable on inspector

        public void SetSummarize()
        {
            Name = $"{QuestType}||";
            for (int i = 0; i < Objectives.Count; i++)
            {
                var obj = Objectives[i];
                obj.Name = $"{obj.ObjectiveType} x{obj.Target}";

                Name += $" {obj.Name}";
            }
        }
    }
    [Serializable]
    public class QuestObjective
    {
        [HideInInspector] public string Name; 
        public QuestObjectiveType ObjectiveType;
        public int Current;
        public int Target;
    }

    public enum QuestType
    {
        None = 0,
        Win = 1,
        WinAndLose = 2,
    }

    public enum QuestObjectiveType
    {
        None = 0,
        Win = 1,
        Lose = 2,
        VideoWatched = 3,
    }
    public enum QuestStatus
    {
        Incomplete = 0,
        AllObjectiveCompleted = 1,
        Claimed = 2,
    }
}
