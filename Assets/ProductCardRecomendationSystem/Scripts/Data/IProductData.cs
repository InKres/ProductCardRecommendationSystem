using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public interface IProductData
{
    string GetId();
    string GetName();
    string GetCategory();
    IReadOnlyList<string> GetTags();
    float GetPrice();
    string GetBrand();
}