using UnityEngine;

[CreateAssetMenu(fileName = "ProductSO", menuName = "RecomendationSystem/Data/ProductSO", order = 1)]
public class ProductDataSO : ScriptableObject
{
    [SerializeField]
    private ProductData m_ProductData;
    public ProductData ProductData => m_ProductData;

    public void SetNewProductData(ProductData productData)
    {
        m_ProductData = productData;
    }
}