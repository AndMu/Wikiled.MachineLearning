using System;
using NUnit.Framework;
using Wikiled.MachineLearning.Normalization;
using Wikiled.MachineLearning.Mathematics.Vectors;

namespace Wikiled.MachineLearning.Tests.Mathematics.Vectors
{
    [TestFixture]
    public class L2DistanceTests
    {
        private L2Distance instance;

        private VectorDataFactory vectorDataFactory;

        [SetUp]
        public void Setup()
        {
            vectorDataFactory = new VectorDataFactory();
            instance = new L2Distance();
        }

        [Test]
        public void MeasureSimple()
        {
            VectorData vector1 = vectorDataFactory.CreateSimple(
                NormalizationType.None,
                new SimpleCell("1", 1),
                new SimpleCell("2", 2),
                new SimpleCell("3", 3));

            VectorData vector2 = vectorDataFactory.CreateSimple(
                NormalizationType.None,
                new SimpleCell("4", 4),
                new SimpleCell("-2", -2),
                new SimpleCell("15", 15));

            double result = instance.Measure(vector1, vector2);
            Assert.AreEqual(13, result);
        }

        [Test]
        public void MeasureNormalized()
        {
            VectorData vector1 = vectorDataFactory.CreateSimple(
                NormalizationType.L2,
                new SimpleCell("1", 1),
                new SimpleCell("2", 2),
                new SimpleCell("3", 3));

            VectorData vector2 = vectorDataFactory.CreateSimple(
                NormalizationType.L2,
                new SimpleCell("4", 4),
                new SimpleCell("-2", -2),
                new SimpleCell("15", 15));

            double result = instance.Measure(vector1, vector2);
            Assert.AreEqual(0.68, Math.Round(result, 2));
        }

        [Test]
        public void MeasureDifferentLength()
        {
            VectorData vector1 = vectorDataFactory.CreateSimple(
                NormalizationType.L2,
                new SimpleCell("1", 1),
                new SimpleCell("2", 2),
                new SimpleCell("3", 3));

            VectorData vector2 = vectorDataFactory.CreateSimple(
                NormalizationType.L2,
                new SimpleCell("4", 4),
                new SimpleCell("15", 15));

            Assert.Throws<ArgumentException>(() => instance.Measure(vector1, vector2));
        }

        [Test]
        public void MeasureDifferentNormalization()
        {
            VectorData vector1 = vectorDataFactory.CreateSimple(
                NormalizationType.L2,
                new SimpleCell("3", 3));

            VectorData vector2 = vectorDataFactory.CreateSimple(
                NormalizationType.L1,
                new SimpleCell("4", 4),
                new SimpleCell("15", 15));

            Assert.Throws<ArgumentException>(() => instance.Measure(vector1, vector2));
        }

        [Test]
        public void MeasureNull()
        {
            VectorData vector1 = vectorDataFactory.CreateSimple(
                NormalizationType.L2,
                new SimpleCell("1", 1),
                new SimpleCell("3", 3));

            Assert.Throws<ArgumentNullException>(() => instance.Measure(vector1, null));
        }
    }
}
