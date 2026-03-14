using System;
using UnityEngine;
using UnityEngine.UI;

public class ProductCardView : BaseProductInfoView
{
    public event Action<ProductCardView> OnClick = delegate { };

    [Header("Components")]
    [SerializeField] private Button button;

    public void Init()
    {
        button.onClick.AddListener(OnClicked);
    }

    public void Dispose()
    {
        button.onClick.RemoveListener(OnClicked);
    }

    private void OnClicked()
    {
        OnClick.Invoke(this);
    }
}
