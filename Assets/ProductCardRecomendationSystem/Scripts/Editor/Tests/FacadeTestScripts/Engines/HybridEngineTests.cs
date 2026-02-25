using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[TestFixture]
public class HybridEngineTests
{
    private MockProductRepository _repository;
    private PopularityEngine _popularityEngine;
    private ContentBasedEngine _contentBasedEngine;
    private HybridEngine _hybridEngine;

    [SetUp]
    public void SetUp()
    {
        _repository = new MockProductRepository(TestProductFactory.CreateTestProducts());
        _popularityEngine = new PopularityEngine(_repository);
        _contentBasedEngine = new ContentBasedEngine(_repository);
        _hybridEngine = new HybridEngine(_popularityEngine, _contentBasedEngine);
    }

    [Test]
    public void GetHybridRecommendations_ReturnsPopularAmongSimilar()
    {
        IProductData current = _repository.GetProductById("p1"); // Ќоутбук
        List<IProductData> result = _hybridEngine.GetHybridRecommendations(current, 2);

        // ќжидаем, что среди похожих на ноутбук товаров (из категории "Ёлектроника")
        // самыми попул€рными будут p2 (300 покупок) и p7 (200 покупок)
        List<string> actualIds = result.Select(p => p.GetId()).ToList();

        Assert.AreEqual(2, actualIds.Count);
        Assert.Contains("p2", actualIds);
        Assert.Contains("p7", actualIds);
    }

    [Test]
    public void GetHybridRecommendations_WithNullCurrent_ReturnsEmpty()
    {
        List<IProductData> result = _hybridEngine.GetHybridRecommendations(null, 5);

        Assert.IsEmpty(result);
    }


    [Test]
    public void GetHybridRecommendations_WhenSimilarLessThanCount_ReturnsAllPopular()
    {
        // —оздаЄм ситуацию, где всего 2 похожих товара
        var products = new List<IProductData>
        {
            TestProductFactory.CreateProduct("p1", "Base", "Cat", "BrandA", 100, 4, 10),
            TestProductFactory.CreateProduct("p2", "Similar1", "Cat", "BrandA", 90, 4.5f, 100),
            TestProductFactory.CreateProduct("p3", "Similar2", "Cat", "BrandA", 110, 4.2f, 50)
        };
        var repo = new MockProductRepository(products);
        var pop = new PopularityEngine(repo);
        var content = new ContentBasedEngine(repo);
        var hybrid = new HybridEngine(pop, content);

        IProductData current = repo.GetProductById("p1");
        List<IProductData> result = hybrid.GetHybridRecommendations(current, 5);

        // ƒолжно вернутьс€ 2 товара (все похожие, отсортированные по попул€рности)
        Assert.AreEqual(2, result.Count);
        Assert.AreEqual("p2", result[0].GetId()); // 100 покупок
        Assert.AreEqual("p3", result[1].GetId()); // 50 покупок
    }
}