using System.Collections.Generic;
using RecomendationSystem.Data;

namespace RecomendationSystem.Encoding
{
    public class FeatureEncoder : IFeatureEncoder
    {
        private Dictionary<string, int> featureKeyToIndex = new Dictionary<string, int>();

        private Dictionary<string, FloatStats> featureKeyToFloatStats = new Dictionary<string, FloatStats>();

        private Dictionary<FeatureValueType, IFeatureHandler> featureTypeToHandler;

        private int vectorSize;

        public FeatureEncoder()
        {
            InitializeHandlers();
        }

        public void Fit(IReadOnlyList<IProductData> products)
        {
            featureKeyToIndex.Clear();
            featureKeyToFloatStats.Clear();

            int index = 0;

            foreach (IProductData product in products)
            {
                index = ProcessProduct(product, index);
            }

            vectorSize = index;
        }

        public float[] Encode(IProductData product)
        {
            float[] vector = new float[vectorSize];

            IReadOnlyList<IFeatureData> features = product.GetFeatures();

            foreach (IFeatureData feature in features)
            {
                EncodeFeature(feature, vector);
            }

            return vector;
        }

        public int GetVectorSize()
        {
            return vectorSize;
        }

        private void InitializeHandlers()
        {
            featureTypeToHandler = new Dictionary<FeatureValueType, IFeatureHandler>();

            featureTypeToHandler.Add(
                FeatureValueType.Float,
                new FloatFeatureHandler(featureKeyToFloatStats)
            );

            featureTypeToHandler.Add(
                FeatureValueType.Bool,
                new BoolFeatureHandler()
            );

            featureTypeToHandler.Add(
                FeatureValueType.String,
                new StringFeatureHandler()
            );
        }

        private int ProcessProduct(IProductData product, int currentIndex)
        {
            IReadOnlyList<IFeatureData> features = product.GetFeatures();

            foreach (IFeatureData feature in features)
            {
                IFeatureHandler handler = GetHandler(feature);

                handler.UpdateStats(feature);

                string featureKey = handler.BuildKey(feature);

                if (!featureKeyToIndex.ContainsKey(featureKey))
                {
                    featureKeyToIndex.Add(featureKey, currentIndex);
                    currentIndex++;
                }
            }

            return currentIndex;
        }

        private void EncodeFeature(IFeatureData feature, float[] vector)
        {
            IFeatureHandler handler = GetHandler(feature);

            string featureKey = handler.BuildKey(feature);

            int index;

            if (!featureKeyToIndex.TryGetValue(featureKey, out index))
            {
                return;
            }

            float value = handler.ResolveValue(feature);

            vector[index] = value;
        }

        private IFeatureHandler GetHandler(IFeatureData feature)
        {
            FeatureValueType type = feature.GetValueType();

            IFeatureHandler handler;

            if (!featureTypeToHandler.TryGetValue(type, out handler))
            {
                throw new System.Exception("No handler registered for feature type: " + type);
            }

            return handler;
        }
    }
}