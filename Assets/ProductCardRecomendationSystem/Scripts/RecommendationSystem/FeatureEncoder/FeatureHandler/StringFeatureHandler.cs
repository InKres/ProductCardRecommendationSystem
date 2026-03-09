using RecomendationSystem.Data;

namespace RecomendationSystem.Encoding
{
    public class StringFeatureHandler : IFeatureHandler
    {
        public string BuildKey(IFeatureData feature)
        {
            return feature.GetKey() + ":" + feature.GetStringValue();
        }

        public void UpdateStats(IFeatureData feature)
        {
        }

        public float ResolveValue(IFeatureData feature)
        {
            return 1f;
        }
    }
}