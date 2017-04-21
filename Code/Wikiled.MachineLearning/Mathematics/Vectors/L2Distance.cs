using System;

namespace Wikiled.MachineLearning.Mathematics.Vectors
{
    public class L2Distance : DistanceBase
    {
        protected override double Calculate(VectorData vector1, VectorData vector2)
        {
            double sum = 0;
            for (int i = 0; i < vector1.Length; i++)
            {
                sum += Math.Pow(vector1[i].X - vector2[i].X, 2);
            }

            return Math.Sqrt(sum);
        }
    }
}
