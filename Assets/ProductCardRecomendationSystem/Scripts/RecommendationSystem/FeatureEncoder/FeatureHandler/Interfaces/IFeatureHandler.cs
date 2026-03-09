using RecomendationSystem.Data;

namespace RecomendationSystem.Encoding
{
    public interface IFeatureHandler
    {
        string BuildKey(IFeatureData feature);

        float ResolveValue(IFeatureData feature);

        void UpdateStats(IFeatureData feature);
    }
}