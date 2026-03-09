using RecomendationSystem.Data;
using System.Collections.Generic;

namespace RecomendationSystem.Encoding
{
    public class VectorCache : IVectorCache
    {
        private readonly FeatureEncoder featureEncoder;
        private Dictionary<string, float[]> productIdToVector;

        public VectorCache(FeatureEncoder featureEncoder)
        {
            this.featureEncoder = featureEncoder;
            productIdToVector = new Dictionary<string, float[]>();
        }

        /// <summary>
        /// Предварительно кодирует все товары и сохраняет их векторы.
        /// </summary>
        public void Build(IReadOnlyList<IProductData> products)
        {
            productIdToVector.Clear();

            foreach (IProductData product in products)
            {
                float[] vector = featureEncoder.Encode(product);
                productIdToVector.Add(product.GetID(), vector);
            }
        }

        public float[] GetVector(IProductData product)
        {
            float[] vector;

            if (productIdToVector.TryGetValue(product.GetID(), out vector))
            {
                return vector;
            }

            vector = featureEncoder.Encode(product);
            productIdToVector.Add(product.GetID(), vector);

            return vector;
        }

        public void Clear()
        {
            productIdToVector.Clear();
        }
    }
}