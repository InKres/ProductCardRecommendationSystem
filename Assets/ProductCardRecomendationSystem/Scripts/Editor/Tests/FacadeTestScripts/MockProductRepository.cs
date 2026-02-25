using System.Collections.Generic;
using System.Linq;

public class MockProductRepository : IProductRepository
{
    private readonly List<IProductData> _products;

    public MockProductRepository(List<IProductData> products)
    {
        _products = products;
    }

    public List<IProductData> GetAllProducts()
    {
        return _products;
    }

    public IProductData GetProductById(string id)
    {
        return _products.FirstOrDefault(p => p.GetId() == id);
    }

    public List<IProductData> GetProductsByCategory(string categoryName, bool includeSubcategories)
    {
        // Для простоты фильтруем только по прямой категории
        return _products.Where(p => p.GetCategory() == categoryName).ToList();
    }
}