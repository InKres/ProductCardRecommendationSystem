using MVP;
using RecomendationSystem.Data;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatalogWindowPresenter : PresenterBehaviour<IReadOnlyList<ICategoryData>>, IShowablePanel
{
    public event Action<ICategoryData> OnCategorySelected;

    [Header("Components")]
    [SerializeField]
    private CategoryCollectionView categoryCollectionView;
    [SerializeField]
    private UIShowController showController;

    [Space]
    [SerializeField]
    private Button hideButton;

    public bool IsShown => showController.IsShown;

    protected override void OnInjectModel(IReadOnlyList<ICategoryData> model)
    {
        categoryCollectionView.Init(model);
        categoryCollectionView.OnCategorySelected += OnSelectCategory;

        hideButton.onClick.AddListener(OnClickHideButton);
    }

    protected override void OnRemoveModel(IReadOnlyList<ICategoryData> model)
    {
        hideButton.onClick.RemoveListener(OnClickHideButton);

        categoryCollectionView.OnCategorySelected -= OnSelectCategory;
        categoryCollectionView.Dispose();
    }

    public void Show(bool isImmediately = false)
    {
        showController.Show(isImmediately);
    }

    public void Hide(bool isImmediately = false)
    {
        showController.Hide(isImmediately);
    }

    private void OnSelectCategory(ICategoryData data)
    {
        OnCategorySelected?.Invoke(data);
    }

    private void OnClickHideButton()
    {
        Hide();
    }
}