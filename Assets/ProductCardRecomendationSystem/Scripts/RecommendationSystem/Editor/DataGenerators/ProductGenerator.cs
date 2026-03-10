using RecomendationSystem.Data;
using System.Collections.Generic;
using UnityEngine;

namespace RecomendationSystem.DataGeneration
{
    public static class ProductGenerator
    {
        public static List<ProductData> Generate(
            int count,
            CategoryDatabaseSO categoryDatabase)
        {
            List<ProductData> result = new List<ProductData>();

            IReadOnlyList<CategoryData> categories =
                categoryDatabase.GetAllCategories();

            for (int i = 0; i < count; i++)
            {
                CategoryData category =
                    categories[Random.Range(0, categories.Count)];

                List<FeatureData> features =
                    FeatureGenerator.GenerateFeatures(category);



                ProductData product = new ProductData(
                    id: System.Guid.NewGuid().ToString(),
                    name: GenerateName(category),
                    price: Random.Range(500f, 150000f),
                    categoryID: category.GetID(),
                    popularity: Random.Range(0, 100),
                    rating: GetRating(),
                    buyersCount: Random.Range(0, 5000),
                    features);

                result.Add(product);
            }

            return result;
        }

        private static string GenerateName(CategoryData category)
        {
            return category.GetName() + " " + Random.Range(1000, 9999);
        }

        private static float GetRating()
        {
            float rawValue = Random.Range(0f, 5f);
            return Mathf.Round(rawValue * 10f) / 10f;
        }
    }
}