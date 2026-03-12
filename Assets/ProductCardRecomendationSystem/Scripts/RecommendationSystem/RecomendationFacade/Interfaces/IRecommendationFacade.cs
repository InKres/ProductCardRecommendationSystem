using RecomendationSystem.Data;
using System.Collections.Generic;

namespace RecomendationSystem.Recommendation
{
    public interface IRecommendationFacade
    {
        IReadOnlyList<IProductData> GetSimilarProducts(IProductData product, int count);

        IReadOnlyList<IProductData> GetPopularSimilarProducts(IProductData product, int count);

        IReadOnlyList<IProductData> GetRecommendedByCategory(string categoryId, int count);
    }
}