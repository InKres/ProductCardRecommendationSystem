using System.Collections.Generic;
using UnityEngine;

namespace RecomendationSystem.Data
{
    [CreateAssetMenu(menuName = "RecommendationSystem/ProductDatabaseSO")]
    public class ProductDatabaseSO : ScriptableObject
    {
        [SerializeField]
        private List<ProductData> products = new List<ProductData>();

        public IReadOnlyList<ProductData> GetProducts()
        {
            return products;
        }
    }
}