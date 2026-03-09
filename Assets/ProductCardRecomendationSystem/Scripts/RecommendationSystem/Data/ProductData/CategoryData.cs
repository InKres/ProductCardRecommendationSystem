using System.Collections.Generic;
using UnityEngine;

namespace RecomendationSystem.Data
{
    [System.Serializable]
    public class CategoryData : ICategoryData
    {
        [SerializeField]
        private string id;

        [SerializeField]
        private string name;

        //TODO: Это "Reference by ID" паттерн. Нужно вставить в курсовую
        [SerializeField]
        private string parentID;

        [SerializeField]
        private List<string> childrenID = new List<string>();
        //=============================================================

        public CategoryData(string id, string name, string parentID)
        {
            this.id = id;
            this.name = name;
            this.parentID = parentID;
        }

        public string GetID()
        {
            return id;
        }

        public string GetName()
        {
            return name;
        }

        public string GetParentID()
        {
            return parentID;
        }

        public IReadOnlyList<string> GetChildrenIDs()
        {
            return childrenID;
        }

        public void AddChild(string childID)
        {
            if (!childrenID.Contains(childID))
            {
                childrenID.Add(childID);
            }
        }
    }
}