using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProductCardView : MonoBehaviour
{
    public event Action<ProductData> OnClickByProductCard;

    [Header("Components")]
    [SerializeField]
    private Button button;

    [SerializeField]
    private TMP_Text ratingText;
    [SerializeField]
    private TMP_Text nameText;
    [SerializeField]
    private TMP_Text descriptionText;
    [SerializeField]
    private Image image;
    [SerializeField]
    private TMP_Text brandText;
    [SerializeField]
    private TMP_Text priceText;

    private ProductData productData;

    private bool isInit;

    public void Init()
    {
        if (isInit) return;

        if (button == null) return;

        button.onClick.AddListener(() => OnClickByProductCard?.Invoke(productData));
        isInit = true;
    }

    public void Dispose()
    {
        if (isInit == false) return;

        if (button == null) return;

        button.onClick.RemoveAllListeners();
        isInit = false;
    }

    public void Setup(ProductData product)
    {
        if (product == null)
        {
            Debug.LogError("Product data not found!!!", this);

            return;
        }

        productData = product;

        SetRatingText(product.GetRating());
        SetNameText(product.GetName());
        SetDescriptionText(product.GetDescription());
        SetImage(product.GetImage());
        SetBrandText(product.GetBrand());
        SetPriceText(product.GetPrice());
    }

    public void Clear()
    {
        productData = null;

        ratingText.text = string.Empty;
        nameText.text = string.Empty;
        descriptionText.text = string.Empty;
        image.sprite = null;
        brandText.text = string.Empty;
        priceText.text = string.Empty;
    }

    private void SetRatingText(float rating)
    {
        if (ratingText != null)
        {
            ratingText.text = rating.ToString();
        }
    }

    private void SetNameText(string name)
    {
        if (nameText != null)
        {
            nameText.text = name;
        }
    }

    private void SetDescriptionText(string description)
    {
        if (descriptionText != null)
        {
            descriptionText.text = description;
        }
    }

    private void SetImage(Sprite productImage)
    {
        if (image != null)
        {
            image.sprite = productImage;
        }
    }

    private void SetBrandText(string brand)
    {
        if (brandText != null)
        {
            brandText.text = brand;
        }
    }

    private void SetPriceText(float price)
    {
        if (priceText != null)
        {
            priceText.text = price.ToString();
        }
    }
}