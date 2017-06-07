namespace Wikiled.MachineLearning.Mathematics.Vectors
{
    public interface IDistance
    {
        double Measure(VectorData vector1, VectorData vector2);
    }
}