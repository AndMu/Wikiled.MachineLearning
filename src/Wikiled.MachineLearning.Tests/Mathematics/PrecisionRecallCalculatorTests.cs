using System;
using NUnit.Framework;
using Wikiled.MachineLearning.Mathematics;

namespace Wikiled.MachineLearning.Tests.Mathematics
{
    [TestFixture]
    public class PrecisionRecallCalculatorTests
    {
        private PrecisionRecallCalculator<bool> instance;

        [SetUp]
        public void Setup()
        {
            instance = new PrecisionRecallCalculator<bool>();
        }

        [Test]
        public void Construct()
        {
            Assert.AreEqual(0, instance.Total);
            Assert.AreEqual(0, instance.F1(true));
            Assert.AreEqual(0, instance.GetPrecision(true));
        }

        [Test]
        public void Add()
        {
            instance.Add(true, true);
            instance.Add(true, false);
            instance.Add(false, true);
            instance.Add(false, true);
            instance.Add(false, false);
            Assert.AreEqual(0.33, Math.Round(instance.GetSingleAccuracy(false), 2));
            Assert.AreEqual(0.4, Math.Round(instance.F1(true), 2));
            Assert.AreEqual(0.5, Math.Round(instance.GetSingleAccuracy(true), 2));
            Assert.AreEqual("Total:<5> Positive:<50.00%> Negative:<33.33%> F1:<0.40>", instance.GetTotalAccuracy());
        }

        [Test]
        public void SimpleMissing()
        {
            for (int i = 0; i < 3; i++)
            {
                instance.Add(true, true);
            }

            for (int i = 0; i < 7; i++)
            {
                instance.Add(true, null);
            }

            Assert.AreEqual(1, instance.GetPrecision(true));
            Assert.AreEqual(0.3, instance.GetRecall(true));
            Assert.AreEqual(0.46, Math.Round(instance.F1(true), 2));
        }

        [Test]
        public void SimpleNLP()
        {
            var result = PrecisionRecallExtension.Generate(90, 210, 140, 9560);
            Assert.AreEqual(0.3913, Math.Round(result.GetPrecision(true)), 4);
            Assert.AreEqual(0.3, result.GetRecall(true));
            Assert.AreEqual(0.965, result.GetAccuracy(true));
        }

        [Test]
        public void SimpleNLP2()
        {
            var result = PrecisionRecallExtension.Generate(15, 4, 18, 6);
            Assert.AreEqual(0.455, Math.Round(result.GetPrecision(true), 3));
            Assert.AreEqual(0.789, Math.Round(result.GetRecall(true), 3));
            Assert.AreEqual(0.488, Math.Round(result.GetAccuracy(true), 3));
            Assert.AreEqual(0.577, Math.Round(result.F1(true), 3));
        }

        [Test]
        public void SimpleNLP3()
        {
            var result = PrecisionRecallExtension.Generate(30, 10, 10, 50);
            Assert.AreEqual(0.75, Math.Round(result.GetPrecision(true), 3));
            Assert.AreEqual(0.75, Math.Round(result.GetRecall(true), 3));
            Assert.AreEqual(0.80, Math.Round(result.GetAccuracy(true), 3));
            Assert.AreEqual(0.75, Math.Round(result.F1(true), 3));
        }

        [Test]
        public void Zeros()
        {
            var result = PrecisionRecallExtension.Generate(0, 0, 0, 0);
            Assert.AreEqual(0, result.GetPrecision(true));
            Assert.AreEqual(0, result.GetRecall(true), 3);
            Assert.AreEqual(0, result.GetAccuracy(true), 3);
            Assert.AreEqual(0, result.F1(true), 3);

            result = PrecisionRecallExtension.Generate(1, 0, 0, 0);
            Assert.AreEqual(0, result.GetRecall(true), 3);
            Assert.AreEqual(0, result.GetAccuracy(true), 3);
            Assert.AreEqual(0, result.F1(true), 3);

            result = PrecisionRecallExtension.Generate(0, 1, 0, 0);
            Assert.AreEqual(0, result.GetPrecision(true));
            Assert.AreEqual(0, result.GetRecall(true), 3);
            Assert.AreEqual(0, result.GetAccuracy(true), 3);
            Assert.AreEqual(0, result.F1(true), 3);

            result = PrecisionRecallExtension.Generate(0, 0, 1, 0);
            Assert.AreEqual(0, result.GetPrecision(true));
            Assert.AreEqual(0, result.GetRecall(true), 3);
            Assert.AreEqual(0, result.GetAccuracy(true), 3);
            Assert.AreEqual(0, result.F1(true), 3);

            result = PrecisionRecallExtension.Generate(0, 0, 0, 1);
            Assert.AreEqual(0, result.GetPrecision(true));
            Assert.AreEqual(0, result.GetRecall(true), 3);
            Assert.AreEqual(0, result.GetAccuracy(true), 3);
            Assert.AreEqual(0, result.F1(true), 3);
        }

        [Test]
        public void Simple1()
        {
            for (int i = 0; i < 3; i++)
            {
                instance.Add(true, true);
            }

            for (int i = 0; i < 7; i++)
            {
                instance.Add(false, true);
            }

            Assert.AreEqual(0.3, instance.GetPrecision(true));
            Assert.AreEqual(1, instance.GetRecall(true));
            Assert.AreEqual(0.46, Math.Round(instance.F1(true), 2));
        }

        [Test]
        public void Simple2()
        {
            for (int i = 0; i < 3; i++)
            {
                instance.Add(true, true);
            }

            for (int i = 0; i < 4; i++)
            {
                instance.Add(false, false);
            }

            for (int i = 0; i < 4; i++)
            {
                instance.Add(false, true);
            }

            Assert.AreEqual(0.43, Math.Round(instance.GetPrecision(true), 2));
            Assert.AreEqual(1, instance.GetRecall(true));
            Assert.AreEqual(0.6, Math.Round(instance.F1(true), 2));
        }

        [Test]
        public void Simple3()
        {
            for (int i = 0; i < 2; i++)
            {
                instance.Add(true, true);
            }

            for (int i = 0; i < 1; i++)
            {
                instance.Add(true, false);
            }

            for (int i = 0; i < 4; i++)
            {
                instance.Add(false, false);
            }

            for (int i = 0; i < 3; i++)
            {
                instance.Add(false, true);
            }

            Assert.AreEqual(0.4, instance.GetPrecision(true));
            Assert.AreEqual(0.67, Math.Round(instance.GetRecall(true), 2));
            Assert.AreEqual(0.5, Math.Round(instance.F1(true), 2));
        }
    }
}
