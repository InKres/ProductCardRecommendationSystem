namespace RecomendationSystem.Data
{
    public interface IFeatureData
    {
        string GetKey();
        FeatureValueType GetValueType();

        float GetFloatValue();
        string GetStringValue();
        bool GetBoolValue();
    }
}