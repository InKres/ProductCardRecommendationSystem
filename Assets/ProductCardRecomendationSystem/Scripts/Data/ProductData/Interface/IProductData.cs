using UnityEngine;

public interface IProductData
{
    string GetId();
    string GetName();
    string GetDescription();
    Sprite GetImage();
    int GetPurchasedQuantity();
    string GetCategory();
    string GetBrand();
    float GetRating();
    float GetPrice();
}