using System;
using System.Linq;
using NUnit.Framework;
using Wikiled.MachineLearning.Mathematics.Statistics;

namespace Wikiled.MachineLearning.Tests.Mathematics.Statistics
{
    [TestFixture]
    public class OLSMultipleLinearRegressionTests
    {
        /// <summary>
        /// Test Longley dataset against certified values provided by NIST.
        /// Data Source: J. Longley (1967) "An Appraisal of Least Squares
        /// Programs for the Electronic Computer from the Point of View of the User"
        /// Journal of the American Statistical Association, vol. 62. September,
        /// pp. 819-841.
        /// 
        /// Certified values (and data) are from NIST:
        /// http://www.itl.nist.gov/div898/strd/lls/data/LINKS/DATA/Longley.dat
        /// </summary>
        [Test]
        public void TestLongly()
        {
            double[] data =
            {
                60323,
                61122,
                60171,
                61187,
                63221,
                63639,
                64989,
                63761,
                66019,
                67857,
                68169,
                66513,
                68655,
                69564,
                69331,
                70551
            };

            double[,] xData =
            {
                {83.0, 234289, 2356, 1590, 107608, 1947},
                {88.5, 259426, 2325, 1456, 108632, 1948},
                {88.2, 258054, 3682, 1616, 109773, 1949},
                {89.5, 284599, 3351, 1650, 110929, 1950},
                {96.2, 328975, 2099, 3099, 112075, 1951},
                {98.1, 346999, 1932, 3594, 113270, 1952},
                {99.0, 365385, 1870, 3547, 115094, 1953},
                {100.0, 363112, 3578, 3350, 116219, 1954},
                {101.2, 397469, 2904, 3048, 117388, 1955},
                {104.6, 419180, 2822, 2857, 118734, 1956},
                {108.4, 442769, 2936, 2798, 120445, 1957},
                {110.8, 444546, 4681, 2637, 121950, 1958},
                {112.6, 482704, 3813, 2552, 123366, 1959},
                {114.2, 502601, 3931, 2514, 125368, 1960},
                {115.7, 518173, 4806, 2572, 127852, 1961},
                {116.9, 554894, 4007, 2827, 130081, 1962}
            };

            OLSMultipleLinearRegression regression = new OLSMultipleLinearRegression(data);
            double[] xSelected = new double[data.Length];
            for (int j = 0; j < 6; j++)
            {
                for (int i = 0; i < data.Length; i++)
                {
                    xSelected[i] = xData[i, j];
                }

                regression.AddXData(xSelected);
            }

            regression.Calculate();

            Assert.AreEqual(
                new[]
                {
                    -3482258.63,
                    15.06,
                    -0.04,
                    -2.02,
                    -1.03,
                    -0.05,
                    1829.15
                },
                regression.RegressionParameters.Select(item => Math.Round(item, 2)).ToArray());

            var result = regression.CalculateY(new []{83.0, 234289, 2356, 1590, 107608, 1947});
            Assert.AreEqual(60056, Math.Round(result, 0));

            result = regression.CalculateY(new[] { 116.9, 554894, 4007, 2827, 130081, 1962 });
            Assert.AreEqual(70758, Math.Round(result, 0));

            Assert.AreEqual(
                new[]
                {
                    267.34,
                    -94.01,
                    46.29,
                    -410.11,
                    309.71,
                    -249.31,
                    -164.05,
                    -13.18,
                    14.30,
                    455.39,
                    -17.27,
                    -39.06,
                    -155.55,
                    -85.67,
                    341.93,
                    -206.76
                },
                regression.Residuals.Select(item => Math.Round(item, 2)).ToArray());
        }
    }
}
