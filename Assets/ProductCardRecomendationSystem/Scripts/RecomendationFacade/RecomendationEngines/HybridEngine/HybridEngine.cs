using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// √ибридный движок: берЄт похожие товары и сортирует их по попул€рности
/// </summary>
public class HybridEngine
{
    private readonly PopularityEngine popularityEngine;
    private readonly ContentBasedEngine contentBasedEngine;

    public HybridEngine(PopularityEngine popularityEngine, ContentBasedEngine contentBasedEngine)
    {
        this.popularityEngine = popularityEngine;
        this.contentBasedEngine = contentBasedEngine;
    }

    /// <summary>
    /// ¬озвращает рекомендации дл€ текущего товара:
    /// сначала получает много похожих товаров, затем выбирает из них самые попул€рные
    /// </summary>
    /// <param name="currentProduct">“екущий товар (не может быть null)</param>
    /// <param name="count"> оличество итоговых рекомендаций</param>
    public List<IProductData> GetHybridRecommendations(IProductData currentProduct, int count)
    {
        if (currentProduct == null)
        {
            Debug.Log($"HybridEngine: currentProduct не может быть null");
            return new List<IProductData>();
        }

        // «апрашиваем достаточно большое количество похожих товаров
        int extendedCount = count * 2;
        List<IProductData> similarProducts =
            contentBasedEngine.GetSimilar(currentProduct, extendedCount);

        // »з полученных похожих товаров выбираем самые попул€рные
        List<IProductData> result =
            popularityEngine.GetPopular(similarProducts, count);

        return result;
    }
}