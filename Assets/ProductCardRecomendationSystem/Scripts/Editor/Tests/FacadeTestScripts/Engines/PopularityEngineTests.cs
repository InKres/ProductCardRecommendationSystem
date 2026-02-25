using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

public class PopularityEngineTests
{
    private MockProductRepository _repository;
    private PopularityEngine _engine;

    [SetUp]
    public void SetUp()
    {
        _repository = new MockProductRepository(TestProductFactory.CreateTestProducts());
        _engine = new PopularityEngine(_repository);
    }

    [Test]
    public void GetPopular_WithCount_ReturnsTopPopularProducts()
    {
        List<IProductData> result = _engine.GetPopular(3);

        Assert.AreEqual(3, result.Count);
        Assert.AreEqual("p4", result[0].GetId()); // 500 покупок
        Assert.AreEqual("p5", result[1].GetId()); // 400
        Assert.AreEqual("p2", result[2].GetId()); // 300
    }

    [Test]
    public void GetPopular_WithList_ReturnsPopularFromGivenList()
    {
        List<IProductData> electronics = _repository
            .GetAllProducts()
            .Where(p => p.GetCategory() == "Электроника")
            .ToList();

        List<IProductData> result = _engine.GetPopular(electronics, 2);

        Assert.AreEqual(2, result.Count);
        Assert.AreEqual("p2", result[0].GetId()); // 300
        Assert.AreEqual("p7", result[1].GetId()); // 200
    }

    [Test]
    public void GetPopular_WithCategory_ReturnsPopularFromCategory()
    {
        List<IProductData> result = _engine.GetPopular("Одежда", 2);

        Assert.AreEqual(2, result.Count);
        Assert.AreEqual("p4", result[0].GetId()); // 500
        Assert.AreEqual("p5", result[1].GetId()); // 400
    }

    [Test]
    public void GetPopular_WithCountGreaterThanAvailable_ReturnsAllAvailable()
    {
        int total = _repository.GetAllProducts().Count;
        List<IProductData> result = _engine.GetPopular(total + 10);

        Assert.AreEqual(total, result.Count);
    }

    [Test]
    public void GetPopular_WithEmptyList_ReturnsEmpty()
    {
        List<IProductData> result = _engine.GetPopular(new List<IProductData>(), 5);
        Assert.IsEmpty(result);
    }
}