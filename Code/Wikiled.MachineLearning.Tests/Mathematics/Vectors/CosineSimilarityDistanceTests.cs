using System;
using NUnit.Framework;
using Wikiled.Arff.Normalization;
using Wikiled.MachineLearning.Mathematics.Vectors;

namespace Wikiled.MachineLearning.Tests.Mathematics.Vectors
{
    [TestFixture]
    public class CosineSimilarityDistanceTests
    {
        [Test]
        public void TestSimilar()
        {
            VectorData vector1 = VectorDataFactory.Instance.CreateSimple(NormalizationType.None, 1, 1, 1);
            VectorData vector2 = VectorDataFactory.Instance.CreateSimple(NormalizationType.None, 2, 2, 2);
            double similarity = CosineSimilarityDistance.Instance.Measure(vector1, vector2);
            Assert.AreEqual(1, Math.Round(similarity, 4));
        }

        [Test]
        public void TestSimilarNormalized()
        {
            VectorData vector1 = VectorDataFactory.Instance.CreateSimple(NormalizationType.L2, 1, 1, 1);
            VectorData vector2 = VectorDataFactory.Instance.CreateSimple(NormalizationType.L2, 2, 2, 2);
            double similarity = CosineSimilarityDistance.Instance.Measure(vector1, vector2);
            Assert.AreEqual(1, Math.Round(similarity, 4));
        }

        [Test]
        public void TestOposite()
        {
            VectorData vector1 = VectorDataFactory.Instance.CreateSimple(NormalizationType.None, 1, 1, 1);
            VectorData vector2 = VectorDataFactory.Instance.CreateSimple(NormalizationType.None, -1, -1, -1);
            double similarity = CosineSimilarityDistance.Instance.Measure(vector1, vector2);
            Assert.AreEqual(-1, Math.Round(similarity, 4));
        }
    }
}
