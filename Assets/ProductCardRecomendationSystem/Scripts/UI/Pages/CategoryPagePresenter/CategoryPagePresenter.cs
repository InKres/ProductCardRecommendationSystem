using MVP;
using RecomendationSystem.Data;
using RecomendationSystem.Recommendation;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class CategoryPagePresenter : PresenterBehaviour<IRecommendationFacade>
{
    public enum SortMethod
    {
        Popularity,
        PriceAscending,
        PriceDescending,
        BuyersCount
    }

    public class SortMethodSettings
    {
        [SerializeField]
        private string m_SortMethodName;
        public string SortMethodName => m_SortMethodName;

        [SerializeField]
        private SortMethod m_SortMethod = SortMethod.Popularity;
        public SortMethod SortMethod => m_SortMethod;
    }

    public event Action<IProductData> OnProductSelected;

    [Header("Components")]
    [SerializeField]
    private Dropdown dropdown;
    [SerializeField]
    private ProductCollectionView productCollectionView;

    [SerializeField]
    private UIShowController showController;

    [Header("Settings")]
    [SerializeField]
    private List<SortMethodSettings> sortMethods = new List<SortMethodSettings>();

    [SerializeField]
    private int itemsPerPage = 30;

    private Dictionary<SortMethod, Action> sortMethodToAction;

    private string categoryUID;
    private bool isInit;

    protected override void OnInjectModel(IRecommendationFacade model)
    {
        InitDropdown();

        InitSortFunctions();

        dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
    }

    protected override void OnRemoveModel(IRecommendationFacade model)
    {
        dropdown.onValueChanged.RemoveListener(OnDropdownValueChanged);
    }

    public void Init(string categoryUID)
    {
        if (isInit) return;

        this.categoryUID = categoryUID;

        //Тут нужно вывести карточки по 1 виду сортирвки.
        Hide();

        isInit = true;
    }

    public void Dispose()
    {
        if (!isInit) return;

        productCollectionView.Dispose();

        categoryUID = String.Empty;
        dropdown.value = 0;

        Show();

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

    private void InitDropdown()
    {
        List<string> options = new List<string>();

        foreach (SortMethodSettings sortMethod in sortMethods)
        {
            options.Add(sortMethod.SortMethodName);
        }

        dropdown.AddOptions(options);
        dropdown.value = 0;
    }

    private void InitSortFunctions()
    {
        sortMethodToAction = new Dictionary<SortMethod, Action>();
        sortMethodToAction.Add(SortMethod.Popularity, SortByPopularity);
        sortMethodToAction.Add(SortMethod.PriceAscending, SortByPriceAscending);
        sortMethodToAction.Add(SortMethod.PriceDescending, SortByPriceDescending);
        sortMethodToAction.Add(SortMethod.BuyersCount, SortByBuyersCount);
    }

    private void OnSelectProduct(IProductData data)
    {
        OnProductSelected?.Invoke(data);
    }

    private void OnDropdownValueChanged(int value)
    {
        
    }

    private void ShowProductsByCurrentSortMethod(SortMethod method)
    {
        if (model != null)
        {
            
        }
    }

    private void SortByPopularity()
    {

    }

    private void SortByPriceAscending()
    {

    }

    private void SortByPriceDescending()
    {

    }

    private void SortByBuyersCount()
    {

    }
}