using Monster.FEATURES.DailyGift;
using Monster.Utilities;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Monster.Quests
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