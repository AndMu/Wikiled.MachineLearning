using System;
using System.Collections.Generic;

namespace Wikiled.MachineLearning.Mathematics.Vectors
{
    public class CosineSimilarityDistance : DistanceBase
    {
        protected override double Calculate(VectorData vector1, VectorData vector2)
        {
            if (vector1.Length != vector2.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(vector1), "Vectors length does not match");
            }

            double dot = 0.0d;
            double mag1 = 0.0d;
            double mag2 = 0.0d;
            var calculated = new HashSet<int>();
            foreach (var vectorCell in vector1.DataTable)
            {
                var x1 = vectorCell.Value.X;
                var x2 = 0.0d;
                mag1 += Math.Pow(x1, 2);
                if (vector2.DataTable.TryGetValue(vectorCell.Key, out var another))
                {
                    x2 = another.X;
                    mag2 += Math.Pow(x2, 2);
                    dot += x1 * x2;
                    calculated.Add(vectorCell.Key);
                }
            }

            foreach (var vectorCell in vector2.DataTable)
            {
                if (!calculated.Contains(vectorCell.Key))
                {
                    var x2 = vectorCell.Value.X;
                    mag2 += Math.Pow(x2, 2);
                }
            }

            return dot / (Math.Sqrt(mag1) * Math.Sqrt(mag2));
        }
    }
}
