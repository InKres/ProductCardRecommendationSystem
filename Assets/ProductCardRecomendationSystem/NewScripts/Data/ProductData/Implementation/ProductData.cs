using System.Collections.Generic;
using UnityEngine;

namespace RecomendationSystem.Data
{
    [System.Serializable]
    public class ProductData : IProductData
    {
        [SerializeField]
        private string id;

        [SerializeField]
        private string name;

        [SerializeField]
        private float price;

        [SerializeField]
        private string categoryID;

        [SerializeField]
        private List<FeatureData> features = new List<FeatureData>();

        private Dictionary<string, FeatureData> keyToFeatureData;

        public ProductData(
            string id,
            string name,
            float price,
            string categoryID,
            List<FeatureData> features)
        {
            this.id = id;
            this.name = name;
            this.price = price;
            this.categoryID = categoryID;
            this.features = features;

            BuildLookup();
        }

        public string GetID()
        {
            return id;
        }

        public string GetName()
        {
            return name;
        }

        public float GetPrice()
        {
            return price;
        }

        public string GetCategoryID()
        {
            return categoryID;
        }

        public IReadOnlyList<IFeatureData> GetFeatures()
        {
            return features;
        }

        public bool TryGetFeature(string key, out IFeatureData feature)
        {
            if (keyToFeatureData == null)
            {
                BuildLookup();
            }

            FeatureData foundFeature;

            if (keyToFeatureData.TryGetValue(key, out foundFeature))
            {
                feature = foundFeature;
                return true;
            }

            feature = null;
            return false;
        }

        private void BuildLookup()
        {
            keyToFeatureData = new Dictionary<string, FeatureData>();

            foreach (FeatureData feature in features)
            {
                string key = feature.GetKey();

                if (!keyToFeatureData.ContainsKey(key))
                {
                    keyToFeatureData.Add(key, feature);
                }
            }
        }
    }
}