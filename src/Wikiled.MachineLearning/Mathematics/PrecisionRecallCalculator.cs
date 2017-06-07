using System;
using System.Collections.Concurrent;
using System.Linq;

namespace Wikiled.MachineLearning.Mathematics
{
    public class PrecisionRecallCalculator<T>
        where T : struct
    {
        private readonly ConcurrentBag<Tuple<T, T?>> list = new ConcurrentBag<Tuple<T, T?>>();

        public int Total => list.Count;

        public void Add(T expected, T? actual)
        {
            list.Add(new Tuple<T, T?>(expected, actual));
        }

        public double F1(T classType)
        {
            var sum = GetPrecision(classType) + GetRecall(classType);
            if (sum == 0)
            {
                return 0;
            }

            return list.Count > 0
                       ? 2 * GetPrecision(classType) * GetRecall(classType) / sum
                       : 0;
        }

        public double GetSingleAccuracy(T classType)
        {
            if (list.Count == 0)
            {
                return 0;
            }

            var tp = TruePositive(classType);
            return tp / (tp + FalseNegative(classType));
        }

        public double GetAccuracy(T classType)
        {
            if (list.Count == 0)
            {
                return 0;
            }

            var top = TruePositive(classType) + TrueNegative(classType);
            return top / (top + FalsePositive(classType) + FalseNegative(classType));
        }

        public double GetPrecision(T classType)
        {
            var sum = TruePositive(classType) + FalsePositive(classType);
            if (sum == 0)
            {
                return 0;
            }

            return list.Count > 0
                       ? TruePositive(classType) / sum
                       : 0;
        }

        public double GetRecall(T classType)
        {
            var sum = TruePositive(classType) + FalseNegative(classType);
            if (sum == 0)
            {
                return 0;
            }

            return list.Count > 0
                       ? TruePositive(classType) / sum
                       : 0;
        }

        private double FalseNegative(T classType)
        {
            return list.Count(
                item =>
                item.Item1.Equals(classType) &&
                ((item.Item2.HasValue && !item.Item2.Value.Equals(classType)) || !item.Item2.HasValue));
        }

        private double FalsePositive(T classType)
        {
            return list.Count(item => !item.Item1.Equals(classType) && item.Item2.HasValue && item.Item2.Value.Equals(classType));
        }

        private double TrueNegative(T classType)
        {
            return list.Count(item => !item.Item1.Equals(classType) && item.Item2.HasValue && !item.Item2.Value.Equals(classType));
        }

        private double TruePositive(T classType)
        {
            return list.Count(item => item.Item1.Equals(classType) && item.Item2.HasValue && item.Item2.Value.Equals(classType));
        }
    }
}
