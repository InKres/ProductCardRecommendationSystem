using RecomendationSystem.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CategoryCollectionView : MonoBehaviour
{
    public event Action<ICategoryData> OnCategorySelected;

    [Header("Components")]
    [SerializeField]
    private CategoryView categoryViewPrefab;
    [SerializeField]
    private Transform container;

    private Dictionary<CategoryView, ICategoryData> viewsToCategoryData;

    private bool isInit;

    public void Init(IReadOnlyList<ICategoryData> categories)
    {
        if (isInit)
        {
            Dispose();
        }

        CreateViews(categories);

        isInit = true;
    }

    public void Dispose()
    {
        if (!isInit) return;

        DestroyViews();

        isInit = false;
    }

    private void CreateViews(IReadOnlyList<ICategoryData> categories)
    {
        viewsToCategoryData = new Dictionary<CategoryView, ICategoryData>();

        foreach (var category in categories)
        {
            CreateView(category);
        }
    }

    private void CreateView(ICategoryData category)
    {
        CategoryView view = Instantiate(categoryViewPrefab, container);
        view.Init();
        view.SetCategoryName(category.GetName());
        view.OnClick += OnClickByCategoryView;

        viewsToCategoryData.Add(view, category);
    }

    private void DestroyViews()
    {
        foreach (CategoryView view in viewsToCategoryData.Keys)
        {
            view.Dispose();
            view.OnClick -= OnClickByCategoryView;

            Destroy(view.gameObject);
        }

        viewsToCategoryData.Clear();
        viewsToCategoryData = null;
    }

    private void OnClickByCategoryView(CategoryView view)
    {
        if (viewsToCategoryData.TryGetValue(view, out ICategoryData categoryData))
        {
            OnCategorySelected?.Invoke(categoryData);
        }
    }
}