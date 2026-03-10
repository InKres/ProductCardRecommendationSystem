using RecomendationSystem.Data;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace RecomendationSystem.DataGeneration
{
    public class RecommendationDataGeneratorWindow : EditorWindow
    {
        private int productsToGenerate = 100;

        private string dataRootPath = "Assets/ProductCardRecomendationSystem/Resources/Data";
        private string productsPath = "Assets/ProductCardRecomendationSystem/Resources/Data/Products";
        private string categoriesPath = "Assets/ProductCardRecomendationSystem/Resources/Data/Categories";

        private CategoryDatabaseSO categoryDatabase;
        private ProductDatabaseSO productDatabase;

        [MenuItem("Tools/Recommendation/Data Generator")]
        public static void Open()
        {
            RecommendationDataGeneratorWindow window =
                GetWindow<RecommendationDataGeneratorWindow>();

            window.titleContent = new GUIContent("Data Generator");
        }

        private void OnGUI()
        {
            GUILayout.Space(10);

            EditorGUILayout.LabelField("Генератор данных", EditorStyles.boldLabel);

            GUILayout.Space(10);

            productsToGenerate = EditorGUILayout.IntField(
                "Количество товаров",
                productsToGenerate);

            GUILayout.Space(5);

            dataRootPath = EditorGUILayout.TextField("Root Path", dataRootPath);
            productsPath = EditorGUILayout.TextField("Products Path", productsPath);
            categoriesPath = EditorGUILayout.TextField("Categories Path", categoriesPath);

            GUILayout.Space(15);

            if (GUILayout.Button("Сгенерировать данные", GUILayout.Height(40)))
            {
                Generate();
            }
        }

        // ===================== GENERATION =====================

        private void Generate()
        {
            EnsureFolders();

            CreateCategoriesIfNeeded();
            CreateOrLoadProductDatabase();

            List<ProductData> newProducts = ProductGenerator.Generate(productsToGenerate, categoryDatabase);

            productDatabase.AddProducts(newProducts);

            EditorUtility.SetDirty(productDatabase);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log("Генерация завершена");
        }

        // ===================== FOLDERS =====================

        private void EnsureFolders()
        {
            if (!Directory.Exists(dataRootPath))
            {
                Directory.CreateDirectory(dataRootPath);
            }

            if (!Directory.Exists(productsPath))
            {
                Directory.CreateDirectory(productsPath);
            }

            if (!Directory.Exists(categoriesPath))
            {
                Directory.CreateDirectory(categoriesPath);
            }
        }

        // ===================== CATEGORIES =====================

        private void CreateCategoriesIfNeeded()
        {
            string assetPath = categoriesPath + "/CategoryDatabase.asset";

            categoryDatabase =
                AssetDatabase.LoadAssetAtPath<CategoryDatabaseSO>(assetPath);

            if (categoryDatabase != null)
            {
                return;
            }

            categoryDatabase = ScriptableObject.CreateInstance<CategoryDatabaseSO>();

            CategoryGenerator.FillDatabase(categoryDatabase);

            AssetDatabase.CreateAsset(categoryDatabase, assetPath);

            EditorUtility.SetDirty(categoryDatabase);
            AssetDatabase.SaveAssets();
        }

        // ===================== PRODUCTS =====================

        private void CreateOrLoadProductDatabase()
        {
            string assetPath = productsPath + "/ProductDatabase.asset";

            productDatabase =
                AssetDatabase.LoadAssetAtPath<ProductDatabaseSO>(assetPath);

            if (productDatabase != null)
            {
                return;
            }

            productDatabase = ScriptableObject.CreateInstance<ProductDatabaseSO>();

            AssetDatabase.CreateAsset(productDatabase, assetPath);
        }
    }
}