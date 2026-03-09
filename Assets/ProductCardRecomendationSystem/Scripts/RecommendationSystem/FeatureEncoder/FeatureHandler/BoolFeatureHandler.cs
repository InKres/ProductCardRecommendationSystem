using RecomendationSystem.Data;

namespace RecomendationSystem.Encoding
{
    public class BoolFeatureHandler : IFeatureHandler
    {
        public string BuildKey(IFeatureData feature)
        {
            return feature.GetKey() + ":" + feature.GetBoolValue();
        }

        public void UpdateStats(IFeatureData feature)
        {
        }

        public float ResolveValue(IFeatureData feature)
        {
            if (feature.GetBoolValue())
            {
                return 1f;
            }

            return 0f;
        }
    }
}
