namespace RecomendationSystem.Encoding
{
    public class FloatStats
    {
        public float Min = float.MaxValue;
        public float Max = float.MinValue;

        public void AddValue(float value)
        {
            if (value < Min)
                Min = value;

            if (value > Max)
                Max = value;
        }

        public float Normalize(float value)
        {
            if (Max - Min == 0f)
                return 0f;

            return (value - Min) / (Max - Min);
        }
    }
}