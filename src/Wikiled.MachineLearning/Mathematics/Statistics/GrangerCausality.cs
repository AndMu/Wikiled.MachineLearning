using System;
using System.Collections.Concurrent;
using System.Linq;
using Wikiled.Common.Arguments;

namespace Wikiled.MachineLearning.Mathematics.Statistics
{
    public static class GrangerCausality
    {
        /// <summary>
        /// Returns p-value for TestGranger causality test. 
        /// </summary>
        /// <param name="x">predictor</param>
        /// <param name="y">predictable variable</param>
        /// <param name="lag">lag, should be 1 or greater.</param>
        /// <returns>p-value of TestGranger causality</returns>
        public static double TestGranger(double[] x, double[] y, int lag)
        {
            Guard.IsValid(() => lag, lag, item => item >= 1, "lag, should be 1 or greater");
            var yData = y.Skip(lag).ToArray();
            var n = yData.Length;

            OLSMultipleLinearRegression h0 = new OLSMultipleLinearRegression(yData);
            OLSMultipleLinearRegression h1 = new OLSMultipleLinearRegression(yData);

            for (int i = 0; i < lag; i++)
            {
                double[] xOffset = new double[n];
                double[] yOffset = new double[n];
                Array.Copy(x, i, xOffset, 0, n);
                Array.Copy(y, i, yOffset, 0, n);

                h0.AddXData(yOffset);

                h1.AddXData(xOffset);
                h1.AddXData(yOffset);
            }

            h0.Calculate();
            h1.Calculate();

            double[] rs0 = h0.Residuals;
            double[] rs1 = h1.Residuals;

            double rss0 = rs0.Sum(item => item * item);
            double rss1 = rs1.Sum(item => item * item);

            double ftest = ((rss0 - rss1) / lag) / (rss1 / (n - 2 * lag - 1));

            var fDist = alglib.fdistribution(lag, n - 2 * lag - 1, ftest);
            double pValue = 1.0 - fDist;
            return pValue;
        }

        public static GrangerResult MultiTestGranger(double[] x, double[] y, int lag)
        {
            var xResult = TestGranger(x, y, lag);
            var yResult = TestGranger(y, x, lag);
            return new GrangerResult(lag, xResult, yResult);
        }

        public static GrangerResult[] MultiTestGranger(double[] x, double[] y, int lagFrom, int lagTo)
        {
            var min =  Math.Min(x.Length, y.Length) / 2 - 2;
            lagTo = lagTo > min ? min : lagTo;
            ConcurrentBag<GrangerResult> results = new ConcurrentBag<GrangerResult>();
            System.Threading.Tasks.Parallel.For(
                lagFrom,
                lagTo,
                index =>
                {
                    var result = MultiTestGranger(x, y, index);
                    results.Add(result);
                });

            return results.OrderBy(item => item.Lag).ToArray();
        }
    }
}
