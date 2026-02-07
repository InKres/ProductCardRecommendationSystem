using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Product : IProductData
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
    private float price;

    public Product(string id, string name, string category)
    {
        this.id = id;
        this.name = name;
        this.category = category;
        this.tags = new string[0];
        this.brand = string.Empty;
        this.price = 0f;
    }

    public string GetId() => id;
    public string GetName() => name;
    public string GetCategory() => category;
    public IReadOnlyList<string> GetTags() => tags;
    public float GetPrice() => price;
    public string GetBrand() => brand;
}