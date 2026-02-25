using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Движок, рекомендующий товары, похожие на заданный, на основе характеристик
/// </summary>
public class ContentBasedEngine
{
    private readonly IProductRepository productRepository;
    private float minPrice;
    private float maxPrice;
    private bool priceRangeInitialized;

    // Веса признаков (сумма = 1)
    private const float CategoryWeight = 0.5f;
    private const float BrandWeight = 0.3f;
    private const float PriseWeight = 0.2f;

    public ContentBasedEngine(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    /// <summary>
    /// Возвращает товары, похожие на указанный
    /// </summary>
    /// <param name="currentProduct">Исходный товар</param>
    /// <param name="count">Количество результатов</param>
    public List<IProductData> GetSimilar(IProductData currentProduct, int count)
    {
        List<IProductData> allProducts = productRepository.GetAllProducts();

        if (!priceRangeInitialized)
            InitializePriceRange(allProducts);

        List<(IProductData product, float score)> similarities = new List<(IProductData product, float score)>();

        foreach (IProductData temporaryProduct in allProducts)
        {
            if (temporaryProduct.GetId() == currentProduct.GetId()) continue; // исключаем сам товар

            float score = ComputeSimilarity(currentProduct, temporaryProduct);
            similarities.Add((temporaryProduct, score));
        }

        List<IProductData> result = similarities
            .OrderByDescending(x => x.score)
            .Take(count)
            .Select(x => x.product)
            .ToList();

        return result;
    }

    private float ComputeSimilarity(IProductData a, IProductData b)
    {
        float score = 0f;

        // Категория
        if (a.GetCategory() == b.GetCategory())
            score += CategoryWeight;

        // Бренд
        if (a.GetBrand() == b.GetBrand())
            score += BrandWeight;

        // Цена
        float priceRange = maxPrice - minPrice;
        if (priceRange > 0)
        {
            float priceDiff = Mathf.Abs(a.GetPrice() - b.GetPrice());
            float priceSimilarity = 1f - (priceDiff / priceRange);
            if (priceSimilarity < 0) priceSimilarity = 0;
            score += PriseWeight * priceSimilarity;
        }
        else
        {
            // Если все цены одинаковы, цена не влияет на различие
            score += PriseWeight;
        }

        return score;
    }

    private void InitializePriceRange(List<IProductData> products)
    {
        if (products.Count == 0)
        {
            minPrice = 0;
            maxPrice = 0;
        }
        else
        {
            minPrice = products.Min(product => product.GetPrice());
            maxPrice = products.Max(product => product.GetPrice());
        }

        priceRangeInitialized = true;
    }
}