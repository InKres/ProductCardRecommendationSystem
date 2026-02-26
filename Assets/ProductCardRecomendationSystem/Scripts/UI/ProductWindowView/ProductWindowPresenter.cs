using MVP;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductWindowPresenter : PresenterBehaviour<IProductData>
{
    public event Action<IProductData> OnSelectProductCard;

    [Header("Components")]
    [SerializeField]
    private Button closeButton;

    [Space]
    [SerializeField]
    private WindowView windowView;
    [SerializeField]
    private ProductView productView;

    [Space]
    [SerializeField]
    private ProductCardsPresenter hybridRecommendationsPresenter;
    [SerializeField]
    private ProductCardsPresenter contentRecommendationsPresenter;

    private void Start()
    {
        closeButton.onClick.AddListener(OnClickCloseWindow);
        windowView.OnHideWindow += OnHideProductWindow;

        hybridRecommendationsPresenter.OnSelectProductCard += OnSelectCard;
        contentRecommendationsPresenter.OnSelectProductCard += OnSelectCard;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        closeButton.onClick.RemoveListener(OnClickCloseWindow);
        windowView.OnHideWindow -= OnHideProductWindow;

        hybridRecommendationsPresenter.OnSelectProductCard -= OnSelectCard;
        contentRecommendationsPresenter.OnSelectProductCard -= OnSelectCard;
    }

    protected override void OnInjectModel(IProductData model)
    {
        productView.Setup(model);

        Show();
    }

    protected override void OnRemoveModel(IProductData model)
    {
        Clear();
    }

    public void SetHybridRecommendations(List<IProductData> products)
    {
        hybridRecommendationsPresenter.InjectModel(products);
    }

    public void SetContentRecommendations(List<IProductData> products)
    {
        contentRecommendationsPresenter.InjectModel(products);
    }

    public void Clear()
    {
        productView.Clear();

        hybridRecommendationsPresenter.RemoveModel();
        contentRecommendationsPresenter.RemoveModel();
    }

    public void Show(bool isImmediately = false)
    {
        windowView.Show(isImmediately);
    }

    public void Hide(bool isImmediately = false)
    {
        windowView.Hide(isImmediately);
    }

    private void OnClickCloseWindow()
    {
        Hide();
    }

    private void OnHideProductWindow()
    {
        RemoveModel();
    }

    private void OnSelectCard(IProductData product)
    {
        OnSelectProductCard?.Invoke(product);
    }
}