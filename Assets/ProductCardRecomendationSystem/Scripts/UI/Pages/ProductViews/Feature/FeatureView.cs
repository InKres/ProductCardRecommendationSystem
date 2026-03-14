using TMPro;
using UnityEngine;

public class FeatureView : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private TMP_Text feauterNameText;
    [SerializeField]
    private TMP_Text feauterValueText;

    public void SetFeauterName(string name)
    {
        feauterNameText.text = name;
    }

    public void SetFeauterValue(string value)
    {
        feauterValueText.text = value;
    }

    public void Clear()
    {
        SetFeauterName(string.Empty);

        SetFeauterValue(string.Empty);
    }
}