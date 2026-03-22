using MVP;
using RecomendationSystem.Data;
using RecomendationSystem.Recommendation;
using System;
using System.Collections.Generic;
using UnityEngine;

public class BaseProductsPagePresenter : PresenterBehaviour<IRecommendationFacade>, IShowablePanel
{
    public event Action<IProductData> OnProductSelected;

    [Header("Components")]
    [SerializeField]
    protected SortMethodSelector sortMethodSelector;
    [SerializeField]
    protected ProductCollectionView productCollectionView;
    [SerializeField]
    protected UIShowController showController;

    [Header("Settings")]
    [SerializeField]
    protected int itemsPerPage = 30;

    protected SortingEngine sortingEngine = new SortingEngine();
    protected Dictionary<SortMethod, Action> sortMethodToAction;

    protected override void OnInjectModel(IRecommendationFacade model)
    {
        InitSortFunctions();

        sortMethodSelector.OnSortMethodSelected += ShowProductsByCurrentSortMethod;
        sortMethodSelector.Init();

        productCollectionView.OnProductSelected += OnSelectProduct;
    }

    protected override void OnRemoveModel(IRecommendationFacade model)
    {
        productCollectionView.OnProductSelected -= OnSelectProduct;
        productCollectionView.Dispose();

        sortMethodSelector.OnSortMethodSelected -= ShowProductsByCurrentSortMethod;
        sortMethodSelector.Dispose();
    }

    public virtual void Show(bool isImmediately = true)
    {
        showController.Show(isImmediately);
    }

    public virtual void Hide(bool isImmediately = true)
    {
        showController.Hide(isImmediately);
    }

    public virtual void ResetSorting()
    {
        sortMethodSelector.ResetSelector();
    }

    protected virtual void InitSortFunctions()
    {
        sortMethodToAction = new Dictionary<SortMethod, Action>
        {
            { SortMethod.Popularity, SortByPopularity },
            { SortMethod.PriceAscending, SortByPriceAscending },
            { SortMethod.PriceDescending, SortByPriceDescending },
            { SortMethod.BuyersCount, SortByBuyersCount }
        };
    }

    protected virtual void SortByPopularity()
    {
        IReadOnlyList<IProductData> products = model.GetPopularProducts(itemsPerPage);

        productCollectionView.Init(sortingEngine.GetPopularProducts(products));
    }

    protected virtual void SortByPriceAscending()
    {
        IReadOnlyList<IProductData> products = model.GetPopularProducts(itemsPerPage);

        productCollectionView.Init(sortingEngine.GetProductsSortedByAscendingPrice(products));
    }

    protected virtual void SortByPriceDescending()
    {
        IReadOnlyList<IProductData> products = model.GetPopularProducts(itemsPerPage);

        productCollectionView.Init(sortingEngine.GetProductsSortedByDescendingPrice(products));
    }

    protected virtual void SortByBuyersCount()
    {
        IReadOnlyList<IProductData> products = model.GetPopularProducts(itemsPerPage);

        productCollectionView.Init(sortingEngine.GetMostPurchasedProducts(products));
    }

    protected virtual void ShowProductsByCurrentSortMethod(SortMethod method)
    {
        if (model != null)
        {
            if (sortMethodToAction.TryGetValue(method, out Action sortMethod))
            {
                sortMethod?.Invoke();
            }
        }
    }

    protected virtual void OnSelectProduct(IProductData data)
    {
        OnProductSelected?.Invoke(data);
    }
}