using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Wikiled.MachineLearning.Normalization;

namespace Wikiled.MachineLearning.Tests.Normalization
{
    [TestFixture]
    public class SimpleNormalizationTests
    {
        [Test]
        public void Windowed()
        {
            double[] data = { 1, 2, 2, 3, 4, 10 };
            var result = Enumerable.ToArray<double[]>(data.Windowed(2));
            Assert.AreEqual(5, result.Length);
            Assert.AreEqual(1, result[0][0]);
            Assert.AreEqual(2, result[0][1]);
            Assert.AreEqual(4, result[4][0]);
            Assert.AreEqual(10, result[4][1]);
        }

        [Test]
        public void WindowedEx()
        {
            double[] data = { 1, 2, 2, 3, 4, 10 };
            var result = Enumerable.ToArray<double[]>(data.WindowedEx(2, null));
            Assert.AreEqual(5, result.Length);
            Assert.AreEqual(1, result[0][0]);
            Assert.AreEqual(2, result[0][1]);
            Assert.AreEqual(4, result[4][0]);
            Assert.AreEqual(10, result[4][1]);
        }

        [Test]
        public void WindowedExWithCondition()
        {
            double[] data = { 1, 2, 2, 3, 4, 10 };
            var result = Enumerable.ToArray<double[]>(data.WindowedEx(2, collected => Enumerable.Sum((IEnumerable<double>)collected) >= 4));
            Assert.AreEqual(4, result.Length);
            Assert.AreEqual(1, result[0][0]);
            Assert.AreEqual(2, result[0][1]);
            Assert.AreEqual(2, result[0][2]);
            Assert.AreEqual(4, result[3][0]);
            Assert.AreEqual(10, result[3][1]);
        }

        [Test]
        public void MovingAverage()
        {
            double[] data = { 1, 2, 2, 3, 4, 10 };
            var result = Enumerable.ToArray<double>(data.MovingAverage(2));
            Assert.AreEqual(5, result.Length);
            Assert.AreEqual(1.5, result[0]);
            Assert.AreEqual(2, result[1]);
            Assert.AreEqual(2.5, result[2]);
            Assert.AreEqual(3.5, result[3]);
            Assert.AreEqual(7, result[4]);
        }

        [Test]
        public void MovingAverage2()
        {
            double[] data = { 1.2640, 1.2641, 1.2642, 1.2641, 3 };
            var result = Enumerable.ToArray<double>(data.MovingAverage(4));
            Assert.AreEqual(2, result.Length);
            Assert.AreEqual(1.2641, result[0]);
            Assert.AreEqual(1.6981, result[1]);
        }

        [Test]
        public void ExponentialMovingAverage()
        {
            double[] data = { 22.27, 22.19, 22.08, 22.17, 22.18, 22.13, 22.23, 22.43, 22.24, 22.29, 22.15, 22.39, 22.38, 22.61, 23.36, 24.05};
            var result = Enumerable.ToArray<double>(data.ExponentialMovingAverage(10));
            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(22.22, Math.Round(result[0], 2));
            Assert.AreEqual(22.21, Math.Round(result[1], 2));
            Assert.AreEqual(22.24, Math.Round(result[2], 2));
            Assert.AreEqual(22.27, Math.Round(result[3], 2));
            Assert.AreEqual(22.33, Math.Round(result[4], 2));
            Assert.AreEqual(22.52, Math.Round(result[5], 2));
            Assert.AreEqual(22.80, Math.Round(result[6], 2));

        }

        [Test]
        public void WeightedMovingAverage()
        {
            double[] data = { 1.2900, 1.2900, 1.2903, 1.2904, 3 };
            var result = Enumerable.ToArray<double>(data.WeightedMovingAverage(4));
            Assert.AreEqual(2, result.Length);
            Assert.AreEqual(1.29025, result[0]);
            Assert.AreEqual(1.9742, Math.Round(result[1], 4));
        }

        [Test]
        public void MeanNormalized()
        {
            double[] data = { 2, 2, 2, 3, 3, 3 };
            var result = Enumerable.ToArray<double>(data.MeanNormalized());
            Assert.AreEqual(6, result.Length);
            Assert.AreEqual(-0.5, result[0]);
            Assert.AreEqual(-0.5, result[1]);
            Assert.AreEqual(-0.5, result[2]);
            Assert.AreEqual(0.5, result[3]);
            Assert.AreEqual(0.5, result[4]);
            Assert.AreEqual(0.5, result[5]);
        }

        [Test]
        public void MeanNormalizedPositive()
        {
            double[] data = { 2, 2, 2, 3, 3, 3 };
            var result = Enumerable.ToArray<double>(data.MeanNormalized(true));
            Assert.AreEqual(6, result.Length);
            Assert.AreEqual(0, result[0]);
            Assert.AreEqual(0, result[1]);
            Assert.AreEqual(0, result[2]);
            Assert.AreEqual(1, result[3]);
            Assert.AreEqual(1, result[4]);
            Assert.AreEqual(1, result[5]);
        }

        [Test]
        public void MeanNormalizedlarge()
        {
            double[] data = { 2000, 3000, 1000 };
            var result = Enumerable.ToArray<double>(data.MeanNormalized());
            Assert.AreEqual(3, result.Length);
            Assert.AreEqual(0, result[0]);
            Assert.AreEqual(0.5, result[1]);
            Assert.AreEqual(-0.5, result[2]);
        }
    }
}
