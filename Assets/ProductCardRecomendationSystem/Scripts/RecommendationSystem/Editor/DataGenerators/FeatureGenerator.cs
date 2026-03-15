using RecomendationSystem.Data;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace RecomendationSystem.DataGeneration
{
    public static class FeatureGenerator
    {
        private static readonly string[] colors =
        { "Красный","Синий","Черный","Белый","Зеленый","Желтый","Серый" };

        private static readonly string[] materials =
        { "Хлопок","Шерсть","Полиэстер","Кожа","Синтетика" };

        // Бренды по категориям

        private static readonly string[] clothesBrands =
        { "Nike","Adidas","Puma","Reebok","Under Armour" };

        private static readonly string[] pcBrands =
        { "ASUS","MSI","Gigabyte","Intel","AMD","Corsair" };

        private static readonly string[] peripheralBrands =
        { "Logitech","Razer","SteelSeries","HyperX","Corsair" };

        private static readonly string[] gameBrands =
        { "Sony","Microsoft","Nintendo","Ubisoft","Electronic Arts","Valve" };

        private static readonly string[] bookBrands =
        { "Эксмо","АСТ","Питер","Манн, Иванов и Фербер","Бомбора" };

        private static readonly string[] gameGenres =
        { "Экшен","Стратегия","РПГ","Симулятор","Приключение" };

        private static readonly string[] bookGenres =
        { "Фантастика","Детектив","Роман","Научная литература","Фэнтези" };

        public static List<FeatureData> GenerateFeatures(CategoryData category)
        {
            switch (category.GetName())
            {
                case "Одежда": return GenerateClothesFeatures();
                case "Компьютерная периферия": return GeneratePeripheralFeatures();
                case "Комплектующие ПК": return GeneratePcComponentFeatures();
                case "Видеоигры": return GenerateGameFeatures();
                case "Книги": return GenerateBookFeatures();
            }

            return new List<FeatureData>();
        }

        // ===================== ОДЕЖДА =====================

        private static List<FeatureData> GenerateClothesFeatures()
        {
            return new List<FeatureData>
            {
                new FeatureData("Цвет", RandomFrom(colors)),
                new FeatureData("Материал", RandomFrom(materials)),
                new FeatureData("Бренд", RandomFrom(clothesBrands)),
                new FeatureData("Размер", Random.Range(42,56)),
                new FeatureData("Утепленный", RandomBool())
            };
        }

        // ===================== ПЕРИФЕРИЯ =====================

        private static List<FeatureData> GeneratePeripheralFeatures()
        {
            List<FeatureData> features = new List<FeatureData>
            {
                new FeatureData("Бренд", RandomFrom(peripheralBrands)),
                new FeatureData("Беспроводной", RandomBool()),
                new FeatureData("Вес", Round1(Random.Range(50f,1200f))),
                new FeatureData("Подсветка", RandomBool())
            };

            features.AddRange(CreateSizeFeatures());

            return features;
        }

        // ===================== КОМПЛЕКТУЮЩИЕ =====================

        private static List<FeatureData> GeneratePcComponentFeatures()
        {
            List<FeatureData> features = new List<FeatureData>
            {
                new FeatureData("Бренд", RandomFrom(pcBrands)),
                new FeatureData("Энергопотребление", Round1(Random.Range(35f,350f))),
                new FeatureData("Частота", Round1(Random.Range(2.0f,5.5f))),
                new FeatureData("Охлаждение", RandomBool())
            };

            features.AddRange(CreateSizeFeatures());

            return features;
        }

        // ===================== ИГРЫ =====================

        private static List<FeatureData> GenerateGameFeatures()
        {
            return new List<FeatureData>
            {
                new FeatureData("Жанр", RandomFrom(gameGenres)),
                new FeatureData("Мультиплеер", RandomBool()),
                new FeatureData("Возрастной рейтинг", Random.Range(6,18)),
                new FeatureData("Бренд", RandomFrom(gameBrands))
            };
        }

        // ===================== КНИГИ =====================

        private static List<FeatureData> GenerateBookFeatures()
        {
            return new List<FeatureData>
            {
                new FeatureData("Жанр", RandomFrom(bookGenres)),
                new FeatureData("Твердая обложка", RandomBool()),
                new FeatureData("Количество страниц", Random.Range(120,900)),
                new FeatureData("Бренд", RandomFrom(bookBrands))
            };
        }

        // ===================== SIZE =====================

        private static List<FeatureData> CreateSizeFeatures()
        {
            return new List<FeatureData>
            {
                new FeatureData("Размер (Ширина)", Round1(Random.Range(5f,60f))),
                new FeatureData("Размер (Высота)", Round1(Random.Range(5f,60f))),
                new FeatureData("Размер (Длина)", Round1(Random.Range(5f,60f)))
            };
        }

        // ===================== HELPERS =====================

        private static string RandomFrom(string[] array)
        {
            return array[Random.Range(0, array.Length)];
        }

        private static bool RandomBool()
        {
            return Random.Range(0, 2) == 0;
        }

        private static float Round1(float value)
        {
            return (float)Math.Round(value, 1);
        }
    }
}