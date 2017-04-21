﻿using System;
using NUnit.Framework;
using Wikiled.Arff.Normalization;
using Wikiled.MachineLearning.Mathematics.Vectors;

namespace Wikiled.MachineLearning.Tests.Mathematics.Vectors
{
    [TestFixture]
    public class L2DistanceTests
    {
        [Test]
        public void MeasureSimple()
        {
            VectorData vector1 = VectorDataFactory.Instance.CreateSimple(
                NormalizationType.None,
                new SimpleCell("1", 1),
                new SimpleCell("2", 2),
                new SimpleCell("3", 3));

            VectorData vector2 = VectorDataFactory.Instance.CreateSimple(
                NormalizationType.None,
                new SimpleCell("4", 4),
                new SimpleCell("-2", -2),
                new SimpleCell("15", 15));

            double result = L2Distance.Instance.Measure(vector1, vector2);
            Assert.AreEqual(13, result);
        }

        [Test]
        public void MeasureNormalized()
        {
            VectorData vector1 = VectorDataFactory.Instance.CreateSimple(
                NormalizationType.L2,
                new SimpleCell("1", 1),
                new SimpleCell("2", 2),
                new SimpleCell("3", 3));

            VectorData vector2 = VectorDataFactory.Instance.CreateSimple(
                NormalizationType.L2,
                new SimpleCell("4", 4),
                new SimpleCell("-2", -2),
                new SimpleCell("15", 15));

            double result = L2Distance.Instance.Measure(vector1, vector2);
            Assert.AreEqual(0.68, Math.Round(result, 2));
        }

        [Test]
        public void MeasureDifferentLength()
        {
            VectorData vector1 = VectorDataFactory.Instance.CreateSimple(
                NormalizationType.L2,
                new SimpleCell("1", 1),
                new SimpleCell("2", 2),
                new SimpleCell("3", 3));

            VectorData vector2 = VectorDataFactory.Instance.CreateSimple(
                NormalizationType.L2,
                new SimpleCell("4", 4),
                new SimpleCell("15", 15));

            Assert.Throws<ArgumentException>(() => L2Distance.Instance.Measure(vector1, vector2));
        }

        [Test]
        public void MeasureDifferentNormalization()
        {
            VectorData vector1 = VectorDataFactory.Instance.CreateSimple(
                NormalizationType.L2,
                new SimpleCell("3", 3));

            VectorData vector2 = VectorDataFactory.Instance.CreateSimple(
                NormalizationType.L1,
                new SimpleCell("4", 4),
                new SimpleCell("15", 15));

            Assert.Throws<ArgumentException>(() => L2Distance.Instance.Measure(vector1, vector2));
        }

        [Test]
        public void MeasureNull()
        {
            VectorData vector1 = VectorDataFactory.Instance.CreateSimple(
                NormalizationType.L2,
                new SimpleCell("1", 1),
                new SimpleCell("3", 3));

            Assert.Throws<ArgumentNullException>(() => L2Distance.Instance.Measure(vector1, null));
        }
    }
}
