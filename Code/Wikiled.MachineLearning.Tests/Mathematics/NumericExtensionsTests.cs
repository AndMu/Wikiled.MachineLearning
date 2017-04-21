using System;
using NUnit.Framework;
using Wikiled.MachineLearning.Mathematics;
using Wikiled.MachineLearning.Mathematics.Vectors;

namespace Wikiled.MachineLearning.Tests.Mathematics
{
    [TestFixture]
    public class NumericExtensionsTests
    {
        private static readonly object[] dates =
        {
            new object[] {new DateTime(1978, 01, 01), 0.02, 1},
            new object[] {new DateTime(1978, 03, 01), 0.86, 0.51},
            new object[] {new DateTime(1978, 06, 01), 0.5, -0.87},
            new object[] {new DateTime(1978, 09, 01), -0.87, -0.49},
            new object[] {new DateTime(1978, 11, 01), -0.86, 0.51},
            new object[] {new DateTime(1978, 12, 31), 0, 1}
        };

        private static readonly object[] datesWeeks =
        {
            new object[] {new DateTime(1978, 01, 01), 0, 1},
            new object[] {new DateTime(1978, 03, 01), 0.43, -0.9},
            new object[] {new DateTime(1978, 03, 04), -0.78, 0.62},
        };

        private static readonly object[] datesMonths=
        {
            new object[] {new DateTime(1978, 01, 01), 0.2, 0.98},
            new object[] {new DateTime(1978, 02, 28), 0, 1},
            new object[] {new DateTime(1978, 03, 08), 1, -0.05},
        };


        [TestCase(0, 0)]
        [TestCase(90, 3.14)]
        [TestCase(180, 4.71)]
        [TestCase(270, 5)]
        public void ToRadians(double degrees, double value)
        {
            Assert.AreEqual(value, Math.Round(degrees.ToRadians()), 2);
        }

        [TestCase(0, 0, 1)]
        [TestCase(90, 1, 0)]
        [TestCase(180, 0, -1)]
        [TestCase(270, -1, 0)]
        public void GetVector(double degrees, double x, double y)
        {
            Vector2 result = NumericExtensions.GetVector(degrees);
            Assert.AreEqual(x, Math.Round(result.X, 2));
            Assert.AreEqual(y,  Math.Round(result.Y, 2));
        }

        [Test, TestCaseSource("dates")]
        public void GetVectorDate(DateTime date, double x, double y)
        {
            Vector2 result = NumericExtensions.GetYearVector(date);
            Assert.AreEqual(x, Math.Round(result.X, 2));
            Assert.AreEqual(y, Math.Round(result.Y, 2));
        }

        [Test, TestCaseSource("datesWeeks")]
        public void GetWeekVector(DateTime date, double x, double y)
        {
            Vector2 result = NumericExtensions.GetWeekVector(date);
            Assert.AreEqual(x, Math.Round(result.X, 2));
            Assert.AreEqual(y, Math.Round(result.Y, 2));
        }

        [Test, TestCaseSource("datesMonths")]
        public void GetMonthVector(DateTime date, double x, double y)
        {
            Vector2 result = NumericExtensions.GetMonthVector(date);
            Assert.AreEqual(x, Math.Round(result.X, 2));
            Assert.AreEqual(y, Math.Round(result.Y, 2));
        }
    }
}
