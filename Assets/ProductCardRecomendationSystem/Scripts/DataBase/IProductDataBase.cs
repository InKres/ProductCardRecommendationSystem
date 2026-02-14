using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface IProductDataBase
{
    IProductData GetProductById(string id);
    IReadOnlyList<IProductData> GetAllProducts();
    IReadOnlyList<IProductData> GetProductsByCategory(string category);
    IReadOnlyList<IProductData> GetProductsByTag(string tag);
}

public class ProductDataBase : MonoBehaviour, IProductDataBase
{
    private List<IProductData> allProducts;
    private Dictionary<string, IProductData> idToProduct;
    private Dictionary<string, List<IProductData>> categoryToProducts;
    private Dictionary<string, List<IProductData>> tagToProducts;

    private bool isInit = false;

    public void Init()
    {
        if (isInit) return;

        LoadProducts();
        CacheData();

        isInit = true;
    }

    public IProductData GetProductById(string id)
    {
        if (!isInit) return null;

        return idToProduct.TryGetValue(id, out IProductData product) ? product : null;
    }

    public IReadOnlyList<IProductData> GetAllProducts()
    {
        if (!isInit) return new List<IProductData>().AsReadOnly();

        return allProducts.AsReadOnly();
    }

    public IReadOnlyList<IProductData> GetProductsByCategory(string category)
    {
        if (!isInit) return new List<IProductData>().AsReadOnly();

        return categoryToProducts.TryGetValue(category, out List<IProductData> products)
            ? products.AsReadOnly()
            : new List<IProductData>().AsReadOnly();
    }

    public IReadOnlyList<IProductData> GetProductsByTag(string tag)
    {
        if (!isInit) return new List<IProductData>().AsReadOnly();

        return tagToProducts.TryGetValue(tag, out List<IProductData> products)
            ? products.AsReadOnly()
            : new List<IProductData>().AsReadOnly();
    }

    private void LoadProducts()
    {
        ProductDataSO[] productSOs = Resources.LoadAll<ProductDataSO>("Data");
        
        allProducts = new List<IProductData>();
        foreach (ProductDataSO so in productSOs)
        {
            allProducts.Add(so.ProductData);
        }
    }

    private void CacheData()
    {
        CacheDataByID();
        CacheDataByCategory();
        CacheDataByTag();
    }

    private void CacheDataByID()
    {
        idToProduct = allProducts.ToDictionary(product => product.GetId(), product => product);
    }

    private void CacheDataByCategory()
    {
        categoryToProducts = new Dictionary<string, List<IProductData>>();

        foreach (IProductData product in allProducts)
        {
            string category = product.GetCategory();
            AddToIndex(categoryToProducts, category, product);
        }
    }

    private void CacheDataByTag()
    {
        tagToProducts = new Dictionary<string, List<IProductData>>();

        foreach (IProductData product in allProducts)
        {
            foreach (string tag in product.GetTags())
            {
                AddToIndex(tagToProducts, tag, product);
            }
        }
    }

    private void AddToIndex(Dictionary<string, List<IProductData>> index, string key, IProductData product)
    {
        if (!index.ContainsKey(key))
        {
            index[key] = new List<IProductData>();
        }

        index[key].Add(product);
    }
}