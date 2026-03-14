using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BaseProductInfoView : MonoBehaviour
{
    [Header("Base info")]
    [SerializeField]
    protected TMP_Text ratingText;
    [SerializeField]
    protected TMP_Text nameText;
    [SerializeField]
    protected TMP_Text descriptionText;
    [SerializeField]
    protected Image image;
    [SerializeField]
    protected TMP_Text purchasedQuantityText;
    [SerializeField]
    protected TMP_Text priceText;

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
}