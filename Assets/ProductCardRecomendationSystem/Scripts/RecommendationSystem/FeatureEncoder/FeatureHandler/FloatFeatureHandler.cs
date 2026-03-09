using System.Collections.Generic;
using RecomendationSystem.Data;

namespace RecomendationSystem.Encoding
{
    public class FloatFeatureHandler : IFeatureHandler
    {
        private Dictionary<string, FloatStats> featureKeyToFloatStats;

        public FloatFeatureHandler(Dictionary<string, FloatStats> featureKeyToFloatStats)
        {
            this.featureKeyToFloatStats = featureKeyToFloatStats;
        }

        public string BuildKey(IFeatureData feature)
        {
            return feature.GetKey();
        }

        public void UpdateStats(IFeatureData feature)
        {
            string key = feature.GetKey();

            FloatStats stats;

            if (!featureKeyToFloatStats.TryGetValue(key, out stats))
            {
                stats = new FloatStats();
                featureKeyToFloatStats.Add(key, stats);
            }

            stats.AddValue(feature.GetFloatValue());
        }

        public float ResolveValue(IFeatureData feature)
        {
            string key = feature.GetKey();

            FloatStats stats;

            if (featureKeyToFloatStats.TryGetValue(key, out stats))
            {
                return stats.Normalize(feature.GetFloatValue());
            }

            return feature.GetFloatValue();
        }
    }
}
