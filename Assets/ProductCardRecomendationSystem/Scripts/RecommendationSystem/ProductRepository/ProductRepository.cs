using System.Collections.Generic;
using RecomendationSystem.Data;

namespace RecomendationSystem.Recommendation
{
    public class ProductRepository : IProductRepository
    {
        private ProductDatabaseSO productDatabase;

        private Dictionary<string, IProductData> productIDToProduct;
        private Dictionary<string, List<IProductData>> categoryIDToProducts;

        public ProductRepository(ProductDatabaseSO productDatabase)
        {
            this.productDatabase = productDatabase;
        }

        public void Init()
        {
            BuildProductLookup();
            BuildCategoryIndex();
        }

        public IProductData GetProductByID(string productID)
        {
            IProductData product;

            if (productIDToProduct.TryGetValue(productID, out product))
            {
                return product;
            }

            return null;
        }

        public IReadOnlyList<IProductData> GetAllProducts()
        {
            return new List<IProductData>(productIDToProduct.Values);
        }

        public IReadOnlyList<IProductData> GetProductsByCategory(string categoryID)
        {
            List<IProductData> products;

            if (categoryIDToProducts.TryGetValue(categoryID, out products))
            {
                return products;
            }

            return new List<IProductData>();
        }

        private void BuildProductLookup()
        {
            productIDToProduct = new Dictionary<string, IProductData>();

            IReadOnlyList<ProductData> products = productDatabase.GetProducts();

            foreach (ProductData product in products)
            {
                string id = product.GetID();

                if (!productIDToProduct.ContainsKey(id))
                {
                    productIDToProduct.Add(id, product);
                }
            }
        }

        private void BuildCategoryIndex()
        {
            categoryIDToProducts = new Dictionary<string, List<IProductData>>();

            IReadOnlyList<ProductData> products = productDatabase.GetProducts();

            foreach (ProductData product in products)
            {
                string categoryID = product.GetCategoryID();

                if (!categoryIDToProducts.ContainsKey(categoryID))
                {
                    categoryIDToProducts.Add(categoryID, new List<IProductData>());
                }

                categoryIDToProducts[categoryID].Add(product);
            }
        }
    }
}