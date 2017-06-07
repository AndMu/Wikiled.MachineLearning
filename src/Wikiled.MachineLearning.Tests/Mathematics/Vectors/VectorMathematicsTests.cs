using System;
using System.Collections.Generic;
using NUnit.Framework;
using Wikiled.Arff.Normalization;
using Wikiled.MachineLearning.Mathematics.Vectors;

namespace Wikiled.MachineLearning.Tests.Mathematics.Vectors
{
    [TestFixture]
    public class VectorMathematicsTests
    {
        [Test]
        public void SimpleSum()
        {
            List<VectorData> vectors = new List<VectorData>();
            vectors.Add(VectorDataFactory.Instance.CreateSimple(NormalizationType.None, 1, 2, 3));
            vectors.Add(VectorDataFactory.Instance.CreateSimple(NormalizationType.None, -2, 0, 4));
            VectorData sumVector = vectors.Sum(NormalizationType.None);
            Assert.AreEqual(-1, sumVector[0].X);
            Assert.AreEqual(2, sumVector[1].X);
            Assert.AreEqual(7, sumVector[2].X);
        }

        [Test]
        public void NormalizedSum()
        {
            List<VectorData> vectors = new List<VectorData>();
            vectors.Add(VectorDataFactory.Instance.CreateSimple(NormalizationType.None, 1, 2, 3));
            vectors.Add(VectorDataFactory.Instance.CreateSimple(NormalizationType.None, -2, 0, 4));
            VectorData sumVector = vectors.Sum(NormalizationType.L2);
            Assert.AreEqual(-0.14, Math.Round(sumVector[0].X, 2));
            Assert.AreEqual(0.27, Math.Round(sumVector[1].X, 2));
            Assert.AreEqual(0.95, Math.Round(sumVector[2].X, 2));
        }

        [Test]
        public void DotProduct()
        {
            List<VectorData> vectors = new List<VectorData>();
            vectors.Add(VectorDataFactory.Instance.CreateSimple(NormalizationType.None, 1, 3, -5));
            vectors.Add(VectorDataFactory.Instance.CreateSimple(NormalizationType.None, 4, -2, -1));
            var product = vectors.DotProduct();
            Assert.AreEqual(3, product);
        }

        [Test]
        public void DotProductSparse()
        {
            VectorData vector1 = new VectorData(
                new[]
                    {
                        new VectorCell(0, new SimpleCell("0", 1), 0),
                        new VectorCell(3, new SimpleCell("3", 3), 0),
                        new VectorCell(4, new SimpleCell("4", -5), 0),
                    },
                5,
                NormalizationType.None);
            VectorData vector2 = new VectorData(
                new[]
                    {
                        new VectorCell(0, new SimpleCell("0", 4), 0),
                        new VectorCell(3, new SimpleCell("3", -2), 0),
                        new VectorCell(4, new SimpleCell("4", -1), 0),
                    },
                5,
                NormalizationType.None);
            var product = VectorMathematics.DotProduct(vector1, vector2);
            Assert.AreEqual(3, product);
        }
    }
}
