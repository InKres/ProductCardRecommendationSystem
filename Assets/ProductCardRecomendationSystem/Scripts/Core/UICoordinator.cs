using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICoordinator : MonoBehaviour
{
    public event Action OnCloseApplicationEvent;

    [Header("Top panel components")]
    [SerializeField]
    private Button showCategoryWindowButton;
    [SerializeField]
    private Button showHelpWindowButton;
    [SerializeField]
    private Button closeApplicationButton;
    [SerializeField]
    private TMP_Text headerText;

    [Header("View components")]
    [SerializeField]
    private ProductCardsPresenter cardsPresenter;

    [SerializeField]
    private WindowView helpWindow;
    [SerializeField]
    private CategoryWindowView categoryWindow;
    [SerializeField]
    private ProductWindowPresenter productWindowPresenter;


    [Header("Settings")]
    [SerializeField]
    [Tooltip("The display quantity of products that will be shown on the main page")]
    private int displayedProductCount = 30;

    private RecommendationFacade recommendationFacade;

    private bool isInitialized = false;

    public void Init(RecommendationFacade facade)
    {
        if (isInitialized == true) return;

        this.recommendationFacade = facade;

        InitAllWindows();

        LoadProductsByCategoryName("Главная страница");

        SubscribeOnEvents();

        isInitialized = true;
    }

    public void Dispose()
    {
        if (isInitialized == false) return;

        UnsubscribeFromEvents();

        categoryWindow.Dispose();
        helpWindow.Dispose();

        isInitialized = false;
    }

    private void OnDestroy()
    {
        Dispose();
    }

    private void InitAllWindows()
    {
        categoryWindow.Init();
        categoryWindow.Hide(true);

        helpWindow.Init();
        helpWindow.Hide(true);

        productWindowPresenter.Hide(true);
    }

    private void SubscribeOnEvents()
    {
        cardsPresenter.OnSelectProductCard += OnProductSelected;
        categoryWindow.OnSelectCategoryEvent += LoadProductsByCategoryName;
        productWindowPresenter.OnSelectProductCard += OnProductSelected;

        showCategoryWindowButton.onClick.AddListener(ShowCategoryWindow);
        showHelpWindowButton.onClick.AddListener(ShowHelpWindow);
        closeApplicationButton.onClick.AddListener(OnCloseApplication);
    }

    private void UnsubscribeFromEvents()
    {
        cardsPresenter.OnSelectProductCard -= OnProductSelected;
        categoryWindow.OnSelectCategoryEvent -= LoadProductsByCategoryName;
        productWindowPresenter.OnSelectProductCard -= OnProductSelected;

        showCategoryWindowButton.onClick.RemoveListener(ShowCategoryWindow);
        showHelpWindowButton.onClick.RemoveListener(ShowHelpWindow);
        closeApplicationButton.onClick.RemoveListener(OnCloseApplication);
    }

    private void LoadProductsByCategoryName(string categoryName)
    {
        if (recommendationFacade == null)
        {
            Debug.LogError("RecommendationFacade not found!", this);
            return;
        }

        List<IProductData> categoryProducts = recommendationFacade.GetPopularProductsFromCategory(categoryName, displayedProductCount);
        cardsPresenter.InjectModel(categoryProducts);

        headerText.text = categoryName;
    }

    private void OnProductSelected(IProductData product)
    {
        if (recommendationFacade == null)
        {
            Debug.LogError("RecommendationFacade not found!", this);
            return;
        }

        List<IProductData> hybridRecomendations = recommendationFacade.GetHybridForProduct(product, displayedProductCount);
        List<IProductData> contentRecommendations = recommendationFacade.GetSimilarProducts(product, displayedProductCount);

        productWindowPresenter.InjectModel(product);
        productWindowPresenter.SetHybridRecommendations(hybridRecomendations);
        productWindowPresenter.SetContentRecommendations(contentRecommendations);
    }

    private void ShowHelpWindow()
    {
        helpWindow.Show();
    }

    private void ShowCategoryWindow()
    {
        categoryWindow.Show();
    }

    private void OnCloseApplication()
    {
        OnCloseApplicationEvent?.Invoke();
    }
}