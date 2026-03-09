using UnityEngine;

namespace RecomendationSystem.Recommendation
{
    public class SimilarityCalculator
    {
        /// <summary>
        /// ¬ычисл€ет cosine similarity между двум€ векторами признаков.
        /// </summary>
        public float Calculate(float[] firstVector, float[] secondVector)
        {
            float dotProduct = 0f;
            float firstVectorLengthSquared = 0f;
            float secondVectorLengthSquared = 0f;

            for (int i = 0; i < firstVector.Length; i++)
            {
                dotProduct += firstVector[i] * secondVector[i];

                firstVectorLengthSquared += firstVector[i] * firstVector[i];
                secondVectorLengthSquared += secondVector[i] * secondVector[i];
            }

            if (firstVectorLengthSquared == 0f || secondVectorLengthSquared == 0f)
                return 0f;

            float firstVectorLength = Mathf.Sqrt(firstVectorLengthSquared);
            float secondVectorLength = Mathf.Sqrt(secondVectorLengthSquared);

            return dotProduct / (firstVectorLength * secondVectorLength);
        }
    }
}