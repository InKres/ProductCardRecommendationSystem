using System;
using System.Collections.Generic;
using UnityEngine;

public class CategoryWindowView : WindowView
{
    public event Action<string> OnSelectCategoryEvent;

    [Header("Components")]
    [SerializeField]
    private List<CategorySelectorView> categorySelectors = new List<CategorySelectorView>();

    public override void Init()
    {
        if (isInitialized == true) return;

        if (categorySelectors.Count == 0)
        {
            FindAllCategorySelectors();
        }

        foreach (CategorySelectorView categorySelector in categorySelectors)
        {
            categorySelector.Init();
            categorySelector.OnSelectCategory += OnSelectCategory;
        }

        base.Init();
    }

    public override void Dispose()
    {
        if(isInitialized == false) return;

        foreach (CategorySelectorView categorySelector in categorySelectors)
        {
            categorySelector.OnSelectCategory -= OnSelectCategory;
            categorySelector.Dispose();
        }

        base.Dispose();
    }

    private void FindAllCategorySelectors()
    {
        categorySelectors.AddRange(GetComponentsInChildren<CategorySelectorView>());
    }

    private void OnSelectCategory(string categoryName)
    {
        OnSelectCategoryEvent?.Invoke(categoryName);
    }
}