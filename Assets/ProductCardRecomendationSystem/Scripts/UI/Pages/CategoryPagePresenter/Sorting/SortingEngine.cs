using RecomendationSystem.Data;
using System.Collections.Generic;
using System.Linq;

public class SortingEngine
{
    /// <summary>
    /// ѕолучить самые попул€рные товары.
    /// </summary>
    public IReadOnlyList<IProductData> GetPopularProducts(IReadOnlyList<IProductData> products)
    {
        return products
            .OrderByDescending(product => product.GetPopularity())
            .ToList();
    }

    /// <summary>
    /// ѕолучить товары отсортированные по возрастанию цены.
    /// </summary>
    public IReadOnlyList<IProductData> GetProductsSortedByAscendingPrice(IReadOnlyList<IProductData> products)
    {
        return products
            .OrderBy(product => product.GetPrice())
            .ToList();
    }

    /// <summary>
    /// ѕолучить товары отсортированные по убыванию цены.
    /// </summary>
    public IReadOnlyList<IProductData> GetProductsSortedByDescendingPrice(IReadOnlyList<IProductData> products)
    {
        return products
            .OrderByDescending(product => product.GetPrice())
            .ToList();
    }

    /// <summary>
    /// ѕолучить товары с самым большим количеством покупателей.
    /// </summary>
    public IReadOnlyList<IProductData> GetMostPurchasedProducts(IReadOnlyList<IProductData> products)
    {
        return products
            .OrderByDescending(product => product.GetBuyersCount())
            .ToList();
    }
}