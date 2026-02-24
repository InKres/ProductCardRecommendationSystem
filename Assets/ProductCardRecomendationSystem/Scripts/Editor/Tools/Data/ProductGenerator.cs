#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

public static class ProductGenerator
{
    private static readonly string[] LoremWords = {
        "lorem", "ipsum", "dolor", "sit", "amet", "consectetur", "adipiscing", "elit",
        "sed", "do", "eiusmod", "tempor", "incididunt", "ut", "labore", "et", "dolore",
        "magna", "aliqua"
    };

    public static string GenerateLoremIpsum(int minWords = 10, int maxWords = 30)
    {
        int wordCount = Random.Range(minWords, maxWords + 1);
        var words = new List<string>();
        for (int i = 0; i < wordCount; i++)
        {
            words.Add(LoremWords[Random.Range(0, LoremWords.Length)]);
        }
        return string.Join(" ", words).First().ToString().ToUpper() + string.Join("", words.Skip(1)) + ".";
    }

    // ---- Компьютерные компоненты ----
    public static List<ProductData> GenerateComputerComponents(int count)
    {
        var products = new List<ProductData>();
        string[] types = { "Процессор", "Видеокарта", "Материнская плата", "Оперативная память", "SSD накопитель", "Блок питания", "Кулер", "Корпус" };
        string[] brands = { "Intel", "AMD", "NVIDIA", "ASUS", "MSI", "Gigabyte", "Corsair", "Kingston", "Seagate", "Western Digital" };

        for (int i = 0; i < count; i++)
        {
            string type = types[Random.Range(0, types.Length)];
            string brand = brands[Random.Range(0, brands.Length)];
            string model = $"{brand} {type} {Random.Range(100, 999)}";
            float price = type switch
            {
                "Процессор" => Random.Range(5000, 50000),
                "Видеокарта" => Random.Range(10000, 150000),
                "Материнская плата" => Random.Range(4000, 30000),
                "Оперативная память" => Random.Range(2000, 20000),
                "SSD накопитель" => Random.Range(2000, 30000),
                "Блок питания" => Random.Range(2000, 15000),
                "Кулер" => Random.Range(500, 5000),
                "Корпус" => Random.Range(2000, 15000),
                _ => Random.Range(1000, 50000)
            };

            products.Add(CreateProduct(
                name: model,
                category: "Компьютерные компоненты",
                brand: brand,
                price: price
            ));
        }
        return products;
    }

    // ---- Периферия ----
    public static List<ProductData> GeneratePeripherals(int count)
    {
        var products = new List<ProductData>();
        var peripheralTypes = new Dictionary<string, string[]>
        {
            { "Мышь", new[] { "с регулируемым DPI", "с RGB-подсветкой", "беспроводная", "эргономичная", "игровая" } },
            { "Клавиатура", new[] { "механическая", "с подсветкой", "беспроводная", "игровая", "компактная" } },
            { "Монитор", new[] { "4K", "игровой", "изогнутый", "с высокой частотой обновления", "ультраширокий" } },
            { "Наушники", new[] { "с шумоподавлением", "игровые", "беспроводные", "с микрофоном", "стерео" } },
            { "Колонки", new[] { "акустические", "стерео", "с сабвуфером", "для компьютера", "компактные" } }
        };
        string[] brands = { "Logitech", "Razer", "SteelSeries", "HyperX", "Corsair", "Samsung", "Dell", "ASUS", "Acer", "BenQ" };

        var typeList = peripheralTypes.Keys.ToList();

        for (int i = 0; i < count; i++)
        {
            string type = typeList[Random.Range(0, typeList.Count)];
            string brand = brands[Random.Range(0, brands.Length)];
            string spec = "";
            if (peripheralTypes.ContainsKey(type) && Random.Range(0, 2) == 0)
            {
                string[] availableSpecs = peripheralTypes[type];
                spec = " " + availableSpecs[Random.Range(0, availableSpecs.Length)];
            }
            string name = $"{type} {brand}{spec}";

            float price = type switch
            {
                "Монитор" => Random.Range(10000, 70000),
                "Мышь" => Random.Range(500, 10000),
                "Клавиатура" => Random.Range(1000, 20000),
                "Наушники" => Random.Range(1000, 25000),
                "Колонки" => Random.Range(1500, 30000),
                _ => Random.Range(500, 15000)
            };

            products.Add(CreateProduct(name, "Периферия", brand, price));
        }
        return products;
    }

    // ---- Ноутбуки ----
    public static List<ProductData> GenerateLaptops(int count)
    {
        var products = new List<ProductData>();
        string[] types = { "Игровой", "Ультрабук", "Для работы", "Бюджетный" };
        string[] brands = { "ASUS", "Acer", "HP", "Lenovo", "Dell", "MSI", "Apple" };
        for (int i = 0; i < count; i++)
        {
            string type = types[Random.Range(0, types.Length)];
            string brand = brands[Random.Range(0, brands.Length)];
            string name = $"{brand} {type} ноутбук";
            float price = type switch
            {
                "Игровой" => Random.Range(50000, 200000),
                "Ультрабук" => Random.Range(40000, 150000),
                "Для работы" => Random.Range(20000, 80000),
                "Бюджетный" => Random.Range(15000, 40000),
                _ => Random.Range(20000, 100000)
            };
            products.Add(CreateProduct(name, "Ноутбуки", brand, price));
        }
        return products;
    }

    // ---- Одежда (универсальный метод с учётом пола) ----
    public static List<ProductData> GenerateClothing(string gender, int count)
    {
        var products = new List<ProductData>();

        var maleTypes = new[] { "Свитер", "Пиджак", "Плащ", "Жилет", "Кардиган", "Пуховик" };
        var femaleTypes = new[] { "Футболка", "Блузка", "Куртка", "Кофта", "Юбка", "Платье", "Туника" };
        var neutralTypes = new[] { "Шорты", "Брюки", "Джинсы", "Леггинсы", "Комбинезон" };

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

        var styles = new Dictionary<string, (string male, string female, string plural)>
        {
            { "оверсайз", ("оверсайз", "оверсайз", "оверсайз") },
            { "приталенный", ("приталенный", "приталенная", "приталенные") },
            { "классический", ("классический", "классическая", "классические") },
            { "спортивный", ("спортивный", "спортивная", "спортивные") },
            { "повседневный", ("повседневный", "повседневная", "повседневные") },
            { "деловой", ("деловой", "деловая", "деловые") }
        };

        string[] brands = { "Zara", "H&M", "Nike", "Adidas", "Levi's", "Gucci", "Puma", "Reebok", "Columbia", "The North Face" };

        for (int i = 0; i < count; i++)
        {
            string type;
            string genderKey;

            if (gender == "Мужская")
            {
                type = maleTypes[Random.Range(0, maleTypes.Length)];
                genderKey = "male";
            }
            else if (gender == "Женская")
            {
                type = femaleTypes[Random.Range(0, femaleTypes.Length)];
                genderKey = "female";
            }
            else // Детская
            {
                int r = Random.Range(0, 3);
                if (r == 0) { type = maleTypes[Random.Range(0, maleTypes.Length)]; genderKey = "male"; }
                else if (r == 1) { type = femaleTypes[Random.Range(0, femaleTypes.Length)]; genderKey = "female"; }
                else { type = neutralTypes[Random.Range(0, neutralTypes.Length)]; genderKey = "plural"; }
            }

            var materialEntry = materials.ElementAt(Random.Range(0, materials.Count));
            string materialGen = materialEntry.Value;

            var colorEntry = colors.ElementAt(Random.Range(0, colors.Count));
            string colorGen = colorEntry.Value;

            bool hasStyle = Random.Range(0, 2) == 0;
            string stylePart = "";
            if (hasStyle)
            {
                var styleEntry = styles.ElementAt(Random.Range(0, styles.Count));
                var styleForms = styleEntry.Value;
                string styleForm = genderKey switch
                {
                    "male" => styleForms.male,
                    "female" => styleForms.female,
                    "plural" => styleForms.plural,
                    _ => styleForms.male
                };
                stylePart = styleForm + " ";
            }

            string name = $"{stylePart}{type} из {materialGen} {colorGen} цвета".Replace("  ", " ").Trim();
            string brand = brands[Random.Range(0, brands.Length)];
            float price = Random.Range(500, 15000);

            products.Add(CreateProduct(name, "Одежда", brand, price));
        }
        return products;
    }

    // ---- Книги ----
    public static List<ProductData> GenerateBooks(string genre, int count)
    {
        var products = new List<ProductData>();

        Dictionary<string, string[]> authorsByGenre = new Dictionary<string, string[]>
        {
            { "Художественная литература", new[] { "Лев Толстой", "Федор Достоевский", "Эрих Мария Ремарк", "Джейн Остин", "Чарльз Диккенс" } },
            { "Научная литература", new[] { "Роберт Мартин", "Эндрю Троелсен", "Стив Макконнелл", "Мартин Фаулер", "Ричард Докинз" } },
            { "Детская литература", new[] { "Корней Чуковский", "Агния Барто", "Самуил Маршак", "Астрид Линдгрен", "Туве Янссон" } }
        };

        string[] adjectives = { "Захватывающая", "Интересная", "Познавательная", "Классическая", "Современная", "Практическая" };
        string[] formats = { "в твёрдом переплёте", "в мягкой обложке", "электронная книга", "аудиокнига" };

        for (int i = 0; i < count; i++)
        {
            string adjective = adjectives[Random.Range(0, adjectives.Length)];
            string format = Random.Range(0, 3) == 0 ? ", " + formats[Random.Range(0, formats.Length)] : "";

            string[] authors = authorsByGenre[genre];
            string author = authors[Random.Range(0, authors.Length)];

            string name = $"{adjective} {genre} от {author}{format}";
            float price = Random.Range(200, 5000);

            products.Add(CreateProduct(name, "Книги", "Издательство", price));
        }
        return products;
    }

    // ---- Телевизоры ----
    public static List<ProductData> GenerateTVs(int count)
    {
        var products = new List<ProductData>();
        string[] brands = { "Samsung", "LG", "Sony", "Philips", "Panasonic" };
        string[] specs = { "4K UHD", "8K", "OLED", "QLED", "HDR", "Smart TV" };

        for (int i = 0; i < count; i++)
        {
            string brand = brands[Random.Range(0, brands.Length)];
            string spec = specs[Random.Range(0, specs.Length)];
            string name = $"{brand} Телевизор {spec} {Random.Range(40, 85)}\"";
            float price = Random.Range(20000, 300000);
            products.Add(CreateProduct(name, "Телевизоры", brand, price));
        }
        return products;
    }

    // ---- Аудиотехника ----
    public static List<ProductData> GenerateAudio(int count)
    {
        var products = new List<ProductData>();
        string[] types = { "Саундбар", "Акустическая система", "Ресивер", "Портативная колонка" };
        string[] brands = { "Yamaha", "Sony", "JBL", "Bose", "Panasonic" };

        for (int i = 0; i < count; i++)
        {
            string type = types[Random.Range(0, types.Length)];
            string brand = brands[Random.Range(0, brands.Length)];
            string name = $"{brand} {type}";
            float price = type switch
            {
                "Саундбар" => Random.Range(5000, 50000),
                "Акустическая система" => Random.Range(10000, 150000),
                "Ресивер" => Random.Range(15000, 80000),
                "Портативная колонка" => Random.Range(1000, 20000),
                _ => Random.Range(2000, 30000)
            };
            products.Add(CreateProduct(name, "Аудиотехника", brand, price));
        }
        return products;
    }

    // ---- Компьютерные игры ----
    public static List<ProductData> GenerateGames(string platform, int count)
    {
        var products = new List<ProductData>();

        string[] gameTitles = {
            "Cyberpunk 2077", "The Witcher 3", "GTA V", "Red Dead Redemption 2", "Minecraft",
            "Call of Duty", "FIFA", "Battlefield", "Assassin's Creed", "Far Cry",
            "The Legend of Zelda", "Super Mario", "God of War", "Uncharted", "Halo"
        };

        string[] genres = { "экшн", "шутер", "RPG", "стратегия", "симулятор", "гонки", "спорт" };
        string[] publishers = { "Ubisoft", "Electronic Arts", "Activision", "Bethesda", "CD Projekt", "Rockstar Games", "Nintendo", "Sony" };

        for (int i = 0; i < count; i++)
        {
            string title = gameTitles[Random.Range(0, gameTitles.Length)];
            string genre = genres[Random.Range(0, genres.Length)];
            string publisher = publishers[Random.Range(0, publishers.Length)];
            string name = $"{genre} {title} ({platform}) от {publisher}";
            float price = Random.Range(500, 5000);
            products.Add(CreateProduct(name, "Компьютерные игры", publisher, price));
        }
        return products;
    }

    // ---- Базовый метод создания продукта ----
    private static ProductData CreateProduct(string name, string category, string brand, float price)
    {
        string id = Guid.NewGuid().ToString("N");
        string description = GenerateLoremIpsum();
        int purchased = Random.Range(1, 10001);
        float rating = (float)Math.Round(Random.Range(20, 50) / 10f, 1);

        return new ProductData(id, name, description, null, purchased, category, brand, rating, price);
    }
}

#endif