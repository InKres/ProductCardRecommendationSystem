using System.Collections.Generic;

public interface IProductRepository
{
    /// <summary> ¬озвращает все товары из всех категорий </summary>
    List<IProductData> GetAllProducts();

    /// <summary> Ќаходит товар по уникальному ID </summary>
    IProductData GetProductById(string id);

    /// <summary> ¬озвращает товары из указанной категории (с подкатегори€ми или без) </summary>
    List<IProductData> GetProductsByCategory(string categoryName, bool includeSubcategories = true);
}