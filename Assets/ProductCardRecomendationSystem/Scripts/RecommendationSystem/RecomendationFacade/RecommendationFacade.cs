using RecomendationSystem.Data;
using System.Collections.Generic;

namespace RecomendationSystem.Recommendation
{
    public class RecommendationFacade : IRecommendationFacade
    {
        private readonly IProductRepository productRepository;

        private readonly SimilarItemsEngine similarItemsEngine;
        private readonly PopularItemsEngine popularItemsEngine;

        public RecommendationFacade(IProductRepository productRepository, SimilarItemsEngine similarItemsEngine, PopularItemsEngine popularItemsEngine)
        {
            this.productRepository = productRepository;
            this.similarItemsEngine = similarItemsEngine;
            this.popularItemsEngine = popularItemsEngine;
        }

        public IReadOnlyList<IProductData> GetPopularProducts(int count)
        {
            IReadOnlyList<IProductData> products = productRepository.GetAllProducts();

            return popularItemsEngine.GetPopular(products, count);
        }

        public IReadOnlyList<IProductData> GetPopularProducts(string categoryId, int count)
        {
            IReadOnlyList<IProductData> products = productRepository.GetProductsByCategory(categoryId);

            return popularItemsEngine.GetPopular(products, count);
        }

        public IReadOnlyList<IProductData> GetSimilarProducts(IProductData product, int count)
        {
            IReadOnlyList<IProductData> products = productRepository.GetAllProducts();

            return similarItemsEngine.GetSimilar(product, products, count);
        }

        public IReadOnlyList<IProductData> GetSimilarProducts(IProductData product, string categoryId, int count)
        {
            IReadOnlyList<IProductData> products = productRepository.GetProductsByCategory(categoryId);

            return similarItemsEngine.GetSimilar(product, products, count);
        }

        public IReadOnlyList<IProductData> GetPopularSimilarProducts(IProductData product, int count)
        {
            return popularItemsEngine.GetPopular(GetSimilarProducts(product, count * 5), count);
        }

        public IReadOnlyList<IProductData> GetPopularSimilarProducts(IProductData product, string categoryId, int count)
        {
            return popularItemsEngine.GetPopular(GetSimilarProducts(product, categoryId, count * 5), count);
        }
    }
}