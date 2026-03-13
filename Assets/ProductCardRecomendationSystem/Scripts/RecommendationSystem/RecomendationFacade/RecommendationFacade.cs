using RecomendationSystem.Data;
using System.Collections.Generic;

namespace RecomendationSystem.Recommendation
{
    public class RecommendationFacade : IRecommendationFacade
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

        public IReadOnlyList<IProductData> GetPopularProducts(int count)
        {
            IReadOnlyList<IProductData> products = productRepository.GetAllProducts();

            return rankingEngine.GetPopularProducts(products, count);
        }

        public IReadOnlyList<IProductData> GetPopularProducts(string categoryId, int count)
        {
            IReadOnlyList<IProductData> products = productRepository.GetProductsByCategory(categoryId);

            return rankingEngine.GetPopularProducts(products, count);
        }

        public IReadOnlyList<IProductData> GetProductsSortedByAscendingPrice(int count)
        {
            IReadOnlyList<IProductData> products = productRepository.GetAllProducts();

            return rankingEngine.GetProductsSortedByAscendingPrice(products, count);
        }

        public IReadOnlyList<IProductData> GetProductsSortedByAscendingPrice(string categoryId, int count)
        {
            IReadOnlyList<IProductData> products = productRepository.GetProductsByCategory(categoryId);

            return rankingEngine.GetProductsSortedByAscendingPrice(products, count);
        }

        public IReadOnlyList<IProductData> GetProductsSortedByDescendingPrice(int count)
        {
            IReadOnlyList<IProductData> products = productRepository.GetAllProducts();

            return rankingEngine.GetProductsSortedByDescendingPrice(products, count);
        }

        public IReadOnlyList<IProductData> GetProductsSortedByDescendingPrice(string categoryId, int count)
        {
            IReadOnlyList<IProductData> products = productRepository.GetProductsByCategory(categoryId);

            return rankingEngine.GetProductsSortedByDescendingPrice(products, count);
        }

        public IReadOnlyList<IProductData> GetMostPurchasedProducts(int count)
        {
            IReadOnlyList<IProductData> products = productRepository.GetAllProducts();

            return rankingEngine.GetMostPurchasedProducts(products, count);
        }

        public IReadOnlyList<IProductData> GetMostPurchasedProducts(string categoryId, int count)
        {
            IReadOnlyList<IProductData> products = productRepository.GetProductsByCategory(categoryId);

            return rankingEngine.GetMostPurchasedProducts(products, count);
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
            return rankingEngine.GetPopularProducts(GetSimilarProducts(product, count * 5), count);
        }

        public IReadOnlyList<IProductData> GetPopularSimilarProducts(IProductData product, string categoryId, int count)
        {
            return rankingEngine.GetPopularProducts(GetSimilarProducts(product, categoryId, count * 5), count);
        }
    }
}