using System;
using System.Linq;
using System.Xml.Linq;
using NUnit.Framework;
using Wikiled.Arff.Normalization;
using Wikiled.Common.Serialization;
using Wikiled.MachineLearning.Mathematics.Vectors;

namespace Wikiled.MachineLearning.Tests.Mathematics.Vectors
{
    [TestFixture]
    public class VectorDataTests
    {
        private VectorDataFactory vectorDataFactory;

        [SetUp]
        public void Setup()
        {
            vectorDataFactory = new VectorDataFactory();
        }

        [Test]
        public void ChangeNormalization()
        {
            var vector = vectorDataFactory.CreateSimple(
                NormalizationType.None,
                new SimpleCell("1", 1000),
                new SimpleCell("2", 2),
                new SimpleCell("3", 0.3));
            Assert.AreEqual(1000, vector[0].X);
            Assert.AreEqual(2, vector[1].X);
            vector.ChangeNormalization(NormalizationType.L2);
            Assert.AreEqual(0.999998, Math.Round(vector[0].X, 6));
            Assert.AreEqual(0.002, Math.Round(vector[1].X, 6));
            vector.ChangeNormalization(NormalizationType.None);
            Assert.AreEqual(1000, vector[0].X);
            Assert.AreEqual(2, vector[1].X);
        }

        [Test]
        public void Enumerable()
        {
            var vector = vectorDataFactory.CreateSimple(
                NormalizationType.None,
                new SimpleCell("1", 1000),
                new SimpleCell("2", 2),
                new SimpleCell("3", 0.3));
            var data = vector.ToArray();
            Assert.AreEqual(3, data.Length);
        }

        [Test]
        public void EnumerableSparse()
        {
            var vector = new VectorData(
                new[]
                    {
                        new VectorCell(0, new SimpleCell("1", 1), 1),
                        new VectorCell(3, new SimpleCell("1", 1), 1)
                    },
                4,
                0,
                NormalizationType.None);
            var data = vector.ToArray();
            Assert.AreEqual(4, data.Length);
        }

        [Test]
        public void IncorrectLength()
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () => new VectorData(
                    new[]
                        {
                            new VectorCell(0, new SimpleCell("1", 1), 1),
                            new VectorCell(3, new SimpleCell("1", 1), 1)
                        },
                    3,
                    0,
                    NormalizationType.None));
        }

        [Test]
        public void MultiSerialize()
        {
            var vector1 = new VectorData(
                new[]
                    {
                        new VectorCell(0, new SimpleCell("1", 1), 1),
                        new VectorCell(3, new SimpleCell("1", 1), 1)
                    },
                4,
                0,
                NormalizationType.Elastic);
            var vector2 = new VectorData(
                new[]
                    {
                        new VectorCell(0, new SimpleCell("1", 1), 1),
                        new VectorCell(3, new SimpleCell("1", 1), 1)
                    },
                4,
                0,
                NormalizationType.Elastic);
            new[] { vector1, vector2 }.XmlSerialize().Save("vectorArray.xml");
            var recovered = XDocument.Load("vectorArray.xml").XmlDeserialize<VectorData[]>();
            Assert.AreEqual(2, recovered.Length);
        }

        [Test]
        public void NormalizeByCoef()
        {
            var vector = vectorDataFactory.CreateSimple(
                NormalizationType.None,
                new SimpleCell("1", 1000),
                new SimpleCell("2", 2),
                new SimpleCell("3", 0.3));
            vector.NormalizeByCoef(10);
            Assert.AreEqual(100, vector[0].X);
            Assert.AreEqual(0.2, vector[1].X);
            Assert.AreEqual(0.03, Math.Round(vector[2].X, 2));
        }

        [Test]
        public void Serialize()
        {
            var vector = new VectorData(
                new[]
                    {
                        new VectorCell(0, new SimpleCell("1", 1), 1),
                        new VectorCell(3, new SimpleCell("1", 1), 1)
                    },
                4,
                0,
                NormalizationType.Elastic);

            vector.XmlSerialize().Save("vector.xml");
            var recovered = XDocument.Load("vector.xml").XmlDeserialize<VectorData>();
            Assert.AreEqual(vector.Length, recovered.Length);
            Assert.AreEqual(vector.RHO, recovered.RHO);
            Assert.AreEqual(vector.Normalization.Type, recovered.Normalization.Type);
            Assert.AreEqual(vector.ValueCellsX.Length, recovered.ValueCellsX.Length);
            Assert.AreEqual(vector.ValueCellsX[0], recovered.ValueCellsX[0]);
            Assert.AreEqual(vector.ValueCellsX[1], recovered.ValueCellsX[1]);
        }
    }
}
