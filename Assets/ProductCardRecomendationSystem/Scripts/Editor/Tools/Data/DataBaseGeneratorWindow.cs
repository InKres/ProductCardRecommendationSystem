#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DataBaseGeneratorWindow : EditorWindow
{
    private const string PREFS_PREFIX = "ProductGenerator_";
    private List<CategoryConfig> categories;
    private Vector2 scrollPosition;
    private string savePath = "Assets/ProductCardRecomendationSystem/Resources/Data/DataBaseSO.asset";

    [MenuItem("RecommendationSystem/Tools/DataBase Generator Window")]
    public static void ShowWindow()
    {
        GetWindow<DataBaseGeneratorWindow>("DataBase Generator");
    }

    private void OnEnable()
    {
        LoadCategories();
    }

    private void LoadCategories()
    {
        categories = new List<CategoryConfig>
        {
            new CategoryConfig("Компьютеры и периферия", true, 10),
            new CategoryConfig("Одежда", true, 10),
            new CategoryConfig("Книги", true, 10),
            new CategoryConfig("Телевизоры и видеотехника", true, 10),
            new CategoryConfig("Компьютерные игры", true, 10)
        };

        foreach (var cat in categories)
        {
            cat.Enabled = EditorPrefs.GetBool($"{PREFS_PREFIX}{cat.Name}_Enabled", cat.Enabled);
            cat.ProductCount = EditorPrefs.GetInt($"{PREFS_PREFIX}{cat.Name}_Count", cat.ProductCount);
        }
    }

    private void SaveCategories()
    {
        foreach (var cat in categories)
        {
            EditorPrefs.SetBool($"{PREFS_PREFIX}{cat.Name}_Enabled", cat.Enabled);
            EditorPrefs.SetInt($"{PREFS_PREFIX}{cat.Name}_Count", cat.ProductCount);
        }
    }

    private void OnGUI()
    {
        GUILayout.Label("Настройки генерации", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

        foreach (var cat in categories)
        {
            EditorGUILayout.BeginHorizontal();
            cat.Enabled = EditorGUILayout.Toggle(cat.Enabled, GUILayout.Width(20));
            EditorGUILayout.LabelField(cat.Name, GUILayout.Width(200));
            EditorGUI.BeginDisabledGroup(!cat.Enabled);
            cat.ProductCount = EditorGUILayout.IntField(cat.ProductCount, GUILayout.Width(60));
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.EndScrollView();

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Путь сохранения DataBaseSO:");
        savePath = EditorGUILayout.TextField(savePath);

        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Generate", GUILayout.Height(30)))
        {
            SaveCategories();
            GenerateDataBase();
        }

        if (GUILayout.Button("Clear", GUILayout.Height(30)))
        {
            if (EditorUtility.DisplayDialog("Очистка данных",
                    "Удалить файл DataBaseSO? (папка останется)", "Да", "Нет"))
            {
                ClearDataBase();
            }
        }
        EditorGUILayout.EndHorizontal();
    }

    private void GenerateDataBase()
    {
        List<CategoryConfig> selected = categories.FindAll(c => c.Enabled);
        if (selected.Count == 0)
        {
            EditorUtility.DisplayDialog("Ошибка", "Выберите хотя бы одну категорию", "OK");
            return;
        }

        // Генерируем каждую выбранную категорию верхнего уровня
        List<Category> topLevelCategories = new List<Category>();
        foreach (var config in selected)
        {
            Category topCategory = CategoryGenerator.Generate(config.Name, config.ProductCount);
            if (topCategory != null)
            {
                topLevelCategories.Add(topCategory);
            }
        }

        // Создаём корневую категорию "Каталог" со всеми верхнеуровневыми категориями
        Category root = new Category("Каталог", topLevelCategories, new List<ProductData>());

        // Создаём DataBaseSO
        DataBaseSO db = ScriptableObject.CreateInstance<DataBaseSO>();
        db.SetNewCategory(root);

        string fullPath = savePath;
        string folder = System.IO.Path.GetDirectoryName(fullPath);
        if (!System.IO.Directory.Exists(folder))
            System.IO.Directory.CreateDirectory(folder);

        AssetDatabase.CreateAsset(db, fullPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log($"DataBaseSO успешно создан по пути: {fullPath}");
    }

    private void ClearDataBase()
    {
        if (System.IO.File.Exists(savePath))
        {
            AssetDatabase.DeleteAsset(savePath);
            AssetDatabase.Refresh();
            Debug.Log($"DataBaseSO удалён: {savePath}");
        }
        else
        {
            Debug.LogWarning("Файл не найден: " + savePath);
        }
    }

    [Serializable]
    private class CategoryConfig
    {
        public string Name;
        public bool Enabled;
        public int ProductCount;

        public CategoryConfig(string name, bool enabled, int count)
        {
            Name = name;
            Enabled = enabled;
            ProductCount = count;
        }
    }
}

#endif