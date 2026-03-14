using RecomendationSystem.Data;
using RecomendationSystem.Encoding;
using System.Collections.Generic;
using System.Linq;

namespace RecomendationSystem.Recommendation
{
    public class SimilarItemsEngine
    {
        private readonly SimilarityCalculator similarityCalculator;
        private readonly IVectorCache vectorCache;

        public SimilarItemsEngine(SimilarityCalculator similarityCalculator, IVectorCache vectorCache)
        {
            this.similarityCalculator = similarityCalculator;
            this.vectorCache = vectorCache;
        }

        /// <summary>
        /// ¬озвращает товары, наиболее похожие на указанный.
        /// </summary>
        public IReadOnlyList<IProductData> GetSimilar(IProductData targetItem, IReadOnlyList<IProductData> candidates, int count)
        {
            float[] targetVector = vectorCache.GetVector(targetItem);

            List<(IProductData item, float score)> scored = CalculateScores(targetItem, targetVector, candidates);

            return SelectTop(scored, count);
        }

        private List<(IProductData, float)> CalculateScores(IProductData targetItem, float[] targetVector, IReadOnlyList<IProductData> candidates)
        {
            List<(IProductData, float)> result = new List<(IProductData, float)>();

            foreach (IProductData candidate in candidates)
            {
                if (candidate.GetID() == targetItem.GetID())
                {
                    continue;
                }

                float[] candidateVector = vectorCache.GetVector(candidate);

                float similarity = similarityCalculator.Calculate(targetVector, candidateVector);

                result.Add((candidate, similarity));
            }

            return result;
        }

        private IReadOnlyList<IProductData> SelectTop(List<(IProductData item, float score)> scored, int count)
        {
            return scored.OrderByDescending(x => x.score).Take(count).Select(x => x.item).ToList();
        }
    }
}