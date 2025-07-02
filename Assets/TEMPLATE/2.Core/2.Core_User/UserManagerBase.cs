using MyBox;
using System.Collections;
using UnityEngine;

namespace Monster.User
{
    public class UserManagerBase : MonoBehaviour, IYieldSaveLoad
    {
        #region USER DATA
        [SerializeField] protected UserData User;
        #endregion

        #region SAVE/LOAD
        public IEnumerator LoadData()
        {
            string json = PlayerPrefs.GetString(PlayerPrefsKey.USER_DATA, string.Empty);
            if (json.IsNullOrEmpty())
            {
                SetDefault();
                Save();
            }
            else
            {
                User = UserData.FromJson(json);
            }
            yield return null;
        }

        public virtual void Save()
        {
            Bug.Log($"Override {"Save".Colored(Color.red)} method please!");
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Set default data to new user, excecute in load process if not found any data
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public virtual void SetDefault()
        {
            Bug.Log($"Override {"SetDefaut".Colored(Color.red)} method please!");
            throw new System.NotImplementedException();
        }
        #endregion

        #region GET/SET
        protected int GetFieldInt(string fieldName, int defaultValue = 0)
        {
            return User.GetFieldInt(fieldName, defaultValue);
        }
        protected float GetFieldFloat(string fieldName, float defaultValue = 0f)
        {
            return User.GetFieldFloat(fieldName, defaultValue);
        }
        protected string GetFieldString(string fieldName, string defaultValue = "")
        {
            return User.GetFieldString(fieldName, defaultValue);
        }


        /// <summary>
        /// Add/minus new value to a field
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="changeAmount"></param>
        /// <returns></returns>
        protected int AddValueToField(string fieldName, int changeAmount)
        {
            return User.AddValueToField(fieldName,changeAmount);
        }

        protected float AddValueToField(string fieldName, float changeAmount)
        {
            return User.AddValueToField(fieldName, changeAmount);
        }
        #endregion
    }
}