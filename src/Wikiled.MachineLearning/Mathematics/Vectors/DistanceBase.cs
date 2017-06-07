using Wikiled.Core.Utility.Arguments;

namespace Wikiled.MachineLearning.Mathematics.Vectors
{
    public abstract class DistanceBase : IDistance
    {
        public double Measure(VectorData vector1, VectorData vector2)
        {
            Guard.NotNull(() => vector1, vector1);
            Guard.NotNull(() => vector2, vector2);
            Guard.IsValid(() => vector1, vector1, item => vector1.Length == vector2.Length, "verctors length doesn't match");
            return Calculate(vector1, vector2);
        }

        protected abstract double Calculate(VectorData vector1, VectorData vector2);
    }
}