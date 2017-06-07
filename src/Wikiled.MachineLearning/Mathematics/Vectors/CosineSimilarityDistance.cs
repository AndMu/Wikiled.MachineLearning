using System;

namespace Wikiled.MachineLearning.Mathematics.Vectors
{
    public class CosineSimilarityDistance : DistanceBase
    {
        protected override double Calculate(VectorData vector1, VectorData vector2)
        {
            int minimumLength = ((vector2.Length < vector1.Length) ? vector2.Length : vector1.Length);
            double dot = 0.0d;
            double mag1 = 0.0d;
            double mag2 = 0.0d;
            for (int n = 0; n < minimumLength; n++)
            {
                dot += vector1[n].X * vector2[n].X;
                mag1 += Math.Pow(vector1[n].X, 2);
                mag2 += Math.Pow(vector2[n].X, 2);
            }

            return dot / (Math.Sqrt(mag1) * Math.Sqrt(mag2));
        }
    }
}
