using System.Collections.Generic;
using RecomendationSystem.Data;

namespace RecomendationSystem.Recommendation
{
    public interface IProductRepository
    {
        void Init();

        IProductData GetProductByID(string productID);

        IReadOnlyList<IProductData> GetAllProducts();

        IReadOnlyList<IProductData> GetProductsByCategory(string categoryID);
    }
}