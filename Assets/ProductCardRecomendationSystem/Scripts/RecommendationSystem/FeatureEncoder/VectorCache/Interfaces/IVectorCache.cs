using RecomendationSystem.Data;
using System.Collections.Generic;

namespace RecomendationSystem.Encoding
{
    public interface IVectorCache
    {
        void Build(IReadOnlyList<IProductData> products);
        float[] GetVector(IProductData product);
        void Clear();
    }
}