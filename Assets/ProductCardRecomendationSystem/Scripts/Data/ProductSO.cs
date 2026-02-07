using UnityEngine;

[CreateAssetMenu(fileName = "ProductSO", menuName = "RecomendationSystem/Data/ProductSO", order = 1)]
public class ProductSO : ScriptableObject
{
    [SerializeField]
    private Product m_Product;
    public Product Product => m_Product;
}