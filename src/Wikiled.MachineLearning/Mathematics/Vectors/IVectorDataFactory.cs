using Wikiled.Arff.Normalization;

namespace Wikiled.MachineLearning.Mathematics.Vectors
{
    public interface IVectorDataFactory
    {
        VectorData CreateSimple(NormalizationType normalizationType, params double[] cells);

        VectorData CreateSimple(NormalizationType normalizationType, params ICell[] cells);
    }
}