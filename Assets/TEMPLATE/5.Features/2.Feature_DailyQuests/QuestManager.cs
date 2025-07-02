using Template.Utilities;
using MyBox;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Template.Quests
{
    public class QuestManager : FeatureBase
    {
        #region PROPERTIES
        [DisplayInspector,SerializeField] QuestLibrary _library;
        [Separator("Quest setting")]
        [Tooltip("Number of quest spawned every day")]
        [SerializeField] int QuestNumber = 10;
        [ReadOnly][SerializeField] public string TimeRemainingInString;
        [Separator("Runtime")]
        [SerializeField] QuestData _data;

        public static QuestManager Instance;
        string DAILY_QUEST_DATA = "daily_quest_data";
        #endregion

        #region MONOBEHAVIOR

        private void Awake()
        {
            Instance = this;
        }
        public override IEnumerator LoadData()
        {
            string json = PlayerPrefs.GetString(DAILY_QUEST_DATA, string.Empty);

            //load data from json
            if (json == string.Empty)
            {
                Bug.Log($"Quest data empty");
                //no data founded
                _data = NewQuestData();
                SaveData();
            }
            else
            {
                Bug.Log($"Quest data found");
                _data = JsonUtility.FromJson<QuestData>(json);
            }
            yield return null;
        }
        public override IEnumerator StartFeature()
        {
            yield return null;
            _data.TimeRemaining = CalculateTimeRemaining();
            //add time listener
            Clock.Tick += OnTick;
            //add quest listener
            AddListener();
        }

        private void OnTick(object sender, EventArgs e)
        {
            if(_data.TimeRemaining > 0)
            {
                _data.TimeRemaining--;
                TimeRemainingInString = _data.TimeRemaining.ToTimeFormat();
            }
            else
            {
                //reset quest data
                _data = NewQuestData();
                SaveData();
            }
        }

        private int CalculateTimeRemaining()
        {
            var endOfDay = DateTime.Now.Date.AddDays(1);
            return (int)(endOfDay - DateTime.Now).TotalSeconds;
        }

        protected override void SaveData()
        {
            string json = JsonUtility.ToJson(_data);
            PlayerPrefs.SetString(DAILY_QUEST_DATA,json);
        }
        private QuestData NewQuestData()
        {
            QuestData newData = new QuestData();
            newData.ListQuests = _library.RandomQuests(QuestNumber);
            return newData;
        }
        #endregion

        #region QUEST LISTENER

        private void AddListener()
        {
            //add listener for objective
            //GameService.AddListener(EventID.Win, OnPlayerWin);
            TemplateEventManager.WinEvent.AddListener(this,OnPlayerWin);
        }

        private void OnPlayerWin(int param)
        {
            UpdateQuestProgress(QuestObjectiveType.Win, 1);
        }

        private void UpdateQuestProgress(QuestObjectiveType type, int amount)
        {
            bool IsAnyQuestUpdated = false;
            //check all quest in list quest
            for (int i = 0; i < _data.ListQuests.Count; i++)
            {
                var quest = _data.ListQuests[i];

                //Skip checking quest if it completed
                if (quest.QuestStatus != QuestStatus.Incomplete) return;

                int objCompleted = 0;

                //check all objective in quest
                for (int j = 0; j < quest.Objectives.Count; j++)
                {
                    var objective = quest.Objectives[j];
                    if (objective.ObjectiveType == type)
                    {
                        if (objective.Current < objective.Target)
                        {
                            objective.Current += amount;
                            IsAnyQuestUpdated = true;
                        }
                    }
                    if(objective.Current >= objective.Target)
                    {
                        objCompleted++;
                    }
                }

                if (objCompleted == quest.Objectives.Count)
                {
                    quest.QuestStatus = QuestStatus.AllObjectiveCompleted;
                    Bug.Log($"Quest {quest.Name} completed");
                }
            }
            if (IsAnyQuestUpdated) SaveData();
        }

        public override IEnumerator CheckNewDays(bool isNewDay)
        {
            yield return null;
            if (isNewDay)
            {
                _data = NewQuestData();
                SaveData();
            }
        }
        #endregion
    }
    [Serializable]
    public struct QuestData
    {
        public List<Quest> ListQuests;
        public int TimeRemaining;
    }
}