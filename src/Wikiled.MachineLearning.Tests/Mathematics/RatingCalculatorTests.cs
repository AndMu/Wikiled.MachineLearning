using System;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using Wikiled.MachineLearning.Mathematics;

namespace Wikiled.MachineLearning.Tests.Mathematics
{
    [TestFixture]
    public class RatingCalculatorTests
    {
        [Test]
        public void ConvertToRaw()
        {
            ClassicAssert.AreEqual(4, RatingCalculator.CalculateStar(0.5));
            ClassicAssert.AreEqual(0.5, RatingCalculator.ConvertToRaw(4));
            ClassicAssert.AreEqual(-1, RatingCalculator.ConvertToRaw(1));
        }

        [Test]
        public void Calculate()
        {
            var value = RatingCalculator.Calculate(10, 0);
            ClassicAssert.AreEqual(1, value);

            value = RatingCalculator.Calculate(10, 10);
            ClassicAssert.AreEqual(0, value);

            value = RatingCalculator.Calculate(0, 10);
            ClassicAssert.AreEqual(-1, value);

            value = RatingCalculator.Calculate(1, 3);
            ClassicAssert.AreEqual(-0.792, Math.Round(value, 3));

            value = RatingCalculator.Calculate(3, 1);
            ClassicAssert.AreEqual(0.792, Math.Round(value, 3));

            value = RatingCalculator.Calculate(1000, 1);
            ClassicAssert.AreEqual(1, value);
        }

        [Test]
        public void CalculateStar()
        {
            ClassicAssert.AreEqual(1, RatingCalculator.CalculateStar(-1));
            ClassicAssert.AreEqual(5, RatingCalculator.CalculateStar(1));
            ClassicAssert.IsNull(RatingCalculator.CalculateStar(null));
            ClassicAssert.AreEqual(3, RatingCalculator.CalculateStar(0));
            ClassicAssert.AreEqual(4, RatingCalculator.CalculateStar(0.5));
            ClassicAssert.AreEqual(2, RatingCalculator.CalculateStar(-0.5));
        }
    }
}
