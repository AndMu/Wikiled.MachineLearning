using System;
using Newtonsoft.Json;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using Wikiled.Arff.Logic;
using Wikiled.MachineLearning.Normalization;

namespace Wikiled.MachineLearning.Tests.Normalization
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
            ClassicAssert.AreEqual(4, result.Length);
            ClassicAssert.AreEqual(3, result[0].Length);
            ClassicAssert.AreEqual(-1, Math.Round(result[0][0], 2));
            ClassicAssert.AreEqual(1, Math.Round(result[0][1], 2));
            if (withY)
            {
                ClassicAssert.AreEqual(2, Math.Round(result[0][2], 2));
            }
            else
            {
                ClassicAssert.AreEqual(1, Math.Round(result[0][2], 2));
            }
        }

        [Test]
        public void GetStandardizer()
        {
            var dataset = ArffDataSet.CreateSimple("Test");
            var document = dataset.AddDocument();
            document.AddRecord("T_One").Value = 1;
            document.AddRecord("T_Two").Value = 2;
            document.AddRecord("Three").Value = 2;

            document = dataset.AddDocument();
            document.AddRecord("T_One").Value = 1;
            document.AddRecord("T_Two").Value = 2;
            document.AddRecord("Three").Value = 2;

            document = dataset.AddDocument();
            document.AddRecord("T_One").Value = 2;
            document.AddRecord("T_Two").Value = 1;
            document.AddRecord("Three").Value = 1;

            document = dataset.AddDocument();
            document.AddRecord("T_One").Value = 2;
            document.AddRecord("T_Two").Value = 1;
            document.AddRecord("Three").Value = 1;

            var data = dataset.GetRawData();
            var result = Standardizer.GetNumericStandardizer(data).StandardizeAll(data);
            ClassicAssert.AreEqual(3, result[0].Length);
            ClassicAssert.AreEqual(-1, Math.Round(result[0][0], 2));
            ClassicAssert.AreEqual(1, Math.Round(result[0][1], 2));
        }

        private static void CheckData(double[][] result)
        {
            ClassicAssert.AreEqual(4, result.Length);
            ClassicAssert.AreEqual(9, result[0].Length);
            ClassicAssert.AreEqual(-1.23, Math.Round(result[0][0], 2));
            ClassicAssert.AreEqual(-1, Math.Round(result[0][1], 2));
            ClassicAssert.AreEqual(-1.34, Math.Round(result[0][2], 2));
            ClassicAssert.AreEqual(0, result[0][3]);
            ClassicAssert.AreEqual(1, result[0][4]);
            ClassicAssert.AreEqual(0, result[0][5]);
            ClassicAssert.AreEqual(0, result[0][6]);
            ClassicAssert.AreEqual(0, result[0][7]);
            ClassicAssert.AreEqual(1, result[0][8]);
        }
    }
}
