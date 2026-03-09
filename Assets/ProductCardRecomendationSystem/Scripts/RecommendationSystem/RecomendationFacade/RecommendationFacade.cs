using RecomendationSystem.Data;
using System.Collections.Generic;

namespace RecomendationSystem.Recommendation
{
    public class RecommendationFacade
    {
        private readonly IProductRepository productRepository;
        private readonly SimilarItemsEngine similarItemsEngine;
        private readonly RankingEngine rankingEngine;

        public RecommendationFacade(IProductRepository productRepository, SimilarItemsEngine similarItemsEngine, RankingEngine rankingEngine)
        {
            this.productRepository = productRepository;
            this.similarItemsEngine = similarItemsEngine;
            this.rankingEngine = rankingEngine;
        }

        public IReadOnlyList<IProductData> GetSimilarProducts(IProductData product, int count)
        {
            IReadOnlyList<IProductData> allProducts = productRepository.GetAllProducts();

            return similarItemsEngine.FindSimilar(product, allProducts, count);
        }

        public IReadOnlyList<IProductData> GetPopularSimilarProducts(IProductData product, int count)
        {
            IReadOnlyList<IProductData> allProducts = productRepository.GetAllProducts();

            IReadOnlyList<IProductData> similar = similarItemsEngine.FindSimilar(product, allProducts, 50);

            return rankingEngine.SelectMostPopular(similar, count);
        }

        public IReadOnlyList<IProductData> GetRecommendedByCategory(string categoryId, int count)
        {
            IReadOnlyList<IProductData> categoryItems = productRepository.GetProductsByCategory(categoryId);

            return rankingEngine.SelectMostPopular(categoryItems, count);
        }
    }
}