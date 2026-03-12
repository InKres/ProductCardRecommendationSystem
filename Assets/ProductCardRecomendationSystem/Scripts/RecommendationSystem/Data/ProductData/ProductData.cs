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
        private int popularity;

        [SerializeField]
        private int buyersCount;

        [SerializeField]
        private string categoryID;

        [SerializeField]
        private List<FeatureData> features = new List<FeatureData>();

        private Dictionary<string, FeatureData> featureKeyToFeatureData;

        public ProductData(
            string id,
            string name,
            float price,
            string categoryID,
            int popularity,
            int buyersCount,
            List<FeatureData> features)
        {
            this.id = id;
            this.name = name;
            this.price = price;
            this.categoryID = categoryID;
            this.popularity = popularity;
            this.buyersCount = buyersCount;
            this.features = features;

            BuildFeatureKeyIndex();
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

        public int GetPopularity()
        {
            return popularity;
        }

        public int GetBuyersCount()
        {
            return buyersCount;
        }

        public IReadOnlyList<IFeatureData> GetFeatures()
        {
            return features;
        }

        public bool TryGetFeature(string key, out IFeatureData feature)
        {
            if (featureKeyToFeatureData == null)
            {
                BuildFeatureKeyIndex();
            }

            FeatureData foundFeature;

            if (featureKeyToFeatureData.TryGetValue(key, out foundFeature))
            {
                feature = foundFeature;
                return true;
            }

            feature = null;
            return false;
        }

        private void BuildFeatureKeyIndex()
        {
            featureKeyToFeatureData = new Dictionary<string, FeatureData>();

            foreach (FeatureData feature in features)
            {
                string key = feature.GetKey();

                if (!featureKeyToFeatureData.ContainsKey(key))
                {
                    featureKeyToFeatureData.Add(key, feature);
                }
            }
        }
    }
}