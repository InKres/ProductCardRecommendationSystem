using System.Collections.Generic;

/// <summary>
/// Фасад, предоставляющий единую точку доступа ко всем рекомендациям
/// </summary>
public class RecommendationFacade
{
    private readonly PopularityEngine popularityEngine;
    private readonly ContentBasedEngine contentBasedEngine;
    private readonly HybridEngine hybridEngine;

    public RecommendationFacade(PopularityEngine popularityEngine, 
        ContentBasedEngine contentBasedEngine, HybridEngine hybridEngine)
    {
        this.popularityEngine = popularityEngine;
        this.contentBasedEngine = contentBasedEngine;
        this.hybridEngine = hybridEngine;
    }

    /// <summary>
    /// Получить популярные товары со всей базы
    /// </summary>
    public List<IProductData> GetPopularProducts(int count)
    {
        return popularityEngine.GetPopular(count);
    }

    /// <summary>
    /// Получить популярные товары из заданного списка
    /// </summary>
    public List<IProductData> GetPopularProducts(List<IProductData> products, int count)
    {
        return popularityEngine.GetPopular(products, count);
    }

    /// <summary>
    /// Получить популярные товары из указанной категории
    /// </summary>
    public List<IProductData> GetPopularProductsFromCategory(string categoryName, int count)
    {
        return popularityEngine.GetPopular(categoryName, count);
    }

    /// <summary>
    /// Получить товары, похожие на указанный (контентный метод)
    /// </summary>
    public List<IProductData> GetSimilarProducts(IProductData product, int count)
    {
        return contentBasedEngine.GetSimilar(product, count);
    }

    /// <summary>
    /// Получить гибридные рекомендации для товара:
    /// сначала ищутся похожие, затем из них выбираются самые популярные
    /// </summary>
    public List<IProductData> GetHybridForProduct(IProductData product, int count)
    {
        return hybridEngine.GetHybridRecommendations(product, count);
    }
}