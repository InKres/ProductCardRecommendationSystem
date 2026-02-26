using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//Ѕыл написан первым
//Ёто что-то типо адаптера между базой данных и фасадом рекомендаций.
public class ProductRepository : IProductRepository
{
    private DataBaseSO dataBase;
    private List<IProductData> allProductsCache;

    public ProductRepository()
    {
        dataBase = Resources.Load<DataBaseSO>("Data/DataBaseSO");
        if (dataBase == null)
            Debug.LogError("DataBaseSO не найден по пути 'Data/DataBaseSO'.");
    }

    public List<IProductData> GetAllProducts()
    {
        if (allProductsCache != null)
            return allProductsCache;

        if (dataBase?.Category == null)
            return new List<IProductData>();

        allProductsCache = dataBase.Category.GetProducts(true);
        return allProductsCache;
    }

    public IProductData GetProductById(string id)
    {
        return GetAllProducts().FirstOrDefault(product => product.GetId() == id);
    }

    public List<IProductData> GetProductsByCategory(string categoryName, bool includeSubcategories = true)
    {
        if (dataBase.Category == null)
            return new List<IProductData>();

        ICategory category = null;
        if (dataBase.Category.GetName() == categoryName)
        {
            category = dataBase.Category;
        }
        else
        {
            category = dataBase.Category.GetSubcategoryByName(categoryName);
        }

        if (category == null)
        {
            Debug.LogWarning($" атегори€ '{categoryName}' не найдена.");
            return new List<IProductData>();
        }

        return category.GetProducts(includeSubcategories);
    }
}