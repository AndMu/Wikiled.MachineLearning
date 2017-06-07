using System;
using NUnit.Framework;
using Wikiled.MachineLearning.Mathematics.Statistics;

namespace Wikiled.MachineLearning.Tests.Mathematics.Statistics
{
    [TestFixture]
    public class SimpleCrossCorrelationTest
    {
        readonly double[] signal1 = { 4, 2, -1, 3, -2, -6, -5, 4, 5 };
        readonly double[] signal2 = { -4, 1, 3, 7, 4, -2, -8, -2, 1 };

        readonly double[] x1 = { 0, 3, 5, 5, 5, 2, 0.5, 0.25, 0 };
        readonly double[] x2 = { 1, 1, 1, 1, 1, 0, 0, 0, 0 };
        readonly double[] x3 = { 0, 9, 15, 15, 15, 6, 1.5, 0.75, 0 };
        readonly double[] x4 = { 2, 2, 2, 2, 2, 0, 0, 0, 0 };

        [Test]
        public void CalculateDefault()
        {
            var result = SimpleCrossCorrelation.Calculate(signal1, signal2);
            Assert.AreEqual(45, result.Result);
            Assert.AreEqual(0.3013, Math.Round(result.Normalized, 4));
        }

        [Test]
        public void CalculateShifted()
        {
            var result = SimpleCrossCorrelation.Calculate(signal1, signal2, 3);
            Assert.AreEqual(12, result.Result);
            Assert.AreEqual(0.0804d, Math.Round(result.Normalized, 4));
        }

        [Test]
        public void CalculateX()
        {
            var result = SimpleCrossCorrelation.Calculate(x1, x2, 1);
            Assert.AreEqual(13, result.Result);
            Assert.AreEqual(0.6187, Math.Round(result.Normalized, 4));

            result = SimpleCrossCorrelation.Calculate(x3, x4, 1);
            Assert.AreEqual(0.6187, Math.Round(result.Normalized, 4));
        }
    }
}