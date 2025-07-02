using Monster.Utilities;
using MyBox;
using System;
using System.Collections;
using UnityEngine;

namespace Monster.Stamina
{
    public class StaminaManager : FeatureBase
    {
        #region PROPERTIES
        public static StaminaManager Instance;
        [SerializeField, DisplayInspector] StaminaConfig config;
        [Separator("RUNTIME")]
        [InitializationField,SerializeField] StaminaData data;
        #endregion

        #region MONOBEHAVIOR
        private void Awake()
        {
            Instance = this;
        }
        public override IEnumerator CheckNewDays(bool isNewDay)
        {
            yield return null;
        }

        public override IEnumerator StartFeature()
        {
            yield return null;

            Clock.Tick += OnTick;
        }
        public override IEnumerator LoadData()
        {
            yield return null;
            string json = PlayerPrefs.GetString(PlayerPrefsKey.STAMINA, string.Empty);

            if (json == string.Empty)
            {
                data = new StaminaData();
                data.CurrentStamina = config.MaxStamina;
                SaveData();
            }
            else
            {
                data = JsonUtility.FromJson<StaminaData>(json);
                if (!data.UnlimitStartTime.IsNullOrEmpty())
                {
                    int UnlimitTimeOffline = (int)(DateTime.Now - data.UnlimitStartTime.ToDetailedDatetime()).TotalSeconds;
                    data.UnlimitTimeRemaining = Mathf.Max(config.UnlimitTime - UnlimitTimeOffline,0);
                }

                if (!data.RecoverStartTime.IsNullOrEmpty())
                {
                    int RecoverOfflineTime = (int)(DateTime.Now - data.RecoverStartTime.ToDetailedDatetime()).TotalSeconds;
                    while (RecoverOfflineTime >= config.RecoverTime && data.CurrentStamina < config.MaxStamina)
                    {
                        RecoverOfflineTime -= config.RecoverTime;
                        data.CurrentStamina++;
                        data.RecoverStartTime = DateTime.Now.ToDetailedString();

                        if (data.CurrentStamina >= config.MaxStamina)
                        {
                            RecoverOfflineTime = 0;
                            data.RecoverStartTime = DateTime.Now.ToDetailedString();
                            data.TimeUntilNextRecover = 0;
                            SaveData();
                            break;
                        }
                    }

                    if (RecoverOfflineTime > 0)
                    {
                        data.RecoverStartTime = DateTime.Now.AddSeconds(-RecoverOfflineTime).ToDetailedString();
                        data.TimeUntilNextRecover = config.RecoverTime - RecoverOfflineTime;
                        SaveData();
                    }
                }
            }
        }

        private void OnTick(object sender, EventArgs e)
        {
            if (data.CurrentStamina < config.MaxStamina)
            {
                if (data.TimeUntilNextRecover > 0)
                {
                    data.TimeUntilNextRecover--;
                    if(data.TimeUntilNextRecover <= 0)
                    {
                        data.CurrentStamina++;

                        if (data.CurrentStamina < config.MaxStamina)
                        {
                            data.RecoverStartTime = DateTime.Now.ToDetailedString();
                            data.TimeUntilNextRecover = config.RecoverTime;
                        }

                        SaveData();
                    }
                }
            }

            if (data.UnlimitTimeRemaining > 0)
            {
                data.UnlimitTimeRemaining--;
                if( data.UnlimitTimeRemaining <= 0)
                {
                    data.UnlimitStartTime = "";
                    SaveData();
                }
            }
        }

        protected override void SaveData()
        {
            string json = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(PlayerPrefsKey.STAMINA, json);
        }

        #endregion

        #region STATIC
        public static int CurrentStamina => Instance.data.CurrentStamina;
        public static int RecoverTimeRemaining => Instance.data.TimeUntilNextRecover;
        public static int UnlimitTimeRemaining => Instance.data.UnlimitTimeRemaining;

        public static void SpendStamina(int amount, Action onSuccess = null, Action onFailed = null)
        {
            if (Instance.data.UnlimitTimeRemaining > 0)
            {
                onSuccess?.Invoke();
                return;
            }

            if (Instance.data.CurrentStamina >= amount)
            {
                Instance.data.CurrentStamina -= amount;

                //if is not recovering, start new recover progress
                if (Instance.data.TimeUntilNextRecover <= 0)
                {
                    Instance.data.RecoverStartTime = DateTime.Now.ToDetailedString();
                    Instance.data.TimeUntilNextRecover = Instance.config.RecoverTime;
                }

                Instance.SaveData();
                onSuccess?.Invoke();
            }
            else
            {
                onFailed?.Invoke();
            }
        }

        public static void ClaimStamina(int amount, Action onComplete = null)
        {
            Instance.data.CurrentStamina += amount;
            Instance.SaveData();
            onComplete?.Invoke();
        }

        public static void UnlimitStamina()
        {
            Instance.data.UnlimitStartTime = DateTime.Now.ToLongTimeString();
            Instance.data.UnlimitTimeRemaining = Instance.config.UnlimitTime;
            Instance.SaveData();
        }
        #endregion
    }
    [Serializable]
    public struct StaminaData
    {
        public int CurrentStamina;

        public string RecoverStartTime;
        public int TimeUntilNextRecover;

        public string UnlimitStartTime;
        public int UnlimitTimeRemaining;
    }
}