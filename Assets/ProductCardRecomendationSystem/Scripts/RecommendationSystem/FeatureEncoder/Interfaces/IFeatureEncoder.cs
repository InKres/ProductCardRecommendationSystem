using RecomendationSystem.Data;
using System.Collections.Generic;

namespace RecomendationSystem.Encoding
{
    public interface IFeatureEncoder
    {
        void Fit(IReadOnlyList<IProductData> products);

        float[] Encode(IProductData product);

        int GetVectorSize();
    }
}