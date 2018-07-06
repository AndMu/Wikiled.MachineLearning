
using System;

namespace Wikiled.MachineLearning.Mathematics
{
    public class PrecisionStars
    {
        public StatisticsCalculator OneStar { get; } = new StatisticsCalculator();

        public StatisticsCalculator TwoStar { get; } = new StatisticsCalculator();

        public StatisticsCalculator ThreeStar { get; } = new StatisticsCalculator();

        public StatisticsCalculator FourStar { get; } = new StatisticsCalculator();

        public StatisticsCalculator FiveStar { get; } = new StatisticsCalculator();

        public StatisticsCalculator AllStar { get; } = new StatisticsCalculator();

        public void Add(double expected, double? actual)
        {
            if (expected <= 0 || expected > 5)
            {
                throw new ArgumentOutOfRangeException(nameof(expected));
            }

            StatisticsCalculator calculator;
            if (expected > 4.5)
            {
                calculator = FiveStar;
            }
            else if (expected > 3.5)
            {
                calculator = FourStar;
            }
            else if (expected > 2.5)
            {
                calculator = ThreeStar;
            }
            else if (expected > 1.5)
            {
                calculator = TwoStar;
            }
            else
            {
                calculator = OneStar;
            }

            if (actual.HasValue)
            {
                calculator.Add(actual.Value, expected);
                AllStar.Add(actual.Value, expected);
            }
            else
            {
                calculator.AddUnknown();
                AllStar.AddUnknown();
            }
        }
    }
}
