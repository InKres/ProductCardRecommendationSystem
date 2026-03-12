using UnityEngine;
using UnityEngine.UI;
using RecomendationSystem.Data;
using RecomendationSystem.Recommendation;

public class UICoordinator : MonoBehaviour
{
    //[Header("Pages")]
    //[SerializeField]
    //private HomePagePresenter homePage;

    //[SerializeField]
    //private CategoryPagePresenter categoryPage;

    //[SerializeField]
    //private ProductPagePresenter productPage;

    [Header("Catalog Window")]
    [SerializeField]
    private UIShowController catalogWindow;
    [SerializeField]
    private Button showCatalogButton;
    [SerializeField]
    private Button hideCatalogButton;

    [Header("Help Window")]
    [SerializeField]
    private UIShowController helpWindow;
    [SerializeField]
    private Button showHelpButton;
    [SerializeField]
    private Button hideHelpButton;

    private IRecommendationFacade recommendationFacade;

    private MonoBehaviour currentPage;

    public void InjectFacade(IRecommendationFacade facade)
    {
        recommendationFacade = facade;
    }

    public void Init()
    {
        HideAllWindowsImmediately();

        BindButtons();

        InitializePages();

        ShowHomePageImmediately();
    }

    public void Dispose()
    {
        UnbindButtons();
    }

    private void InitializePages()
    {
        //homePage.InjectModel(recommendationFacade);
        //categoryPage.InjectFacade(recommendationFacade);
        //productPage.InjectFacade(recommendationFacade);
    }

    private void ShowHomePageImmediately()
    {
        //currentPage = homePage;
        //homePage.Show(true);
    }

    private void ShowHomePage()
    {
        HideCurrentPage();

        //currentPage = homePage;
        //homePage.Show();
    }

    private void ShowCategoryPage(string categoryId)
    {
        HideCurrentPage();

        //currentPage = categoryPage;
        //categoryPage.Show(categoryId);
    }

    private void ShowProductPage(IProductData product)
    {
        HideCurrentPage();

        //currentPage = productPage;
        //productPage.Show(product);
    }

    private void HideCurrentPage()
    {
        if (currentPage == null)
            return;

        //if (currentPage == homePage)
        //    homePage.Hide();
        //else if (currentPage == categoryPage)
        //    categoryPage.Hide();
        //else if (currentPage == productPage)
        //    productPage.Hide();
    }

    private void ShowCatalogWindow()
    {
        catalogWindow.Show(false);
    }

    private void HideCatalogWindow()
    {
        catalogWindow.Hide(false);
    }

    private void ShowHelpWindow()
    {
        helpWindow.Show(false);
    }

    private void HideHelpWindow()
    {
        helpWindow.Hide(false);
    }

    private void HideAllWindowsImmediately()
    {
        catalogWindow.Hide(true);
        helpWindow.Hide(true);
    }

    private void BindButtons()
    {
        showCatalogButton.onClick.AddListener(ShowCatalogWindow);
        showHelpButton.onClick.AddListener(ShowHelpWindow);

        hideCatalogButton.onClick.AddListener(HideCatalogWindow);
        hideHelpButton.onClick.AddListener(HideHelpWindow);
    }

    private void UnbindButtons()
    {
        showCatalogButton.onClick.RemoveListener(ShowCatalogWindow);
        showHelpButton.onClick.RemoveListener(ShowHelpWindow);

        hideCatalogButton.onClick.RemoveListener(HideCatalogWindow);
        hideHelpButton.onClick.RemoveListener(HideHelpWindow);
    }
}