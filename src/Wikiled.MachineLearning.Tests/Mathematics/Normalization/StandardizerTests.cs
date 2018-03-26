using System;
using Newtonsoft.Json;
using NUnit.Framework;
using Wikiled.MachineLearning.Mathematics.Normalization;

namespace Wikiled.MachineLearning.Tests.Mathematics.Normalization
{
    [TestFixture]
    public class StandardizerTests
    {
        [Test]
        public void Construct()
        {
            string[][] data = new string[4][];
            data[0] = new[] { "30", "male", "38000.00", "urban", "democrat" };
            data[1] = new[] { "36", "female", "42000.00", "suburban", "republican" };
            data[2] = new[] { "52", "male", "40000.00", "rural", "independent" };
            data[3] = new[] { "42", "female", "44000.00", "suburban", "other" };
            var standardizer = Standardizer.CreateStandardizer(data, new[] { ColumnType.Numeric, ColumnType.Categorical, ColumnType.Numeric, ColumnType.Categorical, ColumnType.Categorical }, true);
            var result = standardizer.StandardizeAll(data);
            CheckData(result);
            string json = JsonConvert.SerializeObject(standardizer);

            var deserialized = JsonConvert.DeserializeObject<Standardizer>(json);
            result = deserialized.StandardizeAll(data);
            CheckData(result);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GetNumericStandardizer(bool withY)
        {
            double[][] data = new double[4][];
            data[0] = new double[] { 1, 2, 2 };
            data[1] = new double[] { 1, 2, 2 };
            data[2] = new double[] { 2, 1, 1 };
            data[3] = new double[] { 2, 1, 1 };
            var standardizer = Standardizer.GetNumericStandardizer(data, withY);
            var result = standardizer.StandardizeAll(data);
            Assert.AreEqual(4, result.Length);
            Assert.AreEqual(3, result[0].Length);
            Assert.AreEqual(-1, Math.Round(result[0][0], 2));
            Assert.AreEqual(1, Math.Round(result[0][1], 2));
            if (withY)
            {
                Assert.AreEqual(2, Math.Round(result[0][2], 2));
            }
            else
            {
                Assert.AreEqual(1, Math.Round(result[0][2], 2));
            }
        }

        private static void CheckData(double[][] result)
        {
            Assert.AreEqual(4, result.Length);
            Assert.AreEqual(9, result[0].Length);
            Assert.AreEqual(-1.23, Math.Round(result[0][0], 2));
            Assert.AreEqual(-1, Math.Round(result[0][1], 2));
            Assert.AreEqual(-1.34, Math.Round(result[0][2], 2));
            Assert.AreEqual(0, result[0][3]);
            Assert.AreEqual(1, result[0][4]);
            Assert.AreEqual(0, result[0][5]);
            Assert.AreEqual(0, result[0][6]);
            Assert.AreEqual(0, result[0][7]);
            Assert.AreEqual(1, result[0][8]);
        }
    }
}
