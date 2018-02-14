using System;
using System.Collections.Generic;
using NUnit.Framework;
using Wikiled.Arff.Normalization;
using Wikiled.Common.Serialization;
using Wikiled.MachineLearning.Mathematics.Vectors;

namespace Wikiled.MachineLearning.Tests.Mathematics.Vectors
{
    [TestFixture]
    public class DataSetNormalizationTests
    {
        private List<VectorData> vectors;

        private VectorDataFactory vectorDataFactory;

        [SetUp]
        public void Setup()
        {
            vectors = new List<VectorData>();
            vectorDataFactory = new VectorDataFactory();
            vectors.Add(vectorDataFactory.CreateSimple(NormalizationType.None,
                                                                new SimpleCell("1", 1000),
                                                                new SimpleCell("2", 2),
                                                                new SimpleCell("3", 0.3)));
            vectors.Add(vectorDataFactory.CreateSimple(NormalizationType.None,
                                                                new SimpleCell("1", 2000),
                                                                new SimpleCell("2", 3),
                                                                new SimpleCell("3", 0.1)));
            vectors.Add(vectorDataFactory.CreateSimple(NormalizationType.None,
                                                                new SimpleCell("1", 3000),
                                                                new SimpleCell("2", 1),
                                                                new SimpleCell("3", 0.2)));
        }

        [Test]
        public void Serialize()
        {
            var document = vectors.XmlSerialize();
            Assert.IsNotNull(document);
            var result = document.XmlDeserialize<List<VectorData>>();
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(3, result[0].Length);
            Assert.AreEqual(1000, result[0][0].Calculated);
        }

        [Test]
        public void MeanNormalize()
        {
            DataSetNormalization.MeanNormalize(vectors.ToArray());
            Assert.AreEqual(-0.5, vectors[0][0].X);
            Assert.AreEqual(0, vectors[0][1].X);
            Assert.AreEqual(0.5, Math.Round(vectors[0][2].X, 2));

            Assert.AreEqual(0, vectors[1][0].X);
            Assert.AreEqual(0.5, vectors[1][1].X);
            Assert.AreEqual(-0.5, Math.Round(vectors[1][2].X, 2));

            Assert.AreEqual(0.5, vectors[2][0].X);
            Assert.AreEqual(-0.5, vectors[2][1].X);
            Assert.AreEqual(0, Math.Round(vectors[2][2].X, 2));
        }
    }
}
