using MVP;
using RecomendationSystem.Data;
using RecomendationSystem.Recommendation;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ProductPagePresenter : PresenterBehaviour<IRecommendationFacade>, IShowablePanel
{
    public event Action<IProductData> OnProductSelected;

    [Header("Components")]
    [SerializeField]
    private BaseProductInfoView baseProductInfoView;
    [SerializeField]
    private FeatureCollectionView featureCollectionView;
    [SerializeField]
    private ProductCollectionView popularSimilarProductCollectionView;
    [SerializeField]
    private ProductCollectionView similarProductCollectionView;
    [SerializeField]
    private UIShowController showController;

    [Header("Settings")]
    [SerializeField]
    private int itemsPerPage = 30;

    protected override void OnInjectModel(IRecommendationFacade model)
    {
        popularSimilarProductCollectionView.OnProductSelected += OnSelectProduct;
        similarProductCollectionView.OnProductSelected += OnSelectProduct;
    }

    protected override void OnRemoveModel(IRecommendationFacade model)
    {
        featureCollectionView.Dispose();

        popularSimilarProductCollectionView.OnProductSelected -= OnSelectProduct;
        popularSimilarProductCollectionView.Dispose();

        similarProductCollectionView.OnProductSelected -= OnSelectProduct;
        similarProductCollectionView.Dispose();
    }

    public void SetProduct(IProductData product)
    {
        SetupProductView(baseProductInfoView, product);

        featureCollectionView.Init(product.GetFeatures());

        IReadOnlyList<IProductData> popularSimilarProducts = 
            model.GetPopularSimilarProducts(product, itemsPerPage);

        popularSimilarProductCollectionView.Init(popularSimilarProducts);

        IReadOnlyList<IProductData> similarProducts = 
            model.GetSimilarProducts(product, itemsPerPage);

        similarProductCollectionView.Init(similarProducts);
    }

    public void RemoveProduct()
    {
        baseProductInfoView.Clear();
        featureCollectionView.Dispose();
        popularSimilarProductCollectionView.Dispose();
        similarProductCollectionView.Dispose();
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

    private void SetupProductView(BaseProductInfoView view, IProductData product)
    {
        view.SetName(product.GetName());
        view.SetDescription(product.GetDescription());
        view.SetImage(product.GetImage());
        view.SetRating(ConvertPopularityToRating(product.GetPopularity()));
        view.SetPrice(product.GetPrice());
        view.SetPurchasedQuantity(product.GetBuyersCount());
    }

    private float ConvertPopularityToRating(int popularity)
    {
        float rating = 1f + 4f * (popularity / 100f);

        return Mathf.Round(rating * 10f) / 10f;
    }
}