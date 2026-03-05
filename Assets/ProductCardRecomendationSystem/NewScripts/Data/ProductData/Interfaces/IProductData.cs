using System.Collections.Generic;

namespace RecomendationSystem.Data
{
    public interface IProductData
    {
        string GetID();
        string GetName();
        float GetPrice();

        string GetCategoryID();

        IReadOnlyList<IFeatureData> GetFeatures();

        bool TryGetFeature(string key, out IFeatureData feature);
    }
}