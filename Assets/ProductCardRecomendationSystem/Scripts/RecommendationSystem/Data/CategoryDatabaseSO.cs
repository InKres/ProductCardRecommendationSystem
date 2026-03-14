using System.Collections.Generic;
using UnityEngine;

namespace RecomendationSystem.Data
{
    [CreateAssetMenu(menuName = "RecommendationSystem/CategoryDatabaseSO")]
    public class CategoryDatabaseSO : ScriptableObject
    {
        [SerializeField]
        private List<CategoryData> categories = new List<CategoryData>();

        private Dictionary<string, CategoryData> idToCategoryDatas;

        public void Init()
        {
            idToCategoryDatas = new Dictionary<string, CategoryData>();

            foreach (CategoryData category in categories)
            {
                string id = category.GetID();

                if (!idToCategoryDatas.ContainsKey(id))
                {
                    idToCategoryDatas.Add(id, category);
                }
            }
        }

        public void Dispose()
        {
            idToCategoryDatas.Clear();
        }

        public void SetCategories(List<CategoryData> newCategories)
        {
            categories = newCategories;
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
            return categories;
        }
    }
}