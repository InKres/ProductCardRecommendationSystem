using System.Collections.Generic;

public interface ICategory
{
    string GetName();
    List<ICategory> GetCategories(bool isWithSubcategories);
    List<IProductData> GetProducts(bool isWithProductsFromSubcategories);
    ICategory GetSubcategoryByName(string categoryName);
    List<IProductData> GetProductsBySubcategoryName(string categoryName, bool isIncludeSubcategories);
}