using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CategorySelectorView : MonoBehaviour
{
    public event Action<string> OnSelectCategory;

    [Header("Settings")]
    [SerializeField]
    private string categoryName;

    private Button button;

    public void Init()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(OnClick);
    }

    public void Dispose()
    {
        button.onClick.RemoveListener(OnClick);
    }

    private void OnClick()
    {
        OnSelectCategory?.Invoke(categoryName);
    }
}