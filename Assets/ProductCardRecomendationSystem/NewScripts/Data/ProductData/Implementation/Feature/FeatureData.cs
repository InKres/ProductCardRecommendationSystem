using UnityEngine;

namespace RecomendationSystem.Data
{
    [System.Serializable]
    public class FeatureData : IFeatureData
    {
        [SerializeField]
        private string key;

        [SerializeField]
        private FeatureValueType valueType;

        [SerializeField]
        private float floatValue;

        [SerializeField]
        private string stringValue;

        [SerializeField]
        private bool boolValue;

        public FeatureData(string key, float value)
        {
            this.key = key;
            valueType = FeatureValueType.Float;
            floatValue = value;
        }

        public FeatureData(string key, string value)
        {
            this.key = key;
            valueType = FeatureValueType.String;
            stringValue = value;
        }

        public FeatureData(string key, bool value)
        {
            this.key = key;
            valueType = FeatureValueType.Bool;
            boolValue = value;
        }

        public string GetKey()
        {
            return key;
        }

        public FeatureValueType GetValueType()
        {
            return valueType;
        }

        public float GetFloatValue()
        {
            return floatValue;
        }

        public string GetStringValue()
        {
            return stringValue;
        }

        public bool GetBoolValue()
        {
            return boolValue;
        }
    }
}