using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Category : ICategory
{
    [SerializeField]
    private string name;
    [SerializeField]
    private List<Category> categories = new List<Category>();
    [SerializeField]
    private List<ProductData> products = new List<ProductData>();

    private List<ICategory> iCategories = new List<ICategory>();
    private List<IProductData> iProducts = new List<IProductData>();

    public Category(string name, List<Category> categories, List<ProductData> products)
    {
        this.name = name;
        this.categories = categories;
        this.products = products;
    }

    public string GetName()
    {
        return name;
    }

    public ICategory GetSubcategoryByName(string categoryName)
    {
        foreach (Category subCategory in categories)
        {
            if (subCategory.GetName() == categoryName)
            {
                return subCategory;
            }
        }

        ICategory iCategory = null;
        foreach (Category category in categories)
        {
            iCategory = category.GetSubcategoryByName(categoryName);

            if (iCategory != null)
                return iCategory;
        }

        return null;
    }

    public List<IProductData> GetProductsBySubcategoryName(string categoryName, bool isIncludeSubcategories)
    {
        ICategory category = GetSubcategoryByName(categoryName);

        if (category != null)
        {
            return category.GetProducts(isIncludeSubcategories);
        }

        return new List<IProductData>();
    }

    public List<ICategory> GetCategories(bool isIncludeSubcategories)
    {
        if (iCategories == null)
        {
            iCategories = new List<ICategory>();
        }

        SyncCache(categories, iCategories);

        if (isIncludeSubcategories)
        {
            foreach (ICategory category in categories)
            {
                iCategories.AddRange(category.GetCategories(isIncludeSubcategories));
            }
        }

        return iCategories;
    }

    public List<IProductData> GetProducts(bool isIncludeSubcategories)
    {
        if (iProducts == null)
        {
            iProducts = new List<IProductData>();
        }

        SyncCache(products, iProducts);

        if (isIncludeSubcategories)
        {
            foreach (Category category in categories)
            {
                iProducts.AddRange(category.GetProducts(isIncludeSubcategories));
            }
        }

        return iProducts;
    }

    private void SyncCache<T, I>(List<T> source, List<I> cache) where T : I
    {
        if (source.Count == 0)
        {
            cache.Clear();
            return;
        }

        if (cache.Count == source.Count)
            return;

        cache.Clear();
        foreach (T item in source)
        {
            cache.Add(item);
        }
    }
}