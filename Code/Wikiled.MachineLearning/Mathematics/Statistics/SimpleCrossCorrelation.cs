using System;
using System.Linq;

namespace Wikiled.MachineLearning.Mathematics.Statistics
{
    public static class SimpleCrossCorrelation
    {
        public static CorrelationResult Calculate(double[] signal1, double[] signal2, int delay = 0)
        {
            /* Calculate the denominator */
            var denom1 = signal1.Sum(x => x * x);
            var denom2 = signal2.Sum(y => y * y);

            var denom = Math.Sqrt(denom1 * denom2);

            var result = Shift(signal1, signal2, delay);
            signal1 = result.Item1;
            signal2 = result.Item2;
            int minLength = result.Item3;

            double sum = 0;
            for (int i = 0; i < minLength; i++)
            {
                sum += signal1[i] * signal2[i];
            }

            return new CorrelationResult
            {
                Result = sum,
                Normalized = sum / denom
            };

            /* r is the correlation coefficient at "delay" */
        }

        private static Tuple<double[], double[], int> Shift(double[] signal1, double[] signal2, int delay)
        {
            if (delay > 0)
            {
                signal2 = signal2.Skip(delay).ToArray();
            }
            else if (delay < 0)
            {
                signal1 = signal1.Skip(-delay).ToArray();
            }

            var min = Math.Min(signal1.Length, signal2.Length);
            return new Tuple<double[], double[], int>(signal1, signal2, min);
        }
    }
}