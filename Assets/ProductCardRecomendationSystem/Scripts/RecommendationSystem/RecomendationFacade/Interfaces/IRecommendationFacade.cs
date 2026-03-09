using RecomendationSystem.Data;
using System.Collections.Generic;

namespace RecomendationSystem.Recommendation
{
    public interface IRecommendationFacade
    {
        IReadOnlyList<IProductData> GetSimilarItems(string productID, int count);

        IReadOnlyList<IProductData> GetPopularSimilarItems(string productID, int count);

        IReadOnlyList<IProductData> GetRecommendedItemsGlobal(int count);

        IReadOnlyList<IProductData> GetRecommendedItemsByCategory(string categoryID, int count);
    }
}