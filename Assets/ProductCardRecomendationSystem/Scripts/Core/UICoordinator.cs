using RecomendationSystem.Data;
using RecomendationSystem.Recommendation;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICoordinator : MonoBehaviour
{
    public event Action OnClickCloseApplication;

    [Header("Presenters")]
    [SerializeField]
    private BaseProductPagePresenter homePage;
    [SerializeField]
    private CategoryPagePresenter categoryPage;
    [SerializeField]
    private ProductPagePresenter productPage;
    [SerializeField]
    private CatalogWindowPresenter catalogWindow;

    [Header("Other components")]
    [SerializeField]
    private TMP_Text headerText;
    [SerializeField]
    private Button homeButton;
    [SerializeField]
    private Button backToCategoryButton;
    [SerializeField]
    private Button catalogButton;
    [SerializeField]
    private Button closeApplicationButton;

    private IRecommendationFacade recommendationFacade;

    private List<IShowablePanel> panels = new List<IShowablePanel>();

    private bool isInit;

    public void Init(IRecommendationFacade facade, IReadOnlyList<ICategoryData> categories)
    {
        if (isInit) return;

        if (facade == null)
        {
            Debug.LogError($"Recomendation facade in not found!", this);
            return;
        }

        recommendationFacade = facade;

        homePage.InjectModel(recommendationFacade);
        categoryPage.InjectModel(recommendationFacade);
        productPage.InjectModel(recommendationFacade);
        catalogWindow.InjectModel(categories);

        homeButton.onClick.AddListener(OnClickHomeButton);
        backToCategoryButton.onClick.AddListener(OnClickBackToCategoryButton);
        catalogButton.onClick.AddListener(OnClickCatalogButton);
        closeApplicationButton.onClick.AddListener(OnClickCloseApplicationButton);

        homePage.OnProductSelected += OnSelectProcuct;
        categoryPage.OnProductSelected += OnSelectProcuct;
        productPage.OnProductSelected += OnSelectProcuct;
        catalogWindow.OnCategorySelected += OnSelectCategory;

        PreparePanels();

        catalogWindow.Hide(true);
        ShowPage(homePage, OnShowHomePage);

        isInit = true;
    }

    public void Dispose()
    {
        if (!isInit) return;

        homePage.OnProductSelected -= OnSelectProcuct;
        categoryPage.OnProductSelected -= OnSelectProcuct;
        productPage.OnProductSelected -= OnSelectProcuct;
        catalogWindow.OnCategorySelected -= OnSelectCategory;

        homeButton.onClick.RemoveListener(OnClickHomeButton);
        backToCategoryButton.onClick.RemoveListener(OnClickBackToCategoryButton);
        catalogButton.onClick.RemoveListener(OnClickCatalogButton);
        closeApplicationButton.onClick.RemoveListener(OnClickCloseApplicationButton);

        homePage.RemoveModel();
        categoryPage.RemoveModel();
        productPage.RemoveModel();
        catalogWindow.RemoveModel();

        isInit = false;
    }

    private void PreparePanels()
    {
        panels.Add(homePage);
        panels.Add(categoryPage);
        panels.Add(productPage);
        panels.Add(catalogWindow);
    }

    private void OnClickHomeButton()
    {
        ShowPage(homePage, OnShowHomePage);
    }

    private void OnClickBackToCategoryButton()
    {
        ShowPage(categoryPage, OnShowCategoryPage);
    }

    private void OnClickCatalogButton()
    {
        if (catalogWindow.IsShown)
        {
            catalogWindow.Hide();
        }
        else
        {
            catalogWindow.Show();
        }
    }

    private void OnClickCloseApplicationButton()
    {
        OnClickCloseApplication?.Invoke();
    }

    private void ShowPage(IShowablePanel page, Action OnShowAction)
    {
        OnShowAction?.Invoke();

        HideAllPanels();
        ShowPanel(page);
    }

    private void OnShowHomePage()
    {
        headerText.text = "Главная страница";

        categoryPage.RemoveCategory();
        productPage.RemoveProduct();

        homeButton.gameObject.SetActive(false);
        backToCategoryButton.gameObject.SetActive(false);
    }

    private void OnShowCategoryPage()
    {
        homePage.ResetSorting();

        productPage.RemoveProduct();

        homeButton.gameObject.SetActive(true);
        backToCategoryButton.gameObject.SetActive(false);
    }

    private void OnShowProductPage()
    {
        headerText.text = "Страница продукта";

        homeButton.gameObject.SetActive(true);
        
        if (categoryPage.IsHasCategory)
        {
            backToCategoryButton.gameObject.SetActive(true);
        }
        else
        {
            backToCategoryButton.gameObject.SetActive(false);
        }
    }

    private void ShowPanel(IShowablePanel panel)
    {
        panel.Show();
    }

    private void HidePanel(IShowablePanel panel)
    {
        panel.Hide();
    }

    private void HideAllPanels()
    {
        foreach (IShowablePanel panel in panels)
        {
            HidePanel(panel);
        }
    }

    private void OnSelectProcuct(IProductData product)
    {
        productPage.SetProduct(product);

        ShowPage(productPage, OnShowProductPage);
    }

    private void OnSelectCategory(ICategoryData category)
    {
        headerText.text = $"Категория \"{category.GetName()}\"";

        categoryPage.SetCategory(category.GetID());

        ShowPage(categoryPage, OnShowCategoryPage);
    }
}