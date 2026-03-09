using System.Collections.Generic;
using UnityEngine;

namespace RecomendationSystem.Data
{
    [CreateAssetMenu(menuName = "RecommendationSystem/CategoryDatabaseSO")]
    public class CategoryDatabaseSO : ScriptableObject
    {
        [SerializeField]
        private List<CategoryData> ñategories = new List<CategoryData>();

        private Dictionary<string, CategoryData> idToCategoryDatas;

        public void Init()
        {
            idToCategoryDatas = new Dictionary<string, CategoryData>();

            foreach (CategoryData category in ñategories)
            {
                string id = category.GetID();

                if (!idToCategoryDatas.ContainsKey(id))
                {
                    idToCategoryDatas.Add(id, category);
                }
            }
        }

        public bool TryGetCategory(string id, out CategoryData category)
        {
            if (idToCategoryDatas == null)
            {
                Init();
            }

            return idToCategoryDatas.TryGetValue(id, out category);
        }

        public IReadOnlyList<CategoryData> GetAllCategories()
        {
            return ñategories;
        }
    }
}