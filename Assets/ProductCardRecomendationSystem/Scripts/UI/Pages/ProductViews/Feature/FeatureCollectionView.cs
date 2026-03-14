using RecomendationSystem.Data;
using System.Collections.Generic;
using UnityEngine;

public class FeatureCollectionView : MonoBehaviour
{
    [SerializeField]
    private FeatureView featureViewPrefab;
    [SerializeField]
    private Transform container;

    private List<FeatureView> featureViews = new List<FeatureView>();

    private bool isInit;

    public void Init(IReadOnlyList<IFeatureData> features)
    {
        if (isInit)
        {
            Dispose();
        }

        CreateViews(features);

        isInit = true;
    }

    public void Dispose()
    {
        if (!isInit) return;

        DestroyViews();

        isInit = false;
    }

    private void CreateViews(IReadOnlyList<IFeatureData> features)
    {
        foreach (IFeatureData feature in features)
        {
            CreateView(feature);
        }
    }

    private void CreateView(IFeatureData feature)
    {
        FeatureView view = Instantiate(featureViewPrefab, container);
        SetupView(view, feature);

        featureViews.Add(view);
    }

    private void DestroyViews()
    {
        foreach (FeatureView view in featureViews)
        {
            Destroy(view.gameObject);
        }

        featureViews.Clear();
    }

    private void SetupView(FeatureView view, IFeatureData feature)
    {
        view.SetFeauterName(feature.GetKey());
        view.SetFeauterValue(GetFeatureValueText(feature));
    }

    private string GetFeatureValueText(IFeatureData feature)
    {
        FeatureValueType featureType = feature.GetValueType();

        string feauterValueText = string.Empty;

        switch (featureType)
        {
            case FeatureValueType.Float:
                feauterValueText = feature.GetFloatValue().ToString();
                break;
            case FeatureValueType.String:
                feauterValueText = feature.GetStringValue();
                break;
            case FeatureValueType.Bool:
                feauterValueText = feature.GetBoolValue() == true ? "Да" : "Нет";
                break;
            default:
                break;
        }

        return feauterValueText;
    }
}