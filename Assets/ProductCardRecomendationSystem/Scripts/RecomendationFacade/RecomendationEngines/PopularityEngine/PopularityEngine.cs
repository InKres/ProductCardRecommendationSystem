using System.Collections.Generic;
using System.Linq;

/// <summary>
/// ƒвижок, возвращающий самые попул€рные товары (по количеству покупок и рейтингу)
/// </summary>
public class PopularityEngine
{
    private readonly IProductRepository productRepository;

    public PopularityEngine(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    /// <summary>
    /// ¬озвращает топ-N попул€рных товаров из всей базы
    /// </summary>
    public List<IProductData> GetPopular(int count)
    {
        List<IProductData> allProducts = productRepository.GetAllProducts();

        List<IProductData> popular = GetPopularProducts(allProducts, count);

        return popular;
    }

    /// <summary>
    /// ¬озвращает топ-N попул€рных товаров из заданного списка
    /// </summary>
    public List<IProductData> GetPopular(List<IProductData> products, int count)
    {
        if (products == null || products.Count == 0)
            return new List<IProductData>();

        List<IProductData> popular = GetPopularProducts(products, count);

        return popular;
    }

    /// <summary>
    /// ¬озвращает топ-N попул€рных товаров из указанной категории
    /// </summary>
    public List<IProductData> GetPopular(string categoryName, int count, bool includeSubcategories = true)
    {
        List<IProductData> categoryProducts =
            productRepository.GetProductsByCategory(categoryName, includeSubcategories);

        if (categoryProducts == null || categoryProducts.Count == 0)
            return new List<IProductData>();

        List<IProductData> popular = GetPopularProducts(categoryProducts, count);

        return popular;
    }

    private List<IProductData> GetPopularProducts(List<IProductData> products, int count)
    {
        return products
            .OrderByDescending(product => product.GetPurchasedQuantity() * product.GetRating())
            .Take(count)
            .ToList();
    }
}