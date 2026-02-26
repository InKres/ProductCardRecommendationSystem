using MVP;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ProductCardsPresenter : PresenterBehaviour<List<IProductData>>
{
    public event Action<IProductData> OnSelectProductCard;

    [Header("Components")]
    [SerializeField]
    private GameObject productCardPrefab;
    [SerializeField]
    private Transform container;

    private List<ProductView> productCardViews = new List<ProductView>();

    protected override void OnInjectModel(List<IProductData> model)
    {
        DestroyCardViews();

        CreateProductCardViews(model);
    }

    protected override void OnRemoveModel(List<IProductData> model)
    {
        DestroyCardViews();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    private void CreateProductCardViews(List<IProductData> datas)
    {
        foreach (IProductData data in datas)
        {
            CreateProductCardView(data);
        }
    }

    private void CreateProductCardView(IProductData data)
    {
        GameObject viewGO = Instantiate(productCardPrefab, container);

        ProductView view = viewGO.GetComponent<ProductView>();
        view.Init();
        view.Setup(data);
        view.OnClickByProductView += OnClickByProduct;

        productCardViews.Add(view);
    }

    private void DestroyCardViews()
    {
        if (productCardViews.Count == 0) return;

        foreach (ProductView view in productCardViews)
        {
            view.Dispose();
            view.OnClickByProductView -= OnClickByProduct;

            Destroy(view.gameObject);
        }

        productCardViews.Clear();
    }

    private void OnClickByProduct(IProductData data)
    {
        OnSelectProductCard?.Invoke(data);
    }
}