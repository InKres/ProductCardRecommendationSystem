using System.Collections.Generic;

public interface IProductData
{
    string GetId();
    string GetName();
    string GetCategory();
    IReadOnlyList<string> GetTags();
    string GetBrand();
    float GetRating();
    float GetPrice();
}