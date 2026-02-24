using UnityEngine;

public class ProductRepositoryTester : MonoBehaviour
{
    private IProductRepository _repository;

    [Header("Test Settings")]
    [SerializeField] private string testCategoryName = "Ноутбуки";
    [SerializeField] private bool includeSubcategories = true;

    /// <summary>
    /// Запустить все тесты (доступно через контекстное меню компонента)
    /// </summary>
    [ContextMenu("Run All Tests")]
    private void RunAllTests()
    {
        _repository = new ProductRepository();

        Debug.Log("========== НАЧАЛО ТЕСТИРОВАНИЯ РЕПОЗИТОРИЯ ==========");
        TestGetAllProducts();
        TestGetProductById();
        TestGetProductsByCategory();
        Debug.Log("========== ТЕСТИРОВАНИЕ ЗАВЕРШЕНО ==========");
    }

    /// <summary>
    /// Тест метода GetAllProducts()
    /// </summary>
    private void TestGetAllProducts()
    {
        Debug.Log("=== 1. GetAllProducts ===");
        var products = _repository.GetAllProducts();
        Debug.Log($"Всего товаров в базе: {products.Count}");

        if (products.Count > 0)
        {
            // Покажем первые 5 для примера
            int showCount = Mathf.Min(5, products.Count);
            for (int i = 0; i < showCount; i++)
            {
                var p = products[i];
                Debug.Log($"{i + 1}. {p.GetName()} (ID: {p.GetId()}, Категория: {p.GetCategory()})");
            }
            if (products.Count > 5)
                Debug.Log($"... и ещё {products.Count - 5} товаров.");
        }
        else
        {
            Debug.LogWarning("Товары не найдены! Проверьте наличие DataBaseSO.");
        }
    }

    /// <summary>
    /// Тест метода GetProductById()
    /// </summary>
    private void TestGetProductById()
    {
        Debug.Log("=== 2. GetProductById ===");
        var all = _repository.GetAllProducts();
        if (all.Count == 0)
        {
            Debug.LogWarning("Нет товаров для тестирования GetProductById.");
            return;
        }

        // Берём первый товар и ищем его по ID
        var first = all[0];
        var found = _repository.GetProductById(first.GetId());
        if (found != null)
        {
            Debug.Log($"Найден товар по ID: {found.GetName()} (ID: {found.GetId()})");
        }
        else
        {
            Debug.LogError($"Не удалось найти товар с ID: {first.GetId()}");
        }

        // Пробуем найти несуществующий ID
        var notFound = _repository.GetProductById("non-existent-id");
        Debug.Assert(notFound == null, "Ожидался null для несуществующего ID, но получен объект.");
        if (notFound == null)
            Debug.Log("Корректно: несуществующий ID вернул null.");
    }

    /// <summary>
    /// Тест метода GetProductsByCategory()
    /// </summary>
    private void TestGetProductsByCategory()
    {
        Debug.Log($"=== 3. GetProductsByCategory (категория: '{testCategoryName}', includeSubcategories: {includeSubcategories}) ===");
        var products = _repository.GetProductsByCategory(testCategoryName, includeSubcategories);
        Debug.Log($"Найдено товаров в категории '{testCategoryName}': {products.Count}");

        if (products.Count > 0)
        {
            int showCount = Mathf.Min(5, products.Count);
            for (int i = 0; i < showCount; i++)
            {
                var p = products[i];
                Debug.Log($"{i + 1}. {p.GetName()} (Категория: {p.GetCategory()})");
            }
            if (products.Count > 5)
                Debug.Log($"... и ещё {products.Count - 5} товаров.");
        }
        else
        {
            Debug.LogWarning($"Категория '{testCategoryName}' не содержит товаров или не найдена.");
        }
    }
}