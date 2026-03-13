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

    private Dictionary<SortMethod, Action> sortMethodToAction;

    private string categoryUID;
    private bool isInit;

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

    public void Init(string categoryUID)
    {
        if (isInit)
        {
            Dispose();
        }

        this.categoryUID = categoryUID;

        sortMethodSelector.ResetSelector();

        Show();

        isInit = true;
    }

    public void Dispose()
    {
        if (!isInit) return;

        productCollectionView.Dispose();

        categoryUID = String.Empty;

        Hide();

        isInit = false;
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
        if (!string.IsNullOrEmpty(categoryUID))
        {
            IReadOnlyList<IProductData> products = model.GetPopularProducts(categoryUID, itemsPerPage);

            productCollectionView.Init(products);
        }
    }

    private void SortByPriceAscending()
    {
        if (!string.IsNullOrEmpty(categoryUID))
        {
            IReadOnlyList<IProductData> products = model.GetProductsSortedByAscendingPrice(categoryUID, itemsPerPage);

            productCollectionView.Init(products);
        }
    }

    private void SortByPriceDescending()
    {
        if (!string.IsNullOrEmpty(categoryUID))
        {
            IReadOnlyList<IProductData> products = model.GetProductsSortedByDescendingPrice(categoryUID, itemsPerPage);

            productCollectionView.Init(products);
        }
    }

    private void SortByBuyersCount()
    {
        if (!string.IsNullOrEmpty(categoryUID))
        {
            IReadOnlyList<IProductData> products = model.GetMostPurchasedProducts(categoryUID, itemsPerPage);

            productCollectionView.Init(products);
        }
    }
}