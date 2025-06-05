using System;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using Wikiled.MachineLearning.Mathematics.Statistics;

namespace Wikiled.MachineLearning.Tests.Mathematics.Statistics
{
    [TestFixture]
    public class FullCrossCorrelationTests
    {
        readonly double[] signal1 = { 4, 2, -1, 3, -2, -6, -5, 4, 5 };
        readonly double[] signal2 = { -4, 1, 3, 7, 4, -2, -8, -2, 1 };

        [Test]
        public void Calculate()
        {
            FullCrossCorrelation correlation = new FullCrossCorrelation(false, signal1, signal2);
            correlation.Calculate();
            ClassicAssert.AreEqual(-1, correlation.Offset);
            ClassicAssert.AreEqual(81, correlation.Result.Result);
            ClassicAssert.AreEqual(0.5424, Math.Round(correlation.Result.Normalized, 4));
            ClassicAssert.AreEqual(39, correlation.Auto);
        }
    }
}
