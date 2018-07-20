using System;
using System.Linq;
using Wikiled.MachineLearning.Normalization;

namespace Wikiled.MachineLearning.Mathematics.Vectors
{
    public static class DataSetNormalization
    {
        public static void MeanNormalize(VectorData[] vectors)
        {
            if (vectors == null)
            {
                throw new ArgumentNullException(nameof(vectors));
            }

            if (vectors.Length == 0)
            {
                return;
            }

            if (vectors.Any(vector => vector.Length != vectors[0].Length))
            {
                throw new ArgumentOutOfRangeException(nameof(vectors), "Vectors should be same length");
            }

            for (int i = 0; i < vectors[0].Length; i++)
            {
                var normalized = vectors.Select(item => item[i].X).MeanNormalized().ToArray();
                for (int j = 0; j < normalized.Length; j++)
                {
                    vectors[j][i].X = normalized[j];
                }
            }
        }
    }
}
