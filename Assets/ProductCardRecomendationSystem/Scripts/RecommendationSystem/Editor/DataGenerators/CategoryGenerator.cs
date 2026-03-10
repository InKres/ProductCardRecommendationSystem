using RecomendationSystem.Data;
using System;
using System.Collections.Generic;

namespace RecomendationSystem.DataGeneration
{
    public static class CategoryGenerator
    {
        public static void FillDatabase(CategoryDatabaseSO database)
        {
            List<CategoryData> categories = new List<CategoryData>();

            categories.Add(new CategoryData(Guid.NewGuid().ToString(), "Одежда", null));
            categories.Add(new CategoryData(Guid.NewGuid().ToString(), "Компьютерная периферия", null));
            categories.Add(new CategoryData(Guid.NewGuid().ToString(), "Комплектующие ПК", null));
            categories.Add(new CategoryData(Guid.NewGuid().ToString(), "Видеоигры", null));
            categories.Add(new CategoryData(Guid.NewGuid().ToString(), "Книги", null));

            database.SetCategories(categories);
        }
    }
}