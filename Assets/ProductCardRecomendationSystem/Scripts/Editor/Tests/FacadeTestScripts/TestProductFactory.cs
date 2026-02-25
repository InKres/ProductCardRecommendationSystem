using System.Collections.Generic;

public static class TestProductFactory
{
    public static List<IProductData> CreateTestProducts()
    {
        var products = new List<IProductData>();

        // Электроника
        products.Add(CreateProduct("p1", "Ноутбук", "Электроника", "BrandA", 1000, 4.5f, 150));
        products.Add(CreateProduct("p2", "Смартфон", "Электроника", "BrandB", 800, 4.7f, 300));
        products.Add(CreateProduct("p3", "Планшет", "Электроника", "BrandA", 600, 4.2f, 100));
        products.Add(CreateProduct("p7", "Наушники", "Электроника", "BrandB", 150, 4.8f, 200));

        // Одежда
        products.Add(CreateProduct("p4", "Футболка", "Одежда", "BrandC", 20, 4.0f, 500));
        products.Add(CreateProduct("p5", "Джинсы", "Одежда", "BrandC", 50, 4.3f, 400));
        products.Add(CreateProduct("p6", "Куртка", "Одежда", "BrandD", 120, 4.6f, 80));

        return products;
    }

    public static IProductData CreateProduct(string id, string name, string category, string brand, float price, float rating, int purchasedQuantity)
    {
        return new ProductData(
            id: id,
            name: name,
            description: "Описание",
            image: null,
            purchasedQuantity: purchasedQuantity,
            category: category,
            brand: brand,
            rating: rating,
            price: price
        );
    }
}