using MVP;
using RecomendationSystem.Data;
using RecomendationSystem.Recommendation;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CategoryPagePresenter : PresenterBehaviour<IRecommendationFacade>, IShowablePanel
{
    public event Action<IProductData> OnProductSelected;

    [Header("Components")]
    [SerializeField]
    private SortMethodSelector sortMethodSelector;
    [SerializeField]
    private ProductCollectionView productCollectionView;
    [SerializeField]
    private UIShowController showController;

    [Header("Settings")]
    [SerializeField]
    private int itemsPerPage = 30;

    private SortingEngine sortingEngine = new SortingEngine();

    private Dictionary<SortMethod, Action> sortMethodToAction;

    private string categoryUID;
    public bool IsHasCategory => !string.IsNullOrEmpty(categoryUID);

    protected override void OnInjectModel(IRecommendationFacade model)
    {
        InitSortFunctions();

        sortMethodSelector.Init();
        sortMethodSelector.OnSortMethodSelected += ShowProductsByCurrentSortMethod;

        productCollectionView.OnProductSelected += OnSelectProduct;
    }

    protected override void OnRemoveModel(IRecommendationFacade model)
    {
        productCollectionView.OnProductSelected -= OnSelectProduct;
        productCollectionView.Dispose();

        sortMethodSelector.OnSortMethodSelected -= ShowProductsByCurrentSortMethod;
        sortMethodSelector.Dispose();
    }

    public void SetCategory(string categoryUID)
    {
        if (IsHasCategory)
        {
            RemoveCategory();
        }

        this.categoryUID = categoryUID;
        sortMethodSelector.ResetSelector();
    }

    public void RemoveCategory()
    {
        this.categoryUID = string.Empty;
    }

    public void Show(bool isImmediately = true)
    {
        showController.Show(isImmediately);
    }

    public void Hide(bool isImmediately = true)
    {
        showController.Hide(isImmediately);
    }

    private void InitSortFunctions()
    {
        sortMethodToAction = new Dictionary<SortMethod, Action>();

        sortMethodToAction.Add(SortMethod.Popularity, SortByPopularity);
        sortMethodToAction.Add(SortMethod.PriceAscending, SortByPriceAscending);
        sortMethodToAction.Add(SortMethod.PriceDescending, SortByPriceDescending);
        sortMethodToAction.Add(SortMethod.BuyersCount, SortByBuyersCount);
    }

    private void ShowProductsByCurrentSortMethod(SortMethod method)
    {
        if (model != null)
        {
            if (sortMethodToAction.TryGetValue(method, out Action sortMethod))
            {
                sortMethod?.Invoke();
            }
        }
    }

    private void OnSelectProduct(IProductData data)
    {
        OnProductSelected?.Invoke(data);
    }

    private void SortByPopularity()
    {
        if (IsHasCategory)
        {
            IReadOnlyList<IProductData> products = model.GetPopularProducts(categoryUID, itemsPerPage);

            productCollectionView.Init(sortingEngine.GetPopularProducts(products));
            return;
        }

        productCollectionView.Dispose();
    }

    private void SortByPriceAscending()
    {
        if (IsHasCategory)
        {
            IReadOnlyList<IProductData> products = model.GetPopularProducts(categoryUID, itemsPerPage);

            productCollectionView.Init(sortingEngine.GetProductsSortedByAscendingPrice(products));
            return;
        }

        productCollectionView.Dispose();
    }

    private void SortByPriceDescending()
    {
        if (IsHasCategory)
        {
            IReadOnlyList<IProductData> products = model.GetPopularProducts(categoryUID, itemsPerPage);

            productCollectionView.Init(sortingEngine.GetProductsSortedByDescendingPrice(products));
            return;
        }

        productCollectionView.Dispose();
    }

    private void SortByBuyersCount()
    {
        if (IsHasCategory)
        {
            IReadOnlyList<IProductData> products = model.GetPopularProducts(categoryUID, itemsPerPage);

            productCollectionView.Init(sortingEngine.GetMostPurchasedProducts(products));
            return;
        }

        productCollectionView.Dispose();
    }
}