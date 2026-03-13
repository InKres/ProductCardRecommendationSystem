using RecomendationSystem.Data;
using System.Collections.Generic;
using System.Linq;

namespace RecomendationSystem.Recommendation
{
    public class RankingEngine
    {
        /// <summary>
        /// ѕолучить самые попул€рные товары.
        /// </summary>
        public IReadOnlyList<IProductData> GetPopularProducts(IReadOnlyList<IProductData> products, int count)
        {
            return products
                .OrderByDescending(product => product.GetPopularity())
                .Take(count)
                .ToList();
        }

        /// <summary>
        /// ѕолучить товары отсортированные по возрастанию цены.
        /// </summary>
        public IReadOnlyList<IProductData> GetProductsSortedByAscendingPrice(IReadOnlyList<IProductData> products, int count)
        {
            return products
                .OrderBy(product => product.GetPrice())
                .Take(count)
                .ToList();
        }

        /// <summary>
        /// ѕолучить товары отсортированные по убыванию цены.
        /// </summary>
        public IReadOnlyList<IProductData> GetProductsSortedByDescendingPrice(IReadOnlyList<IProductData> products, int count)
        {
            return products
                .OrderByDescending(product => product.GetPrice())
                .Take(count)
                .ToList();
        }

        /// <summary>
        /// ѕолучить товары с самым большим количеством покупателей.
        /// </summary>
        public IReadOnlyList<IProductData> GetMostPurchasedProducts(IReadOnlyList<IProductData> products, int count)
        {
            return products
                .OrderByDescending(product => product.GetBuyersCount())
                .Take(count)
                .ToList();
        }
    }
}