using MyBox;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
namespace Monster.User
{
    public class UserManager : UserManagerBase, IYieldSaveLoad
    {
        #region COMMON
        public static UserManager Instance;

        private void Awake()
        {
            Instance = this;
        }

        [ButtonMethod]
        public override void Save()
        {
            string json = User.ToJson();
            PlayerPrefs.SetString(PlayerPrefsKey.USER_DATA, json);
        }

        /// <summary>
        /// Save and report change of resource type
        /// </summary>
        /// <param name="type"></param>
        public void Save(ResourceType type)
        {
            string json = User.ToJson();
            PlayerPrefs.SetString(PlayerPrefsKey.USER_DATA, json);
            Bug.Report("USER REPORT", $"{type} changed. Current {type}: {Instance._GetResourceInt(type)}");
        }

        [ButtonMethod]
        private void Load()
        {
            StartCoroutine(LoadData());
        }

        private int _GetResourceInt(ResourceType type)
        {
            return Instance.GetFieldInt(ResourceMap[type]);
        }

        /// <summary>
        /// Just change resource value, not save/report yet
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        private void _AddResource(ResourceType type, int value)
        {
            //checking if resource registed in resource map
            if (Instance.ResourceMap.ContainsKey(type) == false)
            {
                Bug.Report("USER REPORT", "Resource Key not registed, please add new pair <Type,Key> to ResourceMap");
                return;
            }

            AddValueToField(ResourceMap[type], value);
        }
        #endregion

        #region KEY
        private static string NAME = "user_name";
        private static string ID = "user_id";
        private static string AVATAR = "user_avatar";
        private static string FRAME = "user_frame";
        private static string LEVEL = "user_level";
        //resource key
        private static string COIN = "user_coin";

        private Dictionary<ResourceType, string> ResourceMap = new Dictionary<ResourceType, string>
        {
            // ===>>>>  ADD NEW <RESOURCE TYPE, RESOURCE KEY> HERE

            {ResourceType.Coin, COIN},
        };
        #endregion

        #region DEFAULT USER DATA
        [ButtonMethod]
        public override void SetDefault()
        {
            User = new UserData();

            User.AddOrUpdateField(NAME, "NewPlayer");
            User.AddOrUpdateField(ID, "01");
            User.AddOrUpdateField(AVATAR, 0);
            User.AddOrUpdateField(FRAME, 0);
            User.AddOrUpdateField(LEVEL, 0);

            //resources
            User.AddOrUpdateField(COIN, 0);
        }

        #endregion

        #region COMMON SERVICE
        public static int Level => Instance.GetFieldInt(LEVEL);
        public static string UserName => Instance.GetFieldString(NAME);
        public static void LevelUp()
        {
            Instance.AddValueToField(LEVEL, 1);
            Instance.Save();
        }
        #endregion

        #region RESOURCE SERVICE
        public static int Coin => GetResourceValue(ResourceType.Coin);

        public static int GetResourceValue(ResourceType type)
        {
            //checking if resource registed in resource map
            if (Instance.ResourceMap.ContainsKey(type) == false)
            {
                Bug.Report("USER REPORT", "Resource Key not registed, please add new pair <Type,Key> to ResourceMap");
                return 0;
            }

            //return value
            return Instance.GetFieldInt(Instance.ResourceMap[type]); ;
        }

        public static void ClaimResource(ResourceData data, Action onSuccess = null)
        {
            //checking value
            if (data.ResourceValue < 0)
            {
                Bug.Report("USER REPORT", "Math error! Claim value must > 0");
                return;
            }

            //change resource value
            Instance._AddResource(data.ResourceType, data.ResourceValue);

            //callback
            onSuccess?.Invoke();

            //Save and report
            Instance.Save(data.ResourceType);
        }

        public static void ClaimResource(ResourceType type, int value, Action onSuccess = null)
        {
            //checking value
            if (value < 0)
            {
                Bug.Report("USER REPORT", "Math error! Claim value must > 0");
                return;
            }

            //change resource value
            Instance._AddResource(type, value);

            //callback
            onSuccess?.Invoke();

            //Save and report
            Instance.Save(type);
        }

        public static void ClaimResource(List<ResourceData> datas, int multiply, Action onSuccess = null)
        {
            foreach (var resource in datas)
            {
                //checking value
                if (resource.ResourceValue < 0)
                {
                    Bug.Report("USER REPORT", "Math error! Claim value must > 0");
                    continue;
                }

                //change resource value
                Instance._AddResource(resource.ResourceType, resource.ResourceValue * multiply);
            }

            //callback
            onSuccess?.Invoke();

            //Save and report
            Instance.Save();
            Bug.Report("USER REPORT", $"Claim {datas.Count} resources with multiply by {multiply} successed!");
        }

        public static void ClaimResource(List<ResourceData> datas, Action onSuccess = null)
        {
            foreach (var resource in datas)
            {
                //checking value
                if (resource.ResourceValue < 0)
                {
                    Bug.Report("USER REPORT", "Math error! Claim value must > 0");
                    continue;
                }

                //change resource value
                Instance._AddResource(resource.ResourceType, resource.ResourceValue);
            }

            //callback
            onSuccess?.Invoke();

            //Save and report
            Instance.Save();
            Bug.Report("USER REPORT", $"Claim {datas.Count} resources successed!");
        }

        public static void SpendResource(ResourceData data, Action onSuccess = null, Action onFailed = null)
        {
            //checking value
            if (data.ResourceValue < 0)
            {
                Bug.Report("USER REPORT", "Math error! Claim value must > 0");
            }

            //check can spend
            int currentResource = Instance._GetResourceInt(data.ResourceType);
            if (data.ResourceValue > currentResource)
            {
                Bug.Report("USER REPORT", $"Not enought {data.ResourceType} spend {data.ResourceValue}/ remaining {currentResource}");
                onFailed?.Invoke();
                return;
            }

            //change resource value
            Instance._AddResource(data.ResourceType, -data.ResourceValue);

            //callback
            onSuccess?.Invoke();

            //Save and report
            Instance.Save(data.ResourceType);
        }

        public static void SpendResource(ResourceType type, int spendValue, Action onSuccess = null, Action onFailed = null)
        {
            //checking value
            if (spendValue < 0)
            {
                Bug.Report("USER REPORT", "Math error! Claim value must > 0");
            }

            //check can spend
            int currentResource = Instance._GetResourceInt(type);
            if (spendValue > currentResource)
            {
                Bug.Report("USER REPORT", $"Not enought {type} spend {spendValue}/ remaining {currentResource}");
                onFailed?.Invoke();
                return;
            }

            //change resource value
            Instance._AddResource(type, -spendValue);

            //Save and report
            Instance.Save(type);
        }
        #endregion

        #region EDITOR

#if UNITY_EDITOR
        [Separator("Import/Export data")]
        [SerializeField] TextAsset importedData;

        [ButtonMethod]
        void ImportDataFromTextAsset()
        {
            string json = importedData.text;
            var userData = UserData.FromJson(json);
            User = userData;
            Save();
            Bug.Report("DATA REPORT", "Import completed!");
        }

        [ButtonMethod]
        void ExportData()
        {
            string json = PlayerPrefs.GetString(PlayerPrefsKey.USER_DATA, string.Empty);

            if (json == string.Empty)
            {
                Bug.Report("DATA REPORT", "User not found!");
            }

            //if has target text asset
            if (importedData != null)
            {
                string path = AssetDatabase.GetAssetPath(importedData);
                using (StreamWriter writer = new StreamWriter(path))
                {
                    writer.WriteLine(json);
                }

                Bug.Report("DATA REPORT", $"Exported to {path}");
                return;
            }
            else //if has NO target text asset
            {
                string fileName = "ExportedUserData";
                string path = "Assets/Resources/" + fileName + ".txt";
                using (StreamWriter writer = new StreamWriter(path))
                {
                    writer.WriteLine(json);
                }

                Bug.Report("DATA REPORT", $"Exported to {path}");
                return;
            }
        }
#endif
        #endregion
    }
}
