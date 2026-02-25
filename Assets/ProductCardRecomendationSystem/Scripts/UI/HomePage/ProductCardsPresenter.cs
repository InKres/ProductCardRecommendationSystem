using System;
using System.Collections.Generic;
using UnityEngine;
using MVP;

public class ProductCardsPresenter : PresenterBehaviour<List<ProductData>>
{
    public event Action<ProductData> OnSelectProductCard;

    [Header("Components")]
    [SerializeField]
    private GameObject productCardPrefab;
    [SerializeField]
    private Transform container;

    private List<GameObject> productCardViews = new List<GameObject>();

    protected override void OnInjectModel(List<ProductData> model)
    {

    }

    protected override void OnRemoveModel(List<ProductData> model)
    {

    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    private void CreateProductCardViews(List<ProductData> datas)
    {
        foreach (ProductData data in datas)
        {
            CreateProductCardView(data);
        }
    }

    private void CreateProductCardView(ProductData data)
    {
        GameObject viewGO = Instantiate(productCardPrefab, container);

        ProductView view = viewGO.GetComponent<ProductView>();
        view.Init();
        view.Setup(data);
        view.OnClickByProductView += OnClickByProduct;
    }

    private void DestroyCardViews()
    {

    }

    private void OnClickByProduct(IProductData data)
    {
        throw new NotImplementedException();
    }
}