using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProductData : IProductData
{
    [SerializeField]
    private string id;
    [SerializeField]
    private string name;
    [SerializeField]
    private string category;
    [SerializeField]
    private string[] tags;
    [SerializeField]
    private string brand;
    [SerializeField]
    private float rating;
    [SerializeField]
    private float price;

    public ProductData(string id, string name, string category, string[] tags, string brand, float rating, float price)
    {
        this.id = id;
        this.name = name;
        this.category = category;
        this.tags = tags;
        this.brand = brand;
        this.rating = rating;
        this.price = price;
    }

    public string GetId() => id;
    public string GetName() => name;
    public string GetCategory() => category;
    public IReadOnlyList<string> GetTags() => tags;
    public float GetPrice() => price;
    public float GetRating() => rating;
    public string GetBrand() => brand;
}