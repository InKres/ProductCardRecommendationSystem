using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CategoryView : MonoBehaviour
{
    public event Action<CategoryView> OnClick = delegate { };

    [Header("Components")]
    [SerializeField]
    private Button button;
    [SerializeField]
    private TMP_Text categoryNameText;

    private bool isInit;

    public void Init()
    {
        if (isInit) return;

        button.onClick.AddListener(OnClicked);

        isInit = true;
    }

    public void Dispose()
    {
        if (!isInit) return;

        button.onClick.RemoveListener(OnClicked);

        isInit = false;
    }

    public void SetCategoryName(string name)
    {
        categoryNameText.text = name;
    }

    private void OnClicked()
    {
        OnClick?.Invoke(this);
    }
}
