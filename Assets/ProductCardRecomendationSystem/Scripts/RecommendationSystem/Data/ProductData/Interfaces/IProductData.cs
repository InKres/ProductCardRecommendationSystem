using System.Collections.Generic;
using UnityEngine;

namespace RecomendationSystem.Data
{
    public interface IProductData
    {
        string GetID();
        string GetName();
        string GetDescription();
        Sprite GetImage();
        float GetPrice();
        string GetCategoryID();
        int GetPopularity();
        int GetBuyersCount();
        IReadOnlyList<IFeatureData> GetFeatures();
        bool TryGetFeature(string key, out IFeatureData feature);
    }
}