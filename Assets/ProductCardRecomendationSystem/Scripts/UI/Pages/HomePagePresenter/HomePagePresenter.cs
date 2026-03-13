using MVP;
using RecomendationSystem.Data;
using RecomendationSystem.Recommendation;
using System;
using System.Collections.Generic;
using UnityEngine;

public class HomePagePresenter : PresenterBehaviour<IRecommendationFacade>, IShowablePanel
{
    public event Action<IProductData> OnProductSelected;

    [Header("Components")]
    [SerializeField]
    private ProductCollectionView productCollectionView;

    [SerializeField]
    private UIShowController showController;

    [Header("Settings")]
    [SerializeField]
    private int itemsPerPage = 30;

    protected override void OnInjectModel(IRecommendationFacade model)
    {
        IReadOnlyList<IProductData> products = model.GetPopularProducts(itemsPerPage);

        productCollectionView.Init(products);

        productCollectionView.OnProductSelected += OnSelectProduct;
    }

    protected override void OnRemoveModel(IRecommendationFacade model)
    {
        productCollectionView.OnProductSelected -= OnSelectProduct;

        productCollectionView.Dispose();
    }

    public void Show(bool isImmediately = true)
    {
        showController.Show(isImmediately);
    }

    public void Hide(bool isImmediately = true)
    {
        showController.Hide(isImmediately);
    }

    private void OnSelectProduct(IProductData data)
    {
        OnProductSelected?.Invoke(data);
    }
}