using System;
using System.Collections.Generic;
using System.Linq;

namespace Wikiled.MachineLearning.Normalization
{
    public static class SimpleNormalization
    {
        public static T[] ArrayInit<T>(int size, Func<int, T> func)
        {
            var output = new T[size];
            for (var i = 0; i < size; i++)
            {
                output[i] = func(i);
            }

            return output;
        }

        public static IEnumerable<double> ExponentialMovingAverage(this IEnumerable<double> list, int windowSize)
        {
            double multiplier = 2 / (double)(windowSize + 1);
            double? previousExponentialMovingAverage = null;
            foreach (var window in list.Windowed(windowSize))
            {
                double exponentialMovingAverage;
                if (previousExponentialMovingAverage == null)
                {
                    exponentialMovingAverage = window.Average(x => x);
                }
                else
                {
                    double lastValue = window[window.Length - 1];
                    exponentialMovingAverage = (lastValue - previousExponentialMovingAverage.Value) * multiplier +
                                               previousExponentialMovingAverage.Value;
                }

                previousExponentialMovingAverage = exponentialMovingAverage;
                yield return exponentialMovingAverage;
            }
        }

        public static IEnumerable<double> MeanNormalized(this IEnumerable<double> list, bool positive = false)
        {
            var data = list.ToArray();
            if (data.Length <= 1)
            {
                return data;
            }

            double suma = 0;
            int total = 0;
            double min = 0;
            double max = 0;
            foreach (var item in data)
            {
                total++;
                if (total == 1 ||
                    item < min)
                {
                    min = item;
                }

                if (total == 1 ||
                    item > max)
                {
                    max = item;
                }

                suma += item;
            }

            var mean = suma / data.Length;
            var deviation = max - min;
            if (deviation == 0)
            {
                return data;
            }

            var substractor = positive ? min : mean;
            return data.Select(item => (item - substractor) / deviation);
        }

        public static IEnumerable<double> MovingAverage(this IEnumerable<double> list, int windowSize)
        {
            return list.Windowed(windowSize).Select(item => item.Average(x => x));
        }

        public static IEnumerable<double> WeightedMovingAverage(this IEnumerable<double> list, int windowSize)
        {
            foreach (var window in list.Windowed(windowSize))
            {
                double numinator = 0;
                double denuminator = 0;
                for (int i = 0; i < window.Length; i++)
                {
                    int index = i + 1;
                    numinator += index * window[i];
                    denuminator += index;
                }

                yield return numinator / denuminator;
            }
        }

        public static IEnumerable<T[]> Windowed<T>(this IEnumerable<T> list, int windowSize)
        {
            // Checks elided
            var window = new T[windowSize];
            int r = windowSize - 1;
            int i = 0;
            using (var e = list.GetEnumerator())
            {
                while (e.MoveNext())
                {
                    window[i] = e.Current;
                    i = (i + 1) % windowSize;
                    if (r == 0)
                    {
                        int id = i;
                        yield return ArrayInit(windowSize, j => window[(id + j) % windowSize]);
                    }
                    else
                    {
                        r = r - 1;
                    }
                }
            }
        }

        /// <summary>
        ///     Advanced and more expensive windowing version
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="windowSize"></param>
        /// <param name="additionalCondition"></param>
        /// <returns></returns>
        public static IEnumerable<T[]> WindowedEx<T>(this IEnumerable<T> list, int windowSize, Func<IEnumerable<T>, bool> additionalCondition)
        {
            List<T> items = new List<T>();
            foreach (var item in list)
            {
                if (items.Count >= windowSize &&
                    (additionalCondition == null || additionalCondition(items)))
                {
                    yield return items.ToArray();
                    while (items.Count >= windowSize)
                    {
                        items.RemoveAt(0);
                    }
                }

                items.Add(item);
            }

            if (items.Count > 0)
            {
                yield return items.ToArray();
            }
        }
    }
}
