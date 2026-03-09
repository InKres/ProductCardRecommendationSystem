using RecomendationSystem.Data;
using System.Collections.Generic;
using System.Linq;

namespace RecomendationSystem.Recommendation
{
    public class RankingEngine
    {
        /// <summary>
        /// —амые попул€рные товары.
        /// </summary>
        public IReadOnlyList<IProductData> SelectMostPopular(IReadOnlyList<IProductData> candidates, int count)
        {
            return candidates
                .OrderByDescending(p => p.GetPopularity())
                .Take(count)
                .ToList();
        }

        /// <summary>
        /// —ортировка по цене.
        /// </summary>
        public IReadOnlyList<IProductData> SortByPriceAscending(IReadOnlyList<IProductData> items)
        {
            return items
                .OrderBy(p => p.GetPrice())
                .ToList();
        }

        /// <summary>
        /// —амые покупаемые.
        /// </summary>
        public IReadOnlyList<IProductData> SelectMostPurchased(IReadOnlyList<IProductData> items, int count)
        {
            return items
                .OrderByDescending(p => p.GetBuyersCount())
                .Take(count)
                .ToList();
        }
    }
}