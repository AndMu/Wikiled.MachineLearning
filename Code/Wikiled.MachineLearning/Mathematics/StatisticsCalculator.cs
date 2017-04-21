using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Wikiled.MachineLearning.Mathematics
{
    public class StatisticsCalculator
    {
        private readonly ConcurrentBag<double> rmse = new ConcurrentBag<double>();

        private int correct;

        private int unknown;

        public double Accuracy => correct / (double)Total * 100;

        public double AccuracyWithoutMissing => correct / (double)rmse.Count * 100;

        public double Missing => (double)unknown / Total;

        public int Total => rmse.Count + unknown;

        public static string AggregateOuput(IEnumerable<StatisticsCalculator> calculators)
        {
            var value = calculators.ToArray();
            var total = value.Count();
            return $"Total: {value.Sum(item => item.Total)} RMSE: {value.Sum(item => item.CalculateRmse()) / total:F2} Accuracy: {value.Sum(item => item.Accuracy) / total:F2}% Accuracy (without missing): {value.Sum(item => item.AccuracyWithoutMissing) / total:F2}% Missing {value.Sum(item => item.Missing) / total * 100:F2}%";
        }

        public override string ToString()
        {
            return AggregateOuput(new[] {this});
        }

        public void Add(double actual, double expected)
        {
            var rounded = Math.Round(actual, 0);
            if (expected == rounded)
            {
                correct++;
            }

            var data = Math.Pow(expected - actual, 2);
            rmse.Add(data);
        }

        public void AddUnknown()
        {
            unknown++;
        }

        public double CalculateRmse()
        {
            return Math.Sqrt(rmse.Sum() / rmse.Count());
        }
    }
}
