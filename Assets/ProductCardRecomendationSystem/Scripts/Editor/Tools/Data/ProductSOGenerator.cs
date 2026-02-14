#if UNITY_EDITOR

using System;
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
    private static string baseFolder = "Assets/ProductCardRecomendationSystem/Resources/Data";

    // Меню для генерации всех категорий
    [MenuItem("RecommendationSystem/Tools/Generate Products/Все категории (по 10 в каждой)")]
    public static void GenerateAllProducts()
    {
        var products = GenerateRandomProductData();
        GenerateProducts(products);
    }

    // Меню для генерации по категориям
    [MenuItem("RecommendationSystem/Tools/Generate Products/Одежда (10)")]
    public static void GenerateClothingOnly()
    {
        var products = GenerateClothingProducts();
        GenerateProducts(products);
    }

    [MenuItem("RecommendationSystem/Tools/Generate Products/Компьютерная периферия (10)")]
    public static void GeneratePeripheralsOnly()
    {
        var products = GenerateComputerPeripheralsProducts();
        GenerateProducts(products);
    }

    [MenuItem("RecommendationSystem/Tools/Generate Products/Компьютерные компоненты (10)")]
    public static void GenerateComponentsOnly()
    {
        var products = GenerateComputerComponentsProducts();
        GenerateProducts(products);
    }

    [MenuItem("RecommendationSystem/Tools/Generate Products/Книги (10)")]
    public static void GenerateBooksOnly()
    {
        var products = GenerateBookProducts();
        GenerateProducts(products);
    }

    [MenuItem("RecommendationSystem/Tools/Generate Products/Техника (10)")]
    public static void GenerateAppliancesOnly()
    {
        var products = GenerateHomeAppliancesProducts();
        GenerateProducts(products);
    }

    // Меню для очистки по категориям
    [MenuItem("RecommendationSystem/Tools/Clear Products/Удалить все данные")]
    public static void ClearAllGeneratedProducts()
    {
        if (EditorUtility.DisplayDialog("Удалить все данные",
            "Удалить все созданные данные из всех категорий?", "Да", "Нет"))
        {
            ClearCategoryFolder("Одежда");
            ClearCategoryFolder("Компьютерная периферия");
            ClearCategoryFolder("Компьютерные компоненты");
            ClearCategoryFolder("Книги");
            ClearCategoryFolder("Бытовая техника");
            AssetDatabase.Refresh();
        }
    }

    [MenuItem("RecommendationSystem/Tools/Clear Products/Одежда")]
    public static void ClearClothingProducts()
    {
        ClearCategoryFolder("Одежда");
    }

    [MenuItem("RecommendationSystem/Tools/Clear Products/Компьютерная периферия")]
    public static void ClearPeripheralsProducts()
    {
        ClearCategoryFolder("Компьютерная периферия");
    }

    [MenuItem("RecommendationSystem/Tools/Clear Products/Компьютерные компоненты")]
    public static void ClearComponentsProducts()
    {
        ClearCategoryFolder("Компьютерные компоненты");
    }

    [MenuItem("RecommendationSystem/Tools/Clear Products/Книги")]
    public static void ClearBooksProducts()
    {
        ClearCategoryFolder("Книги");
    }

    [MenuItem("RecommendationSystem/Tools/Clear Products/Техника")]
    public static void ClearAppliancesProducts()
    {
        ClearCategoryFolder("Бытовая техника");
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
        var productsByCategory = products.GroupBy(p => p.GetCategory());

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

    private static void ClearCategoryFolder(string categoryName)
    {
        string categoryFolderPath = Path.Combine(baseFolder, categoryName);

        if (!Directory.Exists(categoryFolderPath))
        {
            Debug.LogWarning($"Category folder {categoryName} does not exist.");
            return;
        }

        // Получаем все файлы .asset и их .meta файлы
        string[] files = Directory.GetFiles(categoryFolderPath, "*.asset");

        foreach (string file in files)
        {
            string metaFile = file + ".meta";

            // Удаляем основной файл
            if (File.Exists(file))
            {
                File.Delete(file);
            }

            // Удаляем мета-файл
            if (File.Exists(metaFile))
            {
                File.Delete(metaFile);
            }
        }

        // Если папка пуста, удаляем её
        if (Directory.GetFiles(categoryFolderPath).Length == 0 &&
            Directory.GetDirectories(categoryFolderPath).Length == 0)
        {
            Directory.Delete(categoryFolderPath);
            string metaFilePath = categoryFolderPath + ".meta";
            if (File.Exists(metaFilePath))
            {
                File.Delete(metaFilePath);
            }
        }

        Debug.Log($"Cleared {files.Length} products from {categoryName} category");
    }

    private static void CreateProductSO(string folderPath, ProductData productData)
    {
        // Создаём ScriptableObject и передаём готовый ProductData
        ProductDataSO productSO = ScriptableObject.CreateInstance<ProductDataSO>();
        productSO.SetNewProductData(productData);

        // Формируем имя файла: Название (ID).asset
        string fileName = $"{SanitizeFileName(productData.GetName())} ({productData.GetId()}).asset";
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

    // 1. ОДЕЖДА (исправленная генерация названий)
    private static List<ProductData> GenerateClothingProducts()
    {
        var products = new List<ProductData>();

        // Типы одежды с указанием рода (для согласования прилагательных)
        var maleTypes = new[] { "Свитер", "Пиджак", "Плащ", "Жилет", "Кардиган", "Пуховик" };
        var femaleTypes = new[] { "Футболка", "Блузка", "Куртка", "Кофта", "Юбка", "Платье", "Туника" };
        var neutralTypes = new[] { "Шорты", "Штаны", "Брюки", "Джинсы", "Леггинсы", "Комбинезон" };

        // Материалы в именительном падеже и соответствующий родительный падеж
        var materials = new Dictionary<string, string>
        {
            { "хлопок", "хлопка" },
            { "шерсть", "шерсти" },
            { "лён", "льна" },
            { "синтетика", "синтетики" },
            { "кожа", "кожи" },
            { "джинса", "джинсы" },
            { "кашемир", "кашемира" },
            { "шёлк", "шёлка" }
        };

        // Цвета (прилагательные в именительном падеже мужского рода) и родительный падеж
        var colors = new Dictionary<string, string>
        {
            { "красный", "красного" },
            { "белый", "белого" },
            { "черный", "черного" },
            { "синий", "синего" },
            { "зеленый", "зеленого" },
            { "желтый", "желтого" },
            { "серый", "серого" },
            { "коричневый", "коричневого" },
            { "бордовый", "бордового" },
            { "бежевый", "бежевого" }
        };

        // Стили (прилагательные) и их согласование с родом
        var styles = new Dictionary<string, (string male, string female, string plural)>
        {
            { "оверсайз", ("оверсайз", "оверсайз", "оверсайз") }, // неизменяемое
            { "приталенный", ("приталенный", "приталенная", "приталенные") },
            { "классический", ("классический", "классическая", "классические") },
            { "спортивный", ("спортивный", "спортивная", "спортивные") },
            { "повседневный", ("повседневный", "повседневная", "повседневные") },
            { "деловой", ("деловой", "деловая", "деловые") }
        };

        string[] brands = { "Zara", "H&M", "Nike", "Adidas", "Levi's", "Gucci", "Puma", "Reebok", "Columbia", "The North Face" };

        for (int i = 1; i <= PRODUCTS_PER_CATEGORY; i++)
        {
            // Выбираем тип одежды и определяем род
            string type;
            string genderKey; // "male", "female", "plural"

            int typeChoice = random.Next(3);
            if (typeChoice == 0)
            {
                type = maleTypes[random.Next(maleTypes.Length)];
                genderKey = "male";
            }
            else if (typeChoice == 1)
            {
                type = femaleTypes[random.Next(femaleTypes.Length)];
                genderKey = "female";
            }
            else
            {
                type = neutralTypes[random.Next(neutralTypes.Length)];
                genderKey = "plural"; // для множественного числа (шорты, брюки и т.п.)
            }

            // Выбираем материал и цвет
            var materialEntry = materials.ElementAt(random.Next(materials.Count));
            string materialNom = materialEntry.Key;      // именительный
            string materialGen = materialEntry.Value;    // родительный

            var colorEntry = colors.ElementAt(random.Next(colors.Count));
            string colorNom = colorEntry.Key;            // именительный
            string colorGen = colorEntry.Value;          // родительный

            // Выбираем стиль (с вероятностью 50%)
            bool hasStyle = random.Next(2) == 0;
            string stylePart = "";
            if (hasStyle)
            {
                var styleEntry = styles.ElementAt(random.Next(styles.Count));
                string styleKey = styleEntry.Key;
                var styleForms = styleEntry.Value;

                // Выбираем форму в зависимости от рода
                string styleForm = genderKey switch
                {
                    "male" => styleForms.male,
                    "female" => styleForms.female,
                    "plural" => styleForms.plural,
                    _ => styleForms.male
                };
                stylePart = styleForm + " ";
            }

            // Формируем название: [стиль] [тип] из [материал]а [цвет]ого цвета
            // Для множественного числа (шорты) согласуем: "из [материал]а [цвет]ого цвета" (без изменений)
            string name = $"{stylePart}{type} из {materialGen} {colorGen} цвета".Replace("  ", " ").Trim();

            string brand = brands[random.Next(brands.Length)];

            var product = new ProductData(
                id: Guid.NewGuid().ToString("N"),
                name: name,
                category: "Одежда",
                tags: new[] { "одежда", type.ToLower(), materialNom.ToLower() },
                brand: brand,
                rating: (float)Math.Round(random.Next(20, 50) / 10f, 1),
                price: random.Next(500, 15000)
            );

            products.Add(product);
        }

        return products;
    }

    // 2. КОМПЬЮТЕРНАЯ ПЕРИФЕРИЯ (улучшенные названия)
    private static List<ProductData> GenerateComputerPeripheralsProducts()
    {
        var products = new List<ProductData>();

        // Типы периферии с родом (для согласования прилагательных, если понадобится)
        var peripheralTypes = new Dictionary<string, string>
        {
            { "Мышь", "female" },
            { "Клавиатура", "female" },
            { "Монитор", "male" },
            { "Наушники", "plural" },
            { "Колонки", "plural" },
            { "Веб-камера", "female" },
            { "Микрофон", "male" },
            { "Коврик для мыши", "male" },
            { "Графический планшет", "male" },
            { "Игровой руль", "male" }
        };

        string[] brands = { "Logitech", "Razer", "SteelSeries", "HyperX", "Corsair", "Samsung", "Dell", "ASUS", "Acer", "BenQ" };

        // Характеристики (уже в правильной форме)
        Dictionary<string, string[]> specsByType = new Dictionary<string, string[]>
        {
            { "Мышь", new[] { "с регулируемым DPI", "с RGB-подсветкой", "беспроводная", "эргономичная", "игровая" } },
            { "Клавиатура", new[] { "механическая", "с подсветкой", "беспроводная", "игровая", "компактная" } },
            { "Монитор", new[] { "4K", "игровой", "изогнутый", "с высокой частотой обновления", "ультраширокий" } },
            { "Наушники", new[] { "с шумоподавлением", "игровые", "беспроводные", "с микрофоном", "стерео" } },
            { "Колонки", new[] { "акустические", "стерео", "с сабвуфером", "для компьютера", "компактные" } }
        };

        var typeList = peripheralTypes.Keys.ToList();

        for (int i = 1; i <= PRODUCTS_PER_CATEGORY; i++)
        {
            string type = typeList[random.Next(typeList.Count)];
            string brand = brands[random.Next(brands.Length)];

            string spec = "";
            if (specsByType.ContainsKey(type) && random.Next(2) == 0)
            {
                string[] availableSpecs = specsByType[type];
                spec = " " + availableSpecs[random.Next(availableSpecs.Length)];
            }

            // Название: "Тип Бренд характеристика" (например "Мышь Logitech беспроводная")
            string name = $"{type} {brand}{spec}";

            var product = new ProductData(
                id: Guid.NewGuid().ToString("N"),
                name: name,
                category: "Компьютерная периферия",
                tags: new[] { "компьютерная техника", "периферия", type.ToLower().Split(' ')[0] },
                brand: brand,
                rating: (float)Math.Round(random.Next(25, 50) / 10f, 1),
                price: random.Next(1500, 50000)
            );

            products.Add(product);
        }

        return products;
    }

    // 3. КОМПОНЕНТЫ ДЛЯ ПК (улучшенные названия)
    private static List<ProductData> GenerateComputerComponentsProducts()
    {
        var products = new List<ProductData>();

        string[] types = { "Процессор", "Видеокарта", "Материнская плата", "Оперативная память",
                          "SSD накопитель", "Блок питания", "Кулер", "Корпус", "Жесткий диск", "Звуковая карта" };

        Dictionary<string, string[]> seriesByType = new Dictionary<string, string[]>
        {
            { "Процессор", new[] { "Intel Core i", "AMD Ryzen", "Intel Xeon", "AMD Threadripper" } },
            { "Видеокарта", new[] { "NVIDIA GeForce RTX", "AMD Radeon RX", "NVIDIA Quadro", "AMD Radeon Pro" } },
            { "Материнская плата", new[] { "ASUS ROG", "Gigabyte AORUS", "MSI MPG", "ASRock Phantom" } },
            { "Оперативная память", new[] { "Corsair Vengeance", "Kingston HyperX", "G.Skill Trident", "Crucial Ballistix" } }
        };

        string[] brands = { "ASUS", "MSI", "Gigabyte", "ASRock", "Intel", "AMD", "NVIDIA", "Corsair", "Kingston", "Seagate" };

        for (int i = 1; i <= PRODUCTS_PER_CATEGORY; i++)
        {
            string type = types[random.Next(types.Length)];
            string brand = brands[random.Next(brands.Length)];

            string model = "";
            if (seriesByType.ContainsKey(type))
            {
                string[] series = seriesByType[type];
                string seriesName = series[random.Next(series.Length)];
                model = $"{seriesName} {random.Next(3, 9)}{random.Next(100, 900)}";
            }
            else
            {
                model = $"Model-{random.Next(100, 999)}";
            }

            string name = $"{type} {brand} {model}";

            // Добавляем характеристику для некоторых типов
            if (type == "Оперативная память" && random.Next(2) == 0)
                name += $", {random.Next(8, 64)} ГБ";
            else if ((type == "SSD накопитель" || type == "Жесткий диск") && random.Next(2) == 0)
                name += $", {random.Next(256, 4096)} ГБ";
            else if (type == "Блок питания" && random.Next(2) == 0)
                name += $", {random.Next(500, 1200)} Вт";

            var product = new ProductData(
                id: Guid.NewGuid().ToString("N"),
                name: name,
                category: "Компьютерные компоненты",
                tags: new[] { "компьютерные компоненты", "железо", type.ToLower().Split(' ')[0] },
                brand: brand,
                rating: (float)Math.Round(random.Next(30, 50) / 10f, 1),
                price: random.Next(3000, 150000)
            );

            products.Add(product);
        }

        return products;
    }

    // 4. КНИГИ (улучшенные названия)
    private static List<ProductData> GenerateBookProducts()
    {
        var products = new List<ProductData>();

        Dictionary<string, string[]> authorsByGenre = new Dictionary<string, string[]>
        {
            { "фэнтези", new[] { "Дж.Р.Р. Толкин", "Джоан Роулинг", "Анджей Сапковский", "Джордж Мартин" } },
            { "научная фантастика", new[] { "Айзек Азимов", "Рэй Брэдбери", "Артур Кларк", "Филип Дик" } },
            { "детектив", new[] { "Агата Кристи", "Артур Конан Дойл", "Дэн Браун", "Борис Акунин" } },
            { "роман", new[] { "Лев Толстой", "Федор Достоевский", "Эрих Мария Ремарк", "Джейн Остин" } },
            { "программирование", new[] { "Роберт Мартин", "Эндрю Троелсен", "Стив Макконнелл", "Мартин Фаулер" } }
        };

        string[] adjectives = { "Захватывающая", "Интересная", "Познавательная", "Классическая",
                               "Современная", "Практическая", "Теоретическая", "Увлекательная" };

        string[] formats = { "в твёрдом переплёте", "в мягкой обложке", "электронная книга",
                            "с иллюстрациями", "подарочное издание", "аудиокнига" };

        var genres = authorsByGenre.Keys.ToList();

        for (int i = 1; i <= PRODUCTS_PER_CATEGORY; i++)
        {
            string genre = genres[random.Next(genres.Count)];
            string adjective = adjectives[random.Next(adjectives.Length)];
            string format = random.Next(3) == 0 ? ", " + formats[random.Next(formats.Length)] : "";

            string[] authors = authorsByGenre[genre];
            string author = authors[random.Next(authors.Length)];

            // Название: "Приключенческий роман 'Тихий Дон' (Михаил Шолохов)"
            // Но у нас жанр и автор. Сделаем: "Захватывающий детектив от Агаты Кристи, в мягкой обложке"
            string name = $"{adjective} {genre} от {author}{format}";
            name = name.Replace("  ", " ").Trim();

            var product = new ProductData(
                id: Guid.NewGuid().ToString("N"),
                name: name,
                category: "Книги",
                tags: new[] { "книги", "литература", genre.ToLower() },
                brand: "Издательский дом",
                rating: (float)Math.Round(random.Next(25, 50) / 10f, 1),
                price: random.Next(300, 5000)
            );

            products.Add(product);
        }

        return products;
    }

    // 5. БЫТОВАЯ ТЕХНИКА (улучшенные названия)
    private static List<ProductData> GenerateHomeAppliancesProducts()
    {
        var products = new List<ProductData>();

        var applianceTypes = new Dictionary<string, string>
        {
            { "Холодильник", "male" },
            { "Стиральная машина", "female" },
            { "Пылесос", "male" },
            { "Кофемашина", "female" },
            { "Микроволновая печь", "female" },
            { "Телевизор", "male" },
            { "Кондиционер", "male" },
            { "Блендер", "male" },
            { "Электрочайник", "male" },
            { "Посудомоечная машина", "female" }
        };

        string[] brands = { "Samsung", "LG", "Bosch", "Philips", "Indesit", "Beko", "Miele", "Electrolux", "Ariston", "Panasonic" };

        // Характеристики в правильной форме
        string[] specs = { "с сенсорным управлением", "с функцией пара", "с системой No Frost",
                          "с Wi-Fi", "с таймером", "с турборежимом", "энергосберегающий", "инверторный" };

        var typeList = applianceTypes.Keys.ToList();

        for (int i = 1; i <= PRODUCTS_PER_CATEGORY; i++)
        {
            string type = typeList[random.Next(typeList.Count)];
            string brand = brands[random.Next(brands.Length)];
            string spec = random.Next(2) == 0 ? ", " + specs[random.Next(specs.Length)] : "";

            string name = $"{type} {brand}{spec}";

            var product = new ProductData(
                id: Guid.NewGuid().ToString("N"),
                name: name,
                category: "Бытовая техника",
                tags: new[] { "бытовая техника", "техника для дома", type.ToLower().Split(' ')[0] },
                brand: brand,
                rating: (float)Math.Round(random.Next(30, 50) / 10f, 1),
                price: random.Next(5000, 150000)
            );

            products.Add(product);
        }

        return products;
    }
}

#endif