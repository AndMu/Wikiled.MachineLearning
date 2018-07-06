
using System;

namespace Wikiled.MachineLearning.Mathematics.Vectors
{
    public abstract class DistanceBase : IDistance
    {
        public double Measure(VectorData vector1, VectorData vector2)
        {
            if (vector1 == null)
            {
                throw new ArgumentNullException(nameof(vector1));
            }

            if (vector2 == null)
            {
                throw new ArgumentNullException(nameof(vector2));
            }

            if (vector1.Length != vector2.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(vector1), "vectors length doesn't match");
            }

            return Calculate(vector1, vector2);
        }

        protected abstract double Calculate(VectorData vector1, VectorData vector2);
    }
}