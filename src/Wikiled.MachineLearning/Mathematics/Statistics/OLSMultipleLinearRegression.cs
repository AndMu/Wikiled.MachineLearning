using System;
using System.Collections.Generic;
using Wikiled.Arff.Normalization;
using Wikiled.MachineLearning.Mathematics.Vectors;

namespace Wikiled.MachineLearning.Mathematics.Statistics
{
    public class OLSMultipleLinearRegression
    {
        private readonly List<VectorData> xVectors = new List<VectorData>();

        private readonly double[] yData;
        
        private alglib.sparsematrix xData;

        public OLSMultipleLinearRegression(double[] yData)
        {
            this.yData = yData;
            RegressionParameters = new double[yData.Length];
            Residuals = new double[yData.Length];
        }

        public int DataLength => yData.Length;

        public double[] RegressionParameters
        {
            get; private set;
        }

        public double[] Residuals
        {
            get;
        }

        public int XColumns => xVectors.Count;

        public void AddXData(VectorData xVector)
        {
            xVectors.Add(xVector);
        }

        public void AddXData(double[] xVector)
        {
            var vector = VectorDataFactory.Instance.CreateSimple(NormalizationType.None, xVector);
            AddXData(vector);
        }

        public void AddXData(params double[][] xVectorsItems)
        {
            foreach (var xVector in xVectorsItems)
            {
                AddXData(xVector);
            }
        }

        public void Calculate()
        {
            alglib.sparsecreate(DataLength, xVectors.Count + 1, out xData);
            for (int i = 0; i < yData.Length; i++)
            {
                alglib.sparseset(xData, i, 0, 1);
            }

            int column = 0;
            foreach (var xVector in xVectors)
            {
                column++;
                foreach (var cell in xVector)
                {
                    alglib.sparseset(xData, cell.Index, column, cell.Calculated);
                }
            }

            alglib.sparseconverttocrs(xData);
            alglib.linlsqrstate state;
            alglib.linlsqrreport report;
            double[] x;
            alglib.linlsqrcreate(DataLength, XColumns + 1, out state);
            alglib.linlsqrsolvesparse(state, xData, yData);
            alglib.linlsqrresults(state, out x, out report);

            RegressionParameters = x;
            double[] yref = new double[0];
            alglib.sparsemv(xData, x, ref yref);

            for (int i = 0; i < yData.Length; i++)
            {
                Residuals[i] = yData[i] - yref[i];
            }
        }

        public double CalculateY(double[] data)
        {
            if (data.Length != RegressionParameters.Length - 1)
            {
                throw new ArgumentOutOfRangeException("data");
            }

            var result = RegressionParameters[0];
            for (int i = 1; i < RegressionParameters.Length; i++)
            {
                result += RegressionParameters[i] * data[i - 1];
            }

            return result;
        }
    }
}
