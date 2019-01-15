using System;
using NUnit.Framework;
using System.Linq;
using Wikiled.Arff.Logic;
using Wikiled.MachineLearning.Normalization;

namespace Wikiled.MachineLearning.Tests.Normalization
{
    [TestFixture]
    public class ArffExtensionsTests
    {
        [TestCase(true)]
        [TestCase(false)]
        public void GetDataNormalized(bool withId)
        {
            var dataset = ArffDataSet.CreateSimple("Test");
            dataset.HasId = withId;
            var document = dataset.GetOrCreateDocument("one");
            document.AddRecord("T_One").Value = 1;
            document.AddRecord("T_Two").Value = 2;
            document.AddRecord("Three").Value = 2;

            document = dataset.GetOrCreateDocument("Two");
            document.AddRecord("T_One").Value = 1;
            document.AddRecord("T_Two").Value = 2;
            document.AddRecord("Three").Value = 2;

            document = dataset.GetOrCreateDocument("Three");
            document.AddRecord("T_One").Value = 2;
            document.AddRecord("T_Two").Value = 1;
            document.AddRecord("Three").Value = 1;

            document = dataset.AddDocument();
            document.AddRecord("T_One").Value = 2;
            document.AddRecord("T_Two").Value = 1;
            document.AddRecord("Three").Value = 1;

            var result = dataset.GetDataNormalized(NormalizationType.L2).ToArray();
            Assert.AreEqual(4, result.Length);
            Assert.AreEqual(0.82, Math.Round(result[0].X[0], 2));
            Assert.AreEqual(0.41, Math.Round(result[0].X[1], 2));
            Assert.AreEqual(0.41, Math.Round(result[0].X[2], 2));
        }
    }
}
