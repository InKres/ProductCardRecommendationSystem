using RecomendationSystem.Data;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace RecomendationSystem.DataGeneration
{
    public static class FeatureGenerator
    {
        private static readonly string[] colors =
        {
        "красный",
        "синий",
        "черный",
        "белый",
        "зеленый",
        "желтый",
        "серый"
    };

        private static readonly string[] materials =
        {
        "хлопок",
        "шерсть",
        "полиэстер",
        "кожа",
        "синтетика"
    };

        private static readonly string[] brands =
        {
        "Nike",
        "Adidas",
        "Logitech",
        "ASUS",
        "Sony",
        "Microsoft"
    };

        private static readonly string[] gameGenres =
        {
        "экшен",
        "стратегия",
        "рпг",
        "симулятор",
        "приключение"
    };

        private static readonly string[] bookGenres =
        {
        "фантастика",
        "детектив",
        "роман",
        "научная литература",
        "фэнтези"
    };

        public static List<FeatureData> GenerateFeatures(CategoryData category)
        {
            string categoryName = category.GetName();

            if (categoryName == "Одежда")
            {
                return GenerateClothesFeatures();
            }

            if (categoryName == "Компьютерная периферия")
            {
                return GeneratePeripheralFeatures();
            }

            if (categoryName == "Комплектующие ПК")
            {
                return GeneratePcComponentFeatures();
            }

            if (categoryName == "Видеоигры")
            {
                return GenerateGameFeatures();
            }

            if (categoryName == "Книги")
            {
                return GenerateBookFeatures();
            }

            return new List<FeatureData>();
        }

        // ===================== ОДЕЖДА =====================

        private static List<FeatureData> GenerateClothesFeatures()
        {
            List<FeatureData> features = new List<FeatureData>();

            features.Add(new FeatureData("цвет", RandomFrom(colors)));
            features.Add(new FeatureData("материал", RandomFrom(materials)));
            features.Add(new FeatureData("бренд", RandomFrom(brands)));
            features.Add(new FeatureData("размер", Random.Range(42f, 56f)));
            features.Add(new FeatureData("утепленный", RandomBool()));

            return features;
        }

        // ===================== ПЕРИФЕРИЯ =====================

        private static List<FeatureData> GeneratePeripheralFeatures()
        {
            List<FeatureData> features = new List<FeatureData>();

            features.Add(new FeatureData("бренд", RandomFrom(brands)));
            features.Add(new FeatureData("беспроводной", RandomBool()));
            features.Add(new FeatureData("вес", Random.Range(50f, 1200f)));
            features.Add(new FeatureData("подсветка", RandomBool()));

            return features;
        }

        // ===================== КОМПЛЕКТУЮЩИЕ =====================

        private static List<FeatureData> GeneratePcComponentFeatures()
        {
            List<FeatureData> features = new List<FeatureData>();

            features.Add(new FeatureData("бренд", RandomFrom(brands)));
            features.Add(new FeatureData("энергопотребление", Random.Range(35f, 350f)));
            features.Add(new FeatureData("частота", Random.Range(2.0f, 5.5f)));
            features.Add(new FeatureData("охлаждение", RandomBool()));

            return features;
        }

        // ===================== ИГРЫ =====================

        private static List<FeatureData> GenerateGameFeatures()
        {
            List<FeatureData> features = new List<FeatureData>();

            features.Add(new FeatureData("жанр", RandomFrom(gameGenres)));
            features.Add(new FeatureData("мультиплеер", RandomBool()));
            features.Add(new FeatureData("возрастной рейтинг", Random.Range(6f, 18f)));
            features.Add(new FeatureData("бренд", RandomFrom(brands)));

            return features;
        }

        // ===================== КНИГИ =====================

        private static List<FeatureData> GenerateBookFeatures()
        {
            List<FeatureData> features = new List<FeatureData>();

            features.Add(new FeatureData("жанр", RandomFrom(bookGenres)));
            features.Add(new FeatureData("твердая обложка", RandomBool()));
            features.Add(new FeatureData("количество страниц", Random.Range(120f, 900f)));
            features.Add(new FeatureData("бренд", RandomFrom(brands)));

            return features;
        }

        // ===================== HELPERS =====================

        private static string RandomFrom(string[] array)
        {
            int index = Random.Range(0, array.Length);
            return array[index];
        }

        private static bool RandomBool()
        {
            return Random.Range(0, 2) == 0;
        }
    }
}