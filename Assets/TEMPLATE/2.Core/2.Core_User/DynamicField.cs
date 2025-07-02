using MyBox;
using UnityEngine;
namespace Monster.User
{
    [System.Serializable]
    public class DynamicField
    {
        public string Name;

        public FieldType FieldType;

        [SerializeField,ConditionalField(nameof(FieldType),false,FieldType.String)]
        private string stringValue;

        [SerializeField, ConditionalField(nameof(FieldType), false, FieldType.Int)]
        private int intValue;

        [SerializeField, ConditionalField(nameof(FieldType), false, FieldType.Float)]
        private float floatValue;

        public object Value
        {
            get
            {
                return FieldType switch
                {
                    FieldType.String => stringValue,
                    FieldType.Int => intValue,
                    FieldType.Float => floatValue,
                    _ => null
                };
            }
            set
            {
                switch (value)
                {
                    case string str:
                        FieldType = FieldType.String;
                        stringValue = str;
                        break;
                    case int intVal:
                        FieldType = FieldType.Int;
                        intValue = intVal;
                        break;
                    case float floatVal:
                        FieldType = FieldType.Float;
                        floatValue = floatVal;
                        break;
                    default:
                        FieldType = FieldType.String;
                        stringValue = value.ToString();
                        break;
                }
            }
        }

        public DynamicField(string name, object value)
        {
            Name = name;
            Value = value;
        }
    }
    public enum FieldType
    {
        String = 0,
        Int = 1,
        Float = 2
    }
}
