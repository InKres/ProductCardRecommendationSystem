using RecomendationSystem.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ProductCollectionView : MonoBehaviour
{
    public event Action<IProductData> OnProductSelected = delegate { };

    [Header("View")]
    [SerializeField]
    private ProductCardView productViewPrefab;
    [SerializeField]
    private Transform container;

    private Dictionary<ProductCardView, IProductData> viewsToProductData;

    private bool isInit;

    public void Init(IReadOnlyList<IProductData> products)
    {
        if (isInit)
        {
            Dispose();
        }

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
        viewsToProductData = new Dictionary<ProductCardView, IProductData>();

        foreach (IProductData product in products)
        {
            CreateView(product);
        }
    }

    private void CreateView(IProductData product)
    {
        ProductCardView view = Instantiate(productViewPrefab, container);
        view.Init();
        SetupProductView(view, product);
        view.OnClick += OnClickByProductView;

        viewsToProductData.Add(view, product);
    }

    private void DestroyViews()
    {
        foreach (ProductCardView view in viewsToProductData.Keys)
        {
            view.Dispose();
            view.OnClick -= OnClickByProductView;

            Destroy(view.gameObject);
        }

        viewsToProductData.Clear();
        viewsToProductData = null;
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

    private void OnClickByProductView(ProductCardView view)
    {
        if (viewsToProductData.TryGetValue(view, out IProductData product))
        {
            OnProductSelected?.Invoke(product);
        }
    }
}