using System;
using System.Linq;
using NUnit.Framework;
using Wikiled.MachineLearning.Normalization;

namespace Wikiled.MachineLearning.Tests.Normalization
{
    [TestFixture]
    public class NormalizationTests
    {
        [Test]
        public void L2Normalize()
        {
            var normalization = new double[] { 1, 2, 3 }.Normalize(NormalizationType.L2);
            var vector = normalization.GetNormalized.ToArray();
            Assert.AreEqual(0.2673, Math.Round(vector[0], 4));
            Assert.AreEqual(0.5345, Math.Round(vector[1], 4));
            Assert.AreEqual(0.8018, Math.Round(vector[2], 4));
        }

        [Test]
        public void L1NormalizeNegative()
        {
            var normalization = new double[] { -1, 2, -3 }.Normalize(NormalizationType.L1);
            var vector = normalization.GetNormalized.ToArray();
            Assert.AreEqual(-0.1667, Math.Round(vector[0], 4));
            Assert.AreEqual(0.3333, Math.Round(vector[1], 4));
            Assert.AreEqual(-0.5, Math.Round(vector[2], 4));
        }

        [Test]
        public void L1Normalize()
        {
            var normalization = new double[] { 1, 2, 3 }.Normalize(NormalizationType.L1);
            var vector = normalization.GetNormalized.ToArray();
            Assert.AreEqual(0.1667, Math.Round(vector[0], 4));
            Assert.AreEqual(0.3333, Math.Round(vector[1], 4));
            Assert.AreEqual(0.5, Math.Round(vector[2], 4));
        }

        [Test]
        public void ElasticNormalize()
        {
            var normalization = new double[] { 1, 2, 3 }.Normalize(NormalizationType.Elastic);
            var vector = normalization.GetNormalized.ToArray();
            Assert.AreEqual(0.2451, Math.Round(vector[0], 4));
            Assert.AreEqual(0.4901, Math.Round(vector[1], 4));
            Assert.AreEqual(0.7352, Math.Round(vector[2], 4));
        }

        [Test]
        public void ZeroNormalize()
        {
            var normalization = new double[] { 0 }.Normalize(NormalizationType.L2);
            var vector = normalization.GetNormalized.ToArray();
            Assert.AreEqual(0, Math.Round(vector[0], 4));
            Assert.AreEqual(1, normalization.Coeficient);
        }
    }
}
