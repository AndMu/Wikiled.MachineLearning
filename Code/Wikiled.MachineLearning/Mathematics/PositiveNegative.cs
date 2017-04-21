using Wikiled.Arff.Persistence;

namespace Wikiled.MachineLearning.Mathematics
{
    public class PositiveNegative
    {
        public StatisticsCalculator All { get; } = new StatisticsCalculator();

        public StatisticsCalculator Negative { get; } = new StatisticsCalculator();

        public StatisticsCalculator Positive { get; } = new StatisticsCalculator();

        public void Add(PositivityType expected, PositivityType actual)
        {
            if (expected == PositivityType.Neutral)
            {
                return;
            }

            StatisticsCalculator calculator = null;
            if (expected == PositivityType.Positive)
            {
                calculator = Positive;
            }
            else if (expected == PositivityType.Negative)
            {
                calculator = Negative;
            }

            if (actual != PositivityType.Neutral)
            {
                double expectedV = expected == PositivityType.Positive ? 1 : 0;
                double actualV = actual == PositivityType.Positive ? 1 : 0;
                calculator.Add(actualV, expectedV);
                All.Add(actualV, expectedV);
            }
            else
            {
                calculator.AddUnknown();
                All.AddUnknown();
            }
        }
    }
}
