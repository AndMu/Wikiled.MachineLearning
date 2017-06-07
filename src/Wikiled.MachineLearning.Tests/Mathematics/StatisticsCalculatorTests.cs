using System;
using NUnit.Framework;
using Wikiled.MachineLearning.Mathematics;

namespace Wikiled.MachineLearning.Tests.Mathematics
{
    [TestFixture]
    public class StatisticsCalculatorTests
    {
        private StatisticsCalculator calculator;

        [SetUp]
        public void Setup()
        {
            calculator = new StatisticsCalculator();
        }

        [Test]
        public void TestEmpty()
        {
            Assert.AreEqual(Double.NaN, Math.Round(calculator.CalculateRmse(), 2));
            Assert.AreEqual(
                "Total: 0 RMSE: NaN Accuracy: NaN% Accuracy (without missing): NaN% Missing NaN%",
                StatisticsCalculator.AggregateOuput(new[] {calculator}));

        }

        [Test]
        public void First()
        {
            Add(1, 195, 77, 46, 16, 30);
            Assert.AreEqual(1.56, Math.Round(calculator.CalculateRmse(), 2));
        }

        [Test]
        public void Second()
        {
            Add(2, 90, 124, 80, 44, 29);
            Assert.AreEqual(1.29, Math.Round(calculator.CalculateRmse(), 2));
        }

        [Test]
        public void Third()
        {
            Add(3, 47, 90, 118, 73, 38);
            Assert.AreEqual(1.17, Math.Round(calculator.CalculateRmse(), 2));
        }

        [Test]
        public void Four()
        {
            Add(4, 29, 40, 72, 162, 63);
            Assert.AreEqual(1.23, Math.Round(calculator.CalculateRmse(), 2));
        }

        [Test]
        public void Five()
        {
            Add(5, 48, 26, 48, 82, 164);
            Assert.AreEqual(1.86, Math.Round(calculator.CalculateRmse(), 2));
        }

        [Test]
        public void All()
        {
            Add(1, 195, 77, 46, 16, 30);
            Add(2, 90, 124, 80, 44, 29);
            Add(3, 47, 90, 118, 73, 38);
            Add(4, 29, 40, 72, 162, 63);
            Add(5, 48, 26, 48, 82, 164);
            Assert.AreEqual(1.45, Math.Round(calculator.CalculateRmse(), 2));
        }

        [Test]
        public void All2()
        {
            Add(1, 0, 10, 24, 3, 0);
            Add(2, 0, 68, 482, 17, 0);
            Add(3, 0, 16, 2071, 311, 0);
            Add(4, 0, 2, 505, 1245, 0);
            Add(5, 0, 0, 22, 219, 5);
            Assert.AreEqual(0.6, Math.Round(calculator.CalculateRmse(), 2));
        }

        [Test]
        public void All3()
        {
            Add(1, 0, 5, 317, 42, 0);
            Add(2, 0, 5, 315, 47, 0);
            Add(3, 0, 0, 306, 60, 0);
            Add(4, 0, 0, 248, 118, 0);
            Add(5, 0, 1, 258, 109, 5);
            Assert.AreEqual(1.4, Math.Round(calculator.CalculateRmse(), 2));
        }

        [Test]
        public void All4()
        {
            Add(1, 1, 5, 22, 4, 5);
            Add(2, 39, 129, 258, 40, 101);
            Add(3, 95, 448, 1272, 128, 455);
            Add(4, 41, 201, 869, 143, 498);
            Add(5, 3, 31, 112, 15, 85);
            Assert.AreEqual(1.24, Math.Round(calculator.CalculateRmse(), 2));
        }

        private void Add(int expected, params int[] data)
        {
            for (int i = 0; i < 5; i++)
            {
                Add(data[i], expected, i + 1);
            }
        }

        private void Add(int total, int correct, int current)
        {
            for (int i = 0; i < total; i++)
            {
                calculator.Add(correct, current);
            }
        }
    }
}
