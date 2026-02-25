#if UNITY_EDITOR

using System.Collections.Generic;
using UnityEngine;

public static class CategoryGenerator
{
    public static Category Generate(string categoryName, int totalProducts)
    {
        switch (categoryName)
        {
            case "Компьютеры и периферия":
                return GenerateComputerCategory(totalProducts);
            case "Одежда":
                return GenerateClothingCategory(totalProducts);
            case "Книги":
                return GenerateBooksCategory(totalProducts);
            case "Телевизоры и аудиотехника":
                return GenerateTvCategory(totalProducts);
            case "Компьютерные игры":
                return GenerateGamesCategory(totalProducts);
            default:
                Debug.LogWarning($"Неизвестная категория: {categoryName}");
                return null;
        }
    }

    private static Category GenerateComputerCategory(int totalProducts)
    {
        int subCount = 3;
        int perSub = totalProducts / subCount;
        int remainder = totalProducts % subCount;

        // 1. Комплектующие ПК
        var componentsProducts = ProductGenerator.GenerateComputerComponents(perSub + (remainder-- > 0 ? 1 : 0));
        var components = new Category("Комплектующие ПК", new List<Category>(), componentsProducts);

        // 2. Периферия
        var peripheralsProducts = ProductGenerator.GeneratePeripherals(perSub + (remainder-- > 0 ? 1 : 0));
        var peripherals = new Category("Периферия", new List<Category>(), peripheralsProducts);

        // 3. Ноутбуки
        var laptopsProducts = ProductGenerator.GenerateLaptops(perSub + (remainder-- > 0 ? 1 : 0));
        var laptops = new Category("Ноутбуки", new List<Category>(), laptopsProducts);

        var subCategories = new List<Category> { components, peripherals, laptops };
        return new Category("Компьютеры и периферия", subCategories, new List<ProductData>());
    }

    private static Category GenerateClothingCategory(int totalProducts)
    {
        int subCount = 3;
        int perSub = totalProducts / subCount;
        int remainder = totalProducts % subCount;

        // Мужская одежда
        var maleProducts = ProductGenerator.GenerateClothing("Мужская", perSub + (remainder-- > 0 ? 1 : 0));
        var male = new Category("Мужская одежда", new List<Category>(), maleProducts);

        // Женская одежда
        var femaleProducts = ProductGenerator.GenerateClothing("Женская", perSub + (remainder-- > 0 ? 1 : 0));
        var female = new Category("Женская одежда", new List<Category>(), femaleProducts);

        // Детская одежда
        var kidsProducts = ProductGenerator.GenerateClothing("Детская", perSub + (remainder-- > 0 ? 1 : 0));
        var kids = new Category("Детская одежда", new List<Category>(), kidsProducts);

        var subCategories = new List<Category> { male, female, kids };
        return new Category("Одежда", subCategories, new List<ProductData>());
    }

    private static Category GenerateBooksCategory(int totalProducts)
    {
        var genres = new[] { "Художественная литература", "Научная литература", "Детская литература" };
        int perGenre = totalProducts / genres.Length;
        int remainder = totalProducts % genres.Length;

        var subCategories = new List<Category>();
        foreach (var genre in genres)
        {
            var products = ProductGenerator.GenerateBooks(genre, perGenre + (remainder-- > 0 ? 1 : 0));
            subCategories.Add(new Category(genre, new List<Category>(), products));
        }

        return new Category("Книги", subCategories, new List<ProductData>());
    }

    private static Category GenerateTvCategory(int totalProducts)
    {
        int tvCount = totalProducts / 2;
        int audioCount = totalProducts - tvCount;

        var tvProducts = ProductGenerator.GenerateTVs(tvCount);
        var tv = new Category("Телевизоры", new List<Category>(), tvProducts);

        var audioProducts = ProductGenerator.GenerateAudio(audioCount);
        var audio = new Category("Аудиотехника", new List<Category>(), audioProducts);

        var subCategories = new List<Category> { tv, audio };
        return new Category("Телевизоры и аудиотехника", subCategories, new List<ProductData>());
    }

    private static Category GenerateGamesCategory(int totalProducts)
    {
        var platforms = new[] { "PC", "PlayStation", "Xbox", "Nintendo" };
        int perPlatform = totalProducts / platforms.Length;
        int remainder = totalProducts % platforms.Length;

        var subCategories = new List<Category>();
        foreach (var platform in platforms)
        {
            var products = ProductGenerator.GenerateGames(platform, perPlatform + (remainder-- > 0 ? 1 : 0));
            subCategories.Add(new Category($"Игры для {platform}", new List<Category>(), products));
        }

        return new Category("Компьютерные игры", subCategories, new List<ProductData>());
    }
}

#endif