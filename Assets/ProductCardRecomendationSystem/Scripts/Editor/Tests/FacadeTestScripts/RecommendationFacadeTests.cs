
using NUnit.Framework;
using System.Collections.Generic;

[TestFixture]
public class RecommendationFacadeTests
{
    private MockProductRepository _repository;
    private PopularityEngine _popularityEngine;
    private ContentBasedEngine _contentBasedEngine;
    private HybridEngine _hybridEngine;
    private RecommendationFacade _facade;

    [SetUp]
    public void SetUp()
    {
        _repository = new MockProductRepository(TestProductFactory.CreateTestProducts());
        _popularityEngine = new PopularityEngine(_repository);
        _contentBasedEngine = new ContentBasedEngine(_repository);
        _hybridEngine = new HybridEngine(_popularityEngine, _contentBasedEngine);
        _facade = new RecommendationFacade(_popularityEngine, _contentBasedEngine, _hybridEngine);
    }

    [Test]
    public void GetPopularProducts_ReturnsPopular()
    {
        List<IProductData> result = _facade.GetPopularProducts(2);
        Assert.AreEqual(2, result.Count);
        Assert.AreEqual("p4", result[0].GetId());
    }

    [Test]
    public void GetPopularProductsFromCategory_ReturnsCategoryPopular()
    {
        List<IProductData> result = _facade.GetPopularProductsFromCategory("Ёлектроника", 2);
        Assert.AreEqual(2, result.Count);
        Assert.AreEqual("p2", result[0].GetId());
    }

    [Test]
    public void GetSimilarProducts_ReturnsSimilar()
    {
        IProductData current = _repository.GetProductById("p1");
        List<IProductData> result = _facade.GetSimilarProducts(current, 2);
        Assert.AreEqual(2, result.Count);
    }

    [Test]
    public void GetHybridForProduct_ReturnsHybrid()
    {
        IProductData current = _repository.GetProductById("p1");
        List<IProductData> result = _facade.GetHybridForProduct(current, 2);
        Assert.AreEqual(2, result.Count);
    }
}