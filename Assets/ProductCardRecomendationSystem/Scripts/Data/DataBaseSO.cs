using UnityEngine;

[CreateAssetMenu(fileName = "DataBaseSO", menuName = "RecomendationSystem/Data/DataBaseSO", order = 1)]
public class DataBaseSO : ScriptableObject
{
    [SerializeField]
    private Category m_Category;
    public Category Category => m_Category;

    public void SetNewCategory(Category category)
    {
        m_Category = category;
    }
}
