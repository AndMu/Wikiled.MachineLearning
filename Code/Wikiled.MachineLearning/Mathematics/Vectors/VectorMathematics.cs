using System;
using System.Collections.Generic;
using System.Linq;
using Wikiled.Arff.Normalization;

namespace Wikiled.MachineLearning.Mathematics.Vectors
{
    public static class VectorMathematics
    {
        public static VectorData Sum(this IEnumerable<VectorData> vectors, NormalizationType normalizationType)
        {
            return VectorOperation(
                vectors,
                normalizationType,
                (a, b) => a + b);
        }

        public static double DotProduct(this IEnumerable<VectorData> vectors)
        {
            return DotProduct(vectors.ToArray());
        }

        public static double DotProduct(params VectorData[] vectors)
        {
            return VectorOperation(
                vectors,
                NormalizationType.None,
                (a, b) => a * b).Cells.Select(x => x.X).Sum();
        }

        private static VectorData VectorOperation(IEnumerable<VectorData> vectors,
            NormalizationType normalizationType,
            Func<double, double, double> operation)
        {
            Dictionary<int, double> values = new Dictionary<int, double>();
            int vectorID = 0;
            int length = 0;
            foreach (var vectorData in vectors)
            {
                vectorID++;
                length = vectorData.Length;
                foreach (var cell in vectorData.Cells)
                {
                    double value;
                    if (!values.TryGetValue(cell.Index, out value))
                    {
                        values[cell.Index] = cell.X;
                    }
                    else
                    {
                        values[cell.Index] = operation(cell.X, value);
                    }
                }
            }

            var cells = values.OrderBy(item => item.Key)
                .Select(item => new VectorCell(item.Key, new SimpleCell(item.Key.ToString(), item.Value), 0));
            return new VectorData(cells.ToArray(), length, normalizationType);
        }
    }
}
