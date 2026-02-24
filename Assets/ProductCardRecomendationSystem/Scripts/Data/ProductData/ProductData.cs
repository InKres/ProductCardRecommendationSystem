using System;
using UnityEngine;

[Serializable]
public class ProductData : IProductData
{
    [SerializeField]
    private string id;

    [Space]
    [SerializeField]
    private string name;
    [SerializeField]
    private string description;
    [SerializeField]
    private Sprite image;
    [SerializeField]
    private int purchasedQuantity;
    [SerializeField]
    private string category;
    [SerializeField]
    private string brand;
    [SerializeField]
    private float rating;
    [SerializeField]
    private float price;

    public ProductData(string id, string name, string description, Sprite image, int purchasedQuantity,
        string category, string brand, float rating, float price)
    {
        this.id = id;
        this.name = name;
        this.description = description;
        this.image = image;
        this.purchasedQuantity = purchasedQuantity;
        this.category = category;
        this.brand = brand;
        this.rating = rating;
        this.price = price;
    }

    public string GetId()
    {
        return id;
    }

    public string GetName()
    {
        return name;
    }

    public string GetDescription()
    {
        return description;
    }

    public Sprite GetImage()
    {
        return image;
    }

    public int GetPurchasedQuantity()
    {
        return purchasedQuantity;
    }

    public string GetCategory()
    {
        return category;
    }

    public float GetPrice()
    {
        return price;
    }

    public float GetRating()
    {
        return rating;
    }

    public string GetBrand()
    {
        return brand;
    }
}