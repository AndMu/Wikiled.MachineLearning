namespace Wikiled.MachineLearning.Mathematics.Statistics
{
    public struct CorrelationResult
    {
        public double Result;

        public double Normalized;

        public override string ToString()
        {
            return string.Format("{{Result: {0}, Normalized: {1}}}", Result, Normalized);
        }
    }
}
