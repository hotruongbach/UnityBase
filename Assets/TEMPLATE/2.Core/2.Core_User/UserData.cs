using MyBox;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;
namespace Template.User
{
    [System.Serializable]
    public class UserData
    {
        [SerializeField]
        private List<DynamicField> data = new List<DynamicField>();

        /// <summary>
        /// Add or replace field value to newValue. Field type is define base on type of newValue.
        /// Can be use to change type of field
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="newValue"></param>
        public void AddOrUpdateField(string fieldName, object newValue)
        {
            var existingField = data.Find(f => f.Name == fieldName);
            if (existingField != null)
            {
                existingField.Value = newValue;
            }
            else
            {
                data.Add(new DynamicField(fieldName, newValue));
            }
        }

        /// <summary>
        /// Increase field int and return new value, support int
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="changeAmount"></param>
        public int AddValueToField(string fieldName, int changeAmount)
        {
            var existingField = data.Find(f => f.Name == fieldName);
            if (existingField == null)
            {
                Bug.Log($"Field {fieldName.Colored(Color.red)} missing, added new field with input");
                data.Add(new DynamicField(fieldName, changeAmount));
                return changeAmount;
            }

            switch (existingField.FieldType)
            {
                case FieldType.String:
                    Bug.Log("String type can not be calculated!", "red");
                    return 0;
                case FieldType.Int:
                    existingField.Value = (int)existingField.Value + changeAmount;
                    break;
                case FieldType.Float:
                    existingField.Value = (float)existingField.Value + changeAmount;
                    break;
                default:
                    break;
            }

            return (int)existingField.Value;
        }

        /// <summary>
        /// Increase field value, support float
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="changeAmount"></param>
        public float AddValueToField(string fieldName, float changeAmount)
        {
            var existingField = data.Find(f => f.Name == fieldName);
            if (existingField == null)
            {
                Bug.Log($"Field {fieldName.Colored(Color.red)} missing, added new field with input");
                data.Add(new DynamicField(fieldName, changeAmount));
                return changeAmount;
            }

            switch (existingField.FieldType)
            {
                case FieldType.String:
                    Bug.Log("String type can not be calculated!", "red");
                    return 0;
                case FieldType.Int:
                    Bug.Log("Int type can not be calculated with float!", "red");
                    return 0;
                case FieldType.Float:
                    existingField.Value = (float)existingField.Value + changeAmount;
                    break;
                default:
                    break;
            }
            return (float)existingField.Value;
        }

        /// <summary>
        /// Find or create a field int
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public int GetFieldInt(string fieldName,int defaultValue = 0)
        {
            var field = data.Find(f => f.Name == fieldName);

            //if field not exist, create new field
            if(field == null)
            {
                AddOrUpdateField(fieldName, defaultValue);
                return defaultValue;
            }

            //found that field, but has wrong type of data
            if(field.FieldType != FieldType.Int)
            {
                Bug.Log($"Field {fieldName.Colored(Color.red)} containing other type of data");
                return -9999;
            }

            return (int)field.Value;
        }

        /// <summary>
        /// Find or create a field float
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public float GetFieldFloat(string fieldName, float defaultValue = 0f)
        {
            var field = data.Find(f => f.Name == fieldName);

            //if field not exist, create new field
            if (field == null)
            {
                AddOrUpdateField(fieldName, defaultValue);
                return defaultValue;
            }

            //found that field, but has wrong type of data
            if (field.FieldType != FieldType.Float)
            {
                Bug.Log($"Field {fieldName.Colored(Color.red)} containing other type of data");
                return -9999f;
            }

            return (float)field.Value;
        }
        /// <summary>
        /// Find or create a field string
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public string GetFieldString(string fieldName, string defaultValue = "")
        {
            var field = data.Find(f => f.Name == fieldName);

            //if field not exist, create new field
            if (field == null)
            {
                AddOrUpdateField(fieldName, defaultValue);
                return defaultValue;
            }

            //found that field, but has wrong type of data
            if (field.FieldType != FieldType.String)
            {
                Bug.Log($"Field {fieldName.Colored(Color.red)} containing other type of data");
                return "";
            }

            return (string)field.Value;
        }

        // Serialize the data to JSON using JsonConvert
        public string ToJson()
        {
            var dictionary = new Dictionary<string, object>();
            foreach (var field in data)
            {
                dictionary.Add(field.Name, new
                {
                    field.Value,
                    field.FieldType
                });
            }
            return JsonConvert.SerializeObject(dictionary, Formatting.None);
        }

        // Deserialize from JSON using JsonConvert
        public static UserData FromJson(string json)
        {
            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
            var loadedData = new UserData();

            foreach (var kvp in dictionary)
            {
                var valueObj = kvp.Value as JObject;

                //check null value
                if (valueObj == null) continue;

                string typeToken = valueObj["FieldType"]?.ToString();
                FieldType type = TypeTokenMapping(typeToken);
                object value = valueObj["Value"];

                //import data to user
                switch (type)
                {
                    case FieldType.Int:
                        loadedData.AddOrUpdateField(kvp.Key, Convert.ToInt32(value));
                        break;
                    case FieldType.Float:
                        loadedData.AddOrUpdateField(kvp.Key, Convert.ToSingle(value));
                        break;
                    case FieldType.String:
                        loadedData.AddOrUpdateField(kvp.Key, value.ToString());
                        break;
                    default:
                        loadedData.AddOrUpdateField(kvp.Key, value?.ToString() ?? "");
                        break;
                }
            }
            return loadedData;
        }
        private static FieldType TypeTokenMapping(string token)
        {
            return token switch
            {
                "0" => FieldType.String,
                "1" => FieldType.Int,
                "2" => FieldType.Float,
                _ => throw new NotImplementedException(),
            };
        }
    }
}
