using RecomendationSystem.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ProductCollectionView : MonoBehaviour
{
    public event Action<IProductData> OnProductSelected = delegate { };

    [Header("View")]
    [SerializeField]
    private ProductView productViewPrefab;
    [SerializeField]
    private Transform container;

    private Dictionary<ProductView, IProductData> viewsToProductData;

    private bool isInit;

    public void Init(IReadOnlyList<IProductData> products)
    {
        if (isInit) return;

        CreateViews(products);

        isInit = true;
    }

    public void Dispose()
    {
        if (!isInit) return;

        DestroyViews();

        isInit = false;
    }

    private void CreateViews(IReadOnlyList<IProductData> products)
    {
        viewsToProductData = new Dictionary<ProductView, IProductData>();

        foreach (IProductData product in products)
        {
            CreateView(product);
        }
    }

    private void CreateView(IProductData product)
    {
        ProductView productView = Instantiate(productViewPrefab, container);
        productView.Init();
        SetupView(productView, product);
        productView.OnClick += OnClickByProductView;

        viewsToProductData.Add(productView, product);
    }

    private void DestroyViews()
    {
        foreach (ProductView view in viewsToProductData.Keys)
        {
            view.Dispose();
            view.OnClick -= OnClickByProductView;

            Destroy(view.gameObject);
        }

        viewsToProductData.Clear();
        viewsToProductData = null;
    }

    private void SetupView(ProductView view, IProductData product)
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
        float scaled = popularity / 20.0f;

        return Mathf.Round(scaled * 10f) / 10f;
    }

    private void OnClickByProductView(ProductView view)
    {
        if (viewsToProductData.TryGetValue(view, out var product))
        {
            OnProductSelected?.Invoke(product);
        }
    }
}