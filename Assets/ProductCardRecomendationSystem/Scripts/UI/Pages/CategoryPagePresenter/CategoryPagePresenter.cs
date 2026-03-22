using RecomendationSystem.Data;
using System.Collections.Generic;

public class CategoryPagePresenter : BaseProductsPagePresenter
{
    private string categoryUID;
    public bool IsHasCategory => !string.IsNullOrEmpty(categoryUID);

    public void SetCategory(string categoryUID)
    {
        if (IsHasCategory)
        {
            RemoveCategory();
        }

        this.categoryUID = categoryUID;
        ResetSorting();
    }

    public void RemoveCategory()
    {
        this.categoryUID = string.Empty;
    }

    protected override void SortByPopularity()
    {
        if (IsHasCategory)
        {
            IReadOnlyList<IProductData> products = model.GetPopularProducts(categoryUID, itemsPerPage);

            productCollectionView.Init(sortingEngine.GetPopularProducts(products));
            return;
        }

        productCollectionView.Dispose();
    }

    protected override void SortByPriceAscending()
    {
        if (IsHasCategory)
        {
            IReadOnlyList<IProductData> products = model.GetPopularProducts(categoryUID, itemsPerPage);

            productCollectionView.Init(sortingEngine.GetProductsSortedByAscendingPrice(products));
            return;
        }

        productCollectionView.Dispose();
    }

    protected override void SortByPriceDescending()
    {
        if (IsHasCategory)
        {
            IReadOnlyList<IProductData> products = model.GetPopularProducts(categoryUID, itemsPerPage);

            productCollectionView.Init(sortingEngine.GetProductsSortedByDescendingPrice(products));
            return;
        }

        productCollectionView.Dispose();
    }

    protected override void SortByBuyersCount()
    {
        if (IsHasCategory)
        {
            IReadOnlyList<IProductData> products = model.GetPopularProducts(categoryUID, itemsPerPage);

            productCollectionView.Init(sortingEngine.GetMostPurchasedProducts(products));
            return;
        }

        productCollectionView.Dispose();
    }
}