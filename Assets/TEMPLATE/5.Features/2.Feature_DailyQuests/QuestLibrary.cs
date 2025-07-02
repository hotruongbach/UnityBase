using Template.FEATURES.DailyGift;
using Template.Utilities;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Template.Quests
{
    [CreateAssetMenu(fileName = "QuestLibrary", menuName = "GAME/QUEST/QuestLibrary")]
    public class QuestLibrary : ScriptableObject
    {
        public List<Quest> questPool = new List<Quest>();

        private void OnValidate()
        {
            foreach(Quest quest in questPool)
            {
                quest.SetSummarize();
            }
        }

        public List<Quest> RandomQuests(int questNumber)
        {
            return questPool.GetRandomElements(questNumber);
        }
    }
}