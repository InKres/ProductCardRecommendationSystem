using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProductView : MonoBehaviour
{
    public event Action<ProductView> OnClick = delegate { };

    [Header("Components")]
    [SerializeField] private Button button;

    [Space]
    [SerializeField]
    private TMP_Text ratingText;
    [SerializeField]
    private TMP_Text nameText;
    [SerializeField]
    private TMP_Text descriptionText;
    [SerializeField]
    private Image image;
    [SerializeField]
    private TMP_Text purchasedQuantityText;
    [SerializeField]
    private TMP_Text priceText;

    private bool isInit;

    public void Init()
    {
        if (isInit) return;

        if (button != null)
        {
            button.onClick.AddListener(OnClicked);
        }

        isInit = true;
    }

    public void Dispose()
    {
        if (!isInit) return;

        if (button != null)
        {
            button.onClick.RemoveListener(OnClicked);
        }

        isInit = false;
    }

    public void SetName(string productName)
    {
        if (nameText != null)
            nameText.text = productName;
    }

    public void SetDescription(string description)
    {
        if (descriptionText != null)
            descriptionText.text = description;
    }

    public void SetImage(Sprite productImage)
    {
        if (image != null)
        {
            if (productImage != null)
            {
                image.sprite = productImage;
            }
        }
    }

    public void SetRating(float rating)
    {
        if (ratingText != null)
            ratingText.text = rating.ToString();
    }

    public void SetPrice(float price)
    {
        if (priceText != null)
            priceText.text = price.ToString();
    }

    public void SetPurchasedQuantity(int quantity)
    {
        if (purchasedQuantityText != null)
            purchasedQuantityText.text = quantity.ToString();
    }

    public void Clear()
    {
        SetName(string.Empty);
        SetDescription(string.Empty);
        SetPrice(0);
        SetRating(0);
        SetPurchasedQuantity(0);

        if (image != null)
            image.sprite = null;
    }

    private void OnClicked()
    {
        OnClick.Invoke(this);
    }
}