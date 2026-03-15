using RecomendationSystem.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace RecomendationSystem.DataGeneration
{
    public static class ProductGenerator
    {
        private static readonly string[] clothes =
        { "Футболка","Куртка","Толстовка","Джинсы","Шорты" };

        private static readonly string[] peripherals =
        { "Игровая мышь","Клавиатура","Гарнитура","Веб-камера" };

        private static readonly string[] pcComponents =
        { "Видеокарта","Процессор","Материнская плата","Блок питания" };

        private static readonly string[] games =
        { "Видеоигра","Коллекционное издание" };

        private static readonly string[] books =
        { "Книга","Роман","Фэнтези книга","Научная книга" };

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
                    categories[UnityEngine.Random.Range(0, categories.Count)];

                List<FeatureData> features =
                    FeatureGenerator.GenerateFeatures(category);

                ProductData product = new ProductData(
                    id: Guid.NewGuid().ToString(),
                    name: GenerateName(category, features),
                    description: "Описание товара",
                    image: null,
                    price: Round1(UnityEngine.Random.Range(500f, 150000f)),
                    categoryID: category.GetID(),
                    popularity: UnityEngine.Random.Range(0, 100),
                    buyersCount: UnityEngine.Random.Range(0, 5000),
                    features);

                result.Add(product);
            }

            return result;
        }

        private static string GenerateName(CategoryData category, List<FeatureData> features)
        {
            string baseName = "Товар";

            switch (category.GetName())
            {
                case "Одежда": baseName = RandomFrom(clothes); break;
                case "Компьютерная периферия": baseName = RandomFrom(peripherals); break;
                case "Комплектующие ПК": baseName = RandomFrom(pcComponents); break;
                case "Видеоигры": baseName = RandomFrom(games); break;
                case "Книги": return RandomFrom(books);
            }

            string brand = GetBrand(features);

            return string.IsNullOrEmpty(brand)
                ? baseName
                : $"{baseName} {brand}";
        }

        private static string GetBrand(List<FeatureData> features)
        {
            foreach (var feature in features)
                if (feature.GetKey() == "Бренд")
                    return feature.GetStringValue();

            return null;
        }

        private static string RandomFrom(string[] array)
        {
            return array[UnityEngine.Random.Range(0, array.Length)];
        }

        private static float Round1(float value)
        {
            return (float)Math.Round(value, 1);
        }
    }
}