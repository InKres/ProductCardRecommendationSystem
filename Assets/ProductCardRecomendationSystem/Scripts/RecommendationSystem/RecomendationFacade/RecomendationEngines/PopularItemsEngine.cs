using RecomendationSystem.Data;
using System.Collections.Generic;
using System.Linq;

namespace RecomendationSystem.Recommendation
{
    public class PopularItemsEngine
    {
        private const float PopularityWeight = 0.7f;
        private const float BuyersWeight = 0.3f;

        /// <summary>
        /// Возвращает наиболее популярные товары
        /// </summary>
        public IReadOnlyList<IProductData> GetPopular(IReadOnlyList<IProductData> candidates, int count)
        {
            if (candidates == null || candidates.Count == 0 || count <= 0)
            {
                return new List<IProductData>();
            }

            int maxBuyers = GetMaxBuyersCount(candidates);

            List<(IProductData item, float score)> scored = CalculateScores(candidates, maxBuyers);

            return SelectTop(scored, count);
        }

        private int GetMaxBuyersCount(IReadOnlyList<IProductData> candidates)
        {
            int max = 0;

            foreach (IProductData product in candidates)
            {
                int buyers = product.GetBuyersCount();

                if (buyers > max)
                {
                    max = buyers;
                }
            }

            return max;
        }

        private List<(IProductData, float)> CalculateScores(IReadOnlyList<IProductData> candidates, int maxBuyers)
        {
            List<(IProductData, float)> result = new List<(IProductData, float)>();

            foreach (IProductData product in candidates)
            {
                float normalizedPopularity = product.GetPopularity() / 100f;

                float normalizedBuyers = maxBuyers > 0
                    ? (float)product.GetBuyersCount() / maxBuyers
                    : 0f;

                float score =
                    normalizedPopularity * PopularityWeight +
                    normalizedBuyers * BuyersWeight;

                result.Add((product, score));
            }

            return result;
        }

        private IReadOnlyList<IProductData> SelectTop(List<(IProductData item, float score)> scored, int count)
        {
            return scored
                .OrderByDescending(x => x.score)
                .Take(count)
                .Select(x => x.item)
                .ToList();
        }
    }
}