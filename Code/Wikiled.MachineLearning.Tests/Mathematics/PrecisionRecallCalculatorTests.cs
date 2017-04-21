using System;
using NUnit.Framework;
using Wikiled.MachineLearning.Mathematics;

namespace Wikiled.MachineLearning.Tests.Mathematics
{
    [TestFixture]
    public class PrecisionRecallCalculatorTests
    {
        [Test]
        public void SimpleMissing()
        {
            PrecisionRecallCalculator<bool> calculator = new PrecisionRecallCalculator<bool>();
            for (int i = 0; i < 3; i++)
            {
                calculator.Add(true, true);
            }

            for (int i = 0; i < 7; i++)
            {
                calculator.Add(true, null);
            }

            Assert.AreEqual(1, calculator.GetPrecision(true));
            Assert.AreEqual(0.3, calculator.GetRecall(true));
            Assert.AreEqual(0.46, Math.Round(calculator.F1(true), 2));
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
            PrecisionRecallCalculator<bool> calculator = new PrecisionRecallCalculator<bool>();
            for (int i = 0; i < 3; i++)
            {
                calculator.Add(true, true);
            }

            for (int i = 0; i < 7; i++)
            {
                calculator.Add(false, true);
            }

            Assert.AreEqual(0.3, calculator.GetPrecision(true));
            Assert.AreEqual(1, calculator.GetRecall(true));
            Assert.AreEqual(0.46, Math.Round(calculator.F1(true), 2));
        }

        [Test]
        public void Simple2()
        {
            PrecisionRecallCalculator<bool> calculator = new PrecisionRecallCalculator<bool>();
            for (int i = 0; i < 3; i++)
            {
                calculator.Add(true, true);
            }

            for (int i = 0; i < 4; i++)
            {
                calculator.Add(false, false);
            }

            for (int i = 0; i < 4; i++)
            {
                calculator.Add(false, true);
            }

            Assert.AreEqual(0.43, Math.Round(calculator.GetPrecision(true), 2));
            Assert.AreEqual(1, calculator.GetRecall(true));
            Assert.AreEqual(0.6, Math.Round(calculator.F1(true), 2));
        }

        [Test]
        public void Simple3()
        {
            PrecisionRecallCalculator<bool> calculator = new PrecisionRecallCalculator<bool>();
            for (int i = 0; i < 2; i++)
            {
                calculator.Add(true, true);
            }

            for (int i = 0; i < 1; i++)
            {
                calculator.Add(true, false);
            }

            for (int i = 0; i < 4; i++)
            {
                calculator.Add(false, false);
            }

            for (int i = 0; i < 3; i++)
            {
                calculator.Add(false, true);
            }

            Assert.AreEqual(0.4, calculator.GetPrecision(true));
            Assert.AreEqual(0.67, Math.Round(calculator.GetRecall(true), 2));
            Assert.AreEqual(0.5, Math.Round(calculator.F1(true), 2));
        }
    }
}
