using System.Collections.Generic;

namespace RecomendationSystem.Data
{
    public interface ICategoryData
    {
        string GetID();
        string GetName();
        string GetParentID();

        IReadOnlyList<string> GetChildrenIDs();
    }
}