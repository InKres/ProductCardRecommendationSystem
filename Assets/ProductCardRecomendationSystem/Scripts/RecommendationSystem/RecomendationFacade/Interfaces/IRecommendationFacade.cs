using RecomendationSystem.Data;
using System.Collections.Generic;

namespace RecomendationSystem.Recommendation
{
    public interface IRecommendationFacade
    {
        /// <summary>
        /// Возвращает список самых популярных товаров из всех доступных товаров.
        /// </summary>
        IReadOnlyList<IProductData> GetPopularProducts(int count);

        /// <summary>
        /// Возвращает список самых популярных товаров из указанной категории.
        /// </summary>
        IReadOnlyList<IProductData> GetPopularProducts(string categoryId, int count);



        /// <summary>
        /// Возвращает товары отсортированные по возрастанию цены из всех доступных товаров.
        /// </summary>
        IReadOnlyList<IProductData> GetProductsSortedByAscendingPrice(int count);

        /// <summary>
        /// Возвращает товары отсортированные по возрастанию цены из указанной категории.
        /// </summary>
        IReadOnlyList<IProductData> GetProductsSortedByAscendingPrice(string categoryId, int count);



        /// <summary>
        /// Возвращает товары отсортированные по убыванию цены из всех доступных товаров.
        /// </summary>
        IReadOnlyList<IProductData> GetProductsSortedByDescendingPrice(int count);

        /// <summary>
        /// Возвращает товары отсортированные по убыванию цены из указанной категории.
        /// </summary>
        IReadOnlyList<IProductData> GetProductsSortedByDescendingPrice(string categoryId, int count);



        /// <summary>
        /// Возвращает товары с наибольшим количеством покупок из всех доступных товаров.
        /// </summary>
        IReadOnlyList<IProductData> GetMostPurchasedProducts(int count);

        /// <summary>
        /// Возвращает товары с наибольшим количеством покупок из указанной категории.
        /// </summary>
        IReadOnlyList<IProductData> GetMostPurchasedProducts(string categoryId, int count);



        /// <summary>
        /// Возвращает товары наиболее похожие на указанный товар из всех доступных товаров.
        /// </summary>
        IReadOnlyList<IProductData> GetSimilarProducts(IProductData product, int count);

        /// <summary>
        /// Возвращает товары наиболее похожие на указанный товар из указанной категории.
        /// </summary>
        IReadOnlyList<IProductData> GetSimilarProducts(IProductData product, string categoryId, int count);



        /// <summary>
        /// Возвращает самые популярные товары среди товаров похожих на указанный товар из всех доступных товаров
        /// </summary>
        IReadOnlyList<IProductData> GetPopularSimilarProducts(IProductData product, int count);

        /// <summary>
        /// Возвращает самые популярные товары среди товаров похожих на указанный товар из указанной категории.
        /// </summary>
        IReadOnlyList<IProductData> GetPopularSimilarProducts(IProductData product, string categoryId, int count);
    }
}