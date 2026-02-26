using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProductView : MonoBehaviour
{
    public event Action<IProductData> OnClickByProductView;

    [Header("Components")]
    [SerializeField]
    private Button button;

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
    private TMP_Text brandText;
    [SerializeField]
    private TMP_Text purchasedQuantityText;
    [SerializeField]
    private TMP_Text priceText;

    private IProductData productData;

    private bool isInit;

    public void Init()
    {
        if (isInit) return;

        if (button != null)
        {
            button.onClick.AddListener(OnClickByProduct);
        }

        isInit = true;
    }

    public void Dispose()
    {
        if (isInit == false) return;

        if (button != null)
        {
            button.onClick.RemoveListener(OnClickByProduct);
        }

        isInit = false;
    }

    public void Setup(IProductData product)
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
        SetPurchasedQuantityText(product.GetPurchasedQuantity());
        SetPriceText(product.GetPrice());
    }

    public void Clear()
    {
        productData = null;

        if (ratingText != null)
            ratingText.text = string.Empty;

        if (nameText != null)
            nameText.text = string.Empty;

        if (descriptionText != null)
            descriptionText.text = string.Empty;

        if (brandText != null)
            brandText.text = string.Empty;

        if (purchasedQuantityText != null)
            purchasedQuantityText.text = string.Empty;

        if (priceText != null)
            priceText.text = string.Empty;
    }

    private void OnClickByProduct()
    {
        OnClickByProductView?.Invoke(productData);
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
        if (image != null && productImage != null)
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

    private void SetPurchasedQuantityText(int value)
    {
        if (purchasedQuantityText != null)
        {
            purchasedQuantityText.text = value.ToString();
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