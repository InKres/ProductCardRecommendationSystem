#if UNITY_EDITOR

using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public static class ProductSOGenerator
{
    // Конфигурация генерации
    private const int PRODUCTS_PER_CATEGORY = 10;
    private static readonly System.Random random = new System.Random();
    private static string baseFolder = "Assets/ProductCardRecomendationSystem/Data";

    // Меню для генерации всех категорий
    [MenuItem("Tools/Generate Products/All Categories (50 items)")]
    public static void GenerateAllProducts()
    {
        var products = GenerateRandomProductData();
        GenerateProducts(products);
    }

    // Меню для генерации по категориям
    [MenuItem("Tools/Generate Products/Одежда (10 items)")]
    public static void GenerateClothingOnly()
    {
        var products = GenerateClothingProducts();
        GenerateProducts(products);
    }

    [MenuItem("Tools/Generate Products/Компьютерная периферия (10 items)")]
    public static void GeneratePeripheralsOnly()
    {
        var products = GenerateComputerPeripheralsProducts();
        GenerateProducts(products);
    }

    [MenuItem("Tools/Generate Products/Компьютерные компоненты (10 items)")]
    public static void GenerateComponentsOnly()
    {
        var products = GenerateComputerComponentsProducts();
        GenerateProducts(products);
    }

    [MenuItem("Tools/Generate Products/Книги (10 items)")]
    public static void GenerateBooksOnly()
    {
        var products = GenerateBookProducts();
        GenerateProducts(products);
    }

    [MenuItem("Tools/Generate Products/Техника (10 items)")]
    public static void GenerateAppliancesOnly()
    {
        var products = GenerateHomeAppliancesProducts();
        GenerateProducts(products);
    }

    [MenuItem("Tools/Clear All Generated Products")]
    public static void ClearGeneratedProducts()
    {
        if (EditorUtility.DisplayDialog("Clear All Products",
            "Delete all generated products and category folders?", "Yes", "No"))
        {
            if (Directory.Exists(baseFolder))
            {
                Directory.Delete(baseFolder, true);
                File.Delete($"{baseFolder}.meta");
                AssetDatabase.Refresh();
            }
        }
    }

    // Основной метод генерации
    public static void GenerateProducts(List<ProductData> products)
    {
        // Создаем базовую папку если не существует
        if (!Directory.Exists(baseFolder))
        {
            Directory.CreateDirectory(baseFolder);
        }

        // Группируем продукты по категориям
        var productsByCategory = products.GroupBy(p => p.Category);

        foreach (var categoryGroup in productsByCategory)
        {
            string categoryFolderName = GetCategoryFolderName(categoryGroup.Key);
            string categoryFolderPath = Path.Combine(baseFolder, categoryFolderName);

            // Создаем папку категории если не существует
            if (!Directory.Exists(categoryFolderPath))
            {
                Directory.CreateDirectory(categoryFolderPath);
            }

            // Генерируем SO для каждого продукта в категории
            foreach (var productData in categoryGroup)
            {
                CreateProductSO(categoryFolderPath, productData);
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log($"Generated {products.Count} ProductSO assets organized by category");
    }

    private static string GetCategoryFolderName(string category)
    {
        return category switch
        {
            "Одежда" => "Одежда",
            "Компьютерная периферия" => "Компьютерная периферия",
            "Компьютерные компоненты" => "Компьютерные компоненты",
            "Книги" => "Книги",
            "Бытовая техника" => "Бытовая техника",
            _ => "Другое"
        };
    }

    private static void CreateProductSO(string folderPath, ProductData productData)
    {
        Product product = new Product(
            productData.Id,
            productData.Name,
            productData.Category
        );

        SetPrivateField(product, "brand", productData.Brand);
        SetPrivateField(product, "price", productData.Price);
        SetPrivateField(product, "tags", productData.Tags);

        ProductSO productSO = ScriptableObject.CreateInstance<ProductSO>();
        SetPrivateField(productSO, "m_Product", product);

        // Новый формат имени файла: Название (ID)
        string fileName = $"{SanitizeFileName(productData.Name)} ({productData.Id}).asset";
        string assetPath = Path.Combine(folderPath, fileName);
        AssetDatabase.CreateAsset(productSO, assetPath);
    }

    private static string SanitizeFileName(string fileName)
    {
        // Убираем недопустимые символы для имен файлов
        var invalidChars = Path.GetInvalidFileNameChars();
        var cleanName = new string(fileName
            .Where(ch => !invalidChars.Contains(ch))
            .ToArray())
            .Trim();

        // Ограничиваем длину, но сохраняем читаемость
        return cleanName.Length <= 50 ? cleanName : cleanName.Substring(0, 47) + "...";
    }

    private static void SetPrivateField<T>(object obj, string fieldName, T value)
    {
        var fieldInfo = obj.GetType().GetField(fieldName,
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        if (fieldInfo != null)
        {
            fieldInfo.SetValue(obj, value);
        }
    }

    // Структура для данных продукта
    public struct ProductData
    {
        public string Id;
        public string Name;
        public string Category;
        public string Brand;
        public float Price;
        public string[] Tags;
    }

    // Генерация всех категорий
    public static List<ProductData> GenerateRandomProductData()
    {
        var products = new List<ProductData>();

        products.AddRange(GenerateClothingProducts());
        products.AddRange(GenerateComputerPeripheralsProducts());
        products.AddRange(GenerateComputerComponentsProducts());
        products.AddRange(GenerateBookProducts());
        products.AddRange(GenerateHomeAppliancesProducts());

        return products;
    }

    // 1. ОДЕЖДА
    private static List<ProductData> GenerateClothingProducts()
    {
        var products = new List<ProductData>();
        string[] types = { "Футболка", "Шорты", "Штаны", "Куртка", "Кофта", "Рубашка", "Платье", "Юбка" };
        string[] materials = { "хлопковая", "шерстяная", "льняная", "синтетическая", "кожаная", "джинсовая" };
        string[] colors = { "красного цвета", "белого цвета", "черного цвета", "синего цвета",
                               "зеленого цвета", "желтого цвета", "серого цвета", "коричневого цвета" };
        string[] styles = { "оверсайз", "приталенная", "классическая", "спортивная", "повседневная" };
        string[] brands = { "Zara", "H&M", "Nike", "Adidas", "Levi's", "Gucci", "Puma" };

        for (int i = 1; i <= PRODUCTS_PER_CATEGORY; i++)
        {
            string type = types[random.Next(types.Length)];
            string material = materials[random.Next(materials.Length)];
            string color = colors[random.Next(colors.Length)];
            string style = random.Next(3) == 0 ? styles[random.Next(styles.Length)] + " " : "";

            // Корректировка окончаний
            if (type.EndsWith("а") || type.EndsWith("я"))
                material = material.Replace("ая", "ая").Replace("яя", "яя");
            else
                material = material.Replace("ая", "ые").Replace("яя", "ие");

            string name = $"{type} {style}{material} {color}";
            name = name.Replace("  ", " ").Trim();

            products.Add(new ProductData
            {
                Id = $"clothing_{i:000}",
                Name = name,
                Category = "Одежда",
                Brand = brands[random.Next(brands.Length)],
                Price = random.Next(500, 15000),
                Tags = new[] { "одежда", type.ToLower(), material.Split(' ')[0] }
            });
        }

        return products;
    }

    // 2. КОМПЬЮТЕРНАЯ ПЕРИФЕРИЯ
    private static List<ProductData> GenerateComputerPeripheralsProducts()
    {
        var products = new List<ProductData>();
        string[] types = { "Мышь", "Клавиатура", "Монитор", "Наушники", "Колонки", "Веб-камера", "Микрофон" };
        string[] features = { "игровая", "офисная", "беспроводная", "механическая", "сенсорная",
                                 "эргономичная", "RGB-подсветка", "высокоточная" };
        string[] specs = { "с разрешением 4K", "с подсветкой", "с шумоподавлением",
                              "с Bluetooth 5.0", "с регулируемой DPI", "с кнопками макросов" };
        string[] brands = { "Logitech", "Razer", "SteelSeries", "HyperX", "Corsair", "Samsung", "Dell" };

        for (int i = 1; i <= PRODUCTS_PER_CATEGORY; i++)
        {
            string type = types[random.Next(types.Length)];
            string feature = features[random.Next(features.Length)];
            string spec = random.Next(2) == 0 ? " " + specs[random.Next(specs.Length)] : "";

            // Согласование окончаний
            if (type.EndsWith("а") || type.EndsWith("я"))
                feature = feature.Replace("ая", "ая").Replace("яя", "яя");
            else
                feature = feature.Replace("ая", "ый").Replace("яя", "ий");

            string name = $"{feature} {type}{spec}";
            name = name.Replace("  ", " ").Trim();

            products.Add(new ProductData
            {
                Id = $"peripheral_{i:000}",
                Name = name,
                Category = "Компьютерная периферия",
                Brand = brands[random.Next(brands.Length)],
                Price = random.Next(1500, 50000),
                Tags = new[] { "компьютерная техника", "периферия", type.ToLower() }
            });
        }

        return products;
    }

    // 3. КОМПОНЕНТЫ ДЛЯ ПК
    private static List<ProductData> GenerateComputerComponentsProducts()
    {
        var products = new List<ProductData>();
        string[] types = { "Процессор", "Видеокарта", "Материнская плата", "Оперативная память",
                              "SSD накопитель", "Блок питания", "Кулер", "Корпус" };
        string[] series = { "Ryzen", "Core i", "GeForce RTX", "Radeon", "ROG", "TUF", "Nitro", "Vengeance" };
        string[] specs = { "16 ГБ", "32 ГБ", "1 ТБ", "500 ГБ", "750 Вт", "850 Вт", "с водяным охлаждением" };
        string[] brands = { "AMD", "Intel", "NVIDIA", "ASUS", "MSI", "Gigabyte", "Kingston", "Seagate" };

        for (int i = 1; i <= PRODUCTS_PER_CATEGORY; i++)
        {
            string type = types[random.Next(types.Length)];
            string seriesName = series[random.Next(series.Length)];
            string spec = random.Next(2) == 0 ? " " + specs[random.Next(specs.Length)] : "";

            string name = $"{type} {seriesName} {random.Next(3, 9)}000{spec}";
            name = name.Replace("  ", " ").Trim();

            products.Add(new ProductData
            {
                Id = $"component_{i:000}",
                Name = name,
                Category = "Компьютерные компоненты",
                Brand = brands[random.Next(brands.Length)],
                Price = random.Next(3000, 150000),
                Tags = new[] { "компьютерные компоненты", "железо", type.ToLower().Split(' ')[0] }
            });
        }

        return products;
    }

    // 4. КНИГИ
    private static List<ProductData> GenerateBookProducts()
    {
        var products = new List<ProductData>();
        string[] genres = { "Фэнтези", "Научная фантастика", "Детектив", "Роман", "Бизнес",
                               "Программирование", "Психология", "История" };
        string[] adjectives = { "Захватывающая", "Интересная", "Познавательная", "Классическая",
                                   "Современная", "Практическая", "Теоретическая" };
        string[] formats = { "в твердом переплете", "в мягкой обложке", "электронная книга",
                                "с иллюстрациями", "подарочное издание" };
        string[] authors = { "Стивен Кинг", "Джоан Роулинг", "Айзек Азимов", "Федор Достоевский",
                                "Николай Гоголь", "Рэй Брэдбери" };

        for (int i = 1; i <= PRODUCTS_PER_CATEGORY; i++)
        {
            string genre = genres[random.Next(genres.Length)];
            string adjective = adjectives[random.Next(adjectives.Length)];
            string format = random.Next(3) == 0 ? " " + formats[random.Next(formats.Length)] : "";
            string author = random.Next(2) == 0 ? " от " + authors[random.Next(authors.Length)] : "";

            string name = $"{adjective} книга: \"{genre}\"{author}{format}";
            name = name.Replace("  ", " ").Trim();

            products.Add(new ProductData
            {
                Id = $"book_{i:000}",
                Name = name,
                Category = "Книги",
                Brand = "Издательский дом",
                Price = random.Next(300, 5000),
                Tags = new[] { "книги", "литература", genre.ToLower() }
            });
        }

        return products;
    }

    // 5. БЫТОВАЯ ТЕХНИКА
    private static List<ProductData> GenerateHomeAppliancesProducts()
    {
        var products = new List<ProductData>();
        string[] types = { "Холодильник", "Стиральная машина", "Пылесос", "Кофемашина",
                              "Микроволновая печь", "Телевизор", "Кондиционер", "Блендер" };
        string[] features = { "встраиваемая", "отдельностоящая", "инверторная", "умная",
                                 "энергоэффективная", "компактная", "многофункциональная" };
        string[] specs = { "с сенсорным управлением", "с функцией пара", "с системой No Frost",
                              "с Wi-Fi", "с таймером", "с турборежимом" };
        string[] brands = { "Samsung", "LG", "Bosch", "Philips", "Indesit", "Beko", "Miele" };

        for (int i = 1; i <= PRODUCTS_PER_CATEGORY; i++)
        {
            string type = types[random.Next(types.Length)];
            string feature = features[random.Next(features.Length)];
            string spec = random.Next(2) == 0 ? " " + specs[random.Next(specs.Length)] : "";

            // Согласование окончаний
            if (type.EndsWith("ильник") || type.EndsWith("ина"))
                feature = feature.Replace("ая", "ый");
            else
                feature = feature.Replace("ая", "ая");

            string name = $"{feature} {type}{spec}";
            name = name.Replace("  ", " ").Trim();

            products.Add(new ProductData
            {
                Id = $"appliance_{i:000}",
                Name = name,
                Category = "Бытовая техника",
                Brand = brands[random.Next(brands.Length)],
                Price = random.Next(5000, 150000),
                Tags = new[] { "бытовая техника", "техника для дома", type.ToLower().Split(' ')[0] }
            });
        }

        return products;
    }
}
#endif