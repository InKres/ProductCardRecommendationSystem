using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;


[TestFixture]
public class ContentBasedEngineTests
{
    private MockProductRepository _repository;
    private ContentBasedEngine _engine;

    [SetUp]
    public void SetUp()
    {
        _repository = new MockProductRepository(TestProductFactory.CreateTestProducts());
        _engine = new ContentBasedEngine(_repository);
    }

    [Test]
    public void GetSimilar_ReturnsProductsExcludingCurrent()
    {
        IProductData current = _repository.GetProductById("p1");
        List<IProductData> result = _engine.GetSimilar(current, 10);

        Assert.IsFalse(result.Any(p => p.GetId() == current.GetId()));
    }

    [Test]
    public void GetSimilar_ReturnsRequestedCountOrLess()
    {
        IProductData current = _repository.GetProductById("p1");
        List<IProductData> result = _engine.GetSimilar(current, 3);

        Assert.IsTrue(result.Count <= 3);
        Assert.IsTrue(result.Count > 0);
    }

    [Test]
    public void GetSimilar_WithFewProducts_ReturnsLessThanCount()
    {
        var singleProduct = new List<IProductData> {
            TestProductFactory.CreateProduct("p1", "Only", "Cat", "Brand", 100, 4, 10)
        };
        var repo = new MockProductRepository(singleProduct);
        var engine = new ContentBasedEngine(repo);

        IProductData current = repo.GetProductById("p1");
        List<IProductData> result = engine.GetSimilar(current, 5);

        Assert.IsEmpty(result);
    }
}